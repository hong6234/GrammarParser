using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarParser
{
    public class TreeNode : ModelBase
    {
        public bool IsTerminalNode { get; set; }

        public int RuleIndex { get; set; }

        private string _RuleName = "";
        public string RuleName 
        {
            get { return _RuleName; } 
            set 
            { 
                _RuleName = value;
                Notify("RuleName");
                _Text = value;
                Notify("Text");
            } 
        }

        private string _Text = "";
        public string Text 
        { 
            get { return _Text; } 
            set 
            { 
                _Text = value;
                Notify("Text");
            } 
        }

        private ObservableCollection<TreeNode> _TreeNodes = new ObservableCollection<TreeNode>();
        public ObservableCollection<TreeNode> TreeNodes { get { return _TreeNodes; } }

        public void Add(TreeNode node)
        {
            _TreeNodes.Add(node);
            Notify("TreeNodes");
        }

        public void Remove(TreeNode node)
        {
            _TreeNodes.Remove(node);
            Notify("TreeNodes");
        }

        public void Clear()
        {
            _TreeNodes.Clear();
            Notify("TreeNodes");
        }
    }
}
