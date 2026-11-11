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
    /// Part 3 form. The category tree, binary encoding, binary search tree, AVL tree,
    /// red-black tree, heap, graph walks, and minimum spanning tree each have a visible job.
    /// </summary>
    public partial class ServiceRequestForm : Form
    {
        private const int CardSpacing = 8;

        private bool buildingTree;
        private bool byUrgency;
        private bool hasSearched;
        private string filterDepartment;
        private string filterCategory;
        private string selectedReference;
        private List<string> lastVisited = new List<string>();

        public ServiceRequestForm()
        {
            InitializeComponent();
            ApplyTheme();
            ServiceRequestStore.Load();
            cboStatus.Items.Add("All statuses");
            cboStatus.Items.Add(ServiceRequest.StatusText(RequestStatus.Submitted));
            cboStatus.Items.Add(ServiceRequest.StatusText(RequestStatus.InReview));
            cboStatus.Items.Add(ServiceRequest.StatusText(RequestStatus.Assigned));
            cboStatus.Items.Add(ServiceRequest.StatusText(RequestStatus.InProgress));
            cboStatus.Items.Add(ServiceRequest.StatusText(RequestStatus.Resolved));
            cboStatus.SelectedIndex = 0;
            rightScroll.Resize += delegate
            {
                int width = rightScroll.ClientSize.Width;
                if (width > 80)
                {
                    colRight.Width = width;
                }
            };
            flpList.SizeChanged += delegate { FitCards(flpList); };
            flpRelated.SizeChanged += delegate { FitCards(flpRelated); };
            flpChain.SizeChanged += delegate { FitCards(flpChain); };
            flpRoute.SizeChanged += delegate { FitCards(flpRoute); };
            RefreshView();
            if (rightScroll.ClientSize.Width > 80)
            {
                colRight.Width = rightScroll.ClientSize.Width;
            }
        }

        /// <summary>Set before ShowDialog. The matching request is selected when the form opens.</summary>
        public string InitialReference { get; set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!string.IsNullOrEmpty(InitialReference))
            {
                SelectRequest(InitialReference);
            }
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.Background;
            panelHeader.BackColor = AppTheme.Header;
            lblTitle.ForeColor = AppTheme.HeaderText;
            lblTitle.Font = AppTheme.TitleFont;
            lblSubtitle.ForeColor = AppTheme.HeaderText;
            lblSubtitle.Font = AppTheme.SubtitleFont;

            foreach (Label label in new[] { lblDetail, lblUrgent, lblRelated, lblChain, lblRoute })
            {
                label.Font = AppTheme.SectionFont;
                label.ForeColor = AppTheme.BodyText;
            }

            lblBinary.Font = AppTheme.SmallFont;
            lblBinary.ForeColor = AppTheme.MutedText;
            lblSteps.Font = AppTheme.SmallFont;
            lblSteps.ForeColor = AppTheme.BodyText;
            lblSteps.AutoEllipsis = false;
            lblDetailBody.AutoEllipsis = false;
            lblHeights.Font = AppTheme.SmallFont;
            lblHeights.ForeColor = AppTheme.MutedText;
            lblListEmpty.Font = AppTheme.BodyFont;
            lblListEmpty.ForeColor = AppTheme.MutedText;

            treeCategories.Font = AppTheme.BodyFont;
            treeCategories.BackColor = AppTheme.Card;
            txtReference.Font = AppTheme.BodyFont;
            cboStatus.Font = AppTheme.BodyFont;

            panelDetail.BackColor = AppTheme.Card;
            panelDetail.Paint += DrawBorder;
            lblDetailTitle.Font = AppTheme.SectionFont;
            lblDetailTitle.ForeColor = AppTheme.BodyText;
            lblDetailBody.Font = AppTheme.BodyFont;
            lblDetailBody.ForeColor = AppTheme.BodyText;

            StylePrimary(btnFind);
            StylePrimary(btnAdvance);
            StylePrimary(btnAssignNext);
            StyleSecondary(btnBack);
            StyleModeButtons();

            foreach (FlowLayoutPanel panel in new[] { flpList, flpRelated, flpChain, flpRoute })
            {
                panel.BackColor = AppTheme.Background;
            }

            panelUrgent.BackColor = AppTheme.Background;
            rightScroll.BackColor = AppTheme.Background;

            toolTip.SetToolTip(btnFind, "Search the binary search tree by reference.");
            toolTip.SetToolTip(btnByReference, "List every request in AVL reference order.");
            toolTip.SetToolTip(btnByUrgency, "List open requests in red-black urgency order.");
            toolTip.SetToolTip(btnAdvance, "Move the selected request one step forward.");
            toolTip.SetToolTip(btnAssignNext, "Assign the request at the top of the heap.");
            toolTip.SetToolTip(treeCategories, "Filter the list by department or category.");
        }

        private void RefreshView()
        {
            BuildTree();
            lblBinary.Text = "Binary encoding: " + ServiceRequestStore.BinaryPreorder;
            toolTip.SetToolTip(lblBinary, ServiceRequestStore.BinaryPreorder);
            ShowList();
            ShowUrgent();
            if (!string.IsNullOrEmpty(selectedReference) && ServiceRequestStore.Get(selectedReference) != null)
            {
                ShowDetail(ServiceRequestStore.Get(selectedReference));
                ShowRelationships(selectedReference);
            }
            else
            {
                ShowPrompt();
                ShowRelationships(null);
            }

            UpdateHeights();
        }

        private void BuildTree()
        {
            buildingTree = true;
            treeCategories.BeginUpdate();
            treeCategories.Nodes.Clear();
            TreeNode root = AddNode(null, ServiceRequestStore.CategoryRoot);
            root.ExpandAll();
            treeCategories.EndUpdate();
            buildingTree = false;
        }

        private TreeNode AddNode(TreeNode parent, CategoryNode source)
        {
            TreeNode node = new TreeNode(source.Name);
            node.Tag = source;
            if (parent == null)
            {
                treeCategories.Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node);
            }

            for (int i = 0; i < source.Children.Count; i++)
            {
                AddNode(node, source.Children[i]);
            }

            return node;
        }

        private void ShowList()
        {
            flpList.SuspendLayout();
            flpList.Controls.Clear();
            List<ServiceRequest> source = byUrgency ? ServiceRequestStore.ByUrgency() : ServiceRequestStore.ByReference();
            RequestStatus? statusFilter = SelectedStatus();
            int shown = 0;
            for (int i = 0; i < source.Count; i++)
            {
                ServiceRequest request = source[i];
                if (statusFilter.HasValue && request.Status != statusFilter.Value)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(filterDepartment) && request.Department != filterDepartment)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(filterCategory) && request.Category != filterCategory)
                {
                    continue;
                }

                string reference = request.Reference;
                Panel card = BuildCard(280, 78, delegate { SelectRequest(reference); },
                    Line(request.Reference + "  ·  " + request.StatusName, AppTheme.BodyText, AppTheme.SectionFont, 24),
                    Line(request.Area + "  ·  priority " + request.Priority, AppTheme.Accent, AppTheme.SmallFont, 18),
                    Line(request.Description, AppTheme.MutedText, AppTheme.SmallFont, 18));
                flpList.Controls.Add(card);
                shown++;
            }

            FitCards(flpList);
            flpList.ResumeLayout();
            lblListEmpty.Visible = shown == 0;
            lblListEmpty.Text = "No requests match this filter.";
        }

        private RequestStatus? SelectedStatus()
        {
            if (cboStatus.SelectedIndex <= 0)
            {
                return null;
            }

            switch (cboStatus.SelectedIndex)
            {
                case 1:
                    return RequestStatus.Submitted;
                case 2:
                    return RequestStatus.InReview;
                case 3:
                    return RequestStatus.Assigned;
                case 4:
                    return RequestStatus.InProgress;
                case 5:
                    return RequestStatus.Resolved;
                default:
                    return null;
            }
        }

        private void ShowDetail(ServiceRequest request)
        {
            lblDetailTitle.Text = request.Reference + "  ·  " + request.Category;
            string source = request.Source == RequestSource.Resident ? "Resident report" : "Municipal catalogue";
            lblDetailBody.Text = request.StatusName + "  ·  " + request.Department + " / " + request.Category + Environment.NewLine +
                                 request.Location + Environment.NewLine +
                                 "Area: " + request.Area + Environment.NewLine +
                                 "Submitted: " + request.SubmittedAt.ToString("d MMMM yyyy HH:mm") + Environment.NewLine +
                                 "Source: " + source + Environment.NewLine + Environment.NewLine +
                                 request.Description;
            lblSteps.Text = Steps(request.Status);
            SetEnabled(btnAdvance, request.Status != RequestStatus.Resolved, true);
        }

        private void ShowPrompt()
        {
            lblDetailTitle.Text = "Select a request.";
            lblDetailBody.Text = "Choose a card, or search for a reference such as SR-07.";
            lblSteps.Text = "Submitted  >  In review  >  Assigned  >  In progress  >  Resolved";
            SetEnabled(btnAdvance, false, true);
        }

        private void ShowMissing(string title, string body)
        {
            lblDetailTitle.Text = title;
            lblDetailBody.Text = body;
            lblSteps.Text = "Submitted  >  In review  >  Assigned  >  In progress  >  Resolved";
            SetEnabled(btnAdvance, false, true);
        }

        private static string Steps(RequestStatus status)
        {
            string[] names = { "Submitted", "In review", "Assigned", "In progress", "Resolved" };
            int index = (int)status;
            string text = string.Empty;
            for (int i = 0; i < names.Length; i++)
            {
                if (i > 0)
                {
                    text += "  >  ";
                }

                if (i == index)
                {
                    text += "[" + names[i] + "]";
                }
                else
                {
                    text += names[i];
                }
            }

            return text;
        }

        private void ShowUrgent()
        {
            panelUrgent.Controls.Clear();
            ServiceRequest urgent = ServiceRequestStore.PeekUrgent();
            if (urgent == null)
            {
                panelUrgent.Controls.Add(Hint("No request is waiting to be assigned.", panelUrgent.Width));
                SetEnabled(btnAssignNext, false, true);
                return;
            }

            string reference = urgent.Reference;
            Panel card = BuildCard(280, 68, delegate { SelectRequest(reference); },
                Line(urgent.Reference + "  ·  " + urgent.StatusName, AppTheme.BodyText, AppTheme.SectionFont, 24),
                Line("Priority " + urgent.Priority + "  ·  " + urgent.Area, AppTheme.Accent, AppTheme.SmallFont, 18));
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(0);
            panelUrgent.Controls.Add(card);
            SetEnabled(btnAssignNext, true, true);
        }

        private void ShowRelationships(string reference)
        {
            ShowFlow(flpRelated, null);
            ShowFlow(flpChain, null);
            ShowFlow(flpRoute, null);

            if (string.IsNullOrEmpty(reference))
            {
                ShowFlow(flpRelated, new[] { Hint("Select a request to see related work.", 260) });
                ShowFlow(flpChain, new[] { Hint("Select a request to see the dependency chain.", 260) });
            }
            else
            {
                List<RelatedStep> related = ServiceRequestStore.RelatedWork(reference);
                if (related.Count == 0)
                {
                    ShowFlow(flpRelated, new[] { Hint("No related work in this area.", 260) });
                }
                else
                {
                    List<Control> cards = new List<Control>();
                    for (int i = 0; i < related.Count; i++)
                    {
                        string id = related[i].Reference;
                        int steps = related[i].Steps;
                        string stepText = steps == 1 ? "1 step away" : steps + " steps away";
                        ServiceRequest item = ServiceRequestStore.Get(id);
                        cards.Add(BuildCard(260, 60, delegate { SelectRequest(id); },
                            Line(id + "  ·  " + (item == null ? string.Empty : item.StatusName), AppTheme.BodyText, AppTheme.SectionFont, 24),
                            Line(stepText, AppTheme.Accent, AppTheme.SmallFont, 18)));
                    }

                    ShowFlow(flpRelated, cards);
                }

                List<string> chain = ServiceRequestStore.DependencyChain(reference);
                if (chain.Count <= 1)
                {
                    ShowFlow(flpChain, new[] { Hint("No earlier work. This request does not depend on another.", 260) });
                }
                else
                {
                    List<Control> cards = new List<Control>();
                    for (int i = 0; i < chain.Count; i++)
                    {
                        string id = chain[i];
                        ServiceRequest item = ServiceRequestStore.Get(id);
                        int place = i + 1;
                        cards.Add(BuildCard(260, 60, delegate { SelectRequest(id); },
                            Line(place + ".  " + id, AppTheme.BodyText, AppTheme.SectionFont, 24),
                            Line(item == null ? string.Empty : item.StatusName + "  ·  " + item.Category, AppTheme.MutedText, AppTheme.SmallFont, 18)));
                    }

                    ShowFlow(flpChain, cards);
                }
            }

            IList<RouteEdge> route = ServiceRequestStore.CrewRoute;
            if (route.Count == 0)
            {
                ShowFlow(flpRoute, new[] { Hint("No open requests to route.", 260) });
            }
            else
            {
                List<Control> cards = new List<Control>();
                cards.Add(Hint(route.Count + " edges, " + ServiceRequestStore.CrewKilometres + " km.", 260));
                for (int i = 0; i < route.Count; i++)
                {
                    RouteEdge edge = route[i];
                    cards.Add(Hint(edge.From + " to " + edge.To + ", " + edge.Kilometres + " km", 260));
                }

                ShowFlow(flpRoute, cards);
            }
        }

        private void ShowFlow(FlowLayoutPanel panel, System.Collections.IEnumerable controls)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();
            if (controls != null)
            {
                foreach (Control control in controls)
                {
                    panel.Controls.Add(control);
                }
            }

            FitCards(panel);
            panel.ResumeLayout();
        }

        private void UpdateHeights()
        {
            if (hasSearched)
            {
                lblHeights.Text = "BST visited " + lastVisited.Count + " nodes, height " + ServiceRequestStore.BstHeight
                    + ". AVL height " + ServiceRequestStore.AvlHeight + ".";
            }
            else
            {
                lblHeights.Text = "BST height " + ServiceRequestStore.BstHeight + ". AVL height " + ServiceRequestStore.AvlHeight + ".";
            }
        }

        private void SelectRequest(string reference)
        {
            ServiceRequest request = ServiceRequestStore.Get(reference);
            if (request == null)
            {
                ShowMissing("No request with that reference.", "Check the reference and try again. References look like SR-07.");
                ShowRelationships(null);
                return;
            }

            selectedReference = reference;
            ShowDetail(request);
            ShowRelationships(reference);
            UpdateHeights();
        }

        private void TreeCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (buildingTree)
            {
                return;
            }

            CategoryNode node = e.Node == null ? null : e.Node.Tag as CategoryNode;
            if (node == null || string.IsNullOrEmpty(node.Department))
            {
                filterDepartment = null;
                filterCategory = null;
            }
            else if (string.IsNullOrEmpty(node.Category))
            {
                filterDepartment = node.Department;
                filterCategory = null;
            }
            else
            {
                filterDepartment = node.Department;
                filterCategory = node.Category;
            }

            ShowList();
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            string reference = txtReference.Text.Trim();
            if (reference.Length == 0)
            {
                hasSearched = false;
                ShowMissing("Enter a reference number.", "Try SR-07.");
                UpdateHeights();
                return;
            }

            ReferenceSearch search = ServiceRequestStore.Find(reference);
            hasSearched = true;
            lastVisited = search.Visited;
            if (!search.Found)
            {
                selectedReference = null;
                ShowMissing("No request with that reference.", "Check the reference and try again. References look like SR-07.");
                ShowRelationships(null);
                UpdateHeights();
                return;
            }

            SelectRequest(reference);
        }

        private void BtnByReference_Click(object sender, EventArgs e)
        {
            byUrgency = false;
            StyleModeButtons();
            ShowList();
        }

        private void BtnByUrgency_Click(object sender, EventArgs e)
        {
            byUrgency = true;
            StyleModeButtons();
            ShowList();
        }

        private void CboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.SelectedIndex < 0)
            {
                return;
            }

            ShowList();
        }

        private void BtnAdvance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReference))
            {
                return;
            }

            ServiceRequestStore.TryAdvance(selectedReference);
            RefreshView();
        }

        private void BtnAssignNext_Click(object sender, EventArgs e)
        {
            ServiceRequest urgent = ServiceRequestStore.PeekUrgent();
            if (urgent == null)
            {
                return;
            }

            string reference = urgent.Reference;
            ServiceRequestStore.AssignNext();
            selectedReference = reference;
            RefreshView();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StyleModeButtons()
        {
            if (byUrgency)
            {
                StyleSecondary(btnByReference);
                StylePrimary(btnByUrgency);
            }
            else
            {
                StylePrimary(btnByReference);
                StyleSecondary(btnByUrgency);
            }
        }

        private static void StylePrimary(Button button)
        {
            button.BackColor = AppTheme.Accent;
            button.ForeColor = AppTheme.AccentText;
            button.Font = AppTheme.ButtonFont;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
        }

        private static void StyleSecondary(Button button)
        {
            button.BackColor = AppTheme.Card;
            button.ForeColor = AppTheme.BodyText;
            button.Font = AppTheme.ButtonFont;
            button.FlatAppearance.BorderColor = AppTheme.Border;
            button.FlatAppearance.BorderSize = 1;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
        }

        private static void SetEnabled(Button button, bool enabled, bool primary)
        {
            button.Enabled = enabled;
            if (!enabled)
            {
                button.BackColor = AppTheme.Disabled;
                button.ForeColor = AppTheme.MutedText;
                return;
            }

            if (primary)
            {
                StylePrimary(button);
            }
            else
            {
                StyleSecondary(button);
            }
        }

        private Panel BuildCard(int width, int height, Action onClick, params Label[] lines)
        {
            Panel card = new Panel
            {
                Width = width,
                Height = height,
                BackColor = AppTheme.Card,
                Margin = new Padding(0, 0, 0, CardSpacing),
                Padding = new Padding(10, 6, 10, 4),
                Cursor = Cursors.Hand
            };

            card.Paint += DrawBorder;
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                card.Controls.Add(lines[i]);
            }

            EventHandler click = delegate { onClick(); };
            EventHandler enter = delegate { card.BackColor = AppTheme.CardHover; };
            EventHandler leave = delegate
            {
                if (!card.ClientRectangle.Contains(card.PointToClient(Cursor.Position)))
                {
                    card.BackColor = AppTheme.Card;
                }
            };

            card.Click += click;
            card.MouseEnter += enter;
            card.MouseLeave += leave;
            foreach (Control child in card.Controls)
            {
                child.Click += click;
                child.MouseEnter += enter;
                child.MouseLeave += leave;
                child.Cursor = Cursors.Hand;
            }

            return card;
        }

        private static void DrawBorder(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (Pen pen = new Pen(AppTheme.CardBorder))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
            }
        }

        private static Label Line(string text, Color color, Font font, int height)
        {
            return new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                AutoSize = false,
                AutoEllipsis = true,
                Height = height,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static Label Hint(string text, int width)
        {
            return new Label
            {
                Text = text,
                Font = AppTheme.BodyFont,
                ForeColor = AppTheme.MutedText,
                AutoSize = false,
                Width = width < 180 ? 180 : width,
                Height = 40
            };
        }

        private static void FitCards(FlowLayoutPanel panel)
        {
            int width = panel.ClientSize.Width - panel.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 4;
            if (width < 160)
            {
                return;
            }

            foreach (Control control in panel.Controls)
            {
                control.Width = width;
            }
        }
    }
}
