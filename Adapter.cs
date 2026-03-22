public interface IRuntimeEngine
{
    void simulateTick(double dt);
    void blitEntity(string entity_name);
}

public class MyCoolRuntimeEngine : IRuntimeEngine
{
    public void simulateTick(double dt)
    {
        Console.WriteLine("SIMULATING A TICK IN MY COOL RUNTIME ENGINE WITH DT " + dt + "...");
    }

    public void blitEntity(string entity_name)
    {
        Console.WriteLine("BLITTING AN ENTITY \"" + entity_name + "\" IN MY COOL RUNTIME ENGINE...");
    }
}

public class GodotEngine
{
    public void process(float delta)
    {
        Console.WriteLine("PROCESS CALLED IN GODOT ENGINE WITH DELTA " + delta + "...");
    }

    public void draw(int node_id)
    {
        Console.WriteLine("DRAWING NODE \"" + node_id + "\" IN GODOT ENGINE...");
    }
}

public class UnrealEngine5
{
    public void iteration(int fps)
    {
        Console.WriteLine("DOING AN ITERATION IN UNREAL ENGINE 5 WITH " + fps + " FPS...");
    }

    public void drawObj(long obj_ptr)
    {
        Console.WriteLine("DRAWING OBJECT \"" + obj_ptr + "\" IN UNREAL ENGINE 5...");
    }
}

public class GodotAdapter : IRuntimeEngine
{
    GodotEngine engine;

    public GodotAdapter(GodotEngine engine)
    {
        this.engine = engine;
    }

    public void simulateTick(double dt)
    {
        Console.WriteLine("[GODOT ADAPTER]");
        engine.process((float)dt);
    }

    public void blitEntity(string entity_name)
    {
        Console.WriteLine("[GODOT ADAPTER]");
        engine.draw(int.Parse(entity_name));
    }
}

public class UE5Adapter : IRuntimeEngine
{
    UnrealEngine5 engine;

    public UE5Adapter(UnrealEngine5 engine)
    {
        this.engine = engine;
    }

    public void simulateTick(double dt)
    {
        Console.WriteLine("[UE5 ADAPTER]");
        engine.iteration((int)(1.0 / dt));
    }

    public void blitEntity(string entity_name)
    {
        Console.WriteLine("[UE5 ADAPTER]");
        engine.drawObj(long.Parse(entity_name));
    }
}

public class Adapter
{
    public static void doClientCode()
    {
        IRuntimeEngine engine_1 = new MyCoolRuntimeEngine();
        IRuntimeEngine engine_2 = new GodotAdapter(new GodotEngine());
        IRuntimeEngine engine_3 = new UE5Adapter(new UnrealEngine5());

        engine_1.simulateTick(0.008);
        engine_1.blitEntity("DEVICE_HAND");
        Console.WriteLine();

        engine_2.simulateTick(0.016);
        engine_2.blitEntity("34235");
        Console.WriteLine();

        engine_3.simulateTick(0.016);
        engine_3.blitEntity("66445346");
    }
}
