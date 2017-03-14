using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarParser
{
    class JavaMethodListener : JavaBaseListener
    {
        public event EventHandler<string> Process;

        public override void EnterMethodDeclaration(JavaParser.MethodDeclarationContext context) 
        {
            var tokens = context.GetTokens(JavaParser.Identifier);
            var parameters = context.GetRuleContexts<JavaParser.FormalParametersContext>();
            foreach (var token in tokens)
            {
                if (Process != null)
                {
                    //打印方法名
                    Process(this, "Method : " + token.Symbol.Text + "(");
                }
            }
            foreach (var param in parameters)
            {
                if (Process != null)
                {
                    var paramListCtxs = param.GetRuleContexts<JavaParser.FormalParameterListContext>();
                    foreach (var paramListCtx in paramListCtxs) 
                    {
                        var fpcs = paramListCtx.GetRuleContexts<JavaParser.FormalParameterContext>();
                        foreach (var fpc in fpcs)
                        {
                            var varIdCtxs = fpc.GetRuleContexts<JavaParser.VariableDeclaratorIdContext>();
                            foreach (var varIdCtx in varIdCtxs)
                            {
                                var varTokens = varIdCtx.GetTokens(JavaParser.Identifier);
                                foreach (var token in varTokens)
                                {
                                    //打印参数名
                                    Process(this, "    " + token.Symbol.Text + ",");
                                }
                            }
                        }
                    }
                }
            }
            foreach (var token in tokens)
            {
                if (Process != null)
                {
                    Process(this, ")");
                }
            }
        }

        public override void EnterInterfaceMethodDeclaration(JavaParser.InterfaceMethodDeclarationContext context)
        {
            var tokens = context.GetTokens(JavaParser.Identifier);
            var parameters = context.GetRuleContexts<JavaParser.FormalParametersContext>();
            foreach (var token in tokens)
            {
                if (Process != null)
                {
                    //打印方法名
                    Process(this, "Method : " + token.Symbol.Text + "(");
                }
            }
            foreach (var param in parameters)
            {
                if (Process != null)
                {
                    var paramListCtxs = param.GetRuleContexts<JavaParser.FormalParameterListContext>();
                    foreach (var paramListCtx in paramListCtxs)
                    {
                        var fpcs = paramListCtx.GetRuleContexts<JavaParser.FormalParameterContext>();
                        foreach (var fpc in fpcs)
                        {
                            var varIdCtxs = fpc.GetRuleContexts<JavaParser.VariableDeclaratorIdContext>();
                            foreach (var varIdCtx in varIdCtxs)
                            {
                                var varTokens = varIdCtx.GetTokens(JavaParser.Identifier);
                                foreach (var token in varTokens)
                                {
                                    //打印参数名
                                    Process(this, "    " + token.Symbol.Text + ",");
                                }
                            }
                        }
                    }
                }
            }
            foreach (var token in tokens)
            {
                if (Process != null)
                {
                    Process(this, ")");
                }
            }
        }
    }
}
