using Connectify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Services
{
    public static class MessageService
    {
        public static string ValidateMessage(string content)
        {
            if (content == null || content.Length == 0)
                return "";

            return CheckForSlangInMessageContent(content).Trim();
        }


        // simple AI model ---> LMAO XD
        private static string CheckForSlangInMessageContent(string content)
        {
            List<string> slangs = new List<string>()
            {
                "Screw", "Crap", "Stupid", "Moron", "Pig", "Fat"
            };

            foreach (string slang in slangs)
            {
                if (content.Contains(slang, StringComparison.OrdinalIgnoreCase))
                {
                    content.Replace(slang, new String('*', slang.Length));
                }
            }
            return content;
        }
    }
}
