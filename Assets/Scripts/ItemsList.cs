using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemsCanBeTaken", menuName = "ScriptableObjects.ItemsList")]
public class ItemsList : ScriptableObject
{
    // public List<ItemSO> items = new List<ItemSO>();
    // public List<int> itemCount = new List<int>();
    
    [Serializable]
    public class ItemWithCount
    {
        public ItemSO item;
        public int count;
    }

    public List<ItemWithCount> itemsWithCount = new List<ItemWithCount>();
}
