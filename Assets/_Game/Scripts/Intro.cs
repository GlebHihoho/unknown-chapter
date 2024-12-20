using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{

    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI[] textFields;
    [SerializeField] TextMeshProUGUI authorField;

    [SerializeField] TextMeshProUGUI skipMessage;
    [SerializeField, Range(0, 10)] float skipTime = 3;

    float skipTimer = 0;

    public UnityEvent AfterIntro;

    [SerializeField] bool skipInEditor = true;

    UIController uiController;

    Sequence sequence;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Color color;

        foreach (TextMeshProUGUI textField in textFields)
        {
            color = textField.color;
            color.a = 0;
            textField.color = color;
        }

        color = authorField.color;
        color.a = 0;
        authorField.color = color;

        skipMessage.enabled = false;

        SaveManager.OnLoadCompleted += OnLoad;

        uiController = FindFirstObjectByType<UIController>();
        
    }


    private void Update()
    {
        if (skipTimer > 0)
        {
            skipTimer -= Time.deltaTime;

            if (skipTimer <= 0) SkipIntro();
        }
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

        Color textColor;
        textColor.a = 1;

        Color authorColor = authorField.color;
        authorColor.a = 1;

        background.gameObject.SetActive(true);

        sequence = DOTween.Sequence();

        GameControls.instance.OnSkipIntroStarted += StartSkipIntro;
        GameControls.instance.OnSkipIntroEnded += EndSkipIntro;

        const float startNext = 7;
        const float showTime = 5;
        float startTime = 0;

        for (int i = 0; i < textFields.Length; i++)
        {
            textColor = textFields[i].color;
            textColor.a = 1f;

            sequence.Insert(startTime, textFields[i].DOColor(textColor, showTime));

            sequence.Insert(30, textFields[i].DOFade(0, 6));

            startTime += startNext;
        }

       
        sequence.Insert(startTime + 0.5f, authorField.DOColor(authorColor, 2f));
       
        
        sequence.Insert(30, authorField.DOFade(0, 6));

        sequence.Insert(36, background.DOFade(0, 3));

        sequence.OnComplete(() =>
        {
            background.gameObject.SetActive(false);

            Pause.instance.SetPause(false, true);

            GameControls.instance.OnSkipIntroStarted -= StartSkipIntro;
            GameControls.instance.OnSkipIntroEnded -= EndSkipIntro;

            uiController.EnableMainMenu();

            AfterIntro.Invoke();
        });

        sequence.Play();

    }


    private void StartSkipIntro()
    {
        skipMessage.enabled = true;
        skipTimer = skipTime;
    }

    private void EndSkipIntro() => skipMessage.enabled = false;

    private void SkipIntro() => sequence.Complete();
}
