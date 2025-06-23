using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mauszeiger : MonoBehaviour
{
    [HideInInspector]
    public Texture2D mauszeiger;

    void OnMouseEnter()
    {
        Cursor.SetCursor(mauszeiger, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
