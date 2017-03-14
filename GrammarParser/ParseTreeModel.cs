using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarParser
{
    public class ParseTreeModel
    {
        private TreeNode _Root = new TreeNode();

        public TreeNode Root 
        { 
            get { return _Root; }
        }

        public void Clear()
        {
            Root.Clear();
        }
    }
}
