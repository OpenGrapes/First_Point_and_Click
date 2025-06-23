using UnityEngine;
using TMPro;

public class GegenstandVerwalten : MonoBehaviour
{
    public Inventar inventar; // Referenz zum Inventar-Skript
    public string nameGegenstand; // Name des Gegenstands, der verwaltet wird
    public TextMeshProUGUI ausgabe; // Text-Element, um den Namen des Gegenstands anzuzeigen

    private bool zeigeHinweis = false; // Flag, um anzuzeigen, ob ein Hinweis angezeigt werden soll
    private float anzeigeZeit = 0f; // Zeit, die der Hinweis angezeigt wird

    private void OnMouseDown()
    {
        if (inventar == null)
        {
            Debug.Log("Das Inventar-Skript muss angegeben werden.");
            return;
        }
        if (inventar.FindeSlot(nameGegenstand))
        {
            OnMouseExit(); // Mauszeiger zurücksetzen
            Destroy(gameObject);
        }
        else
        {
            ausgabe.text = "Der Gegenstand kann nicht noch einmalaufgenommen werden";
            zeigeHinweis = true;
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Start()
    {
        ausgabe.text = ""; // Setzt den Text zu Beginn leer
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
}
