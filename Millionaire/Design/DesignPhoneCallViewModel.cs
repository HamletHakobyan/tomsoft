using System.Linq;

namespace Millionaire.Design
{
    class DesignPhoneCallViewModel
    {
        public DesignPhoneCallViewModel()
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
            this.FriendToCall = "A friend";
        }

        public string QuestionText { get; set; }

        public string[] Answers { get; set; }
        public bool[] AnswerVisible { get; set; }
        public string FriendToCall { get; set; }
    }
}
