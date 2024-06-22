using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CursorSettings", menuName ="Gameplay/Cursor settings")]
public class CursorData : ScriptableObject
{

    [SerializeField] Texture2D sprite;
    public Texture2D Sprite => sprite;


    [SerializeField] Color color = Color.yellow;
    public Color Color => color;

}
