using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Map : MonoBehaviour, ISaveable
{
    [SerializeField] ButtonUpdated mapButton;

    bool mapEnabled = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lua.RegisterFunction("UnlockMap", this, SymbolExtensions.GetMethodInfo(() => UnlockMap()));

        mapButton.gameObject.SetActive(false);

        gameObject.SetActive(false);       
    }

    private void OnDestroy() => Lua.UnregisterFunction("UnlockMap");


    private void OnEnable() => mapButton.ResetUpdate();


    public void ToggleMap()
    {
        if (mapEnabled) gameObject.SetActive(!gameObject.activeSelf);
    }


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
