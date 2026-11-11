using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class ServiceRequestForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private TableLayoutPanel layoutMain;
        private TableLayoutPanel colLeft;
        private TreeView treeCategories;
        private Label lblBinary;
        private TableLayoutPanel layoutSearch;
        private TextBox txtReference;
        private Button btnFind;
        private TableLayoutPanel layoutMode;
        private Button btnByReference;
        private Button btnByUrgency;
        private ComboBox cboStatus;
        private FlowLayoutPanel flpList;
        private Label lblListEmpty;
        private TableLayoutPanel colMiddle;
        private Label lblDetail;
        private Panel panelDetail;
        private Label lblDetailTitle;
        private Label lblDetailBody;
        private Label lblSteps;
        private Button btnAdvance;
        private Label lblHeights;
        private Panel rightScroll;
        private TableLayoutPanel colRight;
        private Label lblUrgent;
        private Panel panelUrgent;
        private Button btnAssignNext;
        private Label lblRelated;
        private FlowLayoutPanel flpRelated;
        private Label lblChain;
        private FlowLayoutPanel flpChain;
        private Label lblRoute;
        private FlowLayoutPanel flpRoute;
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

        private static Label Heading(string name, string text)
        {
            return new Label
            {
                Name = name,
                Text = text,
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static FlowLayoutPanel ListPanel(string name)
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new ToolTip(this.components);

            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(18, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Service Request Status";
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new Point(20, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Find a request, follow its status, and see what the crews should do next.";
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new Size(1180, 84);

            this.treeCategories = new TreeView();
            this.treeCategories.Dock = DockStyle.Fill;
            this.treeCategories.Name = "treeCategories";
            this.treeCategories.HideSelection = false;
            this.treeCategories.FullRowSelect = true;
            this.treeCategories.ShowLines = true;
            this.treeCategories.BorderStyle = BorderStyle.FixedSingle;
            this.treeCategories.AfterSelect += new TreeViewEventHandler(this.TreeCategories_AfterSelect);

            this.lblBinary = Heading("lblBinary", "Binary encoding");
            this.lblBinary.AutoEllipsis = true;

            this.txtReference = new TextBox();
            this.txtReference.Dock = DockStyle.Fill;
            this.txtReference.Name = "txtReference";
            this.txtReference.Margin = new Padding(0, 6, 8, 6);
            this.btnFind = new Button();
            this.btnFind.Dock = DockStyle.Fill;
            this.btnFind.Name = "btnFind";
            this.btnFind.Text = "Find";
            this.btnFind.FlatStyle = FlatStyle.Flat;
            this.btnFind.Margin = new Padding(0, 4, 0, 4);
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            this.layoutSearch = new TableLayoutPanel();
            this.layoutSearch.Dock = DockStyle.Fill;
            this.layoutSearch.ColumnCount = 2;
            this.layoutSearch.RowCount = 1;
            this.layoutSearch.Margin = new Padding(0);
            this.layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
            this.layoutSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layoutSearch.Controls.Add(this.txtReference, 0, 0);
            this.layoutSearch.Controls.Add(this.btnFind, 1, 0);

            this.btnByReference = new Button();
            this.btnByReference.Dock = DockStyle.Fill;
            this.btnByReference.Name = "btnByReference";
            this.btnByReference.Text = "By reference";
            this.btnByReference.FlatStyle = FlatStyle.Flat;
            this.btnByReference.Margin = new Padding(0, 4, 6, 4);
            this.btnByReference.Click += new System.EventHandler(this.BtnByReference_Click);
            this.btnByUrgency = new Button();
            this.btnByUrgency.Dock = DockStyle.Fill;
            this.btnByUrgency.Name = "btnByUrgency";
            this.btnByUrgency.Text = "By urgency";
            this.btnByUrgency.FlatStyle = FlatStyle.Flat;
            this.btnByUrgency.Margin = new Padding(0, 4, 6, 4);
            this.btnByUrgency.Click += new System.EventHandler(this.BtnByUrgency_Click);
            this.cboStatus = new ComboBox();
            this.cboStatus.Dock = DockStyle.Fill;
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStatus.Margin = new Padding(0, 6, 0, 6);
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.CboStatus_SelectedIndexChanged);
            this.layoutMode = new TableLayoutPanel();
            this.layoutMode.Dock = DockStyle.Fill;
            this.layoutMode.ColumnCount = 3;
            this.layoutMode.RowCount = 1;
            this.layoutMode.Margin = new Padding(0);
            this.layoutMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            this.layoutMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            this.layoutMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            this.layoutMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layoutMode.Controls.Add(this.btnByReference, 0, 0);
            this.layoutMode.Controls.Add(this.btnByUrgency, 1, 0);
            this.layoutMode.Controls.Add(this.cboStatus, 2, 0);

            this.lblListEmpty = Heading("lblListEmpty", "No requests match this filter.");
            this.lblListEmpty.Visible = false;
            this.lblListEmpty.AutoSize = true;
            this.flpList = ListPanel("flpList");

            this.colLeft = new TableLayoutPanel();
            this.colLeft.Dock = DockStyle.Fill;
            this.colLeft.ColumnCount = 1;
            this.colLeft.RowCount = 6;
            this.colLeft.Margin = new Padding(0, 0, 12, 0);
            this.colLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.colLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 168F));
            this.colLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this.colLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.colLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.colLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.colLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.colLeft.Controls.Add(this.treeCategories, 0, 0);
            this.colLeft.Controls.Add(this.lblBinary, 0, 1);
            this.colLeft.Controls.Add(this.layoutSearch, 0, 2);
            this.colLeft.Controls.Add(this.layoutMode, 0, 3);
            this.colLeft.Controls.Add(this.lblListEmpty, 0, 4);
            this.colLeft.Controls.Add(this.flpList, 0, 5);

            this.lblDetail = Heading("lblDetail", "Selected request");
            this.lblDetailTitle = new Label();
            this.lblDetailTitle.Dock = DockStyle.Top;
            this.lblDetailTitle.Height = 28;
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.AutoEllipsis = true;
            this.lblDetailTitle.Text = "Select a request.";
            this.lblDetailBody = new Label();
            this.lblDetailBody.Dock = DockStyle.Fill;
            this.lblDetailBody.Name = "lblDetailBody";
            this.lblDetailBody.Text = "The reference, status, and description appear here.";
            this.panelDetail = new Panel();
            this.panelDetail.Dock = DockStyle.Fill;
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new Padding(12);
            this.panelDetail.Margin = new Padding(0, 0, 0, 8);
            this.panelDetail.Controls.Add(this.lblDetailBody);
            this.panelDetail.Controls.Add(this.lblDetailTitle);
            this.lblSteps = Heading("lblSteps", "Submitted  >  In review  >  Assigned  >  In progress  >  Resolved");
            this.btnAdvance = new Button();
            this.btnAdvance.Dock = DockStyle.Fill;
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Text = "Advance status";
            this.btnAdvance.FlatStyle = FlatStyle.Flat;
            this.btnAdvance.Margin = new Padding(0, 4, 0, 4);
            this.btnAdvance.Click += new System.EventHandler(this.BtnAdvance_Click);
            this.lblHeights = Heading("lblHeights", "BST height and AVL height appear after the catalogue loads.");

            this.colMiddle = new TableLayoutPanel();
            this.colMiddle.Dock = DockStyle.Fill;
            this.colMiddle.ColumnCount = 1;
            this.colMiddle.RowCount = 5;
            this.colMiddle.Margin = new Padding(0, 0, 12, 0);
            this.colMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.colMiddle.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.colMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.colMiddle.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            this.colMiddle.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            this.colMiddle.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this.colMiddle.Controls.Add(this.lblDetail, 0, 0);
            this.colMiddle.Controls.Add(this.panelDetail, 0, 1);
            this.colMiddle.Controls.Add(this.lblSteps, 0, 2);
            this.colMiddle.Controls.Add(this.btnAdvance, 0, 3);
            this.colMiddle.Controls.Add(this.lblHeights, 0, 4);

            this.lblUrgent = Heading("lblUrgent", "Next urgent (heap)");
            this.panelUrgent = new Panel();
            this.panelUrgent.Dock = DockStyle.Fill;
            this.panelUrgent.Name = "panelUrgent";
            this.panelUrgent.Margin = new Padding(0);
            this.btnAssignNext = new Button();
            this.btnAssignNext.Dock = DockStyle.Fill;
            this.btnAssignNext.Name = "btnAssignNext";
            this.btnAssignNext.Text = "Assign next";
            this.btnAssignNext.FlatStyle = FlatStyle.Flat;
            this.btnAssignNext.Margin = new Padding(0, 4, 0, 8);
            this.btnAssignNext.Click += new System.EventHandler(this.BtnAssignNext_Click);
            this.lblRelated = Heading("lblRelated", "Related work (breadth-first)");
            this.flpRelated = ListPanel("flpRelated");
            this.lblChain = Heading("lblChain", "Dependency chain (depth-first)");
            this.flpChain = ListPanel("flpChain");
            this.lblRoute = Heading("lblRoute", "Crew route (minimum spanning tree)");
            this.flpRoute = ListPanel("flpRoute");

            this.colRight = new TableLayoutPanel();
            this.colRight.Dock = DockStyle.Top;
            this.colRight.AutoSize = true;
            this.colRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.colRight.ColumnCount = 1;
            this.colRight.RowCount = 9;
            this.colRight.Margin = new Padding(0);
            this.colRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.colRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            this.colRight.Controls.Add(this.lblUrgent, 0, 0);
            this.colRight.Controls.Add(this.panelUrgent, 0, 1);
            this.colRight.Controls.Add(this.btnAssignNext, 0, 2);
            this.colRight.Controls.Add(this.lblRelated, 0, 3);
            this.colRight.Controls.Add(this.flpRelated, 0, 4);
            this.colRight.Controls.Add(this.lblChain, 0, 5);
            this.colRight.Controls.Add(this.flpChain, 0, 6);
            this.colRight.Controls.Add(this.lblRoute, 0, 7);
            this.colRight.Controls.Add(this.flpRoute, 0, 8);

            this.rightScroll = new Panel();
            this.rightScroll.Dock = DockStyle.Fill;
            this.rightScroll.AutoScroll = true;
            this.rightScroll.Name = "rightScroll";
            this.rightScroll.Controls.Add(this.colRight);

            this.layoutMain = new TableLayoutPanel();
            this.layoutMain.Dock = DockStyle.Fill;
            this.layoutMain.ColumnCount = 3;
            this.layoutMain.RowCount = 1;
            this.layoutMain.Padding = new Padding(16, 10, 16, 8);
            this.layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            this.layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            this.layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layoutMain.Controls.Add(this.colLeft, 0, 0);
            this.layoutMain.Controls.Add(this.colMiddle, 1, 0);
            this.layoutMain.Controls.Add(this.rightScroll, 2, 0);

            this.btnBack = new Button();
            this.btnBack.Dock = DockStyle.Right;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(200, 40);
            this.btnBack.Text = "Back to Main Menu";
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            Panel footer = new Panel();
            footer.Dock = DockStyle.Bottom;
            footer.Height = 52;
            footer.Padding = new Padding(16, 6, 16, 8);
            footer.Controls.Add(this.btnBack);

            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(1180, 820);
            this.Controls.Add(this.layoutMain);
            this.Controls.Add(footer);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new Size(1000, 700);
            this.Name = "ServiceRequestForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Municipal Services - Service Request Status";
        }
    }
}
