using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CursorSettings", menuName ="Gameplay/Create cursor settings")]
public class CursorData : ScriptableObject
{

    [SerializeField] Texture2D viewCursor;
    public Texture2D ViewCursor => viewCursor;


    [SerializeField] Color inspectColor = Color.yellow;
    public Color InspectColor => inspectColor;

    [SerializeField] Texture2D takeCursor;
    public Texture2D TakeCursor => takeCursor;


    [SerializeField] Color takeColor = Color.green;
    public Color TakeColor => takeColor;

    [SerializeField] Texture2D talkCursor;
    public Texture2D TalkCursor => talkCursor;

    [SerializeField] Color talkColor = Color.green;
    public Color TalkColor => talkColor;
}
