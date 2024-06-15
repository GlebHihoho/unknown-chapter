using NUnit.Framework.Interfaces;
using PixelCrushers.DialogueSystem;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ConversationPartner : MonoBehaviour
{

    enum Status { CanTalk, CanInspect, Paused }

    Status status = Status.CanTalk;
    Status prevStatus = Status.CanTalk;

    [SerializeField] CursorData cursorData;
    [SerializeField, Min(0)] float interactDistance = 2;

    [SerializeField, TextArea(0, 3)] string examineBark;


    [SerializeField] string conversation;


    Outline outline;

    private Transform player;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable() => Pause.OnPause += SetPause;
    private void OnDisable() => Pause.OnPause -= SetPause;
    private void OnMouseDown() => Talk();


    private void OnMouseOver()
    {
        if (status == Status.Paused) return;

        if (Vector3.Magnitude(transform.position - player.position) <= interactDistance) status = Status.CanTalk;
        else status = Status.CanInspect;


        if (status == Status.CanTalk)
        {
            Cursor.SetCursor(cursorData.TalkCursor, hotSpot, cursorMode);
            outline.OutlineColor = cursorData.TalkColor;
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


    private void Talk()
    {
        if (status == Status.Paused) return;

        if (status == Status.CanTalk)
        {
            DialogueManager.StartConversation(conversation);
        }
        else
        {
            DialogueManager.BarkString(examineBark, transform);
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
