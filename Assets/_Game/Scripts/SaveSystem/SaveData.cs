using System.Collections.Generic;
using System;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class SaveData
{

    public enum Type { Normal, Quick, Auto}

    [Serializable]
    public struct InventoryItem
    {
        public int id;
        public int quantity;
    }

    [Serializable]
    public struct MapZones
    {
        public string name;
        public bool locked;
    }

    [Serializable]
    public struct RecordStatus
    {
        public string id;
        public QuestState state;
        public List<int> entriesChanged;
    }


    public class Save
    {
        public Type type;

        public string location;

        public string timeStamp;

        public Vector3 playerPosition;
        public Quaternion playerRotation;

        public bool mapUnlocked = false;

        public List<MapZones> mapZones = new List<MapZones>();

        public string zoneID;

        public float timeOfDay;

        public double physical;
        public double perception;
        public double intellect;

        public string dialogues;

        public List<string> journalRecords = new List<string>();
        public List<RecordStatus> recordStatus = new List<RecordStatus>();

        public List<bool> collectibles = new List<bool>();

        public List<InventoryItem> inventory = new List<InventoryItem>();

        public bool wallOfMistActive = true;
    }

}
