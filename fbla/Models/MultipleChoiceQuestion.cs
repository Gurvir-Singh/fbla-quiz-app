using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
namespace fbla.Models
{
    [Serializable]
    public class MultipleChoiceQuestion
    {
        public string type = "MultipleChoice";
        private bool _answered = false;
        public bool answered { get { return _answered; } set { _answered = value; } }
        public MultipleChoiceQuestion() { }
        public MultipleChoiceQuestion(string[] response, int questionNum)
        {
            Question = questionNum.ToString() + ". " + response[1];
            Answer1 = response[2];
            Answer2 = response[3];
            Answer3 = response[4];
            Answer4 = response[5];
            correctAnswer = Int32.Parse(response[6]);
            answerSelected = 0;
        }
        //Fields for data
        public string Question { get; set; }
        public string Answer1 { get { return _Answer1; } set { _Answer1 = value; } }
        private string _Answer1 { get; set; }
        public string Answer2 { get { return _Answer2; } set { _Answer2 = value; } }
        private string _Answer2 { get; set; }
        public string Answer3 { get { return _Answer3; } set { _Answer3 = value; } }
        private string _Answer3 { get; set; }
        public string Answer4 { get { return _Answer4; } set { _Answer4 = value; } }
        private string _Answer4 { get; set; }
        public int correctAnswer { get; set; }
        public int answerSelected { get; set; }

        public bool answeredCorrectly()
        {
            if (answerSelected == correctAnswer)
            {
                return true;
            }
            return false;
        }
        public bool correct { get { return answeredCorrectly(); } }
    }
}
