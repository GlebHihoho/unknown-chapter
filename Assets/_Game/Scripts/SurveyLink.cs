using UnityEngine;

public class SurveyLink : MonoBehaviour
{

    public void OpenSurvey()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSft907KUIlsgNG-AIBzy4k-7Wp7tr2kajKHwsjfztyNB7c6aQ/viewform?usp=dialog");
    }
}
