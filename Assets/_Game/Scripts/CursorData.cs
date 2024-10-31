using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CursorSettings", menuName ="Gameplay/Cursor settings")]
public class CursorData : ScriptableObject
{

    [SerializeField] Sprite sprite;
    public Sprite Sprite => sprite;


    [SerializeField] Color color = Color.yellow;
    public Color Color => color;

}
