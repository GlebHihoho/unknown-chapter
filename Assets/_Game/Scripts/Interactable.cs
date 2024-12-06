using System;
using UnityEngine;
using UnityEngine.InputSystem;


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
    }



    private void OnMouseEnter()
    {
        if (status == Status.Paused) return;

        OnPointerEnter?.Invoke();

        if (Vector3.Magnitude(transform.position - player.position) <= interactData.InteractDistance) status = Status.CanInteract;
        else status = Status.Unavailable;


        if (status == Status.CanInteract)
        {
            GameCursor.instance.SetCursor(interactData.InteractCursor); //  Cursor.SetCursor(interactData.InteractCursor.Sprite, hotSpot, cursorMode);
            outline.OutlineColor = interactData.InteractCursor.Color;
        }
        else
        {
            GameCursor.instance.SetCursor(interactData.UnavailableCursor); //, hotSpot, cursorMode);
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


    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
