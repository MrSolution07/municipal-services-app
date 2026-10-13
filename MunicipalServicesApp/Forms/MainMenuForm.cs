using System;
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Forms
{
    /// <summary>
    /// Startup form. Presents the three municipal tasks; Report Issues and Local Events are
    /// active in Part 2. Service Request Status remains disabled until Part 3.
    /// </summary>
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            ApplyTheme();
            UpdateSessionCount();
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.Background;

            panelHeader.BackColor = AppTheme.Header;
            lblTitle.ForeColor = AppTheme.HeaderText;
            lblTitle.Font = AppTheme.TitleFont;
            lblSubtitle.ForeColor = AppTheme.HeaderText;
            lblSubtitle.Font = AppTheme.SubtitleFont;

            lblChoose.Font = AppTheme.SectionFont;
            lblChoose.ForeColor = AppTheme.BodyText;
            lblSessionCount.Font = AppTheme.BodyFont;
            lblSessionCount.ForeColor = AppTheme.MutedText;

            StyleActiveButton(btnReportIssues);
            StyleActiveButton(btnLocalEvents);
            StyleDisabledButton(btnServiceStatus);

            toolTip.SetToolTip(btnReportIssues, "Report a municipal issue such as a water leak or a pothole.");
            toolTip.SetToolTip(btnLocalEvents, "Find upcoming events, announcements and recommendations from the municipality.");
            toolTip.SetToolTip(btnServiceStatus, "This service is added in Part 3 of the project.");
        }

        private static void StyleActiveButton(Button button)
        {
            button.BackColor = AppTheme.Accent;
            button.ForeColor = AppTheme.AccentText;
            button.Font = AppTheme.ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.Padding = new Padding(18, 0, 0, 0);
        }

        private static void StyleDisabledButton(Button button)
        {
            button.BackColor = AppTheme.Disabled;
            button.ForeColor = AppTheme.MutedText;
            button.Font = AppTheme.ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.Padding = new Padding(18, 0, 0, 0);
        }

        /// <summary>Shows how many reports the List currently holds for this session.</summary>
        private void UpdateSessionCount()
        {
            lblSessionCount.Text = "Issues reported this session: " + IssueStore.Count;
        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            using (ReportIssuesForm reportForm = new ReportIssuesForm())
            {
                reportForm.ShowDialog(this);
            }

            UpdateSessionCount();
        }

        private void BtnLocalEvents_Click(object sender, EventArgs e)
        {
            using (LocalEventsForm eventsForm = new LocalEventsForm())
            {
                eventsForm.ShowDialog(this);
            }

            UpdateSessionCount();
        }
    }
}
