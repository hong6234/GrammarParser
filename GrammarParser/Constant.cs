using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarParser
{
    class Constant
    {
        private const string LOG_CONFIG_PATH = @"App.config";

        public static string LogConfigFile
        {
            get { return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, LOG_CONFIG_PATH); }
        }
    }
}
