using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private TableLayoutPanel layoutBody;
        private Label lblFeatured;
        private FlowLayoutPanel panelFeatured;
        private Label lblSearch;
        private TableLayoutPanel layoutSearch;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Label lblDate;
        private CheckBox chkAnyDate;
        private DateTimePicker dtpEventDate;
        private Button btnSearch;
        private Button btnClear;
        private Label lblSearchHint;

        private TableLayoutPanel layoutMain;
        private TableLayoutPanel colEvents;
        private TableLayoutPanel colDetail;
        private TableLayoutPanel colLists;

        private Label lblResults;
        private FlowLayoutPanel flpResults;
        private Label lblNoResults;
        private Label lblDetail;
        private Panel panelDetail;
        private Label txtDetailTitle;
        private Label txtDetailBody;
        private Label lblRecommendations;
        private FlowLayoutPanel flpRecommendations;
        private Label lblRecent;
        private Label lblStackInfo;
        private FlowLayoutPanel flpRecent;
        private Button btnPopRecent;
        private Label lblNotices;
        private Label lblQueueInfo;
        private FlowLayoutPanel flpNotices;
        private Button btnNextNotice;
        private Button btnBack;
        private ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private static Label NewLabel(string name, string text)
        {
            return new Label
            {
                Name = name,
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
        }

        private static FlowLayoutPanel NewFlow(string name)
        {
            return new FlowLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Margin = new Padding(0)
            };
        }

        private static Button NewButton(string name, string text)
        {
            return new Button
            {
                Name = name,
                Text = text,
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                Margin = new Padding(0, 4, 0, 4)
            };
        }

        private static TableLayoutPanel NewColumn(string name, params RowStyle[] rows)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = rows.Length,
                Margin = new Padding(0, 0, 14, 0)
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            foreach (RowStyle row in rows)
            {
                table.RowStyles.Add(row);
            }

            return table;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new ToolTip(this.components);

            // ---------- header ----------
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(18, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Local Events and Announcements";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new Point(20, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Find upcoming events, search by category or date, and see recommendations.";

            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new Size(1200, 84);

            // ---------- body ----------
            this.layoutBody = new TableLayoutPanel();
            this.lblFeatured = NewLabel("lblFeatured", "Featured (priority queue)");
            this.panelFeatured = NewFlow("panelFeatured");
            this.panelFeatured.FlowDirection = FlowDirection.LeftToRight;
            this.panelFeatured.AutoScroll = false;
            this.lblSearch = NewLabel("lblSearch", "Search events");
            this.layoutSearch = new TableLayoutPanel();
            this.lblSearchHint = NewLabel("lblSearchHint", "Tip: pick a category, a date, or both, then press Search. Your searches and the events you open shape the recommendations.");

            // ---------- search row ----------
            this.lblCategory = new Label();
            this.cboCategory = new ComboBox();
            this.lblDate = new Label();
            this.dtpEventDate = new DateTimePicker();
            this.chkAnyDate = new CheckBox();
            this.btnSearch = new Button();
            this.btnClear = new Button();

            this.lblCategory.Anchor = AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Text = "Category";

            this.cboCategory.Anchor = AnchorStyles.Left;
            this.cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Width = 220;

            this.lblDate.Anchor = AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.lblDate.Name = "lblDate";
            this.lblDate.Text = "Date";
            this.lblDate.Margin = new Padding(18, 0, 3, 0);

            this.dtpEventDate.Anchor = AnchorStyles.Left;
            this.dtpEventDate.Format = DateTimePickerFormat.Long;
            this.dtpEventDate.Name = "dtpEventDate";
            this.dtpEventDate.Width = 230;

            this.chkAnyDate.Anchor = AnchorStyles.Left;
            this.chkAnyDate.AutoSize = true;
            this.chkAnyDate.Name = "chkAnyDate";
            this.chkAnyDate.Text = "Any date";
            this.chkAnyDate.Margin = new Padding(10, 0, 18, 0);
            this.chkAnyDate.CheckedChanged += new System.EventHandler(this.ChkAnyDate_CheckedChanged);

            this.btnSearch.Anchor = AnchorStyles.Left;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(110, 34);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            this.btnClear.Anchor = AnchorStyles.Left;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(110, 34);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);

            this.layoutSearch.ColumnCount = 8;
            this.layoutSearch.RowCount = 1;
            this.layoutSearch.Dock = DockStyle.Fill;
            this.layoutSearch.Name = "layoutSearch";
            this.layoutSearch.Margin = new Padding(0);
            this.layoutSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 7; i++)
            {
                this.layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }

            this.layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layoutSearch.Controls.Add(this.lblCategory, 0, 0);
            this.layoutSearch.Controls.Add(this.cboCategory, 1, 0);
            this.layoutSearch.Controls.Add(this.lblDate, 2, 0);
            this.layoutSearch.Controls.Add(this.dtpEventDate, 3, 0);
            this.layoutSearch.Controls.Add(this.chkAnyDate, 4, 0);
            this.layoutSearch.Controls.Add(this.btnSearch, 5, 0);
            this.layoutSearch.Controls.Add(this.btnClear, 6, 0);

            // ---------- column 1: events ----------
            this.lblResults = NewLabel("lblResults", "Upcoming events");
            this.lblNoResults = NewLabel("lblNoResults", "No events found.");
            this.lblNoResults.Visible = false;
            this.lblNoResults.AutoSize = true;
            this.lblNoResults.AutoEllipsis = false;
            this.flpResults = NewFlow("flpResults");

            this.colEvents = NewColumn("colEvents",
                new RowStyle(SizeType.Absolute, 30F),
                new RowStyle(SizeType.AutoSize),
                new RowStyle(SizeType.Percent, 100F));
            this.colEvents.Controls.Add(this.lblResults, 0, 0);
            this.colEvents.Controls.Add(this.lblNoResults, 0, 1);
            this.colEvents.Controls.Add(this.flpResults, 0, 2);

            // ---------- column 2: details + recommendations ----------
            this.lblDetail = NewLabel("lblDetail", "Selected event");
            this.panelDetail = new Panel();
            this.txtDetailTitle = new Label();
            this.txtDetailBody = new Label();
            this.lblRecommendations = NewLabel("lblRecommendations", "Recommended for you");
            this.flpRecommendations = NewFlow("flpRecommendations");

            this.txtDetailTitle.AutoEllipsis = true;
            this.txtDetailTitle.Dock = DockStyle.Top;
            this.txtDetailTitle.Height = 30;
            this.txtDetailTitle.Name = "txtDetailTitle";
            this.txtDetailTitle.Text = "Click an event to see its details.";

            this.txtDetailBody.Dock = DockStyle.Fill;
            this.txtDetailBody.Name = "txtDetailBody";
            this.txtDetailBody.Text = "The event you open is also added to the Recently viewed stack.";

            this.panelDetail.Controls.Add(this.txtDetailBody);
            this.panelDetail.Controls.Add(this.txtDetailTitle);
            this.panelDetail.Dock = DockStyle.Fill;
            this.panelDetail.Margin = new Padding(0, 0, 0, 8);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new Padding(12);

            this.colDetail = NewColumn("colDetail",
                new RowStyle(SizeType.Absolute, 30F),
                new RowStyle(SizeType.Absolute, 170F),
                new RowStyle(SizeType.Absolute, 30F),
                new RowStyle(SizeType.Percent, 100F));
            this.colDetail.Controls.Add(this.lblDetail, 0, 0);
            this.colDetail.Controls.Add(this.panelDetail, 0, 1);
            this.colDetail.Controls.Add(this.lblRecommendations, 0, 2);
            this.colDetail.Controls.Add(this.flpRecommendations, 0, 3);

            // ---------- column 3: queue + stack ----------
            this.lblNotices = NewLabel("lblNotices", "Latest notices (queue)");
            this.lblQueueInfo = NewLabel("lblQueueInfo", "");
            this.flpNotices = NewFlow("flpNotices");
            this.btnNextNotice = NewButton("btnNextNotice", "Read next notice");
            this.btnNextNotice.Click += new System.EventHandler(this.BtnNextNotice_Click);

            this.lblRecent = NewLabel("lblRecent", "Recently viewed (stack)");
            this.lblStackInfo = NewLabel("lblStackInfo", "");
            this.flpRecent = NewFlow("flpRecent");
            this.btnPopRecent = NewButton("btnPopRecent", "Undo last view");
            this.btnPopRecent.Click += new System.EventHandler(this.BtnPopRecent_Click);

            this.colLists = NewColumn("colLists",
                new RowStyle(SizeType.Absolute, 30F),
                new RowStyle(SizeType.Absolute, 22F),
                new RowStyle(SizeType.Percent, 50F),
                new RowStyle(SizeType.Absolute, 44F),
                new RowStyle(SizeType.Absolute, 30F),
                new RowStyle(SizeType.Absolute, 22F),
                new RowStyle(SizeType.Percent, 50F),
                new RowStyle(SizeType.Absolute, 44F));
            this.colLists.Margin = new Padding(0);
            this.colLists.Controls.Add(this.lblNotices, 0, 0);
            this.colLists.Controls.Add(this.lblQueueInfo, 0, 1);
            this.colLists.Controls.Add(this.flpNotices, 0, 2);
            this.colLists.Controls.Add(this.btnNextNotice, 0, 3);
            this.colLists.Controls.Add(this.lblRecent, 0, 4);
            this.colLists.Controls.Add(this.lblStackInfo, 0, 5);
            this.colLists.Controls.Add(this.flpRecent, 0, 6);
            this.colLists.Controls.Add(this.btnPopRecent, 0, 7);

            // ---------- three-column area ----------
            this.layoutMain = new TableLayoutPanel();
            this.layoutMain.ColumnCount = 3;
            this.layoutMain.RowCount = 1;
            this.layoutMain.Dock = DockStyle.Fill;
            this.layoutMain.Margin = new Padding(0);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            this.layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layoutMain.Controls.Add(this.colEvents, 0, 0);
            this.layoutMain.Controls.Add(this.colDetail, 1, 0);
            this.layoutMain.Controls.Add(this.colLists, 2, 0);

            // ---------- back button ----------
            this.btnBack = new Button();
            this.btnBack.Dock = DockStyle.Right;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(200, 40);
            this.btnBack.Text = "Back to Main Menu";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);

            Panel footer = new Panel();
            footer.Dock = DockStyle.Fill;
            footer.Margin = new Padding(0, 6, 0, 0);
            footer.Controls.Add(this.btnBack);

            // ---------- body table ----------
            this.layoutBody.ColumnCount = 1;
            this.layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layoutBody.RowCount = 7;
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            this.layoutBody.Controls.Add(this.lblFeatured, 0, 0);
            this.layoutBody.Controls.Add(this.panelFeatured, 0, 1);
            this.layoutBody.Controls.Add(this.lblSearch, 0, 2);
            this.layoutBody.Controls.Add(this.layoutSearch, 0, 3);
            this.layoutBody.Controls.Add(this.lblSearchHint, 0, 4);
            this.layoutBody.Controls.Add(this.layoutMain, 0, 5);
            this.layoutBody.Controls.Add(footer, 0, 6);
            this.layoutBody.Dock = DockStyle.Fill;
            this.layoutBody.Name = "layoutBody";
            this.layoutBody.Padding = new Padding(18, 10, 18, 10);

            // ---------- form ----------
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(1180, 760);
            this.Controls.Add(this.layoutBody);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new Size(1000, 680);
            this.Name = "LocalEventsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Municipal Services - Local Events";
        }
    }
}
