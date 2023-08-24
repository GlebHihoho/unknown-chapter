using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ScreenBrightener : MonoBehaviour
    {
        [SerializeField] private float _fadeSpeed = 1;

        
        
        private IEnumerator Start()
        {
            
            Image fadeImage = GetComponent<Image>();
            Color color = fadeImage.color;
            
            while (color.a > 0)
            {
                color.a -= _fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }

            print("Раззатемнение");
            gameObject.SetActive(false);
        }
    }
}