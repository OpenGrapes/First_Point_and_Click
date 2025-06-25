using UnityEngine;

public class Gegenstand
{
    // Eigenschaften
    int anzahl;
    string name;
    string bedingung;
    // Der Konstruktor zum Setzen der Daten
    public Gegenstand(int anzahl, string name, string bedingung)
    {
        this.anzahl = anzahl;
        this.name = name;
        this.bedingung = bedingung;
    }
    // Zum Beschaffen der Werte
    public string GetName()
    {
        return name;
    }
    public int GetAnzahl()
    {
        return anzahl;
    }
    public string GetBedingung()
    {
        return bedingung;
    }
    // Zum Ändern der Anzahl
    // Zum Reduzieren müssen negative Werte übergeben werden
    public void AendereAnzahl(int zahl)
    {
        anzahl += zahl;
    }
}
