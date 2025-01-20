using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
    [FormerlySerializedAs("sceneName")] [SerializeField] private string _sceneName;

    public void Load()
    {
        LoadingScreen.instance.Load(_sceneName);
    }

}