public class Name
{
    public List<string> prefix;
    public string name_itself;
    public List<string> postfix;

    public Name()
    {
        this.prefix = new List<string>();
        this.name_itself = "";
        this.postfix = new List<string>();
    }

    public override string ToString()
    {
        string result = "";

        if (prefix.Count == 0 && postfix.Count == 0)
        {
            result = "JUST ";
        }

        foreach (string prefix_string in prefix)
        {
            result += prefix_string + ' ';
        }

        result += name_itself;

        if (postfix.Count == 0) return result;

        result += " WITH " + postfix.First();

        if (postfix.Count == 1) return result;

        for (int i = 1; i != postfix.Count - 1; i++)
        {
            result += ", " + postfix[i];
        }

        result += " AND " + postfix.Last();
        return result;
    }
}

public abstract class ABeverage
{
    protected Name name;

    public ABeverage()
    {
        name = new Name();
    }

    public abstract void make();

    public virtual string getName()
    {
        return name.ToString();
    }

    public virtual void addPrefix(string other)
    {
        name.prefix.Add(other);
    }

    public virtual void addPostfix(string other)
    {
        name.postfix.Add(other);
    }

    public virtual void clearName()
    {
        name = new Name();
    }
}

public class QuestionMarks : ABeverage
{
    public override void make()
    {
        clearName();
        name.name_itself = "[???]";
    }
}

public class CocaineCola : ABeverage
{
    public override void make()
    {
        clearName();
        name.name_itself = "COCAINE COLA";
    }
}

public class Tea : ABeverage
{
    public override void make()
    {
        clearName();
        name.name_itself = "TEA";
    }
}

public class Water : ABeverage
{
    public override void make()
    {
        clearName();
        name.name_itself = "WATER";
    }
}





public abstract class ABeverageDecorator : ABeverage
{
    protected ABeverage _beverage;

    public ABeverageDecorator(ABeverage beverage) : base()
    {
        this._beverage = beverage;
    }

    public override void addPostfix(string other)
    {
        _beverage.addPostfix(other);
    }

    public override void addPrefix(string other)
    {
        _beverage.addPrefix(other);
    }

    public override string getName()
    {
        return _beverage.getName();
    }

    public override void clearName()
    {
        _beverage.clearName();
    }
}

class Chainik
{
    public void jopa() { }
}

public class AddMilk : ABeverageDecorator
{
    public AddMilk(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPostfix("MILK");
    }
}

public class AddPoison : ABeverageDecorator
{
    public AddPoison(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPostfix("POISON");
    }
}

public class HeatUp : ABeverageDecorator
{
    public HeatUp(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPrefix("HOT");
    }
}

public class MakeStinky : ABeverageDecorator
{
    public MakeStinky(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPrefix("STINKY");
    }
}

public class CopyBeverage : ABeverageDecorator
{
    public CopyBeverage(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPrefix("MONOCHROME");
    }
}






public abstract class BeverageBaseFactory
{
    public abstract ABeverage getBeverage();
}

public class QuestionMarksFactory : BeverageBaseFactory
{
    public override ABeverage getBeverage()
    {
        return new QuestionMarks();
    }
}

public class CocaineColaFactory : BeverageBaseFactory
{
    public override ABeverage getBeverage()
    {
        return new CocaineCola();
    }
}

public class TeaFactory : BeverageBaseFactory
{
    public override ABeverage getBeverage()
    {
        return new Tea();
    }
}

public class WaterFactory : BeverageBaseFactory
{
    public override ABeverage getBeverage()
    {
        return new Water();
    }
}





public abstract class BeverageDecoratorFactory
{
    public abstract ABeverage getBeverage(ABeverage beverage);
}

public class AddMilkFactory : BeverageDecoratorFactory
{
    public override ABeverage getBeverage(ABeverage beverage)
    {
        return new AddMilk(beverage);
    }
}

public class AddPoisonFactory : BeverageDecoratorFactory
{
    public override ABeverage getBeverage(ABeverage beverage)
    {
        return new AddPoison(beverage);
    }
}

public class HeatUpFactory : BeverageDecoratorFactory
{
    public override ABeverage getBeverage(ABeverage beverage)
    {
        return new HeatUp(beverage);
    }
}

public class MakeStinkyFactory : BeverageDecoratorFactory
{
    public override ABeverage getBeverage(ABeverage beverage)
    {
        return new MakeStinky(beverage);
    }
}

public class CopyBeverageFactory : BeverageDecoratorFactory
{
    public override ABeverage getBeverage(ABeverage beverage)
    {
        return new CopyBeverage(beverage);
    }
}

