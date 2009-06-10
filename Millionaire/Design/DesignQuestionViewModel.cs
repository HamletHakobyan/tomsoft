using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire.Design
{
    public class DesignQuestionViewModel
    {
        public DesignQuestionViewModel()
        {
            this.QuestionNumber = 1;
            this.QuestionText = "Question text";
            this.Answers = new string[]
            {
                "A: Answer A",
                "B: Answer B",
                "C: Answer C",
                "D: Answer D"
            };

            this.AnswerSelected = new bool[] { false, true, false, false };
            this.AnswerVisible = new bool[] { false, true, false, true };
        }

        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        
        public string[] Answers { get; set; }
        public bool[] AnswerSelected { get; set; }
        public bool[] AnswerVisible{ get; set; }

    }
}
