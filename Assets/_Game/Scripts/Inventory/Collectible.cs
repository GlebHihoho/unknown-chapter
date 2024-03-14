using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(Outline))]
public class Collectible : MonoBehaviour
{

    private bool canCollect = false;

    [SerializeField] ItemData itemData;
    [SerializeField] CursorData cursorData;

    [Space]
    [SerializeField] int increment = 1;
 
    Outline outline;

    private Transform player;
  
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;


    public static event Action<ItemData, int> OnItemGiven;


    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        player = GameObject.FindWithTag("Player").transform;

    }

    private void OnMouseDown() => Collect();


    private void OnMouseOver()
    {
        canCollect = Vector3.Magnitude(transform.position - player.position) <= itemData.InteractDistance;
   
        if (canCollect)
        {
            Cursor.SetCursor(cursorData.TakeCursor, hotSpot, cursorMode);
            outline.OutlineColor = cursorData.TakeColor;
        }
        else
        {
            Cursor.SetCursor(cursorData.ViewCursor, hotSpot, cursorMode);
            outline.OutlineColor = cursorData.InspectColor;
        }

        outline.enabled = true;

    }

    private void OnMouseExit() => ResetVisuals();


    private void ResetVisuals()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
        outline.enabled = false;
    }


    private void Collect()
    {
        if (canCollect)
        {
            OnItemGiven?.Invoke(itemData, increment);

            ResetVisuals();
            gameObject.SetActive(false);
        }
        else
        {
            DialogueManager.BarkString(itemData.ExamineBark, transform);        
        }


    }
}
