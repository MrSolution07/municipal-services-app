using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Forms
{
    /// <summary>
    /// Part 2 form: Local Events and Announcements.
    /// Data structures on screen:
    ///   Priority queue  - "Featured" cards (highest priority first)
    ///   Queue           - "Latest notices" (first in, first out; "Read next notice" dequeues)
    ///   Stack           - "Recently viewed" (last in, first out; "Undo last view" pops)
    ///   SortedDictionary- events listed in date order
    ///   Dictionary      - lookup by id and category interest counts
    ///   HashSet         - unique categories in the filter drop-down
    ///   Recommendations - scored from searches, viewed events and priority
    /// </summary>
    public partial class LocalEventsForm : Form
    {
        private const int CardSpacing = 8;

        private readonly List<LocalEvent> currentResults = new List<LocalEvent>();
        private bool filtered;

        public LocalEventsForm()
        {
            InitializeComponent();
            ApplyTheme();
            EventCatalogue.Load();

            flpResults.SizeChanged += (s, e) => FitCards(flpResults);
            flpRecommendations.SizeChanged += (s, e) => FitCards(flpRecommendations);
            flpNotices.SizeChanged += (s, e) => FitCards(flpNotices);
            flpRecent.SizeChanged += (s, e) => FitCards(flpRecent);

            LoadFilters();
            ShowFeatured();
            ShowResults(EventCatalogue.GetUpcomingEvents());
            ShowNotices();
            ShowRecentlyViewed(null);
            ShowRecommendations();
        }

        // ------------------------------------------------------------------ theme

        private void ApplyTheme()
        {
            BackColor = AppTheme.Background;

            panelHeader.BackColor = AppTheme.Header;
            lblTitle.ForeColor = AppTheme.HeaderText;
            lblTitle.Font = AppTheme.TitleFont;
            lblSubtitle.ForeColor = AppTheme.HeaderText;
            lblSubtitle.Font = AppTheme.SubtitleFont;

            foreach (Label label in new[] { lblFeatured, lblSearch, lblResults, lblDetail, lblRecommendations, lblRecent, lblNotices })
            {
                label.Font = AppTheme.SectionFont;
                label.ForeColor = AppTheme.BodyText;
            }

            foreach (Label label in new[] { lblCategory, lblDate })
            {
                label.Font = AppTheme.BodyFont;
                label.ForeColor = AppTheme.BodyText;
            }

            foreach (Label label in new[] { lblNoResults, lblSearchHint, lblQueueInfo, lblStackInfo })
            {
                label.Font = AppTheme.SmallFont;
                label.ForeColor = AppTheme.MutedText;
            }

            lblNoResults.Font = AppTheme.BodyFont;
            lblNoResults.Padding = new Padding(0, 6, 0, 6);

            cboCategory.Font = AppTheme.BodyFont;
            dtpEventDate.Font = AppTheme.BodyFont;
            chkAnyDate.Font = AppTheme.BodyFont;
            chkAnyDate.ForeColor = AppTheme.BodyText;

            StylePrimary(btnSearch);
            StylePrimary(btnNextNotice);
            StyleSecondary(btnClear);
            StyleSecondary(btnPopRecent);
            StyleSecondary(btnBack);

            panelDetail.BackColor = AppTheme.Card;
            panelDetail.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(AppTheme.CardBorder))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panelDetail.Width - 1, panelDetail.Height - 1);
                }
            };
            txtDetailTitle.Font = AppTheme.SectionFont;
            txtDetailTitle.ForeColor = AppTheme.BodyText;
            txtDetailBody.Font = AppTheme.BodyFont;
            txtDetailBody.ForeColor = AppTheme.BodyText;

            foreach (FlowLayoutPanel panel in new[] { panelFeatured, flpResults, flpRecommendations, flpNotices, flpRecent })
            {
                panel.BackColor = AppTheme.Background;
            }

            toolTip.SetToolTip(btnSearch, "Find events matching the chosen category and date.");
            toolTip.SetToolTip(btnClear, "Clear the filters and show all upcoming events.");
            toolTip.SetToolTip(btnNextNotice, "Queue: remove the oldest notice from the front and read it.");
            toolTip.SetToolTip(btnPopRecent, "Stack: remove the most recently viewed event from the top.");
        }

        private static void StylePrimary(Button button)
        {
            button.BackColor = AppTheme.Accent;
            button.ForeColor = AppTheme.AccentText;
            button.Font = AppTheme.ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }

        private static void StyleSecondary(Button button)
        {
            button.BackColor = AppTheme.Card;
            button.ForeColor = AppTheme.BodyText;
            button.Font = AppTheme.ButtonFont;
            button.FlatAppearance.BorderColor = AppTheme.Border;
            button.FlatAppearance.BorderSize = 1;
            button.Cursor = Cursors.Hand;
        }

        private static void SetEnabled(Button button, bool enabled, bool primary)
        {
            button.Enabled = enabled;
            if (enabled)
            {
                if (primary)
                {
                    StylePrimary(button);
                }
                else
                {
                    StyleSecondary(button);
                }
            }
            else
            {
                button.BackColor = AppTheme.Disabled;
                button.ForeColor = AppTheme.MutedText;
            }
        }

        private void LoadFilters()
        {
            cboCategory.Items.Add("All categories");
            foreach (string category in EventCatalogue.GetCategories())
            {
                cboCategory.Items.Add(category);
            }

            cboCategory.SelectedIndex = 0;
            dtpEventDate.Value = DateTime.Today;
            chkAnyDate.Checked = true;
            dtpEventDate.Enabled = false;
        }

        // ------------------------------------------------------------------ sections

        private void ShowFeatured()
        {
            panelFeatured.Controls.Clear();
            int rank = 1;
            foreach (LocalEvent item in EventCatalogue.GetFeatured(3))
            {
                LocalEvent captured = item;
                Panel card = BuildCard(292, 90, () => SelectEvent(captured.Id, true),
                    CreateLine("#" + rank + " featured  \u2022  priority " + item.Priority, AppTheme.Accent, AppTheme.SmallFont, 18),
                    CreateLine(item.Title, AppTheme.BodyText, AppTheme.SectionFont, 24),
                    CreateLine(item.DisplayDate, AppTheme.MutedText, AppTheme.BodyFont, 22));
                card.Margin = new Padding(0, 0, 12, 0);
                panelFeatured.Controls.Add(card);
                rank++;
            }
        }

        private void ShowResults(IEnumerable<LocalEvent> events)
        {
            currentResults.Clear();
            flpResults.SuspendLayout();
            flpResults.Controls.Clear();

            foreach (LocalEvent item in events)
            {
                LocalEvent captured = item;
                currentResults.Add(captured);

                string kind = captured.IsAnnouncement ? "Announcement" : "Event";
                Panel card = BuildCard(300, 92, () => SelectEvent(captured.Id, true),
                    CreateLine(captured.Title, AppTheme.BodyText, AppTheme.SectionFont, 24),
                    CreateLine(kind + "  \u2022  " + captured.Category, AppTheme.Accent, AppTheme.SmallFont, 18),
                    CreateLine(captured.DisplayDate, AppTheme.MutedText, AppTheme.BodyFont, 22));
                flpResults.Controls.Add(card);
            }

            FitCards(flpResults);
            flpResults.ResumeLayout();

            lblResults.Text = (filtered ? "Search results" : "Upcoming events") + " (" + currentResults.Count + ")";

            bool empty = currentResults.Count == 0;
            lblNoResults.Visible = empty;
            if (empty)
            {
                lblNoResults.Text = "No events match this search. Try another category or tick \"Any date\".";
            }
        }

        /// <summary>Queue view: front of the queue is listed first and marked "Next".</summary>
        private void ShowNotices()
        {
            flpNotices.SuspendLayout();
            flpNotices.Controls.Clear();

            int position = 1;
            foreach (LocalEvent item in EventCatalogue.GetAnnouncements())
            {
                LocalEvent captured = item;
                string place = position == 1 ? "NEXT TO READ" : "WAITING #" + position;
                Panel card = BuildCard(300, 60, () => SelectEvent(captured.Id, false),
                    CreateLine(captured.Title, AppTheme.BodyText, AppTheme.SectionFont, 24),
                    CreateLine(place + "  \u2022  " + captured.EventDate.ToString("ddd d MMM"), position == 1 ? AppTheme.Accent : AppTheme.MutedText, AppTheme.SmallFont, 18));
                card.Margin = new Padding(0, 0, 0, CardSpacing);
                flpNotices.Controls.Add(card);
                position++;
            }

            int count = EventCatalogue.AnnouncementCount;
            if (count == 0)
            {
                lblQueueInfo.Text = "All notices read. The queue is empty.";
                flpNotices.Controls.Add(CreateHint("No notices waiting. Press Reload notices to start again."));
            }
            else
            {
                lblQueueInfo.Text = count + " waiting. The oldest notice is read first.";
            }

            btnNextNotice.Text = count == 0 ? "Reload notices" : "Read next notice";
            FitCards(flpNotices);
            flpNotices.ResumeLayout();
        }

        /// <summary>Stack view: top of the stack (most recent) is listed first.</summary>
        private void ShowRecentlyViewed(string statusMessage)
        {
            flpRecent.SuspendLayout();
            flpRecent.Controls.Clear();

            int index = 0;
            foreach (LocalEvent item in EventCatalogue.GetRecentlyViewed())
            {
                LocalEvent captured = item;
                string place = index == 0 ? "TOP OF STACK (removed first)" : "VIEWED EARLIER";
                Panel card = BuildCard(300, 60, () => SelectEvent(captured.Id, false),
                    CreateLine(captured.Title, AppTheme.BodyText, AppTheme.SectionFont, 24),
                    CreateLine(place + "  \u2022  " + captured.Category, index == 0 ? AppTheme.Accent : AppTheme.MutedText, AppTheme.SmallFont, 18));
                card.Margin = new Padding(0, 0, 0, CardSpacing);
                flpRecent.Controls.Add(card);
                index++;
            }

            if (index == 0)
            {
                lblStackInfo.Text = string.IsNullOrEmpty(statusMessage) ? "Stack is empty." : statusMessage;
                flpRecent.Controls.Add(CreateHint("Click an event card and it is pushed onto this stack."));
            }
            else
            {
                lblStackInfo.Text = string.IsNullOrEmpty(statusMessage)
                    ? index + " viewed. The newest is removed first."
                    : statusMessage;
            }

            SetEnabled(btnPopRecent, index > 0, false);
            FitCards(flpRecent);
            flpRecent.ResumeLayout();
        }

        private void ShowRecommendations()
        {
            flpRecommendations.SuspendLayout();
            flpRecommendations.Controls.Clear();

            HashSet<string> viewedIds = new HashSet<string>();
            foreach (LocalEvent viewed in EventCatalogue.GetRecentlyViewed())
            {
                viewedIds.Add(viewed.Id);
            }

            foreach (EventCatalogue.RecommendedEvent rec in EventCatalogue.GetRecommendations(5, viewedIds))
            {
                EventCatalogue.RecommendedEvent captured = rec;
                Panel card = BuildCard(300, 106, () => SelectEvent(captured.Event.Id, true),
                    CreateLine(captured.Event.Title, AppTheme.BodyText, AppTheme.SectionFont, 24),
                    CreateLine(captured.Event.DisplayDate, AppTheme.MutedText, AppTheme.SmallFont, 18),
                    CreateLine("Why: " + captured.Reason, AppTheme.Accent, AppTheme.SmallFont, 34));
                flpRecommendations.Controls.Add(card);
            }

            if (flpRecommendations.Controls.Count == 0)
            {
                flpRecommendations.Controls.Add(CreateHint("Search or open events and suggestions will appear here."));
            }

            FitCards(flpRecommendations);
            flpRecommendations.ResumeLayout();
        }

        private void ShowDetail(LocalEvent item, string prefix)
        {
            txtDetailTitle.Text = item.Title;
            txtDetailBody.Text = (string.IsNullOrEmpty(prefix) ? string.Empty : prefix + Environment.NewLine) +
                                 item.Category + "  \u2022  " + (item.IsAnnouncement ? "Announcement" : "Event") + Environment.NewLine +
                                 item.DisplayDate + Environment.NewLine +
                                 "Venue: " + item.Venue + Environment.NewLine + Environment.NewLine +
                                 item.Summary;
        }

        private void ResetDetail()
        {
            txtDetailTitle.Text = "Click an event to see its details.";
            txtDetailBody.Text = "The event you open is also added to the Recently viewed stack.";
        }

        // ------------------------------------------------------------------ card helpers

        private Panel BuildCard(int width, int height, Action onClick, params Label[] lines)
        {
            Panel card = new Panel
            {
                Width = width,
                Height = height,
                BackColor = AppTheme.Card,
                Margin = new Padding(0, 0, 0, CardSpacing),
                Padding = new Padding(10, 8, 10, 6),
                Cursor = Cursors.Hand
            };

            // Controls added later are docked first (at the top), so add the lines in reverse.
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                card.Controls.Add(lines[i]);
            }

            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(AppTheme.CardBorder))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            EventHandler click = (s, e) => onClick();
            EventHandler enter = (s, e) => card.BackColor = AppTheme.CardHover;
            EventHandler leave = (s, e) =>
            {
                if (!card.ClientRectangle.Contains(card.PointToClient(Cursor.Position)))
                {
                    card.BackColor = AppTheme.Card;
                }
            };

            card.Click += click;
            card.MouseEnter += enter;
            card.MouseLeave += leave;
            foreach (Control child in card.Controls)
            {
                child.Click += click;
                child.MouseEnter += enter;
                child.MouseLeave += leave;
                child.Cursor = Cursors.Hand;
            }

            return card;
        }

        private static Label CreateLine(string text, Color color, Font font, int height)
        {
            return new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                AutoSize = false,
                AutoEllipsis = true,
                Height = height,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static Label CreateHint(string text)
        {
            return new Label
            {
                Text = text,
                Font = AppTheme.BodyFont,
                ForeColor = AppTheme.MutedText,
                AutoSize = false,
                Width = 280,
                Height = 40
            };
        }

        /// <summary>Stretches cards to the visible width so nothing is clipped or horizontally scrolled.</summary>
        private static void FitCards(FlowLayoutPanel panel)
        {
            int width = panel.ClientSize.Width - panel.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 4;
            if (width < 180)
            {
                return;
            }

            foreach (Control control in panel.Controls)
            {
                control.Width = width;
            }
        }

        // ------------------------------------------------------------------ actions

        /// <summary>
        /// Opens an event. When <paramref name="record"/> is true the event is pushed on the
        /// recently viewed stack and counted as interest; links inside the stack and queue only
        /// display the event so they never change the stack by accident.
        /// </summary>
        private void SelectEvent(string eventId, bool record)
        {
            LocalEvent item = EventCatalogue.GetById(eventId);
            if (item == null)
            {
                return;
            }

            ShowDetail(item, null);

            if (record)
            {
                EventCatalogue.PushRecentlyViewed(eventId);
                ShowRecentlyViewed("Pushed: " + item.Title);
                ShowRecommendations();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string category = cboCategory.SelectedIndex <= 0 ? null : cboCategory.SelectedItem.ToString();
            DateTime? date = chkAnyDate.Checked ? (DateTime?)null : dtpEventDate.Value;

            filtered = category != null || date.HasValue;
            EventCatalogue.RecordSearch(category ?? string.Empty, date);
            ShowResults(EventCatalogue.Search(category, date));
            ShowRecommendations();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            cboCategory.SelectedIndex = 0;
            chkAnyDate.Checked = true;
            dtpEventDate.Enabled = false;
            filtered = false;
            ShowResults(EventCatalogue.GetUpcomingEvents());
            ShowRecommendations();
        }

        private void ChkAnyDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEventDate.Enabled = !chkAnyDate.Checked;
        }

        private void BtnNextNotice_Click(object sender, EventArgs e)
        {
            if (EventCatalogue.AnnouncementCount == 0)
            {
                EventCatalogue.ReloadAnnouncements();
                ShowNotices();
                return;
            }

            LocalEvent notice = EventCatalogue.ReadNextAnnouncement();
            ShowNotices();
            if (notice != null)
            {
                ShowDetail(notice, "Notice read (removed from the front of the queue)");
            }
        }

        private void BtnPopRecent_Click(object sender, EventArgs e)
        {
            LocalEvent removed = EventCatalogue.PopRecentlyViewed();
            if (removed == null)
            {
                return;
            }

            ShowRecentlyViewed("Removed: " + removed.Title);

            LocalEvent top = EventCatalogue.PeekRecentlyViewed();
            if (top != null)
            {
                ShowDetail(top, null);
            }
            else
            {
                ResetDetail();
            }

            ShowRecommendations();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
