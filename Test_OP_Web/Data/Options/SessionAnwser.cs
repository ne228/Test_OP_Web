using System.ComponentModel.DataAnnotations;

namespace Test_OP_Web.Data.Options
{
    public class SessionAnwser
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Right { get; set; }

        public bool Enter { get; set; }
        public CopyQuestion SessionQuestion { get; set; }

    }
}
