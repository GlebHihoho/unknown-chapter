using System;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemsCanBeTaken", menuName = "ScriptableObjects.ItemsList")]
public class ItemsList : ScriptableObject
{
    [Serializable]
    public class ItemWithCount
    {
        public ItemSO item;
        public int count;
    }

    public List<ItemWithCount> itemsWithCount = new List<ItemWithCount>();
}
