using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using fbla.Models;
namespace fbla.ViewModels
{
    public class TrueFalseQuestionViewModel : ViewModelBase
    {
        public TrueFalseQuestionViewModel() { }
        public TrueFalseQuestionViewModel(string[] response, int questionNum)
        {
            groupName = questionNum.ToString();
            questionModel = new TrueFalseQuestion(response, questionNum);
        }
        public TrueFalseQuestionViewModel(TrueFalseQuestion q, int questionNum)
        {
            groupName = questionNum.ToString();
            questionModel = q;
            switch (q.answerSelected)
            {
                case 1:
                    Answer1Selected = true;
                    break;
                case 2:
                    Answer2Selected = true;
                    break;
            }
            }

        private bool _Answer1Selected = false;
        public bool Answer1Selected
        {
            get
            {
                return _Answer1Selected;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Answer1Selected, value);
            }
        }
        private bool _Answer2Selected = false;
        public bool Answer2Selected
        {
            get
            {
                return _Answer2Selected;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Answer2Selected, value);
            }
        }
        public TrueFalseQuestion questionModel { get; }

        private bool _correct = false;
        public bool correct
        {
            get { return _correct; }
            set
            {
                this.RaiseAndSetIfChanged(ref _correct, value);
            }
        }

        private string _groupName = "";
        public string groupName { get { return _groupName; } set { this.RaiseAndSetIfChanged(ref _groupName, value); } }

        private bool _enabled = true;
        public bool enabled
        {
            get { return _enabled; }
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }
        //Highlight fields
        private bool _CorrectNotSelected1 = false;
        public bool CorrectNotSelected1
        {
            get { return _CorrectNotSelected1; }
            set { this.RaiseAndSetIfChanged(ref _CorrectNotSelected1, value); }
        }
        private bool _CorrectNotSelected2 = false;
        public bool CorrectNotSelected2
        {
            get { return _CorrectNotSelected2; }
            set { this.RaiseAndSetIfChanged(ref _CorrectNotSelected2, value); }
        }
        //Check mark fields
        private bool _isCorrectAnswer1 = false;
        public bool isCorrectAnswer1
        {
            get
            {
                return _isCorrectAnswer1;
            }
            set
            {

                this.RaiseAndSetIfChanged(ref _isCorrectAnswer1, value);


            }
        }
        //red cross fields
        private bool _isIncorrectAnswer1 = false;
        public bool isIncorrectAnswer1
        {
            get
            {
                return _isIncorrectAnswer1;
            }
            set
            {

                this.RaiseAndSetIfChanged(ref _isIncorrectAnswer1, value);

            }
        }
        private bool _isCorrectAnswer2 = false;
        public bool isCorrectAnswer2
        {
            get
            {
                return _isCorrectAnswer2;
            }
            set
            {

                this.RaiseAndSetIfChanged(ref _isCorrectAnswer2, value);


            }
        }
        private bool _isIncorrectAnswer2 = false;
        public bool isIncorrectAnswer2
        {
            get
            {
                return _isIncorrectAnswer2;
            }
            set
            {

                this.RaiseAndSetIfChanged(ref _isIncorrectAnswer2, value);

            }
        }
        //returns a bool whether the question has been answered by user
        public bool answered()
        {
            return questionModel.answered;
        }
        
        //makes the check marks, highlight or x mark visible where applicable
        public void showResult()
        {
            if (questionModel.answerSelected == questionModel.correctAnswer)
            {
                switch (questionModel.answerSelected)
                {
                    case 1:
                        isCorrectAnswer1 = true;
                        break;
                    case 2:
                        isCorrectAnswer2 = true;
                        break;
                }
            }
            else
            {
                switch (questionModel.answerSelected)
                {
                    case 1:
                        isIncorrectAnswer1 = true;
                        break;
                    case 2:
                        isIncorrectAnswer2 = true;
                        break;
                }
                switch (questionModel.correctAnswer)
                {
                    case 1:
                        CorrectNotSelected1 = true;
                        break;
                    case 2:
                        CorrectNotSelected2 = true;
                        break;
                }
            }
        }
        //Called when a radio button is pressed
        public void Selected1()
        {
            questionModel.answerSelected = 1;
            questionModel.answered = true;
        }
        public void Selected2()
        {
            questionModel.answerSelected = 2;
            questionModel.answered = true;
        }
        //returns the points earned from the question
        public double score
        {
            get
            {
                if (questionModel.answeredCorrectly())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
