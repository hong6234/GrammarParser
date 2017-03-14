using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Pattern;
using Antlr4.Runtime.Tree.Xpath;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrammarParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ParseTreeModel treeModel = new ParseTreeModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = treeModel;
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO ...
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            string filePath = FilePath.Text;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                if (!File.Exists(filePath))
                {
                    System.Windows.MessageBox.Show("File not exist.");
                    return;
                }

                treeModel.Clear();
                string input = File.ReadAllText(filePath);
                var extension = new FileInfo(filePath).Extension.ToLower();
                if (extension == ".java")
                {
                    ParseJava(input);
                }
                else if (extension == ".cs")
                {
                    ParseCSharp(input);
                }
                else if (extension == ".cpp" ||
                    extension == ".h")
                {
                    ParseCpp(input);
                }
                else if (extension == ".json")
                {
                    ParseJson(input);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a file");
            }
        }

        private void ParseJava(string input)
        {
            AntlrInputStream stream = new AntlrInputStream(input);
            ITokenSource lexer = new JavaLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            JavaParser parser = new JavaParser(tokens);
            parser.BuildParseTree = true;
            JavaParser.CompilationUnitContext tree = parser.compilationUnit();
            if (tree != null)
            {
                var builder = new TreeBuilder(parser, tree, treeModel);
                builder.Build();
            }
        }

        private void ParseCSharp(string input)
        {
            Lexer preprocessorLexer = new CSharpLexer(new AntlrInputStream(input));
            // Collect all tokens with lexer (CSharpLexer.g4).
            var tokens = preprocessorLexer.GetAllTokens();
            //TODO: handle preprocessor tokens
            var codeTokenSource = new ListTokenSource(tokens);
            var codeTokenStream = new CommonTokenStream(codeTokenSource);
            CSharpParser parser = new CSharpParser(codeTokenStream);
            // Parse syntax tree (CSharpParser.g4)
            var tree = parser.compilation_unit();
            if (tree != null)
            {
                var builder = new TreeBuilder(parser, tree, treeModel);
                builder.Build();
            }
        }

        private void ParseCpp(string input)
        {
            AntlrInputStream stream = new AntlrInputStream(input);
            ITokenSource lexer = new CPP14Lexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            CPP14Parser parser = new CPP14Parser(tokens);
            parser.BuildParseTree = true;
            CPP14Parser.TranslationunitContext tree = parser.translationunit();
            if (tree != null)
            {
                var builder = new TreeBuilder(parser, tree, treeModel);
                builder.Build();
            }
        }

        private void ParseJson(string input)
        {
            AntlrInputStream stream = new AntlrInputStream(input);
            ITokenSource lexer = new JSONLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            JSONParser parser = new JSONParser(tokens);
            parser.BuildParseTree = true;
            JSONParser.JsonContext tree = parser.json();
            if (tree != null)
            {
                var builder = new TreeBuilder(parser, tree, treeModel);
                builder.Build();
            }
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog pathDlg = new FolderBrowserDialog();
            if (pathDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //show path in TextBox
                FilePath.Text = pathDlg.SelectedPath;
            }
        }

        void PreviewDragOverHandler(object sender, System.Windows.DragEventArgs e)
        {
            e.Handled = true;
        }

        void FileDropHandler(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                FilePath.Text = files[0];
            }
        }

        private void PreViewMouseWheelHandle(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
