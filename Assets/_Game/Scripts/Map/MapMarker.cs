using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapMarker : MonoBehaviour, ISaveable
{

    [SerializeField] string markerName;
    [SerializeField] bool activeOnStart = false;

    [SerializeField] Map map;

    struct MarkerData
    {
        public bool active;
        public string name;
    }

    static Dictionary<string, MarkerData> markersActive = new Dictionary<string, MarkerData>();

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI label;


    public void Activate()
    {
        if (!image.enabled)
        {
            image.enabled = true;
            label.enabled = true;

            if (map.MapEnabled) UIMessage.instance.ShowMessage($"Новая метка на карте: {label.text}");
        }
    }


    public void ChangeLabel(string newLabel)
    {
        if (image.enabled)
        {
            if (map.MapEnabled) UIMessage.instance.ShowMessage($"Метка на карте \"{label.text}\" теперь \"{newLabel}\"");
            label.text = newLabel;
        }
        else
        {
            label.text = newLabel;
            Activate();
        }
    }


    private void SetActive(bool isActive, string newLabel)
    {
        image.enabled = isActive;
        label.enabled=isActive;

        if (newLabel != string.Empty) label.text = newLabel;

    }


    public void Save(ref SaveData.Save save)
    {
        SaveData.MapMarkers markerData;

        markerData.name = markerName;
        markerData.active = image.enabled;
        markerData.label = label.text;

        save.levels[save.level].mapMarkers.Add(markerData);
    }


    public void Load(SaveData.Save save)
    {
        if (markersActive.Count == 0)
        {
            foreach (SaveData.MapMarkers marker in save.levels[save.level].mapMarkers)
            {
                MarkerData markerData;
                markerData.active = marker.active;
                markerData.name = marker.label;


                if (!markersActive.ContainsKey(marker.name))
                    markersActive.Add(marker.name, markerData);
                else
                    markersActive[marker.name] = markerData;
            }
        }

        if (markersActive.ContainsKey(markerName)) SetActive(markersActive[markerName].active, markersActive[markerName].name);
        else SetActive(activeOnStart, string.Empty);
    }
}
