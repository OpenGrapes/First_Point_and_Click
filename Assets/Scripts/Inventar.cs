using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventar : MonoBehaviour
{
    public Button button; // für das Prefab
    public Canvas canvas; // für den Canvas
    public GameObject inventarPanel; // für das Panel
    public GameObject textKeineGegenstaende; // für den Text, wenn keine Gegenstände vorhanden sind

    // für die Gegenstände; Konstruktion ist vorläufig
    public string[] gegenstaende = { "Objekt 1", "Objekt 2", "Objekt 3", "Objekt 4", "leer", "leer", "leer", "leer", "leer" };

    bool zeigeInventar = false; // für die Anzeige des Inventars
    float saveTimeScale; // zum Unterbrechen des Spiels

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
        if(gegenstaende.Length > 0)
        {
            textKeineGegenstaende.SetActive(false);
        }
        else
        {
            textKeineGegenstaende.SetActive(true);
        }

    }
    void Start()
    {
        canvas.enabled = false;
        for (int i = 0; i < gegenstaende.Length; i++)
        {
            Button itemButton = Instantiate(button);
            itemButton.transform.SetParent(inventarPanel.transform, false);
            itemButton.GetComponentInChildren<TextMeshProUGUI>().text = gegenstaende[i];
        }
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
}
