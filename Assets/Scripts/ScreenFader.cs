using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ScreenFader : MonoBehaviour

    {
        [SerializeField] private float _fadeSpeed = 1;
        [SerializeField] private float _fadeDuration = 2; // Длительность затемнения
        [SerializeField] private GameObject[] deathBodyPrefabs; // префабы всех мертвых тел
        [SerializeField] private GameObject burnedBodyPrefab; // префаб сожженных тел
        [SerializeField] private GameObject buriedBodyPrefab; // префаб захороненных тел

        public bool Burning;

        private IEnumerator Start()
        {
            Image fadeImage = GetComponent<Image>();
            Color color = fadeImage.color;

            // Первая часть: Затемнение
            while (color.a < 1f)
            {
                color.a += _fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;

                foreach (var destroyBody in deathBodyPrefabs)
                {
                    Destroy(destroyBody);
                }


            }

            print("Затемнение");
            

            // Задержка перед началом второй корутины
            
            yield return new WaitForSeconds(_fadeDuration);


            (Burning ? buriedBodyPrefab : burnedBodyPrefab).SetActive(true);
            // if (Burning)
            // {
            //     buriedBodyPrefab.SetActive(true);
            // }
            // else
            // {
            //     burnedBodyPrefab.SetActive(true);
            // }

            // Вторая часть: Раззатемнение
            while (color.a > 0)
            {
                color.a -= _fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }

            print("Раззатемнение");
            gameObject.SetActive(false);
        }

        private void BurningBody()
        {
            burnedBodyPrefab.SetActive(true);
        }
        
        private void BurialOfBody()
        {
            buriedBodyPrefab.SetActive(true);
        }

        //Метод называется не корректно, когда Is - ожидается что он вернет какое-то значение а не установит его
        public void IsBurning(int burning)
        {
            //меняется в одну строку
            Burning = burning == 1;
        }
        
    }
}