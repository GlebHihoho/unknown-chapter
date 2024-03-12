using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CursorSettings", menuName ="Gameplay/Create cursor settings")]
public class CursorData : ScriptableObject
{

    [SerializeField] Texture2D _viewCursor;
    public Texture2D viewCursor => _viewCursor;


    [SerializeField] Color _inspectColor = Color.yellow;
    public Color inspectColor => _inspectColor;

    [SerializeField] Texture2D _takeCursor;
    public Texture2D takeCursor => _takeCursor;


    [SerializeField] Color _takeColor = Color.green;
    public Color takeColor => _takeColor;
}
