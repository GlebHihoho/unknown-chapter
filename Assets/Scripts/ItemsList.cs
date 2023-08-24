using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemsCanBeTaken", menuName = "ScriptableObjects.ItemsList")]
public class ItemsList : ScriptableObject
{
    public List<ItemSO> items = new List<ItemSO>();
}
