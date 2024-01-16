using System;
using System.Xml.Serialization;
using UnityEngine;

// Attach this script to a GameObject with a Collider, then mouse over the object to see your cursor change.
public class EnemyCursor : MonoBehaviour
{
    public Texture2D redCursor;
    public Texture2D whiteCursor;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = new Vector2(25, 25);

    public LayerMask layerMask;

    void ChangeCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void Awake(){
        ChangeCursor(whiteCursor);
    }

    void Update(){
        RaycastHit hit;
        Camera myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            ChangeCursor(redCursor);
        }
        else
        {
            ChangeCursor(whiteCursor);
        }
        
    }
}