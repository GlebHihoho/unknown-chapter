using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="AllItems", menuName ="Gameplay/Create list of all items")]
public class AllItems : ScriptableObject
{

    [SerializeField] List<ItemData> items;
    public List<ItemData> Items => items;

}
