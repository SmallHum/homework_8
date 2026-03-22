public class TaskSelector : APromptLoop
{
    bool picked_valid;

    public override void header()
    {
        Console.WriteLine("SELECT TASK (END TO QUIT)\nDECORATOR\nADAPTER");
        Console.Write("> ");
    }

    public override bool continueCondition1()
    {
        return message.isNull();
    }
    public override void printContinueMessage1()
    {
        Console.WriteLine("MESSAGE IS NULL");
    }

    public override bool breakCondition1()
    {
        return message.equals("END");
    }

    public override void printBreakMessage1()
    {
        Console.WriteLine("BYE");
    }

    public override void process()
    {
        picked_valid = false;
        if (message.equals("DECORATOR"))
        {
            picked_valid = true;
            Decorator.doClientCode();
        }
        else if (message.equals("ADAPTER"))
        {
            picked_valid = true;
            Adapter.doClientCode();
        }
    }
    public override bool continueCondition2()
    {
        return !picked_valid;
    }
    public override void printContinueMessage2()
    {
        Console.WriteLine("ERROR: MISSPELED WORD");
    }
    public override bool breakCondition2()
    {
        return true;
    }
    public override void printBreakMessage2()
    {
        Console.WriteLine("BYE");
    }
}

public class Program
{
    public static void Main()
    {
        APromptLoop task_selector = new TaskSelector();
        task_selector.runLoop();
    }
}
