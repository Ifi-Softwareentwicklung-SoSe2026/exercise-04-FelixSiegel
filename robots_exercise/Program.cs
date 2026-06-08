using RoboterDatenverwaltung;

class Program
{
    private const string ROBOT_DATA_FOLDER = "robot_data";
    private const int ROBOT_COUNT = 10;
    private static readonly Random RandomGenerator = new();
    private static readonly string[] StandardTypen =
    [
        "ServiceRoboter",
        "Schwimmroboter",
        "Wartungsroboter",
        "Explorationsroboter"
    ];

    static void Main(string[] args)
    {
        IRoboterRepository repository = new RoboterRepository(
            ROBOT_DATA_FOLDER,
            new CsvRoboterSerializer(),
            new JsonRoboterSerializer()
        );

        List<Roboter> roboter = InitialisiereZufaelligeRoboter(ROBOT_COUNT);

        Console.WriteLine("Initiale Roboter:");
        GibStatusAus(roboter);

        repository.SpeichereAlle(roboter);

        roboter.Clear();
        Console.WriteLine("\nRoboterliste geleert.");

        roboter = repository.LadeAlleCsv();
        Console.WriteLine("\nAus CSV geladene Roboter:");
        GibStatusAus(roboter);

        roboter.Clear();
        roboter = repository.LadeAlleJson();
        Console.WriteLine("\nAus JSON geladene Roboter:");
        GibStatusAus(roboter);
    }

    private static List<Roboter> InitialisiereZufaelligeRoboter(int anzahl)
    {
        var roboter = new List<Roboter>();

        for (int i = 0; i < anzahl; i++)
        {
            roboter.Add(ErzeugeZufaelligenRoboter(i + 1));
        }

        return roboter;
    }

    private static Roboter ErzeugeZufaelligenRoboter(int nummer)
    {
        string name = $"Robo_{nummer:D2}";
        int energielevel = RandomGenerator.Next(0, 101);
        bool istLieferroboter = RandomGenerator.Next(0, 2) == 1;

        if (istLieferroboter)
        {
            int lieferkapazitaet = RandomGenerator.Next(1, 51);
            return new Lieferroboter(name, energielevel, lieferkapazitaet);
        }

        string typ = StandardTypen[RandomGenerator.Next(0, StandardTypen.Length)];
        return new Roboter(name, typ, energielevel);
    }

    private static void GibStatusAus(IEnumerable<Roboter> roboter)
    {
        foreach (Roboter einzelnerRoboter in roboter)
        {
            Console.WriteLine(einzelnerRoboter.GetStatus());
        }
    }
}
