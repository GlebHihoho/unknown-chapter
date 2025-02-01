using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{

    [SerializeField] Image background;


    [SerializeField] Image slide1;
    [SerializeField] Image slide2;


    [SerializeField] Sprite[] slides;
    int slideIndex = 0;

    [SerializeField] TextMeshProUGUI[] textFields;
    [SerializeField] TextMeshProUGUI authorField;

    [SerializeField] GameObject skipPanel;
    [SerializeField] Image timerImage;

    [SerializeField, Range(0, 10)] float skipTime = 3;

    bool timerActive = false;
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

        skipPanel.SetActive(false);

        SaveManager.OnLoadCompleted += OnLoad;

        uiController = FindFirstObjectByType<UIController>();
        
    }


    private void Update()
    {
        if (timerActive)
        {
            skipTimer -= Time.deltaTime;

            timerImage.fillAmount = Mathf.InverseLerp(skipTime, 0, skipTimer);

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

        const float startNext = 8.5f; //7
        const float showTime = 6; //5
        float startTime = 0;

        for (int i = 0; i < textFields.Length; i++)
        {
            textColor = textFields[i].color;
            textColor.a = 1f;

            sequence.InsertCallback(startTime, ChangeSlides);
            sequence.Insert(startTime + 0.5f, textFields[i].DOColor(textColor, showTime));
            
            sequence.Insert(38, textFields[i].DOFade(0, 6));

            startTime += startNext;
        }

       
        sequence.Insert(startTime + 0.5f, authorField.DOColor(authorColor, 2f));
       
        
        sequence.Insert(38, authorField.DOFade(0, 6));


        sequence.Insert(40, slide1.DOFade(0, 3));

        sequence.Insert(40, background.DOFade(0, 3));

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


    private void ChangeSlides()
    {

        const float fadeDuration = 1.2f;

        if (slideIndex > slides.Length - 1) return;

        if (slideIndex == 0)
        {
            slide1.sprite = slides[slideIndex];
            slide1.DOFade(1, fadeDuration);
        }
        else
        {
            slide2.sprite = slides[slideIndex];
            slide2.DOFade(1, fadeDuration).OnComplete(() =>
            {
                slide1.sprite = slide2.sprite;

                Color color = slide2.color;
                color.a = 0;
                slide2.color = color;

            });
        }

        slideIndex++;
    }


    private void StartSkipIntro()
    {
        skipPanel.SetActive(true);
        skipTimer = skipTime;

        timerActive = true;
    }

    private void EndSkipIntro()
    {
        skipPanel.SetActive(false);
        timerActive = false;       
    }

    private void SkipIntro() => sequence.Complete();
}
