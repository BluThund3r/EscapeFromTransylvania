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
        Debug.Log("load cursor");

        string redCursorPath = "Assets\\Sprites\\Cursor\\red.png";
        string whiteCursorPath = "Assets\\Sprites\\Cursor\\white.png";

        // redCursor = Resources.Load<Texture2D>(redCursorPath);
        // whiteCursor = Resources.Load<Texture2D>(whiteCursorPath);

        ChangeCursor(whiteCursor);
    }

    void Update(){
        Console.WriteLine("change cursor");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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