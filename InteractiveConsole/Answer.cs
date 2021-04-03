namespace InteractiveConsole
{
    public enum Answer
    {
        Yes, No    
    }

    public static class Answers
    {
        public static bool IsYes(this Answer answer) => answer == Answer.Yes;

        public static bool IsNo(this Answer answer) => answer == Answer.No;
    }
}