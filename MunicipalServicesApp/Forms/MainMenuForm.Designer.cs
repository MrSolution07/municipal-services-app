namespace MunicipalServicesApp.Forms
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TableLayoutPanel layoutBody;
        private System.Windows.Forms.Label lblChoose;
        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Button btnServiceStatus;
        private System.Windows.Forms.Label lblSessionCount;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.layoutBody = new System.Windows.Forms.TableLayoutPanel();
            this.lblChoose = new System.Windows.Forms.Label();
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.lblSessionCount = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelHeader.SuspendLayout();
            this.layoutBody.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(704, 86);
            this.panelHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(360, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Municipal Services - South Africa";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(21, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Main menu - choose the service you need help with.";
            //
            // layoutBody
            //
            this.layoutBody.ColumnCount = 1;
            this.layoutBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutBody.Controls.Add(this.lblChoose, 0, 0);
            this.layoutBody.Controls.Add(this.btnReportIssues, 0, 1);
            this.layoutBody.Controls.Add(this.btnLocalEvents, 0, 2);
            this.layoutBody.Controls.Add(this.btnServiceStatus, 0, 3);
            this.layoutBody.Controls.Add(this.lblSessionCount, 0, 4);
            this.layoutBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutBody.Location = new System.Drawing.Point(0, 86);
            this.layoutBody.Name = "layoutBody";
            this.layoutBody.Padding = new System.Windows.Forms.Padding(24, 18, 24, 18);
            this.layoutBody.RowCount = 5;
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.layoutBody.Size = new System.Drawing.Size(704, 375);
            this.layoutBody.TabIndex = 1;
            //
            // lblChoose
            //
            this.lblChoose.AutoSize = true;
            this.lblChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChoose.Location = new System.Drawing.Point(27, 18);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(650, 34);
            this.lblChoose.TabIndex = 0;
            this.lblChoose.Text = "Select one of the three municipal services below.";
            this.lblChoose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnReportIssues
            //
            this.btnReportIssues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportIssues.Location = new System.Drawing.Point(27, 61);
            this.btnReportIssues.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(650, 88);
            this.btnReportIssues.TabIndex = 1;
            this.btnReportIssues.Text = "1.  Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = true;
            this.btnReportIssues.Click += new System.EventHandler(this.BtnReportIssues_Click);
            //
            // btnLocalEvents
            //
            this.btnLocalEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLocalEvents.Enabled = false;
            this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalEvents.Location = new System.Drawing.Point(27, 161);
            this.btnLocalEvents.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(650, 88);
            this.btnLocalEvents.TabIndex = 2;
            this.btnLocalEvents.Text = "2.  Local Events and Announcements  (available later)";
            this.btnLocalEvents.UseVisualStyleBackColor = true;
            //
            // btnServiceStatus
            //
            this.btnServiceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnServiceStatus.Enabled = false;
            this.btnServiceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiceStatus.Location = new System.Drawing.Point(27, 261);
            this.btnServiceStatus.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(650, 88);
            this.btnServiceStatus.TabIndex = 3;
            this.btnServiceStatus.Text = "3.  Service Request Status  (available later)";
            this.btnServiceStatus.UseVisualStyleBackColor = true;
            //
            // lblSessionCount
            //
            this.lblSessionCount.AutoSize = true;
            this.lblSessionCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSessionCount.Location = new System.Drawing.Point(27, 361);
            this.lblSessionCount.Name = "lblSessionCount";
            this.lblSessionCount.Size = new System.Drawing.Size(650, 32);
            this.lblSessionCount.TabIndex = 4;
            this.lblSessionCount.Text = "Issues reported this session: 0";
            this.lblSessionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // MainMenuForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.layoutBody);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(620, 460);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services - Main Menu";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.layoutBody.ResumeLayout(false);
            this.layoutBody.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
