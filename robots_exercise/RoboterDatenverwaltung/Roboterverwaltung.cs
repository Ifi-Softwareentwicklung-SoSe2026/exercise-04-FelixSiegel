namespace RoboterDatenverwaltung;

using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Roboter), "roboter")]
[JsonDerivedType(typeof(Lieferroboter), "lieferroboter")]
public class Roboter
{
    public Roboter(string name, string typ, int energielevel)
    {
        Name = name;
        Typ = typ;
        Energielevel = energielevel;
    }

    public Roboter()
    {
        Name = "Unbekannt";
        Typ = "Unbekannt";
    }

    public string Name { get; set; }
    public string Typ { get; set; }
    public int Energielevel { get; set; }

    public virtual string GetStatus()
    {
        return $"Roboter - Name: {Name}, Typ: {Typ}, Energielevel: {Energielevel}";
    }

    public virtual void Activate()
    {
        if (Energielevel > 0)
        {
            Console.WriteLine("activated");
            Energielevel--;
            return;
        }

        Console.WriteLine("energy depleted");
    }
}

public class Lieferroboter : Roboter
{
    public int Lieferkapazität { get; set; }

    public Lieferroboter() : base()
    {
        Typ = "Lieferroboter";
    }

    public Lieferroboter(string name, int energielevel, int lieferkapazität)
        : base(name, "Lieferroboter", energielevel)
    {
        Lieferkapazität = lieferkapazität;
    }

    public override string GetStatus()
    {
        return $"Lieferroboter - Name: {Name}, Typ: {Typ}, Energielevel: {Energielevel}, Lieferkapazität: {Lieferkapazität}";
    }
}
