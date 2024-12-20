using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{

    [SerializeField] string sceneName;
    

    public void LoadMenu()
    {
        SceneManager.LoadScene(sceneName);
    }

}
