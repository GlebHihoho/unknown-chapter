using PixelCrushers.DialogueSystem;
using UnityEngine;

public class WallOfMist : MonoBehaviour, ISaveable
{

    [SerializeField] ParticleSystem wall;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Lua.RegisterFunction("RemoveMist", this, SymbolExtensions.GetMethodInfo(() => RemoveMist()));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("RemoveMist");
    }

    private void RemoveMist()
    {
        wall.gameObject.SetActive(false);
    }

    public void Load(SaveData.Save save)
    {
        wall.gameObject.SetActive(save.wallOfMistActive);
    }

    public void Save(ref SaveData.Save save)
    {
        save.wallOfMistActive = wall.gameObject.activeSelf;
    }
}
