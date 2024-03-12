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
        canCollect = Vector3.Magnitude(transform.position - player.position) <= itemData.interactDistance;
   
        if (canCollect)
        {
            Cursor.SetCursor(cursorData.takeCursor, hotSpot, cursorMode);
            outline.OutlineColor = cursorData.takeColor;
        }
        else
        {
            Cursor.SetCursor(cursorData.viewCursor, hotSpot, cursorMode);
            outline.OutlineColor = cursorData.inspectColor;
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

            if (DialogueManager.Instance != null && DialogueManager.DatabaseManager != null && DialogueManager.MasterDatabase != null)
            {
                int oldValue = DialogueLua.GetVariable(itemData.dialogVariable).asInt;
                int newValue = Mathf.Clamp(oldValue + increment, 0, itemData.inventoryMax);
                DialogueLua.SetVariable(itemData.dialogVariable, newValue);
                DialogueManager.SendUpdateTracker();
            }

            ResetVisuals();
            gameObject.SetActive(false);
        }
        else
        {
            DialogueManager.BarkString(itemData.examineBark, transform);        
        }


    }
}
