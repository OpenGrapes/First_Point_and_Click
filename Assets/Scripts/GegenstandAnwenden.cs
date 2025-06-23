using UnityEngine;
using TMPro;

public class GegenstandAnwenden : MonoBehaviour
{
    public TextMeshProUGUI ausgabe;
    float anzeigeZeit = 0.0f;
    bool zeigeHinweis = false;
    void Start()
    {
        ausgabe = GameObject.Find("CanvasHinweis/AusgabeHinweis").GetComponent<TextMeshProUGUI>();
        ausgabe.text = ""; // Setzt den Text zu Beginn leer
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
    private void OnMouseDown()
    {
        // Den Gegenstand beschaffen
        string gegenstand = Inventar.aktuellerGegenstand;
        ausgabe.text = "Der Gegenstand " + gegenstand + " wurde angewendet.";
        zeigeHinweis = true;
    }
}