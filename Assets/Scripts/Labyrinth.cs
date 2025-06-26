using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    public Transform wand;     // Parent-Objekt, das ein Wandstück enthält
    public int anzahlX;        // Anzahl horizontaler Segmente
    public int anzahlZ;        // Anzahl vertikaler Segmente

    void Start()
    {
        if (wand == null)
        {
            Debug.Log("Es wurde kein Objekt übergeben.");
            return;
        }

        Transform kindElement = wand.transform.GetChild(0);
        if (kindElement == null)
        {
            Debug.Log("Es wurde kein untergeordnetes Objekt gefunden.");
            return;
        }

        MeshFilter meshFilter = kindElement.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.Log("Es wird ein Objekt mit Mesh benötigt.");
            return;
        }

        // → Das Mesh und die Skalierung des Childs auslesen
        Mesh mesh = meshFilter.mesh;
        Vector3 meshGroesse = mesh.bounds.size;
        Vector3 skalierung = kindElement.transform.localScale;

        // → Hier der wichtige Unterschied: wir nehmen die LÄNGERE Seite als Segmentgröße
        float segmentGroesse = Mathf.Max(meshGroesse.x * skalierung.x, meshGroesse.z * skalierung.z);

        // Jetzt alle Wandobjekte instanziieren
        for (int i = 0; i < anzahlX; i++)
        {
            for (int j = 0; j < anzahlZ; j++)
            {
                float neuePosX = wand.position.x + segmentGroesse * i;
                float neuePosZ = wand.position.z + segmentGroesse * j;

                Vector3 position = new Vector3(neuePosX, wand.position.y, neuePosZ);

                // zufällige Drehung in 90°-Schritten
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0);

                Instantiate(wand, position, rotation);
            }
        }
    }
}
