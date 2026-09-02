using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Forms
{
    /// <summary>
    /// Capture form for a single issue report. The ProgressBar and the message label form the
    /// user engagement strategy: the resident always sees how complete the report is.
    /// </summary>
    public partial class ReportIssuesForm : Form
    {
        private const int LocationWeight = 25;
        private const int CategoryWeight = 25;
        private const int DescriptionWeight = 30;
        private const int AttachmentWeight = 20;
        private const int DetailedDescriptionLength = 20;

        private static readonly string[] Categories =
        {
            "Sanitation and refuse removal",
            "Roads and potholes",
            "Water and leaks",
            "Electricity and street lights",
            "Stormwater and drainage",
            "Parks and public spaces",
            "Other"
        };

        private string attachmentPath = string.Empty;

        public ReportIssuesForm()
        {
            InitializeComponent();
            ApplyTheme();
            cboCategory.Items.AddRange(Categories);
            UpdateEngagement();
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.Background;

            panelHeader.BackColor = AppTheme.Header;
            lblTitle.ForeColor = AppTheme.HeaderText;
            lblTitle.Font = AppTheme.TitleFont;
            lblSubtitle.ForeColor = AppTheme.HeaderText;
            lblSubtitle.Font = AppTheme.SubtitleFont;

            foreach (Label label in new[] { lblLocation, lblCategory, lblDescription, lblAttachment })
            {
                label.Font = AppTheme.SectionFont;
                label.ForeColor = AppTheme.BodyText;
            }

            txtLocation.Font = AppTheme.BodyFont;
            cboCategory.Font = AppTheme.BodyFont;
            rtbDescription.Font = AppTheme.BodyFont;

            panelAttachment.BackColor = AppTheme.Background;
            lblAttachmentName.Font = AppTheme.BodyFont;
            lblAttachmentName.ForeColor = AppTheme.MutedText;
            lblEngagement.Font = AppTheme.BodyFont;
            lblEngagement.ForeColor = AppTheme.BodyText;

            btnAttachFile.BackColor = Color.White;
            btnAttachFile.ForeColor = AppTheme.BodyText;
            btnAttachFile.Font = AppTheme.ButtonFont;
            btnAttachFile.FlatAppearance.BorderColor = AppTheme.Header;

            btnBackToMenu.BackColor = Color.White;
            btnBackToMenu.ForeColor = AppTheme.BodyText;
            btnBackToMenu.Font = AppTheme.ButtonFont;
            btnBackToMenu.FlatAppearance.BorderColor = AppTheme.Header;

            btnSubmit.BackColor = AppTheme.Accent;
            btnSubmit.ForeColor = AppTheme.AccentText;
            btnSubmit.Font = AppTheme.ButtonFont;
            btnSubmit.FlatAppearance.BorderSize = 0;

            toolTip.SetToolTip(txtLocation, "For example: 14 Church Street, Bellville.");
            toolTip.SetToolTip(cboCategory, "Choose the municipal service the issue belongs to.");
            toolTip.SetToolTip(btnAttachFile, "Attach a photo or a document. This is optional but it helps the repair team.");
            toolTip.SetToolTip(progressReport, "Shows how complete your report is.");
        }

        /// <summary>Recalculates the progress bar whenever the resident changes any input.</summary>
