using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using fbla.ViewModels;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;

namespace fbla.Models
{
    public class Serializer
    {
        List<List<String>> questions;
        public Serializer()
        {
            string docPath = (Directory.GetParent((Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))).FullName)).FullName;
            //Gets the 50 questions from json file
            questions = JsonConvert.DeserializeObject<List<List<String>>>(File.ReadAllText(docPath + @"\source\repos\fbla-quiz-app\fbla\Assets\Questions.json"));
        }
        //gets 5 random questions from the 50 and returns it
        public List<List<String>> getQuestions()
        {
            
            List<List<String>> results = new List<List<string>>();
            do
            {
                results = new List<List<string>>();
                var rand = new Random();
                for (int i = 0; i < 5; i++)
                {
                    results.Add(questions[rand.Next(49)]);
                }
            } while (results.Count != results.Distinct().Count()); 



            return results;
        }

        public void jsonFormatter(List<dynamic> questionsList)
        {
            JsonSerializer js = new JsonSerializer();
            DateTime currentTime = DateTime.Now;
            char[] pathPrefix = currentTime.ToString("M/d/yyyy HH:mm::ss").ToCharArray();
            
            for (int i = 0; i < pathPrefix.Length; i++)
            {
                if (pathPrefix[i] == '/' || pathPrefix[i] == '\\' || pathPrefix[i] == ':')
                {
                    pathPrefix[i] = ' ';
                }
            }
            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(documentsPath + "\\fblaResults\\");
            String pathPrefixString = String.Concat(pathPrefix);
            //pathPrefixString = pathPrefixString.Substring(0, pathPrefixString.Length - 2);
            String path = documentsPath + "\\fblaResults\\";
            List<dynamic> qModels = new List<dynamic>();
            foreach(dynamic q in questionsList)
            {
                qModels.Add(q.questionModel);
            }
            using (StreamWriter sr = File.CreateText(path + pathPrefixString + @".json")) {
                sr.Write(JsonConvert.SerializeObject(qModels));
            }
        }

        public void pdfFormatter(List<dynamic> questionsList)
        {
            DateTime currentTime = DateTime.Now;
            char[] pathPrefix = currentTime.ToString("M/d/yyyy HH:mm:ss").ToCharArray();

            for (int j = 0; j < pathPrefix.Length; j++)
            {
                if (pathPrefix[j] == '/' || pathPrefix[j] == '\\' || pathPrefix[j] == ':')
                {
                    pathPrefix[j] = ' ';
                }
            }
            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Directory.CreateDirectory(documentsPath + "\\fblaResults\\");
            String pathPrefixString = (String.Concat(pathPrefix)).Trim();
            pathPrefixString = pathPrefixString.Substring(0, pathPrefixString.Length - 2);
            String path = documentsPath + "\\fblaResults\\";
            Document document = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            int i = 0;
            float questionHeight = (page.Dimensions.Height - 100) / 50;
            float answerHeight = (page.Dimensions.Height - 100) / 5 * 0.9f;
            foreach (dynamic d in questionsList)
            {
                if (d.GetType() == (new MultipleChoiceQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    page.Elements.Add(new Label("Quistion: " + d.questionModel.Question, 0, ((page.Dimensions.Height - 100) / 5 * i), page.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Answer 3: " + d.questionModel.Answer3 + "\n";
                    lineText += "Answer 4: " + d.questionModel.Answer4 + "\n";
                    lineText += "Correct Answer: " + d.questionModel.correctAnswer + "\n";
                    lineText += "Answer Selected: " + d.questionModel.answerSelected + "\n";
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    page.Elements.Add(new Label(lineText, 0, ((page.Dimensions.Height - 100) / 5 * i) + (page.Dimensions.Height / 25), page.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                else if (d.GetType() == (new TrueFalseQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    page.Elements.Add(new Label("Question: " + d.questionModel.Question, 0, ((page.Dimensions.Height - 100) / 5 * i), page.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Correct Answer: " + d.questionModel.correctAnswer + "\n";
                    lineText += "Answer Selected: " + d.questionModel.answerSelected + "\n";
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    page.Elements.Add(new Label(lineText, 0, ((page.Dimensions.Height - 100) / 5 * i) + (page.Dimensions.Height / 25), page.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                else if (d.GetType() == (new FillBlankQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    page.Elements.Add(new Label("Question: " + d.questionModel.Question, 0, ((page.Dimensions.Height - 100) / 5 * i), page.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Answer 3: " + d.questionModel.Answer3 + "\n";
                    lineText += "Answer 4: " + d.questionModel.Answer4 + "\n";
                    lineText += "Correct Answer: " + d.questionModel.correctAnswer + "\n";
                    lineText += "Answer Selected: " + d.questionModel.answerSelected + "\n";
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    page.Elements.Add(new Label(lineText, 0, ((page.Dimensions.Height - 100) / 5 * i) + (page.Dimensions.Height / 25), page.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                else if (d.GetType() == (new CheckboxQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    page.Elements.Add(new Label("Question: " + d.questionModel.Question, 0, ((page.Dimensions.Height - 100) / 5 * i), page.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Answer 3: " + d.questionModel.Answer3 + "\n";
                    lineText += "Answer 4: " + d.questionModel.Answer4 + "\n";
                    lineText += "Correct Answers: " + d.questionModel.correctAnswer1 + ", " + d.questionModel.correctAnswer2 + "\n";
                    if (d.choicesSelected.Count == 2) {
                        lineText += "Answer(s) Selected: " + d.questionModel.choicesSelected[0] + ", " + d.questionModel.choicesSelected[1] + "\n";
                    }
                    else if (d.choicesSelected.Count == 1)
                    {
                        lineText += "Answer(s) Selected: " + d.questionModel.choicesSelected[0] + "\n";
                    }
                    else
                    {
                        lineText += "No answer was selected\n";
                    }
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    page.Elements.Add(new Label(lineText, 0, ((page.Dimensions.Height - 100) / 5 * i) + (page.Dimensions.Height / 25), page.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                i++;
            }
            document.Draw(path + pathPrefixString + ".pdf");
        }

        //puts results data into text files in the documents folder, no longer used
        /*
        public void printFormatter(List<dynamic> questionsList)
        {
            DateTime currentTime = DateTime.Now;
            char[] pathPrefix = currentTime.ToString().ToCharArray();
            for (int i = 0; i <pathPrefix.Length; i++)
            {
                if (pathPrefix[i] == '/' || pathPrefix[i] == '\\' || pathPrefix[i] == ':' ) {
                    pathPrefix[i] = ' ';
                }
            }
            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Directory.CreateDirectory(documentsPath + "\\fblaResults\\");
            String pathPrefixString = String.Concat(pathPrefix.Where(c => !Char.IsWhiteSpace(c)));
            pathPrefixString = pathPrefixString.Substring(0, pathPrefixString.Length - 2);
            String path = documentsPath + "\\fblaResults\\";
            using (StreamWriter sr = File.CreateText(path + pathPrefixString + @".txt"))
            {
                foreach (dynamic d in questionsList) {
                    if (d.GetType() == (new MultipleChoiceQuestionViewModel()).GetType())
                    {

                        sr.WriteLine("Question: " + d.questionModel.Question);
                        sr.WriteLine("Answer 1: " + d.questionModel.Answer1);
                        sr.WriteLine("Answer 2: " + d.questionModel.Answer2);
                        sr.WriteLine("Answer 3: " + d.questionModel.Answer3);
                        sr.WriteLine("Answer 4: " + d.questionModel.Answer4);
                        switch (d.questionModel.correctAnswer)
                        {
                            case 1:
                                sr.WriteLine("Correct Answer: " + d.questionModel.Answer1);
                                break;
                            case 2:
                                sr.WriteLine("Correct Answer: " + d.questionModel.Answer2);
                                break;
                            case 3:
                                sr.WriteLine("Correct Answer: " + d.questionModel.Answer3);
                                break;
                            case 4:
                                sr.WriteLine("Correct Answer: " + d.questionModel.Answer4);
                                break;
                        }
                        switch (d.questionModel.answerSelected)
                        {
                            case 1:
                                sr.WriteLine("Answer selected: " + d.questionModel.Answer1);
                                break;
                            case 2:
                                sr.WriteLine("Answer selected: " + d.questionModel.Answer2);
                                break;
                            case 3:
                                sr.WriteLine("Answer selected: " + d.questionModel.Answer3);
                                break;
                            case 4:
                                sr.WriteLine("Answer selected: " + d.questionModel.Answer4);
                                break;
                        }
                        sr.WriteLine("Point(s): " + d.score + "/1");
                        sr.WriteLine();
                    }
                    else if (d.GetType() == (new TrueFalseQuestionViewModel()).GetType())
                    {
                        sr.WriteLine("Question: " + d.questionModel.Question);
                        sr.WriteLine("Answer 1: " + d.questionModel.Answer1);
                        sr.WriteLine("Answer 2: " + d.questionModel.Answer2);
                        switch (d.questionModel.correctAnswer)
                        {
                            case 1:
                                sr.WriteLine("Correct Answer: " + d.questionModel.Answer1);
                                break;
                            case 2:
                                sr.WriteLine("Correct Answer: " + d.questionModel.Answer2);
                                break;
                        }
                        switch (d.questionModel.answerSelected)
                        {
                            case 1:
                                sr.WriteLine("Answer selected: " + d.questionModel.Answer1);
                                break;
                            case 2:
                                sr.WriteLine("Answer selected: " + d.questionModel.Answer2);
                                break;
                        }
                        sr.WriteLine("Point(s): " + d.score + "/1");
                        sr.WriteLine();
                    }
                    else if (d.GetType() == (new FillBlankQuestionViewModel()).GetType())
                    {


                        sr.WriteLine("Question: " + d.questionModel.Question);
                        sr.WriteLine("Answer 1: " + d.questionModel.Answer1);
                        sr.WriteLine("Answer 2: " + d.questionModel.Answer2);
                        sr.WriteLine("Answer 3: " + d.questionModel.Answer3);
                        sr.WriteLine("Answer 4: " + d.questionModel.Answer4);
                        switch (d.questionModel.correctAnswer)
                                {
                                    case 1:
                                        sr.WriteLine("Correct Answer: " + d.questionModel.Answer1);
                                        break;
                                    case 2:
                                        sr.WriteLine("Correct Answer: " + d.questionModel.Answer2);
                                        break;
                                    case 3:
                                        sr.WriteLine("Correct Answer: " + d.questionModel.Answer3);
                                        break;
                                    case 4:
                                        sr.WriteLine("Correct Answer: " + d.questionModel.Answer4);
                                        break;
                                }
                                switch (d.questionModel.answerSelected)
                                {
                                    case 1:
                                        sr.WriteLine("Answer selected: " + d.questionModel.Answer1);
                                        break;
                                    case 2:
                                        sr.WriteLine("Answer selected: " + d.questionModel.Answer2);
                                        break;
                                    case 3:
                                        sr.WriteLine("Answer selected: " + d.questionModel.Answer3);
                                        break;
                                    case 4:
                                        sr.WriteLine("Answer selected: " + d.questionModel.Answer4);
                                        break;
                                }
                                sr.WriteLine("Point(s): " + d.score + "/1");
                                sr.WriteLine();
                     }
                    else if (d.GetType() == (new CheckboxQuestionViewModel()).GetType())
                    {
                        sr.WriteLine("Question: " + d.questionModel.Question);
                        sr.WriteLine("Answer 1: " + d.questionModel.Answer1);
                        sr.WriteLine("Answer 2: " + d.questionModel.Answer2);
                        sr.WriteLine("Answer 3: " + d.questionModel.Answer3);
                        sr.WriteLine("Answer 4: " + d.questionModel.Answer4);
                        switch (d.questionModel.correctAnswer1)
                                {
                                    case 1:
                                        sr.WriteLine("Correct Answer 1: " + d.questionModel.Answer1);
                                        break;
                                    case 2:
                                        sr.WriteLine("Correct Answer 1: " + d.questionModel.Answer2);
                                        break;
                                    case 3:
                                        sr.WriteLine("Correct Answer 1: " + d.questionModel.Answer3);
                                        break;
                                    case 4:
                                        sr.WriteLine("Correct Answer 1: " + d.questionModel.Answer4);
                                        break;
                                }
                                switch (d.questionModel.correctAnswer2)
                                {
                                    case 1:
                                        sr.WriteLine("Correct Answer 2: " + d.questionModel.Answer1);
                                        break;
                                    case 2:
                                        sr.WriteLine("Correct Answer 2: " + d.questionModel.Answer2);
                                        break;
                                    case 3:
                                        sr.WriteLine("Correct Answer 2: " + d.questionModel.Answer3);
                                        break;
                                    case 4:
                                        sr.WriteLine("Correct Answer 2: " + d.questionModel.Answer4);
                                        break;
                                }
                                switch (d.choicesSelected[0])
                                {
                                    case 1:
                                        sr.WriteLine("Answer selected 1: " + d.questionModel.Answer1);
                                        break;
                                    case 2:
                                        sr.WriteLine("Answer selected 1: " + d.questionModel.Answer2);
                                        break;
                                    case 3:
                                        sr.WriteLine("Answer selected 1: " + d.questionModel.Answer3);
                                        break;
                                    case 4:
                                        sr.WriteLine("Answer selected 1: " + d.questionModel.Answer4);
                                        break;
                                }
                                switch (d.choicesSelected[1])
                                {
                                    case 1:
                                        sr.WriteLine("Answer selected 2: " + d.questionModel.Answer1);
                                        break;
                                    case 2:
                                        sr.WriteLine("Answer selected 2: " + d.questionModel.Answer2);
                                        break;
                                    case 3:
                                        sr.WriteLine("Answer selected 2: " + d.questionModel.Answer3);
                                        break;
                                    case 4:
                                        sr.WriteLine("Answer selected 2: " + d.questionModel.Answer4);
                                        break;
                                }
                                sr.WriteLine("Point(s): " + d.score + "/1");
                                sr.WriteLine();
                            }
                }
            }
        }
        */
        
    }
}
