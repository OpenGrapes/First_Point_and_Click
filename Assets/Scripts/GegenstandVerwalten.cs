using UnityEngine;
using TMPro;

public class GegenstandVerwalten : MonoBehaviour
{
    public Inventar inventar; // Referenz zum Inventar-Skript
    public string nameGegenstand; // Name des Gegenstands, der verwaltet wird
    public TextMeshProUGUI ausgabe; // Text-Element, um den Namen des Gegenstands anzuzeigen
    public int maxAnzahl; // Für die maximale Anzahl
    public string bedingungGegenstand; // Für die Bedingung des Gegenstandes

    private bool zeigeHinweis; // Flag, um anzuzeigen, ob ein Hinweis angezeigt werden soll
    private float anzeigeZeit; // Zeit, die der Hinweis angezeigt wird

    void Start()
    {
        ausgabe.text = ""; // Setzt den Text zu Beginn leer
        zeigeHinweis = false; // Setzt das Flag für den Hinweis zurück
        anzeigeZeit = 0.0f; // Setzt die Anzeigezeit zurück
    }

    // Update is called once per frame
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

    private void OnMouseDown()
    {
        if (inventar == null)
        {
            Debug.Log("Das Inventar-Skript muss angegeben werden.");
            return;
        }

        if (inventar.FindeSlot(nameGegenstand, bedingungGegenstand, maxAnzahl))
        {
            ausgabe.text = "Der Gegenstand " + nameGegenstand + " wurde in das Inventar aufgenommen";
            // sofort unsichtbar und nicht mehr anklickbar
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            // nach 3 Sekunden wirklich löschen
            Destroy(gameObject, 5f);
        }
        else
        {
            ausgabe.text = "Der Gegenstand kann nicht noch einmal aufgenommen werden";
        }
        zeigeHinweis = true; // Hinweis anzeigen
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
