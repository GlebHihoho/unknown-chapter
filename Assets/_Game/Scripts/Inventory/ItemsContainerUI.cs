using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemsContainerUI : MonoBehaviour
{

    [SerializeField] Button takeAll;
    [SerializeField] Button close;

    [SerializeField] GameObject containerPanel;
    [SerializeField] Image[] items;

    Action ItemsTaken;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemsContainer.OnOpenContainer += OpenContainer;

        containerPanel.SetActive(false);

        takeAll.onClick.AddListener(TakeAll);
        close.onClick.AddListener(CloseContainer);
    }


    private void OnDestroy()
    {
        ItemsContainer.OnOpenContainer -= OpenContainer;

        takeAll.onClick.RemoveListener(TakeAll);
        close.onClick.RemoveListener(CloseContainer);
    }



    private void OpenContainer(ItemData[] items, Action callback)
    {
        ResetItems();

        for (int i = 0; i < items.Length; i++)
        {
            if (i >= this.items.Length) break;

            this.items[i].enabled = true;
            this.items[i].sprite = items[i].Icon;

        }


        containerPanel.SetActive(true);

        ItemsTaken = callback;
    }


    private void CloseContainer()
    {
        containerPanel.SetActive(false);
    }


    private void TakeAll()
    {
        CloseContainer();
        ItemsTaken();
    }


    private void ResetItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].enabled = false;
        }
    }


}
