using System.Collections.Generic;
using System;
using UnityEngine;


public class SaveData
{
    [Serializable]
    public struct CollectibleData
    {
        public int id;
        public bool enabled;
    }

    [Serializable]
    public struct InventoryItem
    {
        public int id;
        public int quantity;
    }


    public class Save
    {

        public string location;

        public string timeStamp;

        public Vector3 playerPosition;
        public Quaternion playerRotation;

        public double physical;
        public double perception;
        public double intellect;

        public string dialogues;

        public List<CollectibleData> objects = new List<CollectibleData>();
        public Dictionary<int, bool> collectibles = new Dictionary<int, bool>();

        public List<InventoryItem> inventory = new List<InventoryItem>();
    }

}
