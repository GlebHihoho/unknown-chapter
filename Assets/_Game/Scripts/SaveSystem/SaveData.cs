using System.Collections.Generic;
using System;


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
        public ItemData itemData;
        public int quantity;
    }


    public class Save
    {

        public string location;

        public string timeStamp;

        public string dialogues;

        public List<CollectibleData> objects = new List<CollectibleData>();
        public Dictionary<int, bool> collectibles = new Dictionary<int, bool>();

        public List<InventoryItem> inventory = new List<InventoryItem>();
    }

}
