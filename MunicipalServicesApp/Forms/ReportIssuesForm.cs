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
        private void ReportInput_Changed(object sender, EventArgs e)
        {
            UpdateEngagement();
        }

        private void UpdateEngagement()
        {
            int progress = CalculateProgress();
            progressReport.Value = progress;
            lblEngagement.Text = BuildEngagementMessage(progress);
        }

        private int CalculateProgress()
        {
            int progress = 0;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                progress += LocationWeight;
            }

            if (cboCategory.SelectedIndex >= 0)
            {
                progress += CategoryWeight;
            }

            string description = rtbDescription.Text.Trim();
            if (description.Length >= DetailedDescriptionLength)
            {
                progress += DescriptionWeight;
            }
            else if (description.Length > 0)
            {
                progress += DescriptionWeight / 2;
            }

            if (!string.IsNullOrEmpty(attachmentPath))
            {
                progress += AttachmentWeight;
            }

            return progress > 100 ? 100 : progress;
        }

        private string BuildEngagementMessage(int progress)
        {
            string prefix = progress + "% complete. ";

            if (progress >= 100)
            {
                return prefix + "Your report is complete. Press Submit to send it to the municipality.";
            }

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                return prefix + "Start by telling us where the problem is, so the team can find it.";
            }

            if (cboCategory.SelectedIndex < 0)
            {
                return prefix + "Good start. Now choose the category so we send this to the right department.";
            }

            if (rtbDescription.Text.Trim().Length < DetailedDescriptionLength)
            {
                return prefix + "Almost there. Add a little more detail about what you can see.";
            }

            return prefix + "Thank you. Adding a photo or document helps the team even more.";
        }

        private void BtnAttachFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string selectedPath = openFileDialog.FileName;

            if (!File.Exists(selectedPath))
            {
                MessageBox.Show(
                    this,
                    "That file could not be found. Please choose another file.",
                    "Attachment not found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            attachmentPath = selectedPath;
            lblAttachmentName.Text = "Attached: " + Path.GetFileName(selectedPath);
            ShowPreview(selectedPath);
            UpdateEngagement();
        }

        /// <summary>Shows a thumbnail for images. Documents and unreadable files simply show no preview.</summary>
        private void ShowPreview(string path)
        {
            ClearPreview();

            try
            {
                using (FileStream stream = File.OpenRead(path))
                using (Image original = Image.FromStream(stream))
                {
                    picPreview.Image = new Bitmap(original);
                    picPreview.Visible = true;
                }
            }
            catch (Exception)
            {
                ClearPreview();
            }
        }

        private void ClearPreview()
        {
            if (picPreview.Image != null)
            {
                Image previous = picPreview.Image;
                picPreview.Image = null;
                previous.Dispose();
            }

            picPreview.Visible = false;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!IsReportValid())
            {
                return;
            }

            IssueReport report = new IssueReport
            {
                Location = txtLocation.Text.Trim(),
                Category = cboCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text.Trim(),
                AttachmentPath = attachmentPath
            };

            string reference = IssueStore.Add(report);

            progressReport.Value = 100;
            lblEngagement.Text = "100% complete. Report submitted - thank you for helping your municipality.";

            MessageBox.Show(
                this,
                "Thank you. Your issue has been submitted." + Environment.NewLine + Environment.NewLine +
                "Reference number: " + reference + Environment.NewLine +
                "Category: " + report.Category + Environment.NewLine +
                "Reports captured this session: " + IssueStore.Count,
                "Report submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ResetForm();
        }

        private bool IsReportValid()
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                ShowValidationError("Please enter the location of the issue.", txtLocation);
                return false;
            }

            if (cboCategory.SelectedIndex < 0)
            {
                ShowValidationError("Please select the category of the issue.", cboCategory);
                return false;
            }

            if (rtbDescription.Text.Trim().Length == 0)
            {
                ShowValidationError("Please describe the issue so the municipality knows what to fix.", rtbDescription);
                return false;
            }

            return true;
        }

        private void ShowValidationError(string message, Control controlToFocus)
        {
            MessageBox.Show(this, message, "Information still needed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            controlToFocus.Focus();
        }
