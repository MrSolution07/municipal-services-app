using System;
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Forms
{
    /// <summary>
    /// Startup form. Report Issues, Local Events, and Service Request Status are all active.
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
            StyleActiveButton(btnServiceStatus);

            toolTip.SetToolTip(btnReportIssues, "Report a municipal issue such as a water leak or a pothole.");
            toolTip.SetToolTip(btnLocalEvents, "Find upcoming events, announcements and recommendations from the municipality.");
            toolTip.SetToolTip(btnServiceStatus, "Track a service request by its reference number.");
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

        /// <summary>Shows reports captured this session and requests that are not yet resolved.</summary>
        private void UpdateSessionCount()
        {
            ServiceRequestStore.Load();
            lblSessionCount.Text = "Issues reported this session: " + IssueStore.Count
                + "    Open service requests: " + ServiceRequestStore.OpenCount;
        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            using (ReportIssuesForm reportForm = new ReportIssuesForm())
            {
                reportForm.ShowDialog(this);
                if (!string.IsNullOrEmpty(reportForm.TrackReference))
                {
                    using (ServiceRequestForm statusForm = new ServiceRequestForm())
                    {
                        statusForm.InitialReference = reportForm.TrackReference;
                        statusForm.ShowDialog(this);
                    }
                }
            }

            UpdateSessionCount();
        }

        private void BtnServiceStatus_Click(object sender, EventArgs e)
        {
            using (ServiceRequestForm statusForm = new ServiceRequestForm())
            {
                statusForm.ShowDialog(this);
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
