using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
namespace fbla.Models
{
    public class PrevResultListNode
    {
        public PrevResultListNode(string path) 
        { 
            fullPath = path;
            string[] breakApart = fullPath.Split('\\');
            DocName = breakApart[breakApart.Length - 1];
            DocName = DocName.Remove(DocName.IndexOf('.'));
            char[] nameCharArr = DocName.ToCharArray();
            nameCharArr[DocName.Length - 3] = ':';
            nameCharArr[DocName.Length - 6] = ':';
            for (int i = 0; i < 9; i++)
            {
                if (nameCharArr[i] == ' ')
                {
                    nameCharArr[i] = '/';
                }
            }
            DocName = String.Concat(nameCharArr);
            DateTime d = DateTime.Parse(DocName);
            DocName = d.ToString();

        }
        public string DocName { get; set; }
        public string fullPath { get; set; }
    }
}
