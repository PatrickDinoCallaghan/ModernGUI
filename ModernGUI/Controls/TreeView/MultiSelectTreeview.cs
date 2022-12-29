#nullable enable
namespace ModernGUI.Controls
{
    public class MultiSelectTreeview : TreeView
    {
        private List<TreeNode> m_SelectedNodes;
        private TreeNode m_SelectedNode;
        public MultiSelectTreeview.NodeSelect OnNodeSelection;

        public List<TreeNode> SelectedNodes
        {
            get => this.m_SelectedNodes;
            set
            {
                this.ClearSelectedNodes();
                if (value == null)
                    return;
                foreach (TreeNode node in value)
                    this.ToggleNode(node, true);
            }
        }

        public new TreeNode SelectedNode
        {
            get => this.m_SelectedNode;
            set
            {
                this.ClearSelectedNodes();
                if (value == null)
                    return;
                this.SelectNode(value);
            }
        }

        public MultiSelectTreeview()
        {
            this.m_SelectedNodes = new List<TreeNode>();
            base.SelectedNode = (TreeNode)null;
        }

        public void NodeSelection(TreeNode SelectedNode)
        {
            MultiSelectTreeview.NodeSelect onNodeSelection = this.OnNodeSelection;
            if (onNodeSelection == null)
                return;
            onNodeSelection(SelectedNode);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            try
            {
                this.NodeSelection(this.GetNodeAt(e.Location));
                this.OnDoubleClick((EventArgs)e);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            try
            {
                if (this.m_SelectedNode == null && this.TopNode != null)
                    this.ToggleNode(this.TopNode, true);
                base.OnGotFocus(e);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                base.SelectedNode = (TreeNode)null;
                TreeNode nodeAt = this.GetNodeAt(e.Location);
                if (nodeAt != null)
                {
                    int x = nodeAt.Bounds.X;
                    int num = nodeAt.Bounds.Right + 10;
                    if (e.Location.X > x && e.Location.X < num && (Control.ModifierKeys != Keys.None || !this.m_SelectedNodes.Contains(nodeAt)))
                        this.SelectNode(nodeAt);
                }
                base.OnMouseDown(e);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                TreeNode nodeAt = this.GetNodeAt(e.Location);
                if (nodeAt != null && Control.ModifierKeys == Keys.None && this.m_SelectedNodes.Contains(nodeAt))
                {
                    int x = nodeAt.Bounds.X;
                    int num = nodeAt.Bounds.Right + 10;
                    if (e.Location.X > x && e.Location.X < num)
                        this.SelectNode(nodeAt);
                }
                base.OnMouseUp(e);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            try
            {
                if (e.Item is TreeNode node && !this.m_SelectedNodes.Contains(node))
                {
                    this.SelectSingleNode(node);
                    this.ToggleNode(node, true);
                }
                base.OnItemDrag(e);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            try
            {
                base.SelectedNode = (TreeNode)null;
                e.Cancel = true;
                base.OnBeforeSelect(e);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            try
            {
                base.OnAfterSelect(e);
                base.SelectedNode = (TreeNode)null;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.ShiftKey)
                return;
            bool flag = Control.ModifierKeys == Keys.Shift;
            try
            {
                if (this.m_SelectedNode == null && this.TopNode != null)
                    this.ToggleNode(this.TopNode, true);
                if (this.m_SelectedNode == null)
                    return;
                if (e.KeyCode == Keys.Left)
                {
                    if (this.m_SelectedNode.IsExpanded && this.m_SelectedNode.Nodes.Count > 0)
                    {
                        this.m_SelectedNode.Collapse();
                    }
                    else
                    {
                        if (this.m_SelectedNode.Parent == null)
                            return;
                        this.SelectSingleNode(this.m_SelectedNode.Parent);
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (!this.m_SelectedNode.IsExpanded)
                        this.m_SelectedNode.Expand();
                    else
                        this.SelectSingleNode(this.m_SelectedNode.FirstNode);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (this.m_SelectedNode.PrevVisibleNode == null)
                        return;
                    this.SelectNode(this.m_SelectedNode.PrevVisibleNode);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (this.m_SelectedNode.NextVisibleNode == null)
                        return;
                    this.SelectNode(this.m_SelectedNode.NextVisibleNode);
                }
                else if (e.KeyCode == Keys.Home)
                {
                    if (flag)
                    {
                        if (this.m_SelectedNode.Parent == null)
                        {
                            if (this.Nodes.Count <= 0)
                                return;
                            this.SelectNode(this.Nodes[0]);
                        }
                        else
                            this.SelectNode(this.m_SelectedNode.Parent.FirstNode);
                    }
                    else
                    {
                        if (this.Nodes.Count <= 0)
                            return;
                        this.SelectSingleNode(this.Nodes[0]);
                    }
                }
                else if (e.KeyCode == Keys.End)
                {
                    if (flag)
                    {
                        if (this.m_SelectedNode.Parent == null)
                        {
                            if (this.Nodes.Count <= 0)
                                return;
                            this.SelectNode(this.Nodes[this.Nodes.Count - 1]);
                        }
                        else
                            this.SelectNode(this.m_SelectedNode.Parent.LastNode);
                    }
                    else
                    {
                        if (this.Nodes.Count <= 0)
                            return;
                        TreeNode lastNode = this.Nodes[0].LastNode;
                        while (lastNode.IsExpanded && lastNode.LastNode != null)
                            lastNode = lastNode.LastNode;
                        this.SelectSingleNode(lastNode);
                    }
                }
                else if (e.KeyCode == Keys.Prior)
                {
                    int visibleCount = this.VisibleCount;
                    TreeNode node;
                    for (node = this.m_SelectedNode; visibleCount > 0 && node.PrevVisibleNode != null; --visibleCount)
                        node = node.PrevVisibleNode;
                    this.SelectSingleNode(node);
                }
                else if (e.KeyCode == Keys.Next)
                {
                    int visibleCount = this.VisibleCount;
                    TreeNode node;
                    for (node = this.m_SelectedNode; visibleCount > 0 && node.NextVisibleNode != null; --visibleCount)
                        node = node.NextVisibleNode;
                    this.SelectSingleNode(node);
                }
                else
                {
                    string str = ((char)e.KeyValue).ToString();
                    TreeNode node = this.m_SelectedNode;
                    while (node.NextVisibleNode != null)
                    {
                        node = node.NextVisibleNode;
                        if (node.Text.StartsWith(str))
                        {
                            this.SelectSingleNode(node);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private void SelectNode(TreeNode node)
        {
            try
            {
                this.BeginUpdate();
                if (this.m_SelectedNode != null)
                {
                    switch (Control.ModifierKeys)
                    {
                        case Keys.Shift:
                            TreeNode node1 = this.m_SelectedNode;
                            TreeNode treeNode1 = node;
                            if (node1.Parent == treeNode1.Parent)
                            {
                                if (node1.Index < treeNode1.Index)
                                {
                                    while (node1 != treeNode1)
                                    {
                                        node1 = node1.NextVisibleNode;
                                        if (node1 != null)
                                            this.ToggleNode(node1, true);
                                        else
                                            break;
                                    }
                                    goto label_35;
                                }
                                else if (node1.Index != treeNode1.Index)
                                {
                                    while (node1 != treeNode1)
                                    {
                                        node1 = node1.PrevVisibleNode;
                                        if (node1 != null)
                                            this.ToggleNode(node1, true);
                                        else
                                            break;
                                    }
                                    goto label_35;
                                }
                                else
                                    goto label_35;
                            }
                            else
                            {
                                TreeNode treeNode2 = node1;
                                TreeNode treeNode3 = treeNode1;
                                int num = Math.Min(treeNode2.Level, treeNode3.Level);
                                while (treeNode2.Level > num)
                                    treeNode2 = treeNode2.Parent;
                                while (treeNode3.Level > num)
                                    treeNode3 = treeNode3.Parent;
                                for (; treeNode2.Parent != treeNode3.Parent; treeNode3 = treeNode3.Parent)
                                    treeNode2 = treeNode2.Parent;
                                if (treeNode2.Index < treeNode3.Index)
                                {
                                    while (node1 != treeNode1)
                                    {
                                        node1 = node1.NextVisibleNode;
                                        if (node1 != null)
                                            this.ToggleNode(node1, true);
                                        else
                                            break;
                                    }
                                    goto label_35;
                                }
                                else if (treeNode2.Index == treeNode3.Index)
                                {
                                    if (node1.Level < treeNode1.Level)
                                    {
                                        while (node1 != treeNode1)
                                        {
                                            node1 = node1.NextVisibleNode;
                                            if (node1 != null)
                                                this.ToggleNode(node1, true);
                                            else
                                                break;
                                        }
                                        goto label_35;
                                    }
                                    else
                                    {
                                        while (node1 != treeNode1)
                                        {
                                            node1 = node1.PrevVisibleNode;
                                            if (node1 != null)
                                                this.ToggleNode(node1, true);
                                            else
                                                break;
                                        }
                                        goto label_35;
                                    }
                                }
                                else
                                {
                                    while (node1 != treeNode1)
                                    {
                                        node1 = node1.PrevVisibleNode;
                                        if (node1 != null)
                                            this.ToggleNode(node1, true);
                                        else
                                            break;
                                    }
                                    goto label_35;
                                }
                            }
                        case Keys.Control:
                            break;
                        default:
                            this.SelectSingleNode(node);
                            goto label_35;
                    }
                }
                bool flag = this.m_SelectedNodes.Contains(node);
                this.ToggleNode(node, !flag);
            label_35:
                this.OnAfterSelect(new TreeViewEventArgs(this.m_SelectedNode));
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private void ClearSelectedNodes()
        {
            try
            {
                foreach (TreeNode selectedNode in this.m_SelectedNodes)
                {
                    selectedNode.BackColor = this.BackColor;
                    selectedNode.ForeColor = this.ForeColor;
                }
            }
            finally
            {
                this.m_SelectedNodes.Clear();
                this.m_SelectedNode = (TreeNode)null;
            }
        }

        private void SelectSingleNode(TreeNode node)
        {
            if (node == null)
                return;
            this.ClearSelectedNodes();
            this.ToggleNode(node, true);
            node.EnsureVisible();
        }

        private void ToggleNode(TreeNode node, bool bSelectNode)
        {
            if (bSelectNode)
            {
                this.m_SelectedNode = node;
                if (!this.m_SelectedNodes.Contains(node))
                    this.m_SelectedNodes.Add(node);
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
            else
            {
                this.m_SelectedNodes.Remove(node);
                node.BackColor = this.BackColor;
                node.ForeColor = this.ForeColor;
            }
        }

        public void LoadNodesTest()
        {
            TreeNode treeNode1 = new TreeNode("Node16");
            TreeNode treeNode2 = new TreeNode("Node17");
            TreeNode treeNode3 = new TreeNode("Node4", new TreeNode[2]
            {
        treeNode1,
        treeNode2
            });
            TreeNode treeNode4 = new TreeNode("Node18");
            TreeNode treeNode5 = new TreeNode("Node19");
            TreeNode treeNode6 = new TreeNode("Node5", new TreeNode[2]
            {
        treeNode4,
        treeNode5
            });
            TreeNode treeNode7 = new TreeNode("Node20");
            TreeNode treeNode8 = new TreeNode("Node21");
            TreeNode treeNode9 = new TreeNode("Node6", new TreeNode[2]
            {
        treeNode7,
        treeNode8
            });
            TreeNode treeNode10 = new TreeNode("Node0", new TreeNode[3]
            {
        treeNode3,
        treeNode6,
        treeNode9
            });
            TreeNode treeNode11 = new TreeNode("Node22");
            TreeNode treeNode12 = new TreeNode("Node23");
            TreeNode treeNode13 = new TreeNode("Node7", new TreeNode[2]
            {
        treeNode11,
        treeNode12
            });
            TreeNode treeNode14 = new TreeNode("Node24");
            TreeNode treeNode15 = new TreeNode("Node25");
            TreeNode treeNode16 = new TreeNode("Node8", new TreeNode[2]
            {
        treeNode14,
        treeNode15
            });
            TreeNode treeNode17 = new TreeNode("Node26");
            TreeNode treeNode18 = new TreeNode("Node27");
            TreeNode treeNode19 = new TreeNode("Node9", new TreeNode[2]
            {
        treeNode17,
        treeNode18
            });
            TreeNode treeNode20 = new TreeNode("Node1", new TreeNode[3]
            {
        treeNode13,
        treeNode16,
        treeNode19
            });
            TreeNode treeNode21 = new TreeNode("Node38");
            TreeNode treeNode22 = new TreeNode("Node39");
            TreeNode treeNode23 = new TreeNode("Node10", new TreeNode[2]
            {
        treeNode21,
        treeNode22
            });
            TreeNode treeNode24 = new TreeNode("Node36");
            TreeNode treeNode25 = new TreeNode("Node37");
            TreeNode treeNode26 = new TreeNode("Node11", new TreeNode[2]
            {
        treeNode24,
        treeNode25
            });
            TreeNode treeNode27 = new TreeNode("Node34");
            TreeNode treeNode28 = new TreeNode("Node35");
            TreeNode treeNode29 = new TreeNode("Node12", new TreeNode[2]
            {
        treeNode27,
        treeNode28
            });
            TreeNode treeNode30 = new TreeNode("Node2", new TreeNode[3]
            {
        treeNode23,
        treeNode26,
        treeNode29
            });
            TreeNode treeNode31 = new TreeNode("Node32");
            TreeNode treeNode32 = new TreeNode("Node33");
            TreeNode treeNode33 = new TreeNode("Node13", new TreeNode[2]
            {
        treeNode31,
        treeNode32
            });
            TreeNode treeNode34 = new TreeNode("Node30");
            TreeNode treeNode35 = new TreeNode("Node31");
            TreeNode treeNode36 = new TreeNode("Node14", new TreeNode[2]
            {
        treeNode34,
        treeNode35
            });
            TreeNode treeNode37 = new TreeNode("Node28");
            TreeNode treeNode38 = new TreeNode("Node29");
            TreeNode treeNode39 = new TreeNode("Node15", new TreeNode[2]
            {
        treeNode37,
        treeNode38
            });
            TreeNode treeNode40 = new TreeNode("Node3", new TreeNode[3]
            {
        treeNode33,
        treeNode36,
        treeNode39
            });
            treeNode1.Name = "Node16";
            treeNode1.Text = "Node16";
            treeNode2.Name = "Node17";
            treeNode2.Text = "Node17";
            treeNode3.Name = "Node4";
            treeNode3.Text = "Node4";
            treeNode4.Name = "Node18";
            treeNode4.Text = "Node18";
            treeNode5.Name = "Node19";
            treeNode5.Text = "Node19";
            treeNode6.Name = "Node5";
            treeNode6.Text = "Node5";
            treeNode7.Name = "Node20";
            treeNode7.Text = "Node20";
            treeNode8.Name = "Node21";
            treeNode8.Text = "Node21";
            treeNode9.Name = "Node6";
            treeNode9.Text = "Node6";
            treeNode10.BackColor = SystemColors.Highlight;
            treeNode10.ForeColor = SystemColors.HighlightText;
            treeNode10.Name = "Node0";
            treeNode10.Text = "Node0";
            treeNode11.Name = "Node22";
            treeNode11.Text = "Node22";
            treeNode12.Name = "Node23";
            treeNode12.Text = "Node23";
            treeNode13.Name = "Node7";
            treeNode13.Text = "Node7";
            treeNode14.Name = "Node24";
            treeNode14.Text = "Node24";
            treeNode15.Name = "Node25";
            treeNode15.Text = "Node25";
            treeNode16.Name = "Node8";
            treeNode16.Text = "Node8";
            treeNode17.Name = "Node26";
            treeNode17.Text = "Node26";
            treeNode18.Name = "Node27";
            treeNode18.Text = "Node27";
            treeNode19.Name = "Node9";
            treeNode19.Text = "Node9";
            treeNode20.Name = "Node1";
            treeNode20.Text = "Node1";
            treeNode21.Name = "Node38";
            treeNode21.Text = "Node38";
            treeNode22.Name = "Node39";
            treeNode22.Text = "Node39";
            treeNode23.Name = "Node10";
            treeNode23.Text = "Node10";
            treeNode24.Name = "Node36";
            treeNode24.Text = "Node36";
            treeNode25.Name = "Node37";
            treeNode25.Text = "Node37";
            treeNode26.Name = "Node11";
            treeNode26.Text = "Node11";
            treeNode27.Name = "Node34";
            treeNode27.Text = "Node34";
            treeNode28.Name = "Node35";
            treeNode28.Text = "Node35";
            treeNode29.Name = "Node12";
            treeNode29.Text = "Node12";
            treeNode30.Name = "Node2";
            treeNode30.Text = "Node2";
            treeNode31.Name = "Node32";
            treeNode31.Text = "Node32";
            treeNode32.Name = "Node33";
            treeNode32.Text = "Node33";
            treeNode33.Name = "Node13";
            treeNode33.Text = "Node13";
            treeNode34.Name = "Node30";
            treeNode34.Text = "Node30";
            treeNode35.Name = "Node31";
            treeNode35.Text = "Node31";
            treeNode36.Name = "Node14";
            treeNode36.Text = "Node14";
            treeNode37.Name = "Node28";
            treeNode37.Text = "Node28";
            treeNode38.Name = "Node29";
            treeNode38.Text = "Node29";
            treeNode39.Name = "Node15";
            treeNode39.Text = "Node15";
            treeNode40.Name = "Node3";
            treeNode40.Text = "Node3";
            this.Nodes.AddRange(new TreeNode[4]
            {
        treeNode10,
        treeNode20,
        treeNode30,
        treeNode40
            });
        }

        public delegate void NodeSelect(TreeNode SelectedNode);
    }
}
