using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using fbla.ViewModels;
namespace fbla.Models
{
    class Serializer
    {
        List<List<String>> questions;
        public Serializer()
        {
            
        }
        //gets 5 random questions from the 50 and returns it
        public List<List<String>> getQuestions()
        {
            string docPath = (Directory.GetParent((Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))).FullName)).FullName;
            //Gets the 50 questions from json file
            questions = JsonConvert.DeserializeObject<List<List<String>>>(File.ReadAllText(docPath + @"\source\repos\fbla-quiz-app\fbla\Assets\Questions.json"));
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
            char[] pathPrefix = currentTime.ToString().ToCharArray();
            
            for (int i = 0; i < pathPrefix.Length; i++)
            {
                if (pathPrefix[i] == '/' || pathPrefix[i] == '\\' || pathPrefix[i] == ':')
                {
                    pathPrefix[i] = ' ';
                }
            }
            var documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Directory.CreateDirectory(documentsPath + "\\fblaResults\\");
            String pathPrefixString = String.Concat(pathPrefix);
            pathPrefixString = pathPrefixString.Substring(0, pathPrefixString.Length - 2);
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

        //puts results data into text files in the documents folder
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
        
    }
}
