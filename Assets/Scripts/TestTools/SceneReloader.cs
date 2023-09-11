using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // Имя сцены, которую вы хотите перезагрузить
    [SerializeField] private string _startSceneToReload;

    // Вызывается при нажатии на кнопку
    public void ReloadStartScene()
    {
        SceneManager.LoadScene(_startSceneToReload);
    }
}