using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// TODO - затемнение ScreenFader ScreenBrightener - создать задачу на рефакторинг этих методов
// возможно сделать один класс для работы с этой механикой
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
