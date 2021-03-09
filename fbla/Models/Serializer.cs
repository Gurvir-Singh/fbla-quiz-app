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
            if (!Directory.Exists(documentsPath + "\\FBLA"))
            {
                Directory.CreateDirectory(documentsPath + "\\FBLA\\");
            }
            String pathPrefixString = String.Concat(pathPrefix);
            //pathPrefixString = pathPrefixString.Substring(0, pathPrefixString.Length - 2);
            String path = documentsPath + "\\FBLA\\";
            List<dynamic> qModels = new List<dynamic>();
            foreach(dynamic q in questionsList)
            {
                qModels.Add(q.questionModel);
            }
            using (StreamWriter sr = File.CreateText(path + pathPrefixString + @".json")) {
                sr.Write(JsonConvert.SerializeObject(qModels));
            }
        }
        


        //old way to make pdfs, used ceTe DynamicPDF
        
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
            if (!Directory.Exists(documentsPath + "\\FBLA Quiz Results\\"))
            {
                Directory.CreateDirectory(documentsPath + "\\FBLA Quiz Results\\");
            }
            String pathPrefixString = (String.Concat(pathPrefix)).Trim();
            pathPrefixString = pathPrefixString.Substring(0, pathPrefixString.Length - 2);
            String path = documentsPath + "\\FBLA Quiz Results\\";
            Document document = new Document();
            //Page ScorePage = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            Page QustionsPage = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            //document.Pages.Add(ScorePage);
            document.Pages.Add(QustionsPage);


            


            int i = 0;
            float questionHeight = (QustionsPage.Dimensions.Height - 100) / 50;
            float answerHeight = (QustionsPage.Dimensions.Height - 100) / 5 * 0.9f;
            foreach (dynamic d in questionsList)
            {
                if (d.GetType() == (new MultipleChoiceQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    QustionsPage.Elements.Add(new Label("Quistion: " + d.questionModel.Question, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i), QustionsPage.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Answer 3: " + d.questionModel.Answer3 + "\n";
                    lineText += "Answer 4: " + d.questionModel.Answer4 + "\n";
                    lineText += "Correct Answer: " + d.questionModel.correctAnswer + "\n";
                    lineText += "Answer Selected: " + d.questionModel.answerSelected + "\n";
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    QustionsPage.Elements.Add(new Label(lineText, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i) + (QustionsPage.Dimensions.Height / 25), QustionsPage.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                else if (d.GetType() == (new TrueFalseQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    QustionsPage.Elements.Add(new Label("Question: " + d.questionModel.Question, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i), QustionsPage.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Correct Answer: " + d.questionModel.correctAnswer + "\n";
                    lineText += "Answer Selected: " + d.questionModel.answerSelected + "\n";
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    QustionsPage.Elements.Add(new Label(lineText, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i) + (QustionsPage.Dimensions.Height / 25), QustionsPage.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                else if (d.GetType() == (new FillBlankQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    QustionsPage.Elements.Add(new Label("Question: " + d.questionModel.Question, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i), QustionsPage.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
                    lineText += "Answer 1: " + d.questionModel.Answer1 + "\n";
                    lineText += "Answer 2: " + d.questionModel.Answer2 + "\n";
                    lineText += "Answer 3: " + d.questionModel.Answer3 + "\n";
                    lineText += "Answer 4: " + d.questionModel.Answer4 + "\n";
                    lineText += "Correct Answer: " + d.questionModel.correctAnswer + "\n";
                    lineText += "Answer Selected: " + d.questionModel.answerSelected + "\n";
                    lineText += "Point(s): " + d.score + "/1" + "\n";
                    lineText += "\n";
                    QustionsPage.Elements.Add(new Label(lineText, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i) + (QustionsPage.Dimensions.Height / 25), QustionsPage.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                else if (d.GetType() == (new CheckboxQuestionViewModel()).GetType())
                {
                    string lineText = "";
                    QustionsPage.Elements.Add(new Label("Question: " + d.questionModel.Question, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i), QustionsPage.Dimensions.Width - 100, questionHeight, Font.Helvetica, 14, TextAlign.Left));
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
                    QustionsPage.Elements.Add(new Label(lineText, 0, ((QustionsPage.Dimensions.Height - 100) / 5 * i) + (QustionsPage.Dimensions.Height / 25), QustionsPage.Dimensions.Width - 100, answerHeight, Font.Helvetica, 12, TextAlign.Left));
                }
                i++;
            }
            document.Draw(path + pathPrefixString + ".pdf");
        }
    }
}
