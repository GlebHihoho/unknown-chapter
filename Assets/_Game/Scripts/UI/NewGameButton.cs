using UnityEngine;

public class NewGameButton : MonoBehaviour
{

    SaveManager saveManager;

    void Start() => saveManager = FindFirstObjectByType<SaveManager>();

    public void NewGame()
    {
        if (saveManager == null) saveManager = FindFirstObjectByType<SaveManager>();

        saveManager.NewGame();
    }


}
