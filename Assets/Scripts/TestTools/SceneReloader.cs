using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class SceneReloader : MonoBehaviour
{
    // [SerializeField] private Dia
    
    private void Start()
    {
        throw new NotImplementedException();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DialogueManager.ResetDatabase();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}