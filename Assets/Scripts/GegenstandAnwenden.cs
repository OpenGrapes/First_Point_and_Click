using UnityEngine;
using TMPro;

public class GegenstandAnwenden : MonoBehaviour
{
    public TextMeshProUGUI ausgabe;
    public Inventar inventar; // Referenz zum Inventar-Skript
    public string bedingung; // Bedingung, die der Gegenstand erfüllen muss
    float anzeigeZeit;
    bool zeigeHinweis;
    
    void Start()
    {
        ausgabe = GameObject.Find("CanvasHinweis/AusgabeHinweis").GetComponent<TextMeshProUGUI>();
        ausgabe.text = ""; // Setzt den Text zu Beginn leer
        anzeigeZeit = 0.0f;
        zeigeHinweis = false;
    }
    void Update()
    {
        if (zeigeHinweis)
        {
            anzeigeZeit = anzeigeZeit + Time.deltaTime;
            if (anzeigeZeit > 3)
            {
                anzeigeZeit = 0;
                ausgabe.text = "";
                zeigeHinweis = false;
            }
        }
    }
    void OnMouseDown()
    {
        if (inventar == null) return;

        // ───── 1. Prüfen: wurde überhaupt ein Slot gewählt? ─────
        if (Inventar.ausgewaehlterIndex < 0 ||
            Inventar.ausgewaehlterIndex >= Inventar.listeGegenstaende.Count)
        {
            ausgabe.text = "Bitte zuerst einen Gegenstand wählen.";
            zeigeHinweis = true;
            return;                     // <─ kein Zugriff, also kein Crash
        }

        // ───── 2. Prüfen: steckt dort überhaupt noch ein Item? ─────
        if (Inventar.listeGegenstaende[Inventar.ausgewaehlterIndex].GetAnzahl() == 0)
        {
            ausgabe.text = "Sie haben keinen passenden Gegenstand mehr.";
            zeigeHinweis = true;
            return;
        }

        // ───── 3. Jetzt ist der Index sicher gültig ─────
        string itemName = Inventar.listeGegenstaende[Inventar.ausgewaehlterIndex].GetName();

        if (inventar.PruefeGegenstand(bedingung))          // Bedingung passt?
            ausgabe.text = "Die Tür öffnet sich.";
        else
            ausgabe.text = $"Mit {itemName} lässt sich die Tür nicht öffnen.";

        zeigeHinweis = true;
    }
}