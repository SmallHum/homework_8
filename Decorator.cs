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

public class Protons : ABeverage
{
    public override void make()
    {
        clearName();
        name.name_itself = "PROTONS";
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

public class AddSpecialIngredient : ABeverageDecorator
{
    public AddSpecialIngredient(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPostfix("A SPECIAL INGREDIENT");
    }
}

public class AddQuestionMarks : ABeverageDecorator
{
    public AddQuestionMarks(ABeverage beverage) : base(beverage) { }

    public override void make()
    {
        _beverage.make();
        addPostfix("[???]");
    }
}
