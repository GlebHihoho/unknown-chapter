using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MapCover : MonoBehaviour, ISaveable
{

    Image image;

    [SerializeField] string zoneName;

    static Dictionary<string, bool> zonesLocked = new Dictionary<string, bool>();

    public static event Action OnZoneUncovered;


    private void Awake() => image = GetComponent<Image>();


    public void InitCover()
    {
        if (image.enabled) MapCoverTrigger.OnEnter += UncoverZone;
        SaveManager.OnStartingLoad += ClearZonesList;
    }


    private void OnDestroy()
    {
        MapCoverTrigger.OnEnter -= UncoverZone;
        SaveManager.OnStartingLoad -= ClearZonesList;
    }





    private void UncoverZone(string zoneName)
    {
        if (image.enabled && zoneName == this.zoneName)
        {
            image.enabled = false;
            MapCoverTrigger.OnEnter -= UncoverZone;

            OnZoneUncovered?.Invoke();
        }
    }


    private void ClearZonesList() => zonesLocked.Clear();


    public void Save(ref SaveData.Save save)
    {
        SaveData.MapZones zoneData;

        zoneData.name = this.zoneName;
        zoneData.locked = image.enabled;

        save.levels[save.level].mapZones.Add(zoneData);
    }

    public void Load(SaveData.Save save)
    {
        if (zonesLocked.Count == 0)
        {
            foreach (SaveData.MapZones zone in save.levels[save.level].mapZones)
            {
                if (!zonesLocked.ContainsKey(zone.name))
                    zonesLocked.Add(zone.name, zone.locked);
                else 
                    zonesLocked[zone.name] = zone.locked;
            }
        }

        if (zonesLocked.ContainsKey(zoneName)) image.enabled = zonesLocked[zoneName];
        else image.enabled = true;
    }
}
