namespace RoboterDatenverwaltung;

public class CsvRoboterSerializer : IRoboterSerializer
{
    public void Speichern(Roboter roboter, string dateipfad)
    {
        string inhalt = roboter is Lieferroboter lieferroboter
            ? $"{roboter.Name},{roboter.Typ},{roboter.Energielevel},{lieferroboter.Lieferkapazität}"
            : $"{roboter.Name},{roboter.Typ},{roboter.Energielevel}";

        File.WriteAllText(dateipfad, inhalt);
    }

    public Roboter Laden(string dateipfad)
    {
        string[] zeilen = File.ReadAllLines(dateipfad);
        string[] werte = zeilen[0].Split(',');

        string name = werte[0];
        string typ = werte[1];
        int energielevel = int.Parse(werte[2]);

        if (typ == "Lieferroboter" && werte.Length > 3)
        {
            int lieferkapazitaet = int.Parse(werte[3]);
            return new Lieferroboter(name, energielevel, lieferkapazitaet);
        }

        return new Roboter
        {
            Name = name,
            Typ = typ,
            Energielevel = energielevel
        };
    }
}
