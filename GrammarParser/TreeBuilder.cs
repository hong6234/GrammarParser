using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarParser
{
    public class TreeBuilder
    {
        public event EventHandler<TreeNode> NodeAdded;

        protected Parser parser;

        private ParserRuleContext tree;

        private ParseTreeModel model;

        private SynchronizationContext SyncContext;

        public TreeBuilder(Parser parser, ParserRuleContext tree, ParseTreeModel model)
        {
            this.parser = parser;
            this.tree = tree;
            this.model = model;

            SyncContext = SynchronizationContext.Current;
        }

        public ParserRuleContext Tree { get { return tree; } }

        public ParseTreeModel Model { get { return model; } }

        private TreeNode AddNode(TreeNode parent, IParseTree t)
        {
            var node = new TreeNode();
            if (t is ParserRuleContext)
            {
                var ruleIndex = (t as ParserRuleContext).RuleIndex;
                node.RuleIndex = ruleIndex;
                node.RuleName = parser.RuleNames[ruleIndex];
                node.IsTerminalNode = false;
            } 
            else if(t is ITerminalNode) 
            {
                node.Text = (t as ITerminalNode).Symbol.Text;
                node.IsTerminalNode = true;
            }
            parent.Add(node);

            if (NodeAdded != null)
            {
                NodeAdded(this, node);
            }
            return node;
        }

        public void Build()
        {
            ParseNode(Tree, Model.Root);
        }

        private void ParseNode(IParseTree tree, TreeNode parent)
        {
            if (tree != null)
            {
                TreeNode node = null;
                SyncContext.Send((o) => 
                {
                    node = AddNode(parent, tree);
                }, null);
                
                for (int i = 0; i < tree.ChildCount; i++)
                {
                    IParseTree t = tree.GetChild(i);
                    ParseNode(t, node);
                }
            }
        }
    }
}
