using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    [Header("Prefabs")]
    public Transform aussenWand;
    public Transform innenWand;
    public Transform untergrund;

    [Header("Größe (gerade Zahlen ≥ 2)")]
    public int labyrinthBreite = 14;   // X
    public int labyrinthLaenge = 14;   // Z

    // Einstiegspunkt: Initialisiert das Labyrinth
    void Start()
    {
        if (!Validate()) return;

        float wallLen = GetWallLen();
        SetGroundScale(wallLen);

        float bodenOben = GetBodenOben(wallLen);
        Vector3 origin = GetOrigin(wallLen, bodenOben);

        SpawnAussenwaende(origin, wallLen);
        SpawnInnenwaende(origin, wallLen);
    }

    // Gibt die Länge eines Außenwandteils zurück
    float GetWallLen()
    {
        return aussenWand.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * aussenWand.localScale.x;
    }

    // Skaliert den Boden passend zum Labyrinth
    void SetGroundScale(float wallLen)
    {
        untergrund.localScale = new Vector3(
            labyrinthBreite * wallLen,
            untergrund.localScale.y,
            labyrinthLaenge * wallLen
        );
    }

    // Berechnet die Y-Position für die Oberkante des Bodens (für die Wände)
    float GetBodenOben(float wallLen)
    {
        float wandPivotOffset = aussenWand.GetComponent<MeshFilter>().sharedMesh.bounds.min.y * aussenWand.localScale.y;
        return untergrund.position.y + untergrund.localScale.y * 0.5f - wandPivotOffset;
    }

    // Berechnet den Ursprungspunkt (linke-vordere Ecke) für das Labyrinth
    Vector3 GetOrigin(float wallLen, float bodenOben)
    {
        return new Vector3(
            untergrund.position.x - labyrinthBreite * wallLen * 0.5f,
            bodenOben,
            untergrund.position.z - labyrinthLaenge * wallLen * 0.5f
        );
    }

    // Erzeugt die Außenwände inkl. Öffnung
    void SpawnAussenwaende(Vector3 origin, float wallLen)
    {
        int horizontalCount = labyrinthBreite - 1;
        int verticalCount = labyrinthLaenge - 1;
        int openingWidth = 6; // Breite der Öffnung unten/oben
        int openingStart = (horizontalCount - openingWidth) / 2;

        // Unten und oben (entlang X)
        for (int x = 3; x < horizontalCount; x++)
        {
            if (x >= openingStart && x < openingStart + openingWidth) continue;
            // Unten
            Spawn(aussenWand, origin + new Vector3(x * wallLen - wallLen / 2, 0, wallLen / 2), 90f);
            // Oben
            Spawn(aussenWand, origin + new Vector3(x * wallLen - wallLen / 2, 0, (labyrinthLaenge - 1) * wallLen + wallLen / 2), 90f);
        }
        // Links und rechts (entlang Z)
        for (int z = 3; z < verticalCount; z++)
        {
            // Links
            Spawn(aussenWand, origin + new Vector3(-wallLen / 2, 0, z * wallLen - wallLen / 2), 0f);
            // Rechts
            Spawn(aussenWand, origin + new Vector3((labyrinthBreite - 1) * wallLen + wallLen / 2, 0, z * wallLen - wallLen / 2), 0f);
        }
    }

    // Erzeugt die inneren Mauern mit Abstand zu den Außenwänden
    void SpawnInnenwaende(Vector3 origin, float wallLen)
    {
        float abstandLinks = 1.5f;
        float abstandRechts = labyrinthBreite - 0.5f;
        float abstandUnten = 5f;
        float abstandOben = labyrinthLaenge - 4f;
        int innenWandDichte = 2;

        for (float x = abstandLinks; x < abstandRechts; x += innenWandDichte)
            for (float z = abstandUnten; z < abstandOben; z += innenWandDichte)
            {
                float rotY = Random.Range(0, 4) * 90f;
                Vector3 pos = origin + new Vector3(x * wallLen, 0, z * wallLen);
                Spawn(innenWand, pos, rotY);
            }
    }

    // Instanziiert ein Wand- oder Bodenobjekt an der gewünschten Position und Rotation
    void Spawn(Transform pf, Vector3 pos, float rotY = 0)
    {
        Instantiate(pf, pos, Quaternion.Euler(0, rotY, 0), transform);
    }

    // Prüft, ob die Einstellungen im Inspector gültig sind
    bool Validate()
    {
        if (labyrinthBreite < 2 || labyrinthLaenge < 2 ||
            labyrinthBreite % 2 != 0 || labyrinthLaenge % 2 != 0)
        {
            Debug.LogError("Nur gerade Werte ≥ 2 für Breite/Länge zulässig!");
            return false;
        }
        if (labyrinthBreite < 10 || labyrinthLaenge < 10)
        {
            Debug.LogError("Breite und Länge müssen mindestens 10 sein!");
            return false;
        }
        if (!aussenWand || !innenWand || !untergrund)
        {
            Debug.LogError("Bitte Prefabs und Boden zuweisen.");
            return false;
        }
        return true;
    }
}