public class BeverageDynamicFactory
{
    ABeverage? result = null;

    Dictionary<string, BeverageBaseFactory> base_factories = new Dictionary<string, BeverageBaseFactory>();
    Dictionary<string, BeverageDecoratorFactory> decorator_factories = new Dictionary<string, BeverageDecoratorFactory>();

    public BeverageDynamicFactory()
    {
        result = null;
        base_factories = new Dictionary<string, BeverageBaseFactory>();
        decorator_factories = new Dictionary<string, BeverageDecoratorFactory>();

        registerBase(new QuestionMarksFactory(), "[???]");
        registerBase(new CocaineColaFactory(), "COCAINE COLA");
        registerBase(new TeaFactory(), "TEA");
        registerBase(new WaterFactory(), "WATER");

        registerDecorator(new AddMilkFactory(), "MILK");
        registerDecorator(new AddPoisonFactory(), "POISON");
        registerDecorator(new HeatUpFactory(), "HEAT UP");
        registerDecorator(new MakeStinkyFactory(), "MAKE STINKY");
        registerDecorator(new CopyBeverageFactory(), "COPY");
    }

    public void registerBase(BeverageBaseFactory base_factoty, string key)
    {
        base_factories[key] = base_factoty;
    }

    public void registerDecorator(BeverageDecoratorFactory decorator_factoty, string key)
    {
        decorator_factories[key] = decorator_factoty;
    }

    public bool setBase(string key)
    {
        if (!base_factories.ContainsKey(key)) return false;

        if (result != null) return false;

        result = base_factories[key].getBeverage();

        return true;
    }

    public bool addDecorator(string key)
    {
        if (!decorator_factories.ContainsKey(key)) return false;

        if (result == null) return false;

        result = decorator_factories[key].getBeverage(result);

        return true;
    }

    public void makeResult()
    {
        if (result == null) return;
        result.make();
    }

    public ABeverage? getResult()
    {
        return result;
    }

    public ABeverage pickRandomBeverageBase()
    {
        return GameService.randomElement(base_factories).Value.getBeverage();
    }

    public ABeverage pickRandomDecorator(ABeverage beverage)
    {
        return GameService.randomElement(decorator_factories).Value.getBeverage(beverage);
    }

    public void printAllBaseKeys()
    {
        foreach (var pair in base_factories)
        {
            Console.WriteLine(pair.Key);
        }
    }

    public void printAllDecoratorKeys()
    {
        foreach (var pair in decorator_factories)
        {
            Console.WriteLine(pair.Key);
        }
    }
}

public class GameService
{
    public static Random random = new Random();

    public static int decorators_count_min = 0;
    public static int decorators_count_max = 2;

    static List<string> funnyOrders = new List<string> {
        "HELLO CAN I GET UHHH....",
        "AIFDOFOABSBNNLKDKNLKFGKSDFFDS!!!!",
        "I MAY NOT NEED A BEVERAGE BUT...",
        "HI I'M GAY",
        "SORRY, I FORGOT WHAT I WANTED, BUT PLEASE HELP ME REMEMBER"
    };

    static List<string> funnyIncorrectReplies = new List<string> {
        "N[???]A I WANTED {0}",
        "WHERE'S MY {0}?",
        "YOU [???] PIECE OF [???] I WANTED {0}!!!!",
        "NO {0}. I'M SEWING YOU.",
        "SUBJECT RETURNED INCORRECT BEVERAGE. REQUESTED BEVERAGE WAS {0}. TESTING FAILED."
    };

    static List<string> funnyCorrectReplies = new List<string> {
        "YO DUDE YOU SAVED MY LIFE!!",
        "PLEASE, BE MY BOYFRIEND!!!",
        "PLEASE, BE MY GIRLFRIEND!!",
        "PLEASE, BE MY DOG!!!",
        "PLEASE, BE MY CHILD!!",
        "THANKS A LOT!",
        "I'M GAINING TRUST FOR YOU.",
        "SUBJECT RETURNED CORRECT BEVERAGE. TEST COMPLETED WITH SUCCESS."
    };

    public static ABeverage generateDesiredBeverage(BeverageDynamicFactory factory)
    {
        ABeverage result = factory.pickRandomBeverageBase();

        for (int i = 1; i <= random.Next(decorators_count_min, decorators_count_max); i++)
        {
            result = factory.pickRandomDecorator(result);
        }

        return result;
    }

