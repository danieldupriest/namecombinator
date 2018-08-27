using Bridge;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameCombinator
{
    public class App
    {
        static Dictionary dictionary;
        static int minOverlap = 2;

        public static void Search(String pattern)
        {
            int length = pattern.Length;

            // Build word list beginning with pattern
            StringBuilder sbFirst = new StringBuilder();
            for (int i = length - 1; i >= minOverlap; --i)
            {
                String match = pattern.Substring(0, i);
                String remainder = pattern.Substring(i, length - i);
                foreach (String str in dictionary.GetWordsEndingWith(match))
                {
                    sbFirst.Append("<span class='that'>");
                    sbFirst.Append(str.Substring(0, str.Length - i));
                    sbFirst.Append("</span><span class='both'>");
                    sbFirst.Append(str.Substring(str.Length - i, i));
                    sbFirst.Append("</span><span class='this'>");
                    sbFirst.Append(remainder);
                    sbFirst.Append("</span><br/>");
                }
            }

            // Build word list ending with pattern
            StringBuilder sbLast = new StringBuilder();
            for (int i = length - 1; i >= minOverlap; --i)
            {
                String match = pattern.Substring(length - i, i);
                String remainder = pattern.Substring(0, length - i);
                foreach (String str in dictionary.GetWordsBeginningWith(match))
                {
                    sbLast.Append("<span class='this'>");
                    sbLast.Append(remainder);
                    sbLast.Append("</span><span class='both'>");
                    sbLast.Append(str.Substring(0, i));
                    sbLast.Append("</span><span class='that'>");
                    sbLast.Append(str.Substring(i, str.Length - i));
                    sbLast.Append("</span><br/>");
                }
            }

            Script.Call("update", sbFirst.ToString(), sbLast.ToString());
        }

        public static void Main()
        {
            dictionary = new Dictionary();

        }
    }
}