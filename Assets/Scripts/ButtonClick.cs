using UnityEngine;
using TMPro;

public class ButtonClick : MonoBehaviour
{
    // für die Ausgabe
    TextMeshProUGUI ausgabe;

    // für den aktuellen Gegenstand
    string aktuellerGegenstand = "nichts";


    void Start()
    {
        // die Text-Komponente beschaffen
        ausgabe = GameObject.Find("Canvas/Ausgabe").GetComponent<TextMeshProUGUI>();
    }

    public void OnClick()
    {
        // den Text beschaffen und anzeigen
        // aber nur dann, wenn der Gegenstand nicht "leer" ist
        if (GetComponentInChildren<TextMeshProUGUI>().text != "leer")
        {
            aktuellerGegenstand = GetComponentInChildren<TextMeshProUGUI>().text;
            Inventar.aktuellerGegenstand = aktuellerGegenstand;
            ausgabe.text = "Sie tragen gerade " + aktuellerGegenstand;
        }
        else
        {
            ausgabe.text = "Sie tragen gerade nichts";
        }
    }
}
