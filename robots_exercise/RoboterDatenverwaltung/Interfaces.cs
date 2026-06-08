namespace RoboterDatenverwaltung;

public interface IRoboterSerializer
{
    void Speichern(Roboter roboter, string dateipfad);
    Roboter Laden(string dateipfad);
}

public interface IRoboterRepository
{
    void SpeichereAlle(IEnumerable<Roboter> roboter);
    List<Roboter> LadeAlleCsv();
    List<Roboter> LadeAlleJson();
    void LoescheVorhandeneDateien();
}
