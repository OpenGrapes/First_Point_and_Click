using UnityEngine;
using TMPro;

public class ButtonClick : MonoBehaviour
{
    // für die Ausgabe
    public static TextMeshProUGUI ausgabe;

    void Start()
    {
        // die Text-Komponente beschaffen
        ausgabe = GameObject.Find("Canvas/Ausgabe").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }

    public void OnClick()
    {
        int idx = transform.GetSiblingIndex();          // Position des Slots
        var eintrag = Inventar.listeGegenstaende[idx];  // kurzer Alias

        // Slot leer?  → Auswahl zurücksetzen
        if (eintrag.GetAnzahl() == 0)
        {
            Inventar.ausgewaehlterIndex = -1;           // nichts ausgewählt
            return;
        }

        // Slot hat Gegenstand → diesen auswählen
        Inventar.ausgewaehlterIndex = idx;
        ausgabe.text = "Sie tragen gerade " + eintrag.GetName();
    }
}
