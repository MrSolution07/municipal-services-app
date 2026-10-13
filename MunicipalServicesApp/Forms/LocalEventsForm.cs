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
    /// Part 2 form: Local Events and Announcements. It demonstrates stacks, queues, priority
    /// queues, dictionaries, sorted dictionaries, hash sets, and a recommendation algorithm.
    /// </summary>
    public partial class LocalEventsForm : Form
    {
        private const int CardWidth = 360;
        private const int CardHeight = 110;
        private const int CardSpacing = 12;

        public LocalEventsForm()
        {
            InitializeComponent();
            ApplyTheme();
            EventCatalogue.Load();
            LoadFilters();
            ShowFeatured();
            ShowAnnouncements();
            ShowRecentlyViewed();
            ShowRecommendations();
            ShowResults(EventCatalogue.GetUpcomingEvents());
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.Background;

            panelHeader.BackColor = AppTheme.Header;
            lblTitle.ForeColor = AppTheme.HeaderText;
            lblTitle.Font = AppTheme.TitleFont;
            lblSubtitle.ForeColor = AppTheme.HeaderText;
            lblSubtitle.Font = AppTheme.SubtitleFont;

            lblFeatured.Font = AppTheme.SectionFont;
            lblSearch.Font = AppTheme.SectionFont;
            lblResults.Font = AppTheme.SectionFont;
            lblDetail.Font = AppTheme.SectionFont;
            lblRecommendations.Font = AppTheme.SectionFont;
            lblRecent.Font = AppTheme.SectionFont;
            lblNotices.Font = AppTheme.SectionFont;

            foreach (Label label in new[] { lblFeatured, lblSearch, lblResults, lblDetail, lblRecommendations, lblRecent, lblNotices })
            {
                label.ForeColor = AppTheme.BodyText;
            }

            lblCategory.ForeColor = AppTheme.BodyText;
            lblCategory.Font = AppTheme.BodyFont;
            lblDate.ForeColor = AppTheme.BodyText;
            lblDate.Font = AppTheme.BodyFont;
            lblNoResults.ForeColor = AppTheme.MutedText;
            lblNoResults.Font = AppTheme.BodyFont;
            lblSearchHint.ForeColor = AppTheme.MutedText;
            lblSearchHint.Font = AppTheme.SmallFont;

            cboCategory.Font = AppTheme.BodyFont;
            dtpEventDate.Font = AppTheme.BodyFont;
            chkAnyDate.Font = AppTheme.BodyFont;

            btnSearch.BackColor = AppTheme.Accent;
            btnSearch.ForeColor = AppTheme.AccentText;
            btnSearch.Font = AppTheme.ButtonFont;
            btnSearch.FlatAppearance.BorderSize = 0;

            btnClear.BackColor = AppTheme.Card;
            btnClear.ForeColor = AppTheme.BodyText;
            btnClear.Font = AppTheme.ButtonFont;
            btnClear.FlatAppearance.BorderColor = AppTheme.Border;

            btnBack.BackColor = AppTheme.Card;
            btnBack.ForeColor = AppTheme.BodyText;
            btnBack.Font = AppTheme.ButtonFont;
            btnBack.FlatAppearance.BorderColor = AppTheme.Border;

            btnNextNotice.BackColor = AppTheme.Accent;
            btnNextNotice.ForeColor = AppTheme.AccentText;
            btnNextNotice.Font = AppTheme.ButtonFont;
            btnNextNotice.FlatAppearance.BorderSize = 0;

            btnPopRecent.BackColor = AppTheme.Card;
            btnPopRecent.ForeColor = AppTheme.BodyText;
            btnPopRecent.Font = AppTheme.ButtonFont;
            btnPopRecent.FlatAppearance.BorderColor = AppTheme.Border;

            panelFeatured.BackColor = AppTheme.Background;
            panelDetail.BackColor = AppTheme.Card;
            panelDetail.ForeColor = AppTheme.BodyText;

            txtDetailTitle.Font = AppTheme.SectionFont;
            txtDetailTitle.ForeColor = AppTheme.BodyText;
            txtDetailTitle.AutoSize = false;
            txtDetailBody.Font = AppTheme.BodyFont;
            txtDetailBody.ForeColor = AppTheme.BodyText;
            txtDetailBody.BackColor = AppTheme.Card;
            txtDetailBody.AutoSize = false;

            toolTip.SetToolTip(btnSearch, "Find events matching the chosen category and date.");
            toolTip.SetToolTip(btnClear, "Clear the filters and show all upcoming events.");
            toolTip.SetToolTip(btnNextNotice, "Show the next notice in the queue.");
            toolTip.SetToolTip(btnPopRecent, "Remove the most recently viewed event from the list.");
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

        private void ShowFeatured()
        {
            panelFeatured.Controls.Clear();
            foreach (LocalEvent item in EventCatalogue.GetFeatured(3))
            {
                Panel card = BuildFeaturedCard(item);
                card.Margin = new Padding(0, 0, 12, 0);
                panelFeatured.Controls.Add(card);
            }
        }

        private void ShowAnnouncements()
        {
            flpNotices.Controls.Clear();
            foreach (LocalEvent item in EventCatalogue.GetAnnouncements())
            {
                Label notice = new Label
                {
                    Text = "\u2022 " + item.Title + " - " + item.Summary,
                    Font = AppTheme.BodyFont,
                    ForeColor = AppTheme.BodyText,
                    AutoSize = true,
                    MaximumSize = new Size(flpNotices.Width - 12, 0),
                    Padding = new Padding(4)
                };

                flpNotices.Controls.Add(notice);
            }
        }

        private void ShowRecommendations()
        {
            flpRecommendations.Controls.Clear();
            HashSet<string> shownIds = new HashSet<string>();

            foreach (LocalEvent result in currentResults)
            {
                shownIds.Add(result.Id);
            }

            foreach (EventCatalogue.RecommendedEvent rec in EventCatalogue.GetRecommendations(4, shownIds))
            {
                Panel card = BuildRecommendationCard(rec);
                flpRecommendations.Controls.Add(card);
            }

            if (flpRecommendations.Controls.Count == 0)
            {
                Label none = new Label
                {
                    Text = "Search or choose a category to see personalised recommendations.",
                    Font = AppTheme.BodyFont,
                    ForeColor = AppTheme.MutedText,
                    AutoSize = true
                };

                flpRecommendations.Controls.Add(none);
            }
        }

        private List<LocalEvent> currentResults = new List<LocalEvent>();

        private void ShowResults(IEnumerable<LocalEvent> events)
        {
            currentResults.Clear();
            flpResults.Controls.Clear();

            foreach (LocalEvent item in events)
            {
                currentResults.Add(item);
                Panel card = BuildResultCard(item);
                flpResults.Controls.Add(card);
            }

            bool empty = currentResults.Count == 0;
            lblNoResults.Visible = empty;
            if (empty)
            {
                lblNoResults.Text = "No events found for this filter. Try a different category or date.";
            }
        }

        private Panel BuildFeaturedCard(LocalEvent item)
        {
            Panel card = CreateCard(280, 90);
            Label title = CreateCardTitle(item.Title);
            Label meta = CreateCardMeta(item.DisplayDate);
            Label reason = CreateCardMeta("Featured");
            reason.ForeColor = AppTheme.Accent;

            title.Dock = DockStyle.Top;
            meta.Dock = DockStyle.Top;
            reason.Dock = DockStyle.Top;

            card.Controls.Add(title);
            card.Controls.Add(meta);
            card.Controls.Add(reason);

            card.Click += (s, e) => SelectEvent(item.Id);
            foreach (Control c in card.Controls)
            {
                c.Click += (s, e) => SelectEvent(item.Id);
            }

            return card;
        }

        private Panel BuildRecommendationCard(EventCatalogue.RecommendedEvent rec)
        {
            Panel card = CreateCard(340, 90);
            Label title = CreateCardTitle(rec.Event.Title);
            Label reason = CreateCardMeta(rec.Reason);
            reason.ForeColor = AppTheme.Accent;

            title.Dock = DockStyle.Top;
            reason.Dock = DockStyle.Top;

            card.Controls.Add(title);
            card.Controls.Add(reason);

            card.Click += (s, e) => SelectEvent(rec.Event.Id);
            foreach (Control c in card.Controls)
            {
                c.Click += (s, e) => SelectEvent(rec.Event.Id);
            }

            return card;
        }

        private Panel BuildResultCard(LocalEvent item)
        {
            Panel card = CreateCard(CardWidth, CardHeight);
            Label title = CreateCardTitle(item.Title);
            Label meta = CreateCardMeta(item.Category + "  \u2022  " + item.DisplayDate);
            Label venue = CreateCardMeta(item.Venue);
            venue.ForeColor = AppTheme.MutedText;

            title.Dock = DockStyle.Top;
            meta.Dock = DockStyle.Top;
            venue.Dock = DockStyle.Top;

            card.Controls.Add(title);
            card.Controls.Add(meta);
            card.Controls.Add(venue);

            card.Click += (s, e) => SelectEvent(item.Id);
            foreach (Control c in card.Controls)
            {
                c.Click += (s, e) => SelectEvent(item.Id);
            }

            return card;
        }

        private Panel CreateCard(int width, int height)
        {
            return new Panel
            {
                Width = width,
                Height = height,
                BackColor = AppTheme.Card,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 0, 0, CardSpacing),
                Padding = new Padding(12),
                Cursor = Cursors.Hand
            };
        }

        private Label CreateCardTitle(string text)
        {
            return new Label
            {
                Text = text,
                Font = AppTheme.SectionFont,
                ForeColor = AppTheme.BodyText,
                AutoSize = false,
                Height = 22,
                Dock = DockStyle.Top
            };
        }

        private Label CreateCardMeta(string text)
        {
            return new Label
            {
                Text = text,
                Font = AppTheme.BodyFont,
                ForeColor = AppTheme.MutedText,
                AutoSize = false,
                Height = 20,
                Dock = DockStyle.Top
            };
        }

        private void SelectEvent(string eventId)
        {
            LocalEvent item = EventCatalogue.GetById(eventId);
            if (item == null)
            {
                return;
            }

            EventCatalogue.PushRecentlyViewed(eventId);
            ShowRecentlyViewed();

            txtDetailTitle.Text = item.Title;
            txtDetailBody.Text = item.Category + Environment.NewLine +
                                 item.DisplayDate + Environment.NewLine +
                                 "Venue: " + item.Venue + Environment.NewLine + Environment.NewLine +
                                 item.Summary;
        }

        private void ShowRecentlyViewed()
        {
            flpRecent.Controls.Clear();
            foreach (LocalEvent item in EventCatalogue.GetRecentlyViewed())
            {
                LinkLabel link = new LinkLabel
                {
                    Text = item.Title,
                    Font = AppTheme.BodyFont,
                    AutoSize = true,
                    Padding = new Padding(2),
                    Tag = item.Id
                };

                link.LinkClicked += (s, e) => SelectEvent(link.Tag.ToString());
                flpRecent.Controls.Add(link);
            }

            if (flpRecent.Controls.Count == 0)
            {
                Label empty = new Label
                {
                    Text = "Click an event card to build this list.",
                    Font = AppTheme.BodyFont,
                    ForeColor = AppTheme.MutedText,
                    AutoSize = true
                };

                flpRecent.Controls.Add(empty);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string category = cboCategory.SelectedIndex <= 0 ? null : cboCategory.SelectedItem.ToString();
            DateTime? date = chkAnyDate.Checked ? (DateTime?)null : dtpEventDate.Value;

            EventCatalogue.RecordSearch(category ?? string.Empty, date);
            IEnumerable<LocalEvent> matches = EventCatalogue.Search(category, date);
            ShowResults(matches);
            ShowRecommendations();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            cboCategory.SelectedIndex = 0;
            chkAnyDate.Checked = true;
            dtpEventDate.Enabled = false;
            ShowResults(EventCatalogue.GetUpcomingEvents());
            ShowRecommendations();
        }

        private void ChkAnyDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEventDate.Enabled = !chkAnyDate.Checked;
        }

        private void BtnNextNotice_Click(object sender, EventArgs e)
        {
            EventCatalogue.RotateAnnouncement();
            ShowAnnouncements();
        }

        private void BtnPopRecent_Click(object sender, EventArgs e)
        {
            LocalEvent removed = EventCatalogue.PopRecentlyViewed();
            ShowRecentlyViewed();
            if (removed != null)
            {
                MessageBox.Show(this, "Removed: " + removed.Title, "Recently viewed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
