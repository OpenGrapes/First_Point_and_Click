using UnityEngine;
using TMPro;

public class GegenstandAnwendenAttacher : MonoBehaviour
{
    public GameObject anwendbareGruppe;
    public TextMeshProUGUI ausgabeText;

    void Start()
    {
        if (anwendbareGruppe != null)
        {
            foreach (Transform child in anwendbareGruppe.transform)
            {
                if (child.GetComponent<Collider>() == null)
                    child.gameObject.AddComponent<BoxCollider>();

                var anwenden = child.gameObject.AddComponent<GegenstandAnwenden>();
                anwenden.ausgabe = ausgabeText;
            }
        }
    }
}
