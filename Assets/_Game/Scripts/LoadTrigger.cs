using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LoadTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent OnSaveLoaded;


    private void OnEnable() => SaveManager.OnLoadCompleted += AfterLoad;

    private void OnDisable() => SaveManager.OnLoadCompleted -= AfterLoad;

    private void AfterLoad() => OnSaveLoaded.Invoke();

}
