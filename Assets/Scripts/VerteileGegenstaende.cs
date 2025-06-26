using UnityEngine;

public class VerteileGegenstaende : MonoBehaviour
{
    [Header("Objekte & Anzahl")]
    public GegenstandItem[] gegenstaendeScene;

    [Header("Cursor")]
    public Texture2D mauszeigerTexture;      // hier im Inspector dein Cursor-Bild eintragen

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null)
        {
            Debug.LogError("VerteileGegenstaende: Objekt braucht MeshFilter!");
            return;
        }

        Vector3 size  = mf.mesh.bounds.size;
        Vector3 scale = transform.localScale;
        int maxX = Mathf.RoundToInt(size.x * scale.x * 0.5f);
        int maxZ = Mathf.RoundToInt(size.z * scale.z * 0.5f);

        foreach (GegenstandItem item in gegenstaendeScene)
        {
            if (item == null || item.gegenstand == null) continue;

            string suchName = item.gegenstand.name;

            // --- 1) vorhandene Objekte finden & verteilen ------------------
            var schonDa = new System.Collections.Generic.List<Transform>();
            foreach (Transform t in FindObjectsByType<Transform>(FindObjectsSortMode.None))
                if (t.name == suchName) schonDa.Add(t);

            foreach (Transform t in schonDa)
            {
                VerteilenAufFlaeche(t, maxX, maxZ);
                StelleMauszeigerSicher(t);
            }

            // --- 2) fehlende Instanzen ergänzen ---------------------------
            int fehlend = Mathf.Max(0, item.anzahl - schonDa.Count);
            for (int i = 0; i < fehlend; i++)
            {
                Vector3 pos = new Vector3(
                    Random.Range(transform.position.x - maxX, transform.position.x + maxX),
                    item.gegenstand.position.y,
                    Random.Range(transform.position.z - maxZ, transform.position.z + maxZ)
                );
                Transform neu = Instantiate(item.gegenstand, pos, item.gegenstand.rotation);
                StelleMauszeigerSicher(neu);
            }
        }
    }

    // -----------------------------------------------------------
    void VerteilenAufFlaeche(Transform t, int maxX, int maxZ)
    {
        t.position = new Vector3(
            Random.Range(transform.position.x - maxX, transform.position.x + maxX),
            t.position.y,
            Random.Range(transform.position.z - maxZ, transform.position.z + maxZ)
        );
    }

    void StelleMauszeigerSicher(Transform t)
    {
        Mauszeiger mz = t.GetComponent<Mauszeiger>();
        if (mz == null) mz = t.gameObject.AddComponent<Mauszeiger>();
        mz.mauszeiger = mauszeigerTexture;        // Cursor-Grafik setzen
    }
}