using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{

    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] TextMeshProUGUI authorField;

    public UnityEvent AfterIntro;

    [SerializeField] bool skipInEditor = true;

    UIController uiController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Color color;

        color = textField.color;
        color.a = 0;
        textField.color = color;

        color = authorField.color;
        color.a = 0;
        authorField.color = color;

        SaveManager.OnLoadCompleted += OnLoad;

        uiController = FindFirstObjectByType<UIController>();
        
    }


    private void OnLoad()
    {
        SaveManager.OnLoadCompleted -= OnLoad;

        if (!SaveManager.instance.IsNewGame) background.gameObject.SetActive(false);
    }


    public void PlayIntro()
    {

        if (Application.isEditor && skipInEditor)
        {
            background.gameObject.SetActive(false);
            return;
        }

        Pause.instance.SetPause(true, true);
        uiController.DisableMainMenu();

        Color textColor = textField.color;
        textColor.a = 1;

        Color authorColor = authorField.color;
        authorColor.a = 1;

        background.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(textField.DOColor(textColor, 8f));
        sequence.Append(authorField.DOColor(authorColor, 2f));
       
        sequence.Insert(30, textField.DOFade(0, 6));
        sequence.Insert(30, authorField.DOFade(0, 6));

        sequence.Insert(36, background.DOFade(0, 3));

        sequence.InsertCallback(40, () =>
        {
            background.gameObject.SetActive(false);

            Pause.instance.SetPause(false, true);
            uiController.EnableMainMenu();

            AfterIntro.Invoke();
        });

        sequence.Play();



    }
}
