namespace RoboterDatenverwaltung;

using System.Text.Json;

public class JsonRoboterSerializer : IRoboterSerializer
{
    public void Speichern(Roboter roboter, string dateipfad)
    {
        string json = JsonSerializer.Serialize(roboter, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dateipfad, json);
    }

    public Roboter Laden(string dateipfad)
    {
        string json = File.ReadAllText(dateipfad);
        Roboter? roboter = JsonSerializer.Deserialize<Roboter>(json)
            ?? throw new InvalidDataException($"JSON-Datei konnte nicht gelesen werden: {dateipfad}");
        return roboter;
    }
}
