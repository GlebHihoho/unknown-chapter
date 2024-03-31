using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;


public class ShowLog : MonoBehaviour
{

    [SerializeField] Color error = Color.red;
    [SerializeField] Color warning = Color.yellow;
    [SerializeField] Color log = Color.blue;
    [SerializeField] Color other = Color.cyan;

    [Space]
    [SerializeField] Color details = Color.green;

    TextMeshProUGUI text;

    Queue<string> messages = new Queue<string>();

    const int maxMessages = 15;

    StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";

        text.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            text.enabled = !text.enabled;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            AddMessage("Some message");
        }
    }


    private void OnEnable() => Application.logMessageReceived += LogRecieved;
    private void OnDisable() => Application.logMessageReceived -= LogRecieved;


    private void LogRecieved(string condition, string stackTrace, LogType type)
    {

        sb.Clear();

        sb.Append("<color=#");

        if (type == LogType.Error || type == LogType.Exception) sb.Append(ColorUtility.ToHtmlStringRGB(error));
        else if (type == LogType.Warning) sb.Append(ColorUtility.ToHtmlStringRGB(warning));
        else if (type == LogType.Log) sb.Append(ColorUtility.ToHtmlStringRGB(log));
        else sb.Append(ColorUtility.ToHtmlStringRGB(other));

        sb.Append(">");
        
        sb.Append(type);

        sb.Append("</color>: ");

        sb.Append(condition);

        if (stackTrace != string.Empty)
        {
            sb.Append(" <color=#");
            sb.Append(ColorUtility.ToHtmlStringRGB(details));
            sb.Append(">details:</color> ");
            sb.Append(stackTrace);
        }

        AddMessage(sb.ToString());

    }


    private void AddMessage(string message)
    {
        messages.Enqueue(message);

        if (messages.Count > maxMessages) messages.Dequeue();

        sb.Clear();

        foreach (string item in messages)
        {
            sb.Append(item);
            sb.Append("<br>");
        }

        text.text = sb.ToString();
    }
}
