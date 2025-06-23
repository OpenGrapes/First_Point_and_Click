using UnityEngine;
using TMPro;

public class GegenstandVerwaltenAttacher : MonoBehaviour
{
    public GameObject aufnehmbareGruppe;
    public Inventar inventarScript;
    public TextMeshProUGUI ausgabeText;

    void Start()
    {
        if (aufnehmbareGruppe != null)
        {
            foreach (Transform child in aufnehmbareGruppe.transform)
            {
                if (child.GetComponent<Collider>() == null)
                    child.gameObject.AddComponent<BoxCollider>();

                var verwalten = child.gameObject.AddComponent<GegenstandVerwalten>();
                verwalten.nameGegenstand = child.name;
                verwalten.inventar = inventarScript;
                verwalten.ausgabe = ausgabeText;
            }
        }
    }
}
