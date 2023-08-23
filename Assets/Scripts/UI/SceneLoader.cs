using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
    [FormerlySerializedAs("sceneName")] [SerializeField] private string _sceneName;

    public void Load()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}