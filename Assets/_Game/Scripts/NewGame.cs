using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NewGame : MonoBehaviour
{

    bool isNewGame = true;

    public UnityEvent OnNewGame;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartingGame());
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
}
