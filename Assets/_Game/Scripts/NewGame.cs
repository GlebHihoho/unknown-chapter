using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NewGame : MonoBehaviour, ISaveable
{

    bool isNewGame = true;

    public UnityEvent OnNewGame;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => SaveManager.OnLoadCompleted += OnLoad;
    private void OnDestroy() => SaveManager.OnLoadCompleted -= OnLoad;

    private void OnLoad()
    {
        SaveManager.OnLoadCompleted -= OnLoad;

        if (isNewGame) StartCoroutine(StartingGame());
    }

    IEnumerator StartingGame()
    {
        yield return new WaitForSeconds(1);
        StartGame();
    }

    private void StartGame()
    {
        if (isNewGame)
        {
            isNewGame = false;
            OnNewGame?.Invoke();

        }
    }

    public void Save(ref SaveData.Save save) => save.isNewGame = isNewGame;
    public void Load(SaveData.Save save) => isNewGame = save.isNewGame;
}
