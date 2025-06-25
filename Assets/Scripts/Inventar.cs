using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Inventar : MonoBehaviour
{
    public ButtonClick button; // für das Prefab
    public Canvas canvas; // für den Canvas
    public GameObject inventarPanel; // für das Panel
    public GameObject textKeineGegenstaende; // für den Text, wenn keine Gegenstände vorhanden sind
    public int anzahlGegenstaende;

    // für die Gegenstände
    public static List<Gegenstand> listeGegenstaende = new List<Gegenstand>();
    public static int ausgewaehlterIndex; // für den ausgewählten Gegenstand

    bool zeigeInventar; // für die Anzeige des Inventars
    float saveTimeScale; // zum Unterbrechen des Spiels

    void Start()
    {
        // Canvas deaktivieren
        canvas.enabled = false;

        zeigeInventar = false; // Inventar wird nicht angezeigt

        ausgewaehlterIndex = -1; // kein Gegenstand ausgewählt

        // Eine leere Liste erzeugen
        for (int i = 0; i < anzahlGegenstaende; i++)
            listeGegenstaende.Add(new Gegenstand(0, "leer", "leer"));

        InitialisiereInventoryButtons();
    }
    void Update()
    {
        // wenn die Taste i gedrückt wird und das Inventar nicht
        // gezeigt wird, unterbrechen wir das Spiel
        // Wird die Taste i gedrückt und das Inventar bereits
        // angezeigt, setzen wir das Spiel vort.
        if (Input.GetKeyDown("i"))
            if (!zeigeInventar)
                SpielAnhalten();
            else
                SpielFortsetzen();
        // Wenn die Esc-Taste gedrückt wird und das Inventar
        // gezeigt wird, geht es weiter
        if (Input.GetKeyDown(KeyCode.Escape) && zeigeInventar)
            SpielFortsetzen();
    }

    public void GetSiblingIndex(int index)
    {
        // Diese Methode wird aufgerufen, wenn ein Button geklickt wird
        // und setzt den Index des ausgewählten Gegenstands
        ausgewaehlterIndex = index;
    }

    public bool PruefeGegenstand(string bedingung)
    {
        bool ergebnis = false;
        if (bedingung == listeGegenstaende[ausgewaehlterIndex].GetBedingung())
        {
            ergebnis = true;
            // 1 abziehen
            listeGegenstaende[ausgewaehlterIndex].AendereAnzahl(-1);
            // Wenn es der letzte Gegenstand war, löschen
            // wir den Gegenstand durch überschreiben
            if (listeGegenstaende[ausgewaehlterIndex].GetAnzahl() == 0)
                listeGegenstaende[ausgewaehlterIndex] = new Gegenstand(0, "leer", "leer");
                ausgewaehlterIndex = -1;        // Auswahl fällt wieder weg
                if (ButtonClick.ausgabe != null)
                    ButtonClick.ausgabe.text = "Sie tragen gerade nichts.";
            // Die Buttons erstellen
        }
        InitialisiereInventoryButtons();
        return ergebnis;
    }

    public void SpielFortsetzen()
    {
        zeigeInventar = false;
        Time.timeScale = saveTimeScale;
        canvas.enabled = false;
    }
    public void SpielAnhalten()
    {
        zeigeInventar = true;
        saveTimeScale = Time.timeScale;
        Time.timeScale = 0;
        canvas.enabled = true;
        if (anzahlGegenstaende > 0)
        {
            textKeineGegenstaende.SetActive(false);
        }
        else
        {
            textKeineGegenstaende.SetActive(true);
        }
    }

    public bool FindeSlot(string gegenstandName, string gegenstandBedingung, int maxAnzahl)
    {
        // hier prüfen wir zuerst, ob der Gegenstand schon in der
        // Liste ist und noch weitere aufgenommen werden dürfen.
        // Dann suchen wir nach einem freien Platz
        // Wenn der Gegenstand nicht abgelegt wird, geben wir "false" zurück.
        for (int i = 0; i < anzahlGegenstaende; i++)
        {
            if (listeGegenstaende[i].GetName() == gegenstandName)
            {
                if (listeGegenstaende[i].GetAnzahl() == maxAnzahl)
                    return false;
                else
                {
                    listeGegenstaende[i].AendereAnzahl(1);
                    InitialisiereInventoryButtons();
                    return true;
                }
            }
        }
        for (int i = 0; i < anzahlGegenstaende; i++)
        {
            if (listeGegenstaende[i].GetName() == "leer")
            {
                listeGegenstaende[i] = new Gegenstand(1, gegenstandName, gegenstandBedingung);
                InitialisiereInventoryButtons();
                return true;
            }
        }
        return false;
    }

    void InitialisiereInventoryButtons()
    {
        // Alle Game-Objekte mit dem Tag "listButton" besorgen und zerstören.
        foreach (GameObject obj in
        GameObject.FindGameObjectsWithTag("listButton"))
            Destroy(obj);

        // Die Schaltflächen erzeugen
        for (int i = 0; i < anzahlGegenstaende; i++)
        {
            ButtonClick ausgabe = Instantiate(button);
            ausgabe.transform.SetParent(inventarPanel.transform, false);
            ausgabe.GetComponentInChildren<TextMeshProUGUI>().text =
            listeGegenstaende[i].GetName() + "\n" +
            listeGegenstaende[i].GetAnzahl().ToString();
        }
    }

}
