using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour

{
    [SerializeField] private float _fadeSpeed = 1;
    [SerializeField] private float _fadeDuration = 2; // Длительность затемнения
    [SerializeField] private GameObject[] deathBodyPrefabs; // префабы всех мертвых тел
    [SerializeField] private GameObject burnedBodyPrefab; // префаб сожженных тел
    [SerializeField] private GameObject buriedBodyPrefab; // префаб захороненных тел
    [SerializeField] private GameObject gravePrefab; //префаб могилы

    public bool Burning;

    public void StartCoroutine()
    {
        Awader();
    }

    private IEnumerator Awader()
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

    public void IsBurning(int burning)
    {
        if (burning == 1)
        {
            Burning = true;
        }
        else
        {
            Burning = false;
        }
    }

    private void Body()
    {
        gravePrefab.SetActive(false);
        
        if (Burning)
        {
            buriedBodyPrefab.SetActive(true);
        }
        else
        {
            burnedBodyPrefab.SetActive(true);
        }
    }
}
