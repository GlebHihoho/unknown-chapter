using PixelCrushers.DialogueSystem;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour, ISaveable
{
    [SerializeField] ButtonUpdated mapButton;

    [SerializeField] MapSoundData sounds;

    bool mapEnabled = false;

    public bool MapOpened
    {
        get { return gameObject.activeSelf; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lua.RegisterFunction("UnlockMap", this, SymbolExtensions.GetMethodInfo(() => UnlockMap()));

        MapCover.OnZoneUncovered += ZoneUncovered;


        foreach (MapCover item in FindObjectsByType<MapCover>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToArray())
        {
            item.InitCover();
        }


        mapButton.gameObject.SetActive(false);

        gameObject.SetActive(false);       
    }



    private void OnDestroy()
    {
        Lua.UnregisterFunction("UnlockMap");

        MapCover.OnZoneUncovered -= ZoneUncovered;
    }


    private void OnEnable() => mapButton.ResetUpdate();


    private void ZoneUncovered()
    {
        mapButton.ShowUpdate();
        SoundManager.instance.PlayEffect(sounds.NewLocation);
    }

    public void ToggleMap()
    {
        if (mapEnabled) gameObject.SetActive(!gameObject.activeSelf);
    }

    public void HideMap() => gameObject.SetActive(false);


    private void UnlockMap()
    {
        mapEnabled = true;

        mapButton.gameObject.SetActive(true);
        mapButton.ShowUpdate();
    }



    public void Save(ref SaveData.Save save)
    {
        save.mapUnlocked = mapEnabled;
    }

    public void Load(SaveData.Save save)
    {
        mapEnabled = save.mapUnlocked;

        if (mapEnabled) mapButton.gameObject.SetActive(true);
    }
}
