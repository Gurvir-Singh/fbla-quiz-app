using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using fbla.Models;
namespace fbla.ViewModels
{
    public class QuizScreenViewModel : ViewModelBase
    {
        //Constructor
        public QuizScreenViewModel() {
            Serializer bruh = new Serializer();
            List<List<String>> questionsResult = bruh.getQuestions();
            int i = 1;
            
            foreach (List<String> question in questionsResult)
            {
                switch (question[0])
                {
                    case "Multiple":
                        questionList.Add(new MultipleChoiceQuestionViewModel(question.ToArray(), i));
                        break;
                    case "T/F":
                        questionList.Add(new TrueFalseQuestionViewModel(question.ToArray(), i));
                        break;
                    case "FillInBlank":
                        questionList.Add(new FillBlankQuestionViewModel(question.ToArray(), i));
                        break;
                    case "Checkbox":
                        questionList.Add(new CheckboxQuestionViewModel(question.ToArray(), i));
                        break;
                }
                i++;
            }
            question1 = questionList[0];
            question2 = questionList[1];
            question3 = questionList[2];
            question4 = questionList[3];
            question5 = questionList[4];
            
        }
        //warning for not answering a question, also used as a pop up to alert of a successful report export
        private bool _warningVisible = false;
        public bool warningVisible
        {
            get
            {
                return _warningVisible;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _warningVisible, value);
            }
        }
        //bool for if the quiz has been submitted
        private bool _submitted = false;
        public bool submitted
        {
            get {
                return _submitted;
            }
            set {
                this.RaiseAndSetIfChanged(ref _submitted, value);
                if (value == true)
                {
                    question1.enabled = false;
                    question2.enabled = false;
                    question3.enabled = false;
                    question4.enabled = false;
                    question5.enabled = false;
                    
                    
                }
            }
        }
        //string containg the popup/warning text that changes after questions have been submitted
        private string _popupText = "One or more of the questions was not answered please ensure you have answered all questions.";
        public string popupText
        {
            get
            {
                return _popupText;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _popupText, value);
            }
        }
        //fields for questions and score results of each
        public dynamic question1 { get; set; }
        public dynamic question2 { get; set; }
        public dynamic question3 { get; set; }
        public dynamic question4 { get; set; }
        public dynamic question5 { get; set; }

        private string _result1;
        public string result1
        {
            get { return _result1; }
            set => this.RaiseAndSetIfChanged(ref _result1, value);
        }
        private string _result2;
        public string result2
        {
            get { return _result2; }
            set => this.RaiseAndSetIfChanged(ref _result2, value);
        }
        private string _result3;
        public string result3
        {
            get { return _result3; }
            set => this.RaiseAndSetIfChanged(ref _result3, value);
        }
        private string _result4;
        public string result4
        {
            get { return _result4; }
            set => this.RaiseAndSetIfChanged(ref _result4, value);
        }
        private string _result5;
        public string result5
        {
            get { return _result5; }
            set => this.RaiseAndSetIfChanged(ref _result5, value);
        }
        private string _totalResult;
        public string totalResult
        {
            get { return _totalResult; }
            set => this.RaiseAndSetIfChanged(ref _totalResult, value);
        }
        //a list of dynamic objects that you can use to store different types of questions. 
        //Benefit over polymorphism is you dont need to cast for it to act like the derived class
        public List<dynamic> questionList = new List<dynamic>();
        private double _score = 0;
        public double score
        {
            get 
            {
                return _score;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _score, value);
                _score = 0;
            }
        }
        //psuedo-Event handler for when ok button on pop up is pressed
        public void okPressed()
        {
            warningVisible = false;
            if (popupText != "Saved succsessfully to the documents folder")
            {
                question1.enabled = true;
                question2.enabled = true;
                question3.enabled = true;
                question4.enabled = true;
                question5.enabled = true;
            }
        }

        private int _Width = 1000;
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Width, value);
            }
        }

        private bool _SubmitButtonShowing = true;
        public bool SubmitButtonShowing
        {
            get
            {
                return _SubmitButtonShowing;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _SubmitButtonShowing, value);
            }
        }
        private bool _ReturnHomeButtonShowing = false;
        public bool ReturnHomeButtonShowing
        {
            get
            {
                return _ReturnHomeButtonShowing;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _ReturnHomeButtonShowing, value);
            }
        }

        //method that scores and submitts quiz
        public void ScoreQuiz()
        {

            if (!question1.answered() || !question2.answered() || !question3.answered() || !question4.answered() || !question5.answered())
            {
                warningVisible = true; 
                question1.enabled = false;
                question2.enabled = false;
                question3.enabled = false;
                question4.enabled = false;
                question5.enabled = false;
            }
            else
            {

                submitted = true;
                SubmitButtonShowing = false;
                ReturnHomeButtonShowing = true;
                result1 = "Question 1: " + question1.score + "/1";
                result2 = "Question 2: " + question2.score + "/1";
                result3 = "Question 3: " + question3.score + "/1";
                result4 = "Question 4: " + question4.score + "/1";
                result5 = "Question 5: " + question5.score + "/1";
                double totalScore = question1.score + question2.score + question3.score + question4.score + question5.score;
                totalResult = "Total score: " + totalScore + "/5";
                question1.showResult();
                question2.showResult();
                question3.showResult();
                question4.showResult();
                question5.showResult();
                score = totalScore;
            }
            
        }
        //handles saving the results to a txt doc
        public void saveResults()
        {
            Serializer sz = new Serializer();
            sz.printFormatter(questionList);
            popupText = "Saved succsessfully to the documents folder";
            warningVisible = true;
        }
    }
}
