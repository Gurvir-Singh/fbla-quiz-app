using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using fbla.Models;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;


namespace fbla.ViewModels
{
    public class QuizScreenViewModel : ViewModelBase
    {
        //Constructor
        public QuizScreenViewModel(Serializer serZ) {
            viewingOldResult = false;
            Serializer sz = serZ;
            List<List<String>> questionsResult = sz.getQuestions();
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
        public QuizScreenViewModel(string path)
        {
            viewingOldResult = true;
            List<dynamic> dyns = new List<dynamic>();
            List<JObject> questionsD = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(path));
            string s = questionsD[0]["type"].ToObject<string>();
            int i = 1;
            foreach (JObject o in questionsD)
            {
                if (o["type"].ToObject<string>() == "MultipleChoice")
                {
                    dyns.Add(new MultipleChoiceQuestionViewModel(o.ToObject<MultipleChoiceQuestion>(), i));
                }
                else if (o["type"].ToObject<string>() == "FillBlank")
                {
                    dyns.Add(new FillBlankQuestionViewModel(o.ToObject<FillBlankQuestion>()));
                }
                else if (o["type"].ToObject<string>() == "Checkbox")
                {
                    dyns.Add(new CheckboxQuestionViewModel(o.ToObject<CheckboxQuestion>(), i));
                }
                else if (o["type"].ToObject<string>() == "TrueFalse")
                {
                    dyns.Add(new TrueFalseQuestionViewModel(o.ToObject<TrueFalseQuestion>(), i));
                }
                i++;
            }
            question1 = dyns[0];
            question2 = dyns[1];
            question3 = dyns[2];
            question4 = dyns[3];
            question5 = dyns[4];
            this.ShowPrevResults();
        }
        //warning for not answering a question, also used as a pop up to alert of a successful report export
        private bool _viewingOldResult;
        public bool viewingOldResult
        {
            get
            {
                return _viewingOldResult;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _viewingOldResult, value);
            }
        }
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
        private bool _forceSubmitting = false;
        public bool forceSubmitting
        {
            get
            {
                return _forceSubmitting;
            }
            set {
                _forceSubmitting = value;
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
        private string _popupText = "One or more of the questions was not answered. Are you sure you want to submit?";
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
        private bool _returningHome = false;
        public bool returningHome
        {
            get
            {
                return _returningHome;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _returningHome, value);
            }
        }
        private bool _submitEarly = false;
        public bool submitEarly
        {
            get
            {
                return _submitEarly;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _submitEarly, value);
            }
        }
        public void yesPressed()
        {
            noPressed();
            ScoreQuiz(true);
        }

        //psuedo-Event handler for when ok button on pop up is pressed
        public void noPressed()
        {
            warningVisible = false;
            submitEarly = false;
            returningHome = false;
            forceSubmitting = false;
            noVisible = false;
            okVisible = false;
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

        public void EarlyLeaveWarning()
        {
            forceSubmitting = true;
            returningHome = true;
            noVisible = true;
            popupText = "Are you sure you want to exit?";
            warningVisible = true;
            question1.enabled = false;
            question2.enabled = false;
            question3.enabled = false;
            question4.enabled = false;
            question5.enabled = false;
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
        private bool _okVisible = false;
        public bool okVisible
        {
            get
            {
                return _okVisible;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _okVisible, value);
            }
        }
        private bool _noVisible = false;
        public bool noVisible
        {
            get
            {
                return _noVisible;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _noVisible, value);
            }
        }

        //method that scores and submitts quiz

        public void ScoreQuiz(bool submittingEarly = false)
        {

            if ((!question1.answered() || !question2.answered() || !question3.answered() || !question4.answered() || !question5.answered()) && !submittingEarly)
            {
                popupText = "One or more of the questions was not answered. Are you sure you want to submit?";
                submitEarly = true;
                noVisible = true;
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
        public void ShowPrevResults()
        {
            submitted = true;
            SubmitButtonShowing = false;
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

        private bool _saveResultsEnabled = true;
        public bool saveResultsEnabled
        {
            get
            {
                return _saveResultsEnabled;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _saveResultsEnabled, value);
            }
        }

        //handles saving the results to a json file
        public void saveResults()
        {
            saveResultsEnabled = false;
            Serializer sz = new Serializer();
            sz.jsonFormatter(questionList);
            okVisible = true;
            popupText = "Saved succsessfully. You can review this quiz and others on the \"Past Results\" page";
            warningVisible = true;
        }
    }
}
