using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace WindowHolder
{
    public partial class WindowHolderMain : Form
    {
        private TreeNode m_nodeProgramDragSource = null;

        private void tvPrograms_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private void tvPrograms_DragOver(object sender, DragEventArgs e)
        {
            Point pos = tvPrograms.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            TreeNode node = tvPrograms.GetNodeAt(pos);
            if (node != null && node != tvPrograms.SelectedNode)
                tvPrograms.SelectedNode = node;
        }

        private void tvPrograms_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pos = new Point(e.X, e.Y);
                TreeNode node = tvPrograms.GetNodeAt(pos);
                if (node != null)
                    tvPrograms.SelectedNode = node;
            }
        }

        private void tcMain_DragDrop(object sender, DragEventArgs e)
        {
            Default_DragDrop(sender, e);
        }

        private void tvPrograms_DragDrop(object sender, DragEventArgs e)
        {
            object o = e.Data.GetData(DataFormats.FileDrop, false);
            if (o is string[])
            {
                AddFiles((string[])o, tvPrograms.GetNodeAt(tvPrograms.PointToClient(new Point(e.X, e.Y))));
                return;
            }
            if (e.Data.GetFormats().Contains(DataFormats.Text))
            {
                if (e.Data.GetData(typeof(string)).ToString().StartsWith("TreeNode:"))
                {
                    if (m_nodeProgramDragSource != null)
                    {
                        Point pos = tvPrograms.PointToClient(new Point(e.X, e.Y));
                        TreeNode node = tvPrograms.GetNodeAt(pos);
                        if (node == m_nodeProgramDragSource)
                            return;
                        tvPrograms.TreeViewNodeSorter = null;
                        tvPrograms.Sorted = false;
                        m_nodeProgramDragSource.Remove();
                        if (node == null)
                        {
                            // Drop it at the end
                            tvPrograms.Nodes.Add(m_nodeProgramDragSource);
                        }
                        else if (IsFolder(node))
                        {
                            node.Nodes.Add(m_nodeProgramDragSource);
                        }
                        else
                        {
                            // Drop it before the pointed-to node
                            TreeNode nodePrev = node.PrevNode;
                            TreeNodeCollection nodesParent = tvPrograms.Nodes;
                            if (node.Parent != null)
                                nodesParent = node.Parent.Nodes;
                            if (nodePrev == null)
                                nodesParent.Insert(0, m_nodeProgramDragSource);
                            else
                                nodesParent.Insert(nodePrev.Index+1, m_nodeProgramDragSource);
                        }
                        tvPrograms.SelectedNode = m_nodeProgramDragSource;
                        m_nodeProgramDragSource = null;
                    }
                }
            }
        }

        private void tvPrograms_ItemDrag(object sender, ItemDragEventArgs e)
        {
            m_nodeProgramDragSource = (TreeNode)e.Item;
            DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void tvPrograms_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
            else if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tvPrograms_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_bIsEditingProgramTitle)
                return;

            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (tvPrograms.SelectedNode != null)
                        DeleteSelectedPrograms();
                    break;
                case Keys.A:
                    //if (e.Control && !e.Shift && !e.Alt)
                    //    foreach (ListViewItem lvi in tvPrograms.Items)
                    //        lvi.Selected = true;
                    break;
                case Keys.Enter:
                    TreeNodeAction();
                    break;
                case Keys.F2:
                    RenameSelectedItem();
                    break;
                default:
                    break;
            }
        }

        private void tvPrograms_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            m_bIsEditingProgramTitle = false;
            EnableEditMenuItems();

            if (e.Label == null)
            {
                m_undo.InvalidateLastAction();
                return;
            }
            ProgramItemTag tag = (ProgramItemTag)e.Node.Tag;
            tag.FriendlyName = e.Label;
        }

        private void tvPrograms_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            menuEditClone.Enabled = false;
            menuEditDelete.Enabled = false;
            menuEditRename.Enabled = false;
            m_bIsEditingProgramTitle = true;
        }

        private void tvPrograms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNodeAction();
        }

        private void TreeNodeAction()
        {
            if (IsFolder(tvPrograms.SelectedNode))
                return;

            switch(m_options.iDoubleClickAction)
            {
                case DoubleClickAction.RunProgram:
                    RunSelectedProgram(false);
                    break;
                case DoubleClickAction.EditProperties:
                    EditSelectedItemProperties();
                    break;
                case DoubleClickAction.EditProgramOptions:
                    if (SelectedNodeHasProgramOptions())
                        DoOptionsForProgram();
                    else
                        EditSelectedItemProperties();
                    break;
                default:
                    break;
            }
        }

        private void SaveApplicationList()
        {
            SaveApplicationList(Path.Combine(GetUserDataPath(), Strings.FNProgramList));
        }

        private void AddNodesToXML(XmlDocument doc, XmlElement root, TreeNodeCollection treeNodes)
        {
            foreach (TreeNode tnSub in treeNodes)
            {
                ProgramItemTag tag = (ProgramItemTag)tnSub.Tag;
                XmlElement node;
                if (tag.Type == ProgramItemType.Folder)
                {
                    node = doc.CreateElement("Folder");
                    node.SetAttribute("FriendlyName", tag.FriendlyName);
                    root.AppendChild(node);
                    AddNodesToXML(doc, node, tnSub.Nodes);
                }
                else
                {
                    node = doc.CreateElement("Program");
                    tag.SetAttributes(node);
                    root.AppendChild(node);
                }
            }
        }

        private void SaveApplicationList(string strFile)
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", null, null);
            xml.AppendChild(dec);// Create the root element
            XmlElement root = xml.CreateElement("Programs");
            root.SetAttribute("Version", "1.0");
            xml.AppendChild(root);

            AddNodesToXML(xml, root, tvPrograms.Nodes);

            bool bRetry = false;
            do
            {
                try
                {
                    xml.Save(strFile);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to save application list to file \"{0}\":  {1}", strFile, ex.Message), "Error writing file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                }
            } while (bRetry);
        }

        private void LoadApplicationList()
        {
            LoadApplicationList(Path.Combine(GetUserDataPath(), Strings.FNProgramList));
        }

        private void AddXMLToTree(XmlNodeList nodes, TreeNode tnRoot)
        {
            foreach (XmlElement e in nodes)
            {
                switch (e.Name)
                {
                    case "Folder":
                    case "Program":
                        ProgramItemTag tag = new ProgramItemTag(e);
                        TreeNode tn = new TreeNode(tag.FriendlyName);
                        tn.Tag = tag;
                        tn.ImageIndex = (int)tag.Type;
                        tn.SelectedImageIndex = (int)tag.Type;
                        if (tnRoot == null)
                            tvPrograms.Nodes.Add(tn);
                        else
                            tnRoot.Nodes.Add(tn);
                        AddXMLToTree(e.ChildNodes, tn);
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadApplicationList(string strFile)
        {
            XmlDocument xml = new XmlDocument();
            bool bRetry = false;
            if (!File.Exists(strFile))
                return;
            do
            {
                try
                {
                    xml.Load(strFile);
                    XmlNode nodePrograms = xml.SelectSingleNode("/Programs");
                    tvPrograms.BeginUpdate();
                    tvPrograms.Nodes.Clear();
                    AddXMLToTree(nodePrograms.ChildNodes, null);
                    tvPrograms.ExpandAll();
                    tvPrograms.EndUpdate();
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to read application list from file \"{0}\":  {1}", strFile, ex.Message), "Error reading file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                }

            } while (bRetry);
        }

        private void tvPrograms_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;

            if (m_options.bSelectActivates)
            {
                if (IsFolder(e.Node))
                    return;

                foreach (TabPage page in tcMain.TabPages)
                {
                    ProgramItemTag tag = (ProgramItemTag)page.Tag;
                    if (tag == null)
                        continue;

                    if (tag.TreeOrigin == e.Node)
                    {
                        tcMain.SelectedTab = page;
                        return;
                    }
                }
            }
        }

        private void tvPrograms_Enter(object sender, EventArgs e)
        {
            ShowProgramList();
        }

        private void ShowProgramList()
        {
            if ((splitContainer1.SplitterDistance == splitContainer1.Panel1MinSize) && (m_iSplitOriginalPos > 0))
            {
                splitContainer1.SplitterDistance = m_iSplitOriginalPos;
            }
        }

        private TreeNode FindTreeNode(string str, TreeNodeCollection root)
        {
            foreach (TreeNode node in root)
            {
                if (node.Text.ToLower() == str.ToLower())
                    return node;
            }
            return null;
        }

        private TreeNode FindTreeNode(string str)
        {
            List<string> folders = new List<string>();

            // Find a node that matches the path give, e.g. "Programs\Notepad"
            int iBS = str.IndexOf('\\');
            while (iBS != -1)
            {
                if (iBS < str.Length - 2)
                {
                    if (str[iBS + 1] != '\\')
                    {
                        folders.Add(str.Substring(0, iBS));
                        str = str.Substring(iBS + 1);
                    }
                    else
                    {
                        str = str.Substring(0, iBS + 1) + str.Substring(iBS + 2);
                        iBS = str.IndexOf('\\', iBS + 1);
                        continue;
                    }
                }
                else
                    break;
                iBS = str.IndexOf('\\');
            }
            folders.Add(str);

            TreeNodeCollection root = tvPrograms.Nodes;
            TreeNode node = null;
            foreach (string strFolder in folders)
            {
                node = FindTreeNode(strFolder, root);
                if (node == null)
                    return null;

                root = node.Nodes;
            }

            return node;
        }

        private void miRunAllFolderCurrentTab_Click(object sender, EventArgs e)
        {
            RunAllFolder(true);
        }

        private void miRunAllProgramsFolder_Click(object sender, EventArgs e)
        {
            RunAllFolder(false);
        }

    }
}