    public static T randomElement<T>(ICollection<T> collection)
    {
        return collection.ElementAt(GameService.random.Next(0, collection.Count));
    }

    public static void printFunnyOrder()
    {
        Console.WriteLine("CUSTOMER: " + randomElement(funnyOrders));
    }

    public static void printBeverageQuestion(string beverage_name)
    {
        Console.WriteLine("CUSTOMER: IS THAT " + beverage_name + '?');
    }

    public static void printCorrectBeverage(string beverage_name)
    {
        Console.WriteLine(randomElement(funnyCorrectReplies));
    }

    public static void printIncorrectBeverage(string beverage_name)
    {
        Console.WriteLine(String.Format("CUSTOMER: " + randomElement(funnyIncorrectReplies), beverage_name));
    }
}

public class SelectBaseLoop : APromptLoop
{
    public readonly BeverageDynamicFactory factory_link;

    bool set_base_result;

    public SelectBaseLoop(BeverageDynamicFactory factory_link) : base()
    {
        this.factory_link = factory_link;
    }

    public override void header()
    {
        Console.WriteLine("SELECT A DRINK (\"END\" TO EXIT)");
        factory_link.printAllBaseKeys();
        Console.Write("> ");
    }

    public override bool breakCondition1()
    {
        return false;
    }

    public override bool continueCondition1()
    {
        return message.isNull();
    }

    public override void printContinueMessage1()
    {
        Console.WriteLine("ERROR: MESSAGE IS NULL");
    }

    public override void process()
    {
        if (message.equals("END"))
        {
            Console.WriteLine("BYE");
            Environment.Exit(0);
        }
        set_base_result = factory_link.setBase(message.getString());
    }

    public override bool breakCondition2()
    {
        return set_base_result;
    }

    public override bool continueCondition2()
    {
        return !set_base_result;
    }

    public override void printContinueMessage2()
    {
        Console.WriteLine("ERROR: WRONG BASE");
    }
}

public class SelectDecoratorLoop : APromptLoop
{
    public readonly BeverageDynamicFactory factory_link;

    bool set_decorator_result;

    public SelectDecoratorLoop(BeverageDynamicFactory factory_link) : base()
    {
        this.factory_link = factory_link;
    }

    public override void header()
    {
        Console.WriteLine("ADD DECORATORS (\"END\" TO STOP ADDING)");
        factory_link.printAllDecoratorKeys();
        Console.Write("> ");
    }

    public override bool breakCondition1()
    {
        return message.equals("END");
    }

    public override bool continueCondition1()
    {
        return message.isNull();
    }


    public override void printContinueMessage1()
    {
        Console.WriteLine("ERROR: MESSAGE IS NULL");
    }

    public override void process()
    {
        set_decorator_result = factory_link.addDecorator(message.getString());
    }

    public override bool breakCondition2()
    {
        return false;
    }

    public override bool continueCondition2()
    {
        return !set_decorator_result;
    }

    public override void printContinueMessage2()
    {
        Console.WriteLine("ERROR: WRONG DECORATOR");
    }
}

public class Decorator
{
    public static void doClientCode()
    {
        while (true)
        {
            BeverageDynamicFactory factory = new BeverageDynamicFactory();
            APromptLoop base_maker = new SelectBaseLoop(factory);
            APromptLoop deco_maker = new SelectDecoratorLoop(factory);

            GameService.printFunnyOrder();
            Console.WriteLine();

            ABeverage desired_beverage = GameService.generateDesiredBeverage(factory);
            desired_beverage.make();

            base_maker.runLoop();
            deco_maker.runLoop();

            factory.makeResult();

            ABeverage? beverage_result = factory.getResult();

            Console.WriteLine();

            Console.WriteLine("COOKING...");
            Thread.Sleep(500);
            Console.WriteLine("GIVING TO CUSTOMER...");
            Thread.Sleep(500);

            Console.WriteLine();

            if (beverage_result == null)
            {
                Console.WriteLine("ERROR: BEVERAGE RESULT IS NULL");
                continue;
            }

            string beverage_result_name = beverage_result.getName();
            string desired_beverage_name = desired_beverage.getName();

            GameService.printBeverageQuestion(beverage_result_name);

            if (beverage_result_name.Equals(desired_beverage_name))
            {
                GameService.printCorrectBeverage(desired_beverage_name);
            }
            else
            {
                GameService.printIncorrectBeverage(desired_beverage_name);
            }

            Console.WriteLine("PRESS ENTER TO PROCEED");
            Console.ReadLine();
        }
    }
}
