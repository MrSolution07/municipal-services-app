namespace MunicipalServicesApp.Forms
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TableLayoutPanel layoutBody;
        private System.Windows.Forms.Label lblFeatured;
        private System.Windows.Forms.FlowLayoutPanel panelFeatured;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TableLayoutPanel layoutSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.CheckBox chkAnyDate;
        private System.Windows.Forms.DateTimePicker dtpEventDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblSearchHint;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.FlowLayoutPanel flpResults;
        private System.Windows.Forms.Label lblNoResults;
        private System.Windows.Forms.Label lblDetail;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label txtDetailTitle;
        private System.Windows.Forms.Label txtDetailBody;
        private System.Windows.Forms.Label lblRecommendations;
        private System.Windows.Forms.FlowLayoutPanel flpRecommendations;
        private System.Windows.Forms.Label lblRecent;
        private System.Windows.Forms.FlowLayoutPanel flpRecent;
        private System.Windows.Forms.Button btnPopRecent;
        private System.Windows.Forms.Label lblNotices;
        private System.Windows.Forms.FlowLayoutPanel flpNotices;
        private System.Windows.Forms.Button btnNextNotice;
        private System.Windows.Forms.Button btnBack;
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
            this.lblFeatured = new System.Windows.Forms.Label();
            this.panelFeatured = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.layoutSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.chkAnyDate = new System.Windows.Forms.CheckBox();
            this.dtpEventDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblSearchHint = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lblResults = new System.Windows.Forms.Label();
            this.flpResults = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNoResults = new System.Windows.Forms.Label();
            this.lblDetail = new System.Windows.Forms.Label();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.txtDetailBody = new System.Windows.Forms.Label();
            this.txtDetailTitle = new System.Windows.Forms.Label();
            this.lblRecommendations = new System.Windows.Forms.Label();
            this.flpRecommendations = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRecent = new System.Windows.Forms.Label();
            this.flpRecent = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPopRecent = new System.Windows.Forms.Button();
            this.lblNotices = new System.Windows.Forms.Label();
            this.flpNotices = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNextNotice = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelHeader.SuspendLayout();
            this.layoutBody.SuspendLayout();
            this.layoutSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.panelDetail.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(1100, 86);
            this.panelHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(320, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Local Events and Announcements";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(21, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Find upcoming events, search by category or date, and see recommendations.";
            //
            // layoutBody
            //
            this.layoutBody.ColumnCount = 1;
            this.layoutBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutBody.Controls.Add(this.lblFeatured, 0, 0);
            this.layoutBody.Controls.Add(this.panelFeatured, 0, 1);
            this.layoutBody.Controls.Add(this.lblSearch, 0, 2);
            this.layoutBody.Controls.Add(this.layoutSearch, 0, 3);
            this.layoutBody.Controls.Add(this.lblSearchHint, 0, 4);
            this.layoutBody.Controls.Add(this.splitMain, 0, 5);
            this.layoutBody.Controls.Add(this.btnBack, 0, 6);
            this.layoutBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutBody.Location = new System.Drawing.Point(0, 86);
            this.layoutBody.Name = "layoutBody";
            this.layoutBody.Padding = new System.Windows.Forms.Padding(18);
            this.layoutBody.RowCount = 7;
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.layoutBody.Size = new System.Drawing.Size(1100, 634);
            this.layoutBody.TabIndex = 1;
            //
            // lblFeatured
            //
            this.lblFeatured.AutoSize = true;
            this.lblFeatured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeatured.Location = new System.Drawing.Point(21, 18);
            this.lblFeatured.Name = "lblFeatured";
            this.lblFeatured.Size = new System.Drawing.Size(1058, 24);
            this.lblFeatured.TabIndex = 0;
            this.lblFeatured.Text = "Featured";
            //
            // panelFeatured
            //
            this.panelFeatured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFeatured.Location = new System.Drawing.Point(21, 45);
            this.panelFeatured.Name = "panelFeatured";
            this.panelFeatured.Size = new System.Drawing.Size(1058, 104);
            this.panelFeatured.TabIndex = 1;
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearch.Location = new System.Drawing.Point(21, 152);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(1058, 24);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search events";
            //
            // layoutSearch
            //
            this.layoutSearch.ColumnCount = 6;
            this.layoutSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.layoutSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layoutSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.layoutSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.layoutSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.layoutSearch.Controls.Add(this.lblCategory, 0, 0);
            this.layoutSearch.Controls.Add(this.cboCategory, 1, 0);
            this.layoutSearch.Controls.Add(this.lblDate, 2, 0);
            this.layoutSearch.Controls.Add(this.dtpEventDate, 3, 0);
            this.layoutSearch.Controls.Add(this.chkAnyDate, 3, 1);
            this.layoutSearch.Controls.Add(this.btnSearch, 4, 0);
            this.layoutSearch.Controls.Add(this.btnClear, 5, 0);
            this.layoutSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSearch.Location = new System.Drawing.Point(21, 179);
            this.layoutSearch.Name = "layoutSearch";
            this.layoutSearch.RowCount = 2;
            this.layoutSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutSearch.Size = new System.Drawing.Size(1058, 40);
            this.layoutSearch.TabIndex = 3;
            //
            // lblCategory
            //
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(3, 2);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(55, 15);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category";
            //
            // cboCategory
            //
            this.cboCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(73, 3);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(200, 21);
            this.cboCategory.TabIndex = 1;
            //
            // lblDate
            //
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(319, 2);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(30, 15);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            //
            // chkAnyDate
            //
            this.chkAnyDate.AutoSize = true;
            this.chkAnyDate.Location = new System.Drawing.Point(319, 23);
            this.chkAnyDate.Name = "chkAnyDate";
            this.chkAnyDate.Size = new System.Drawing.Size(68, 14);
            this.chkAnyDate.TabIndex = 4;
            this.chkAnyDate.Text = "Any date";
            this.chkAnyDate.UseVisualStyleBackColor = true;
            this.chkAnyDate.CheckedChanged += new System.EventHandler(this.ChkAnyDate_CheckedChanged);
            //
            // dtpEventDate
            //
            this.dtpEventDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEventDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEventDate.Location = new System.Drawing.Point(319, 3);
            this.dtpEventDate.Name = "dtpEventDate";
            this.dtpEventDate.Size = new System.Drawing.Size(110, 20);
            this.dtpEventDate.TabIndex = 3;
            //
            // btnSearch
            //
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(881, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(84, 30);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            //
            // btnClear
            //
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(971, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            //
            // lblSearchHint
            //
            this.lblSearchHint.AutoSize = true;
            this.lblSearchHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchHint.Location = new System.Drawing.Point(21, 222);
            this.lblSearchHint.Name = "lblSearchHint";
            this.lblSearchHint.Size = new System.Drawing.Size(1058, 20);
            this.lblSearchHint.TabIndex = 4;
            this.lblSearchHint.Text = "Tip: choose a category, or a category plus a date, then press Search to see recommendations.";
            //
            // splitMain
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(21, 245);
            this.splitMain.Name = "splitMain";
            //
            // splitMain.Panel1
            //
            this.splitMain.Panel1.Controls.Add(this.lblResults);
            this.splitMain.Panel1.Controls.Add(this.lblNoResults);
            this.splitMain.Panel1.Controls.Add(this.flpResults);
            //
            // splitMain.Panel2
            //
            this.splitMain.Panel2.AutoScroll = true;
            this.splitMain.Panel2.Controls.Add(this.lblDetail);
            this.splitMain.Panel2.Controls.Add(this.panelDetail);
            this.splitMain.Panel2.Controls.Add(this.lblRecommendations);
            this.splitMain.Panel2.Controls.Add(this.flpRecommendations);
            this.splitMain.Panel2.Controls.Add(this.lblRecent);
            this.splitMain.Panel2.Controls.Add(this.flpRecent);
            this.splitMain.Panel2.Controls.Add(this.btnPopRecent);
            this.splitMain.Panel2.Controls.Add(this.lblNotices);
            this.splitMain.Panel2.Controls.Add(this.flpNotices);
            this.splitMain.Panel2.Controls.Add(this.btnNextNotice);
            this.splitMain.Size = new System.Drawing.Size(1058, 320);
            this.splitMain.SplitterDistance = 500;
            this.splitMain.TabIndex = 5;
            //
            // lblResults
            //
            this.lblResults.AutoSize = true;
            this.lblResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResults.Location = new System.Drawing.Point(0, 0);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(500, 17);
            this.lblResults.TabIndex = 0;
            this.lblResults.Text = "Upcoming events";
            //
            // lblNoResults
            //
            this.lblNoResults.AutoSize = true;
            this.lblNoResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNoResults.Location = new System.Drawing.Point(0, 17);
            this.lblNoResults.Name = "lblNoResults";
            this.lblNoResults.Size = new System.Drawing.Size(500, 17);
            this.lblNoResults.TabIndex = 1;
            this.lblNoResults.Text = "No events found.";
            this.lblNoResults.Visible = false;
            //
            // flpResults
            //
            this.flpResults.AutoScroll = true;
            this.flpResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpResults.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpResults.Location = new System.Drawing.Point(0, 34);
            this.flpResults.Name = "flpResults";
            this.flpResults.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpResults.Size = new System.Drawing.Size(500, 286);
            this.flpResults.TabIndex = 2;
            this.flpResults.WrapContents = false;
            //
            // lblDetail
            //
            this.lblDetail.AutoSize = true;
            this.lblDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetail.Location = new System.Drawing.Point(0, 0);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(554, 17);
            this.lblDetail.TabIndex = 0;
            this.lblDetail.Text = "Selected event";
            //
            // panelDetail
            //
            this.panelDetail.Controls.Add(this.txtDetailBody);
            this.panelDetail.Controls.Add(this.txtDetailTitle);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetail.Location = new System.Drawing.Point(0, 17);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new System.Windows.Forms.Padding(12);
            this.panelDetail.Size = new System.Drawing.Size(554, 110);
            this.panelDetail.TabIndex = 1;
            //
            // txtDetailTitle
            //
            this.txtDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDetailTitle.Location = new System.Drawing.Point(12, 12);
            this.txtDetailTitle.Name = "txtDetailTitle";
            this.txtDetailTitle.Size = new System.Drawing.Size(530, 22);
            this.txtDetailTitle.TabIndex = 0;
            this.txtDetailTitle.Text = "Click an event to see details.";
            //
            // txtDetailBody
            //
            this.txtDetailBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetailBody.Location = new System.Drawing.Point(12, 34);
            this.txtDetailBody.Name = "txtDetailBody";
            this.txtDetailBody.Size = new System.Drawing.Size(530, 64);
            this.txtDetailBody.TabIndex = 1;
            this.txtDetailBody.Text = "Details appear here.";
            //
            // lblRecommendations
            //
            this.lblRecommendations.AutoSize = true;
            this.lblRecommendations.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecommendations.Location = new System.Drawing.Point(0, 127);
            this.lblRecommendations.Name = "lblRecommendations";
            this.lblRecommendations.Size = new System.Drawing.Size(554, 17);
            this.lblRecommendations.TabIndex = 2;
            this.lblRecommendations.Text = "Recommended for you";
            //
            // flpRecommendations
            //
            this.flpRecommendations.AutoScroll = true;
            this.flpRecommendations.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpRecommendations.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRecommendations.Location = new System.Drawing.Point(0, 144);
            this.flpRecommendations.Name = "flpRecommendations";
            this.flpRecommendations.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpRecommendations.Size = new System.Drawing.Size(554, 80);
            this.flpRecommendations.TabIndex = 3;
            this.flpRecommendations.WrapContents = false;
            //
            // lblRecent
            //
            this.lblRecent.AutoSize = true;
            this.lblRecent.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecent.Location = new System.Drawing.Point(0, 224);
            this.lblRecent.Name = "lblRecent";
            this.lblRecent.Size = new System.Drawing.Size(554, 17);
            this.lblRecent.TabIndex = 4;
            this.lblRecent.Text = "Recently viewed (stack)";
            //
            // flpRecent
            //
            this.flpRecent.AutoScroll = true;
            this.flpRecent.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpRecent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRecent.Location = new System.Drawing.Point(0, 241);
            this.flpRecent.Name = "flpRecent";
            this.flpRecent.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpRecent.Size = new System.Drawing.Size(554, 70);
            this.flpRecent.TabIndex = 5;
            this.flpRecent.WrapContents = false;
            //
            // btnPopRecent
            //
            this.btnPopRecent.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPopRecent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPopRecent.Location = new System.Drawing.Point(0, 311);
            this.btnPopRecent.Name = "btnPopRecent";
            this.btnPopRecent.Size = new System.Drawing.Size(554, 32);
            this.btnPopRecent.TabIndex = 6;
            this.btnPopRecent.Text = "Remove last viewed";
            this.btnPopRecent.UseVisualStyleBackColor = true;
            this.btnPopRecent.Click += new System.EventHandler(this.BtnPopRecent_Click);
            //
            // lblNotices
            //
            this.lblNotices.AutoSize = true;
            this.lblNotices.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotices.Location = new System.Drawing.Point(0, 343);
            this.lblNotices.Name = "lblNotices";
            this.lblNotices.Size = new System.Drawing.Size(554, 17);
            this.lblNotices.TabIndex = 7;
            this.lblNotices.Text = "Latest notices (queue)";
            //
            // flpNotices
            //
            this.flpNotices.AutoScroll = true;
            this.flpNotices.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpNotices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpNotices.Location = new System.Drawing.Point(0, 360);
            this.flpNotices.Name = "flpNotices";
            this.flpNotices.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpNotices.Size = new System.Drawing.Size(554, 70);
            this.flpNotices.TabIndex = 8;
            this.flpNotices.WrapContents = false;
            //
            // btnNextNotice
            //
            this.btnNextNotice.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNextNotice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextNotice.Location = new System.Drawing.Point(0, 430);
            this.btnNextNotice.Name = "btnNextNotice";
            this.btnNextNotice.Size = new System.Drawing.Size(554, 32);
            this.btnNextNotice.TabIndex = 9;
            this.btnNextNotice.Text = "Next notice";
            this.btnNextNotice.UseVisualStyleBackColor = true;
            this.btnNextNotice.Click += new System.EventHandler(this.BtnNextNotice_Click);
            //
            // btnBack
            //
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(972, 569);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(107, 40);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back to Main Menu";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            //
            // LocalEventsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 720);
            this.Controls.Add(this.layoutBody);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Municipal Services - Local Events";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.layoutBody.ResumeLayout(false);
            this.layoutSearch.ResumeLayout(false);
            this.layoutSearch.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
