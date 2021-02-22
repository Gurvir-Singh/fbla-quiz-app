using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using fbla.Models;
using ReactiveUI;
namespace fbla.ViewModels
{
    public class MultipleChoiceQuestionViewModel : ViewModelBase
    {

        public bool answered()
        {
            return questionModel.answered;
        }
        private bool _correct;
        public bool correct { get { return _correct; } set => this.RaiseAndSetIfChanged(ref _correct, value); }
        public MultipleChoiceQuestion questionModel { get; set; }

        private string _groupName = "";
        public string groupName { get { return _groupName; } set { this.RaiseAndSetIfChanged(ref _groupName, value); } }
        public MultipleChoiceQuestionViewModel() { }
        public MultipleChoiceQuestionViewModel(string[] response, int questionNum)
        {
            groupName = questionNum.ToString();
            questionModel = new MultipleChoiceQuestion(response, questionNum);
            
        }
        public MultipleChoiceQuestionViewModel(MultipleChoiceQuestion q, int questionNum)
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
                case 3:
                    Answer3Selected = true;
                    break;
                case 4:
                    Answer4Selected = true;
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
        private bool _Answer3Selected = false;
        public bool Answer3Selected
        {
            get
            {
                return _Answer3Selected;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Answer3Selected, value);
            }
        }
        private bool _Answer4Selected = false;
        public bool Answer4Selected
        {
            get
            {
                return _Answer4Selected;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Answer4Selected, value);
            }
        }

        private bool _CorrectRect1 = false;
        public bool CorrectRect1
        {
            get { return _CorrectRect1; }
            set { this.RaiseAndSetIfChanged(ref _CorrectRect1, value); }
        }
        private bool _CorrectRect2 = false;
        public bool CorrectRect2
        {
            get { return _CorrectRect2; }
            set { this.RaiseAndSetIfChanged(ref _CorrectRect2, value); }
        }
        private bool _CorrectRect3 = false;
        public bool CorrectRect3
        {
            get { return _CorrectRect3; }
            set { this.RaiseAndSetIfChanged(ref _CorrectRect3, value); }
        }
        private bool _CorrectRect4 = false;
        public bool CorrectRect4
        {
            get { return _CorrectRect4; }
            set { this.RaiseAndSetIfChanged(ref _CorrectRect4, value); }
        }
        //manages the check, x's and 
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
                    case 3:
                        isCorrectAnswer3 = true;
                        break;
                    case 4:
                        isCorrectAnswer4 = true;
                        break;
                }
            }
            if (questionModel.answerSelected != questionModel.correctAnswer)
            {
                switch (questionModel.answerSelected)
                {
                    case 1:
                        isIncorrectAnswer1 = true;
                        break;
                    case 2:
                        isIncorrectAnswer2 = true;
                        break;
                    case 3:
                        isIncorrectAnswer3 = true;
                        break;
                    case 4:
                        isIncorrectAnswer4 = true;
                        break;
                }
                switch (questionModel.correctAnswer)
                {
                    case 1:
                        CorrectRect1 = true;
                        break;
                    case 2:
                        CorrectRect2 = true;
                        break;
                    case 3:
                        CorrectRect3 = true;
                        break;
                    case 4:
                        CorrectRect4 = true;
                        break;
                }
            }
        }
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
        private bool _isIncorrectAnswer1 = false;
        public bool isIncorrectAnswer1
        {
            get
            {
                return _isIncorrectAnswer1;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isIncorrectAnswer1, true);
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
                this.RaiseAndSetIfChanged(ref _isIncorrectAnswer2, true);
            }
        }
        private bool _isCorrectAnswer3 = false;
        public bool isCorrectAnswer3
        {
            get
            {
                return _isCorrectAnswer3;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref _isCorrectAnswer3, value);
            }
        }
        private bool _isIncorrectAnswer3 = false;
        public bool isIncorrectAnswer3
        {
            get
            {
                return _isIncorrectAnswer3;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isIncorrectAnswer3, true);
            }
        }
        private bool _isCorrectAnswer4 = false;
        public bool isCorrectAnswer4
        {
            get
            {
                return _isCorrectAnswer4;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref _isCorrectAnswer4, value);
            }
        }
        private bool _isIncorrectAnswer4 = false;
        public bool isIncorrectAnswer4
        {
            get
            {
                return _isIncorrectAnswer4;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isIncorrectAnswer4, true);
            }
        }
        private bool _enabled = true;
        public bool enabled
        {
            get { return _enabled; }
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }
        //called when a radio button is pressed
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
        public void Selected3()
        {
            questionModel.answerSelected = 3;
            questionModel.answered = true;
        }
        public void Selected4()
        {
            questionModel.answerSelected = 4;
            questionModel.answered = true;
        }
        
        public double score { 
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
