using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class SaveListEntry : MonoBehaviour
{

    [SerializeField] Button button;

    [SerializeField] TextMeshProUGUI numberLabel;
    [SerializeField] TextMeshProUGUI saveNameLabel;
    [SerializeField] TextMeshProUGUI timeStampLabel;

    [Space]
    [SerializeField] Image image;

    [Space]
    [SerializeField, Range(0, 1)] float doubleClick = 0.2f;
    float doubleClickTimer = 0;

    StringBuilder sb = new StringBuilder();


    string saveName;
    SaveData.Type type;


    public static event Action<string, SaveData.Type> OnSaveSelected;
    public static event Action<string> OnDoubleClick;


    private static SaveListEntry selected;

    private void Awake() => image.enabled = false;


    private void OnEnable() => button.onClick.AddListener(Select);

    private void OnDisable() => button.onClick.RemoveListener(Select);


    private void Update()
    {
        if (doubleClickTimer > 0) doubleClickTimer -= Time.deltaTime;
    }


    public void SetName(int number, string saveName, SaveManager.Summary summary)
    {
        this.saveName = saveName;
        type = summary.type;

        sb.Clear();
        sb.Append(number.ToString());
        sb.Append(".");

        numberLabel.text = sb.ToString();

        if (summary.type == SaveData.Type.Normal) saveNameLabel.text = summary.location;
        else
        {
            sb.Clear();
            sb.Append(summary.location);

            sb.Append(" (");
            if (summary.type == SaveData.Type.Quick) sb.Append(SaveManager.quickSaveLabel);
            else sb.Append(SaveManager.autoSaveLabel);
            sb.Append(")");


            saveNameLabel.text = sb.ToString();
        }


        sb.Clear();
        sb.AppendLine(summary.timeStamp.Date.ToShortDateString());
        sb.Append(summary.timeStamp.ToShortTimeString());

        timeStampLabel.text = sb.ToString();
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    public void Select()
    {

        if (selected != null) selected.Deselect();

        image.enabled = true;
        selected = this;

        OnSaveSelected?.Invoke(saveName, type);


        if (doubleClickTimer > 0)
        {
            OnDoubleClick?.Invoke(saveName);
            doubleClickTimer = 0;
        }
        else doubleClickTimer = doubleClick;
    }

    private void Deselect()
    {
        image.enabled = false;
        selected = null;
    }
}
