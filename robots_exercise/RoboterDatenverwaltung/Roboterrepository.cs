namespace RoboterDatenverwaltung;

public class RoboterRepository : IRoboterRepository
{
    private readonly string _ordner;
    private readonly IRoboterSerializer _csvSerializer;
    private readonly IRoboterSerializer _jsonSerializer;

    public RoboterRepository(string ordner, IRoboterSerializer csvSerializer, IRoboterSerializer jsonSerializer)
    {
        _ordner = ordner;
        _csvSerializer = csvSerializer;
        _jsonSerializer = jsonSerializer;
    }

    public void SpeichereAlle(IEnumerable<Roboter> roboter)
    {
        Directory.CreateDirectory(_ordner);
        LoescheVorhandeneDateien();

        int index = 1;
        foreach (Roboter einzelnerRoboter in roboter)
        {
            string basisname = $"roboter_{index:D2}";
            string csvPfad = Path.Combine(_ordner, $"{basisname}.csv");
            string jsonPfad = Path.Combine(_ordner, $"{basisname}.json");

            _csvSerializer.Speichern(einzelnerRoboter, csvPfad);
            _jsonSerializer.Speichern(einzelnerRoboter, jsonPfad);
            index++;
        }
    }

    public List<Roboter> LadeAlleCsv()
    {
        return Directory
            .GetFiles(_ordner, "*.csv")
            .OrderBy(datei => datei)
            .Select(datei => _csvSerializer.Laden(datei))
            .ToList();
    }

    public List<Roboter> LadeAlleJson()
    {
        return Directory
            .GetFiles(_ordner, "*.json")
            .OrderBy(datei => datei)
            .Select(datei => _jsonSerializer.Laden(datei))
            .ToList();
    }

    public void LoescheVorhandeneDateien()
    {
        foreach (string datei in Directory.GetFiles(_ordner, "*.csv"))
            File.Delete(datei);

        foreach (string datei in Directory.GetFiles(_ordner, "*.json"))
            File.Delete(datei);
    }
}
