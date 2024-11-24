using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class ZoneManager : MonoBehaviour, ISaveable
    {

        [SerializeField] ZoneData[] zonesData;

        [SerializeField] ZoneData initialZone;

        Dictionary<string, ZoneData> zones = new Dictionary<string, ZoneData>();

        ZoneData activeZone;


        public static ZoneManager instance;

        public static event Action<AudioClip> OnChangeAmbientMusic;


        private void Awake()
        {

            if (instance == null) instance = this;

            zones.Clear();
            foreach (ZoneData zone in zonesData)
            {
                if (!zones.ContainsKey(zone.SaveID)) zones.Add(zone.SaveID, zone);
                else Debug.LogWarning("Zone " + zone.SaveID + " already in the list.");
            }
        }


        void Start() => ZoneBorder.OnZoneChanged += ChangeZone;
        private void OnDestroy() => ZoneBorder.OnZoneChanged -= ChangeZone;


        private void ChangeZone(ZoneData newZone)
        {
            if (newZone != activeZone)
            {
                activeZone = newZone;
                OnChangeAmbientMusic?.Invoke(activeZone.BackgroundMusic);
            }
        }

        public void Save(ref SaveData.Save save)
        {
            if (activeZone != null) save.zoneID = activeZone.SaveID;
            else save.zoneID = "";
        }

        public void Load(SaveData.Save save)
        {

            if (zones.Count == 0)
                foreach (ZoneData zone in zonesData)
                {
                    if (!zones.ContainsKey(zone.SaveID)) zones.Add(zone.SaveID, zone);
                    else Debug.LogWarning("Zone " + zone.SaveID + " already in the list.");
                }


            if (zones.ContainsKey(save.zoneID)) ChangeZone(zones[save.zoneID]);
            else ChangeZone(initialZone);
        }


        public string GetZoneLabel(string zoneID)
        {
            if (zones.ContainsKey(zoneID)) return zones[zoneID].Label;
            else return (" - ");
        }


    }
}
