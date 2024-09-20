using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Map : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject mapButton;

    bool mapEnabled = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Lua.RegisterFunction("UnlockMap", this, SymbolExtensions.GetMethodInfo(() => UnlockMap()));

        gameObject.SetActive(false);

        mapButton.SetActive(false);
    }

    private void OnDestroy() => Lua.UnregisterFunction("UnlockMap");


    public void ToggleMap()
    {
        if (mapEnabled) gameObject.SetActive(!gameObject.activeSelf);
    }


    private void UnlockMap()
    {
        mapEnabled = true;

        mapButton.SetActive(true);
    }



    public void Save(ref SaveData.Save save)
    {
        save.mapUnlocked = mapEnabled;
    }

    public void Load(SaveData.Save save)
    {
        mapEnabled = save.mapUnlocked;

        if (mapEnabled) mapButton.SetActive(true);
    }
}
