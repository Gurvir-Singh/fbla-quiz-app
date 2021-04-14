using System;
using System.Collections.Generic;
using System.Text;
using fbla.Models;
using ReactiveUI;
namespace fbla.ViewModels
{

    public class CheckboxQuestionViewModel : ViewModelBase
    {
        //Checks wether the user has selected at least one option
        public bool answered() { 
            if (Answer1Checked || Answer2Checked || Answer3Checked || Answer4Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public CheckboxQuestionViewModel() { }
        //Fields for check images and correct answer highlighting
        private bool _Answer1Checked;
        public bool Answer1Checked { 
            get { return _Answer1Checked; }
            set
            {
                this.RaiseAndSetIfChanged(ref _Answer1Checked, value);
            }
        }

        private bool _Answer2Checked;
        public bool Answer2Checked { get { return _Answer2Checked; } set => this.RaiseAndSetIfChanged(ref _Answer2Checked, value); }

        private bool _Answer3Checked;
        public bool Answer3Checked { get { return _Answer3Checked; } set => this.RaiseAndSetIfChanged(ref _Answer3Checked, value); }

        private bool _Answer4Checked;
        public bool Answer4Checked { get { return _Answer4Checked; } set => this.RaiseAndSetIfChanged(ref _Answer4Checked, value); }
        public CheckboxQuestion questionModel { get; set; }

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
                
                    this.RaiseAndSetIfChanged(ref _isIncorrectAnswer3, value);
                
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
                
                    this.RaiseAndSetIfChanged(ref _isIncorrectAnswer4, value);
                
            }
        }
        
        private bool _enabled = true;
        public bool enabled
        {
            get { return _enabled; }
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }
        //shows the checkmarks, x's, and highlights correct answer
        public void showResult()
        {
            if (choicesSelected.Count == 0)
            {
                switch (questionModel.correctAnswer1)
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
                switch (questionModel.correctAnswer2)
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
            foreach (int i in choicesSelected)
            {
                if (i == questionModel.correctAnswer1 || i == questionModel.correctAnswer2)
                {
                    switch (i)
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
                else
                {
                    switch (i)
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
                }

            }
            foreach (int i in choicesSelected)
            {
                switch (questionModel.correctAnswer1)
                {
                    case 1:
                        if (!isCorrectAnswer1)
                        {
                            CorrectRect1 = true;
                        }
                        break;
                    case 2:
                        if (!isCorrectAnswer2)
                        {
                            CorrectRect2 = true;
                        }
                        break;
                    case 3:
                        if (!isCorrectAnswer3)
                        {
                            CorrectRect3 = true;
                        }
                        break;
                    case 4:
                        if (!isCorrectAnswer4)
                        {
                            CorrectRect4 = true;
                        }
                        break;

                }
                switch (questionModel.correctAnswer2)
                {
                    case 1:
                        if (!isCorrectAnswer1)
                        {
                            CorrectRect1 = true;
                        }
                        break;
                    case 2:
                        if (!isCorrectAnswer2)
                        {
                            CorrectRect2 = true;
                        }
                        break;
                    case 3:
                        if (!isCorrectAnswer3)
                        {
                            CorrectRect3 = true;
                        }
                        break;
                    case 4:
                        if (!isCorrectAnswer4)
                        {
                            CorrectRect4 = true;
                        }
                        break;

                }
            }

        }
        public CheckboxQuestionViewModel(string[] response, int questionNum)
        {
            questionModel = new CheckboxQuestion(response, questionNum);

        }
        public CheckboxQuestionViewModel(CheckboxQuestion q, int questionNum)
        {
            questionModel = q;
            choicesSelected = q.choicesSelected;
            foreach (int i in choicesSelected)
            {
                switch (i)
                {
                    case 1:
                        Answer1Checked = true;
                        break;
                    case 2:
                        Answer2Checked = true;
                        break;
                    case 3:
                        Answer3Checked = true;
                        break;
                    case 4:
                        Answer4Checked = true;
                        break;
                }
            }

        }
        public List<int> choicesSelected = new List<int>();
        public void checkSelections()
        {
            if (Answer1Checked)
            {
                choicesSelected.Add(1);
            }
            if (Answer2Checked)
            {
                choicesSelected.Add(2);
                
            }
            if (Answer3Checked)
            {
                choicesSelected.Add(3);
                
            }
            if (Answer4Checked)
            {
                choicesSelected.Add(4);
               
            }
        }
        // checks for the score and returns the response. 
        private double _score = -1;
        public double score
        {
            get
            {
                questionModel.choicesSelected = choicesSelected;
                if (_score != -1)
                {
                    return _score;
                }
                if (!(choicesSelected.Count > 0))
                {
                    checkSelections();
                }
                
                if (choicesSelected.Count == 1)
                {
                    if (questionModel.correctAnswer1 == choicesSelected[0] || questionModel.correctAnswer2 == choicesSelected[0])
                    {
                        _score = 0.5;
                        return 0.5;
                    }
                    _score = 0;
                    return 0;
                }

                else if (choicesSelected.Count == 2)
                {
                    int numCorrect = 0;
                    foreach (int i in choicesSelected)
                    {
                        if (i == questionModel.correctAnswer1 || i == questionModel.correctAnswer2)
                        {
                            numCorrect++;
                        }
                    }
                    _score = numCorrect * 0.5;
                    return numCorrect * 0.5;
                }
                else
                {
                    _score = 0;
                    return 0;
                }
            }
        }

    }
}
