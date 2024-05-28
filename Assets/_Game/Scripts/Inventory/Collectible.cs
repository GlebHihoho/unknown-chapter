using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(Outline))]
public class Collectible : MonoBehaviour
{

    enum Status { CanCollect, CanInspect, Paused}

    Status status = Status.CanCollect;
    Status prevStatus = Status.CanCollect;

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

    private void OnEnable() => Pause.OnPause += SetPause;
    private void OnDisable() => Pause.OnPause -= SetPause;


    private void OnMouseDown() => Collect();


    private void OnMouseOver()
    {
        if (status == Status.Paused) return;

        if (Vector3.Magnitude(transform.position - player.position) <= itemData.InteractDistance) status = Status.CanCollect;
        else status = Status.CanInspect;
                
   
        if (status == Status.CanCollect)
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

        if (status == Status.Paused) return;

        if (status == Status.CanCollect)
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


    private void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            prevStatus = status;
            status = Status.Paused;

            ResetVisuals();
        }
        else
        {
            status = prevStatus;
        }
    }
}
