using UnityEngine;

public class ChildScriptAttacher : MonoBehaviour
{
    public Texture2D mauszeigerTexture;

    void Start()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform subChild in child)
            {
                if (subChild.GetComponent<Mauszeiger>() == null)
                {
                    Mauszeiger mz = subChild.gameObject.AddComponent<Mauszeiger>();
                    mz.mauszeiger = mauszeigerTexture;
                }
            }
        }
    }
}