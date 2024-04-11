using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuTrigger : MonoBehaviour
{

    public UnityEvent OnShowMenu;
    public UnityEvent OnHideMenu;

    bool isShown = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShown = !isShown;

            if (isShown) OnShowMenu.Invoke();
            else OnHideMenu.Invoke();
        }
    }
}
