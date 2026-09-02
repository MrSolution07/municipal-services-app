namespace MunicipalServicesApp.Forms
{
    partial class ReportIssuesForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TableLayoutPanel layoutBody;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.Panel panelAttachment;
        private System.Windows.Forms.Label lblAttachmentName;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.TableLayoutPanel layoutEngagement;
        private System.Windows.Forms.ProgressBar progressReport;
        private System.Windows.Forms.Label lblEngagement;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnBackToMenu;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.layoutBody = new System.Windows.Forms.TableLayoutPanel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.panelAttachment = new System.Windows.Forms.Panel();
            this.lblAttachmentName = new System.Windows.Forms.Label();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.layoutEngagement = new System.Windows.Forms.TableLayoutPanel();
            this.progressReport = new System.Windows.Forms.ProgressBar();
            this.lblEngagement = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnBackToMenu = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelHeader.SuspendLayout();
            this.layoutBody.SuspendLayout();
            this.panelAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.layoutEngagement.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // panelHeader
            //
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.panelHeader.Size = new System.Drawing.Size(784, 86);
            this.panelHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Report an Issue";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(21, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(520, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Tell the municipality where the problem is, what type of problem it is, and what you can see.";
            //
            // layoutBody
            //
            this.layoutBody.ColumnCount = 2;
            this.layoutBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.layoutBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutBody.Controls.Add(this.lblLocation, 0, 0);
            this.layoutBody.Controls.Add(this.txtLocation, 1, 0);
            this.layoutBody.Controls.Add(this.lblCategory, 0, 1);
            this.layoutBody.Controls.Add(this.cboCategory, 1, 1);
            this.layoutBody.Controls.Add(this.lblDescription, 0, 2);
            this.layoutBody.Controls.Add(this.rtbDescription, 1, 2);
            this.layoutBody.Controls.Add(this.lblAttachment, 0, 3);
            this.layoutBody.Controls.Add(this.panelAttachment, 1, 3);
            this.layoutBody.Controls.Add(this.layoutEngagement, 0, 4);
            this.layoutBody.Controls.Add(this.panelButtons, 0, 5);
            this.layoutBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutBody.Location = new System.Drawing.Point(0, 86);
            this.layoutBody.Name = "layoutBody";
            this.layoutBody.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.layoutBody.RowCount = 6;
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.layoutBody.SetColumnSpan(this.layoutEngagement, 2);
            this.layoutBody.SetColumnSpan(this.panelButtons, 2);
            this.layoutBody.Size = new System.Drawing.Size(784, 475);
            this.layoutBody.TabIndex = 1;
            //
            // lblLocation
            //
            this.lblLocation.AutoSize = true;
            this.lblLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(174, 40);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location of the issue *";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtLocation
            //
            this.txtLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLocation.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(575, 22);
            this.txtLocation.TabIndex = 1;
            this.txtLocation.TextChanged += new System.EventHandler(this.ReportInput_Changed);
            //
            // lblCategory
            //
            this.lblCategory.AutoSize = true;
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(174, 40);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category of the issue *";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cboCategory
            //
            this.cboCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(575, 24);
            this.cboCategory.TabIndex = 3;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.ReportInput_Changed);
            //
            // lblDescription
            //
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(174, 100);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description of the issue *";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // rtbDescription
            //
            this.rtbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDescription.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(575, 100);
            this.rtbDescription.TabIndex = 5;
            this.rtbDescription.Text = "";
            this.rtbDescription.TextChanged += new System.EventHandler(this.ReportInput_Changed);
            //
            // lblAttachment
            //
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(174, 96);
            this.lblAttachment.TabIndex = 6;
            this.lblAttachment.Text = "Photo or document (optional)";
            this.lblAttachment.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            //
            // panelAttachment
            //
            this.panelAttachment.Controls.Add(this.lblAttachmentName);
            this.panelAttachment.Controls.Add(this.btnAttachFile);
            this.panelAttachment.Controls.Add(this.picPreview);
            this.panelAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttachment.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.panelAttachment.Name = "panelAttachment";
            this.panelAttachment.Size = new System.Drawing.Size(575, 82);
            this.panelAttachment.TabIndex = 7;
            //
            // picPreview
            //
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(120, 82);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 2;
            this.picPreview.TabStop = false;
            this.picPreview.Visible = false;
            //
            // btnAttachFile
            //
            this.btnAttachFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAttachFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(190, 82);
            this.btnAttachFile.TabIndex = 0;
            this.btnAttachFile.Text = "Attach image or document";
            this.btnAttachFile.UseVisualStyleBackColor = true;
            this.btnAttachFile.Click += new System.EventHandler(this.BtnAttachFile_Click);
            //
            // lblAttachmentName
            //
            this.lblAttachmentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAttachmentName.Name = "lblAttachmentName";
            this.lblAttachmentName.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblAttachmentName.Size = new System.Drawing.Size(265, 82);
            this.lblAttachmentName.TabIndex = 1;
            this.lblAttachmentName.Text = "No file attached yet.";
            this.lblAttachmentName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
