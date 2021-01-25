using System;
using System.Collections.Generic;
using System.Text;

namespace fbla.Models
{
    public class TrueFalseQuestion
    {

        private bool _answered = false;
        public bool answered { get { return _answered; } set { _answered = value; } }
        public TrueFalseQuestion() { }
        public TrueFalseQuestion(string[] response, int questionNum)
        {
            Question = questionNum.ToString() + ". " + response[1];
            Answer1 = response[2];
            Answer2 = response[3];
            correctAnswer = Int32.Parse(response[4]);
            answerSelected = 0;
        }
        //Fields for data
        public string Question { get; set; }

        public string Answer1 { get { return _Answer1; } set { _Answer1 = value; } }
        private string _Answer1 { get; set; }
        public string Answer2 { get { return _Answer2; } set { _Answer2 = value; } }
        private string _Answer2 { get; set; }

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
