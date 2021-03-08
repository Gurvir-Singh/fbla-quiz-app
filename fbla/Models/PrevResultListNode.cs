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
            string[] splitUpFileName = DocName.Split(null);
            string FinalString = "";
            for (int i = 0; i < splitUpFileName.Length; i++)
            {
                if (splitUpFileName[i] != "")
                {
                    if (i < 2)
                    {
                        FinalString += splitUpFileName[i] + "/";
                    }
                    else if (i == 2)
                    {
                        FinalString += splitUpFileName[i] + " ";
                    }
                    else if (i != splitUpFileName.Length - 1)
                    {
                        FinalString += splitUpFileName[i] + ":";
                    }
                    else
                    {
                        FinalString += splitUpFileName[i];
                    }
                }
            }
            DateTime d = DateTime.Parse(FinalString);
            DocName = d.ToString();

        }
        public string DocName { get; set; }
        public string fullPath { get; set; }
    }
}
