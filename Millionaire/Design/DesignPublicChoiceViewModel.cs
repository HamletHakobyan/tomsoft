using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire.Design
{
    class DesignPublicChoiceViewModel
    {
        public DesignPublicChoiceViewModel()
        {
            this.QuestionText = "Question text";
            this.Answers = new string[]
            {
                "A: Answer A",
                "B: Answer B",
                "C: Answer C",
                "D: Answer D"
            };

            this.AnswerVisible = new bool[] { false, true, false, true };
            this.PollResults = new int[] { 55, 30, 10, 5 };
            this.ShowResults = true;
        }

        public string QuestionText { get; set; }

        public string[] Answers { get; set; }
        public bool[] AnswerVisible { get; set; }
        public bool ShowResults { get; set; }
        public int[] PollResults { get; set; }

    }
}
