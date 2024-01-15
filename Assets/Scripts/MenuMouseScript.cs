using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMouseScript : MonoBehaviour
{
    private void Awake() {
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
