using System;
using UnityEngine;


[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    enum Status { CanInteract, Unavailable, Paused }

    Status status = Status.CanInteract;
    Status prevStatus = Status.CanInteract;

    [SerializeField] InteractableData interactData;


    Outline outline;

    private Transform player;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private bool approaching = false;


    public static event Action OnPointerEnter;
    public static event Action OnPointerExit;



    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

    }

    private void Update()
    {
        if (approaching && (Vector3.Magnitude(transform.position - player.position) <= interactData.InteractDistance))
        {
            status = Status.CanInteract;

            MouseInput.instance.ResetMovement();
            ResetLongInteraction();

            PerfomInteraction();
        }
    }


    void Start() => player = GameObject.FindWithTag("Player").transform;


    private void OnEnable()
    {
        Pause.OnPause += SetPause;
        HighlightInteractable.OnHighlightsEnabled += Highlight;       
    }

    private void OnDisable()
    {
        Pause.OnPause -= SetPause;
        HighlightInteractable.OnHighlightsEnabled -= Highlight;

        GameControls.instance.OnAction -= Interact;

        OnPointerExit?.Invoke();
    }



    private void OnMouseEnter()
    {
        if (status == Status.Paused) return;

        OnPointerEnter?.Invoke();

        if (Vector3.Magnitude(transform.position - player.position) <= interactData.InteractDistance) status = Status.CanInteract;
        else status = Status.Unavailable;

        GameCursor.instance.SetCursor(interactData.InteractCursor);

        if (status == Status.CanInteract)
        {
            outline.OutlineColor = interactData.InteractCursor.Color;
        }
        else
        {
            outline.OutlineColor = interactData.UnavailableCursor.Color;
        }

        outline.enabled = true;

        GameControls.instance.OnAction += Interact;

    }


    private void OnMouseExit()
    {
        ResetVisuals();
        GameControls.instance.OnAction -= Interact;

        OnPointerExit?.Invoke();
    }

    protected void ResetVisuals()
    {
        GameCursor.instance.SetCursor(null);// Cursor.SetCursor(null, hotSpot, cursorMode);
        outline.enabled = false;
    }


    private void Interact()
    {
        if (status == Status.Paused) return;

        if (status == Status.CanInteract) PerfomInteraction();
        else 
        {
            approaching = true;
            MouseInput.instance.SetDestination(false);
            MouseInput.instance.OnNewInput += ResetLongInteraction;
        }
    }


    private void ResetLongInteraction()
    {
        approaching = false;
        MouseInput.instance.OnNewInput -= ResetLongInteraction;
    }


    protected virtual void PerfomInteraction()
    {

    }



    private void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            if (status != Status.Paused)
            {
                prevStatus = status;
                status = Status.Paused;
            }

            ResetVisuals();
        }
        else
        {
            status = prevStatus;
        }
    }


    private void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
        {
            outline.OutlineColor = interactData.InteractCursor.Color;
            outline.enabled = true;
        }
        else ResetVisuals();
    }
}
