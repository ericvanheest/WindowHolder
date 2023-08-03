using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowHolder
{
    class UndoObjects
    {
        public TreeView tvPrograms;

        public UndoObjects(TreeView tv)
        {
            tvPrograms = tv;
        }
    }

    abstract class UndoAction
    {
        public UndoAction()
        {
        }

        public abstract void Restore();
        public UndoObjects undoObjects = null;
        public string strUndoName = "";
    }

    class UndoDeletePrograms : UndoAction
    {
        private TreeNode[] collection;
        private TreeNode Parent;
        private TreeNode Previous;

        public UndoDeletePrograms(TreeNode[] tnic, TreeNode tnParent, TreeNode tnPrevious)
        {
            collection = tnic;
            Parent = tnParent;
            Previous = tnPrevious;
            strUndoName = "Delete Program";
            if (tnic.Length > 1)
                strUndoName += "s";
        }

        public override void Restore()
        {
            foreach (TreeNode tn in collection)
            {
                TreeNodeCollection parentnodes = undoObjects.tvPrograms.Nodes;
                if (Parent != null)
                    parentnodes = Parent.Nodes;

                if (Previous == null)
                    parentnodes.Insert(0, tn);
                else
                    parentnodes.Insert(Previous.Index + 1, tn);
            }
        }
    }

    class UndoProgramOptions: UndoAction
    {
        private TreeNode tnChanged;
        private string strOrigOptions;

        public UndoProgramOptions(TreeNode tn)
        {
            tnChanged = tn;
            strOrigOptions = ((ProgramItemTag)tn.Tag).CommandLine;
            strUndoName = "Change Program Options";
        }

        public override void Restore()
        {
            ProgramItemTag tag = (ProgramItemTag) tnChanged.Tag;
            tag.CommandLine = strOrigOptions;
        }
    }

    class UndoSortPrograms : UndoAction
    {
        public struct TreeNodeIndex
        {
            public TreeNode Node;
            public int OriginalIndex;
        }

        private List<TreeNodeIndex> m_saveNodes;

        private void AddNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode tn in nodes)
            {
                if (tn.Nodes != null)
                    AddNodes(tn.Nodes);
                TreeNodeIndex index;
                index.Node = tn;
                index.OriginalIndex = tn.Index;
                m_saveNodes.Add(index);
            }
        }

        public UndoSortPrograms(TreeNodeCollection nodes)
        {
            m_saveNodes = new List<TreeNodeIndex>();
            AddNodes(nodes);
            strUndoName = "Sort Programs";
        }

        public override void Restore()
        {
            // TODO: Restore the order somehow
        }
    }

    class UndoRenameProgram : UndoAction
    {
        private string strPrevName;
        private TreeNode tnAffected;

        public UndoRenameProgram(TreeNode tn, string strPrev)
        {
            tnAffected = tn;
            strPrevName = strPrev;
            strUndoName = "Rename Program";
        }

        public override void Restore()
        {
            if (undoObjects.tvPrograms.Nodes.Contains(tnAffected))
                tnAffected.Text = strPrevName;
        }
    }

    class UndoEditProgramProperties : UndoAction
    {
        private ProgramItemTag tagLastProperties;
        private TreeNode tnAffected;

        public UndoEditProgramProperties(TreeNode lvi, ProgramItemTag prevTag)
        {
            tnAffected = lvi;
            tagLastProperties = prevTag;
            strUndoName = "Change Properties";
        }

        public override void Restore()
        {
            tnAffected.Tag = tagLastProperties;
            tnAffected.Text = tagLastProperties.FriendlyName;
        }
    }

    class Undo
    {
        private UndoAction m_lastAction = null;
        private UndoObjects m_undoObjects;

        public Undo(UndoObjects objects)
        {
            m_undoObjects = objects;
        }

        public void SaveAction(UndoAction action)
        {
            action.undoObjects = m_undoObjects;
            m_lastAction = action;
        }

        public bool RestoreLastAction()
        {
            if (m_lastAction == null)
                return false;
            m_lastAction.Restore();
            m_lastAction = null;
            return true;
        }

        public bool ContainsActions
        {
            get { return (m_lastAction != null); }
        }

        public string LastActionName
        {
            get
            {
                if (m_lastAction == null)
                    return "";
                return m_lastAction.strUndoName;
            }
        }

        public void InvalidateLastAction()
        {
            m_lastAction = null;
        }
    }
}
