public class Message
{
    string? str;

    public bool isNull()
    {
        return str == null;
    }

    public bool readLine()
    {
        str = Console.ReadLine();
        return !isNull();
    }

    public bool equals(string other)
    {
        return !isNull() && str.Equals(other);
    }

    public string getString()
    {
        return str;
    }
}

public abstract class APromptLoop
{
    protected Message message;

    public APromptLoop()
    {
        this.message = new Message();
    }

    public abstract void header();
    public abstract bool breakCondition1();
    public abstract bool continueCondition1();
    public abstract void process();
    public abstract bool breakCondition2();
    public abstract bool continueCondition2();

    public virtual void printBreakMessage1() { }
    public virtual void printContinueMessage1() { }
    public virtual void printBreakMessage2() { }
    public virtual void printContinueMessage2() { }

    public void runLoop()
    {
        while (true)
        {
            header();
            message.readLine();
            if (continueCondition1())
            {
                printContinueMessage1();
                continue;
            }
            if (breakCondition1())
            {
                printBreakMessage1();
                break;
            }
            process();
            if (continueCondition2())
            {
                printContinueMessage2();
                continue;
            }
            if (breakCondition2())
            {
                printBreakMessage2();
                break;
            }
        }
    }
}
