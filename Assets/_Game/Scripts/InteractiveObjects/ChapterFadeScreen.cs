using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ChapterFadeScreen : MonoBehaviour
{

    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI label;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color color;

        color = background.color;
        color.a = 0;
        background.color = color;

        color = label.color;
        color.a = 0;
        label.color = color;

        background.enabled = false;
        label.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndChapter()
    {
        Pause.instance.SetPause(true);
       
        background.enabled = true;
        label.enabled = true;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(background.DOFade(1, 5));
        sequence.AppendInterval(0.5f);
        sequence.Append(label.DOFade(1, 10));

        sequence.AppendInterval(5);
        sequence.AppendCallback(LoadMainMenu);

        sequence.Play();


    }

    private void LoadMainMenu()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSft907KUIlsgNG-AIBzy4k-7Wp7tr2kajKHwsjfztyNB7c6aQ/viewform?usp=dialog");
        SceneManager.LoadScene(0);
    }
}
