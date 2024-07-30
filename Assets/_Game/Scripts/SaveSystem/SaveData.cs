using System.Collections.Generic;
using System;
using UnityEngine;


public class SaveData
{

    public enum Type { Normal, Quick, Auto}

    [Serializable]
    public struct InventoryItem
    {
        public int id;
        public int quantity;
    }


    public class Save
    {
        public Type type;

        public string location;

        public string timeStamp;

        public Vector3 playerPosition;
        public Quaternion playerRotation;

        public string zoneID;

        public float timeOfDay;

        public double physical;
        public double perception;
        public double intellect;

        public string dialogues;

        public List<bool> collectibles = new List<bool>();

        public List<InventoryItem> inventory = new List<InventoryItem>();
    }

}
