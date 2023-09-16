using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour

{
    [SerializeField] private float _fadeSpeed = 1;
    [SerializeField] private float _fadeDuration = 2; // Длительность затемнения

    public void StartCoroutine(float fadeDuration)
    {
        StartCoroutine("Awader", fadeDuration);
    }

    private IEnumerator Awader(float fadeDuration)
    {
        Image fadeImage = GetComponent<Image>();
        Color color = fadeImage.color;

        // Первая часть: Затемнение
        while (color.a < 1f)
        {
            color.a += _fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
        
        // Задержка перед началом второй корутины
        yield return new WaitForSeconds(fadeDuration);

        // Вторая часть: Раззатемнение
        while (color.a > 0)
        {
            color.a -= _fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
//todo переименовать метод
    public void ScreenBrightener(GameObject brightenerObject)
    {
        StartCoroutine("ScreenBrightenerStart", brightenerObject);
    }

    private IEnumerator ScreenBrightenerStart(GameObject brightenerObject)
    {
        
        
        if (brightenerObject.GetComponent<Image>())
        {
            Image fadeImage = brightenerObject.GetComponent<Image>();
            Color color = fadeImage.color;
            while (color.a < 1f)
            {
                color.a += _fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }
        else
        {
            TextMeshProUGUI fadeImage = brightenerObject.GetComponent<TextMeshProUGUI>();
            Color color = fadeImage.color;
            while (color.a < 1f)
            {
                color.a += _fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }

        

        gameObject.SetActive(false);

    }
    
//todo переименовать метод
    public void ScreenDarkener(GameObject darkerObject)
    {
        StartCoroutine("ScreenDarkenerStart", darkerObject);
    }

    private IEnumerator ScreenDarkenerStart(GameObject darkerObject)
    {
        Image fadeImage = darkerObject.GetComponent<Image>();
        Color color = fadeImage.color;
            
        while (color.a < 1f)
        {
            color.a += _fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
