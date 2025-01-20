using UnityEngine;

[CreateAssetMenu(fileName = "InteractData", menuName = "Gameplay/Interactable/InteractableData")]
public class InteractableData : ScriptableObject
{
    [SerializeField, Min(0)] float interactDistace = 2f;
    public float InteractDistance => interactDistace;


    [SerializeField] CursorData unavailableCursor;
    public CursorData UnavailableCursor => unavailableCursor;

    [SerializeField] CursorData interactCursor;
    public CursorData InteractCursor => interactCursor;



}
