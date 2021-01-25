using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;
using fbla.Models;
using ReactiveUI;
using Avalonia.Input;
namespace fbla.ViewModels

{
    public class FillBlankQuestionViewModel : ViewModelBase
    {
        public bool answered()
        {
            return questionModel.answered;
        }
        public FillBlankQuestion questionModel { get; }
        public FillBlankQuestionViewModel() { }
        private string _correctAnswerText = "";
        public string correctAnswerText
        {
            get
            {
                return _correctAnswerText;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _correctAnswerText, value);
            }
        }

        private int _selectedIndex = 0;
        public int selectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value != 0)
                {
                    questionModel.answered = true;
                }
                else
                {
                    questionModel.answered = false;
                }
                this.RaiseAndSetIfChanged(ref _selectedIndex, value);
            }
        }
       
        public FillBlankQuestionViewModel(string[] response, int questionNum)
        {
            questionModel = new FillBlankQuestion(response, questionNum);
            switch (questionModel.correctAnswer)
            {
                case 1:
                    correctAnswerText = "The correct answer was: " + questionModel.Answer1;
                    break;
                case 2:
                    correctAnswerText = "The correct answer was: " + questionModel.Answer2;
                    break;
                case 3:
                    correctAnswerText = "The correct answer was: " + questionModel.Answer3;
                    break;
                case 4:
                    correctAnswerText = "The correct answer was: " + questionModel.Answer4;
                    break;
            }

        }
        private bool _showCorrectAnswer = false;
        public bool showCorrectAnswer
        {
            get { return _showCorrectAnswer; }
            set => this.RaiseAndSetIfChanged(ref _showCorrectAnswer, value);
        }
        private bool _answeredCorrectly = false; 
        public bool answeredCorrectly
        {
            get { return _answeredCorrectly; }
            set
            {
                this.RaiseAndSetIfChanged(ref _answeredCorrectly, value);
            }
        }
        new private bool _enabled = true;
        new public bool enabled
        {
            get { return _enabled; }
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }
        public void showResult()
        {

        }
       
        new public double score
        {
            get
            {
                questionModel.answerSelected = selectedIndex;
                if (questionModel.answeredCorrectly())
                {
                    answeredCorrectly = true;
                    return 1;
                    
                }
                else
                {
                    showCorrectAnswer = true;
                    return 0;
                }
            }
        }
    }
}
