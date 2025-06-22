using UnityEngine;

public class ChildScriptAttacher : MonoBehaviour
{
    public Texture2D mauszeigerTexture;

    void Start()
    {
        foreach (Transform child in transform)
        {
            // Prüfen, ob das Script schon dran ist
            if (child.GetComponent<Mauszeiger>() == null)
            {
                Mauszeiger mz = child.gameObject.AddComponent<Mauszeiger>();
                mz.mauszeiger = mauszeigerTexture;
            }
        }
    }
}