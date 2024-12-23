using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Data.OptionTemplate
{
    public class AnwserTemplate
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Right { get; set; }

        public QuestionTemplate question { get; set; }
    }
}
