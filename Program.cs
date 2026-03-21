public class Program
{
    public static void Main()
    {
        ABeverage something = new CocaineCola();
        ABeverage something2 = new AddPoison(something);
        ABeverage something3 = new AddMilk(something2);
        ABeverage something4 = new HeatUp(something3);
        ABeverage something5 = new MakeStinky(something4);
        ABeverage something6 = new CopyBeverage(something5);
        ABeverage something7 = new AddSpecialIngredient(something6);

        something.make();
        Console.WriteLine(something.getName());

        something2.make();
        Console.WriteLine(something2.getName());

        something3.make();
        Console.WriteLine(something3.getName());

        something4.make();
        Console.WriteLine(something4.getName());

        something5.make();
        Console.WriteLine(something5.getName());

        something6.make();
        Console.WriteLine(something6.getName());

        something7.make();
        Console.WriteLine(something7.getName());

        ABeverage something8 = new QuestionMarks();
        ABeverage something9 = new AddQuestionMarks(something8);
        ABeverage something10 = new AddSpecialIngredient(something9);

        something10.make();
        Console.WriteLine(something10.getName());

    }
}
