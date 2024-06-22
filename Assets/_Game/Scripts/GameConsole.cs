using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameConsole : MonoBehaviour
{

    [SerializeField] GameObject console;
    [SerializeField] TextMeshProUGUI text;

    [Space]
    [SerializeField, Range(0, 500)] int maxMessages = 200;

    [Header("Colouring")]
    [SerializeField] Color error = Color.red;
    [SerializeField] Color warning = Color.yellow;
    [SerializeField] Color log = Color.blue;
    [SerializeField] Color other = Color.cyan;

    [Space]
    [SerializeField] Color details = Color.green;

    [Space]
    [SerializeField] UnityEvent OnMessageAdded;


    public static GameConsole instance;


    Queue<string> messages = new Queue<string>();

    StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        PlayerInputActions inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.GameConsole.performed += GameConsole_performed;
    }

    private void GameConsole_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        console.SetActive(!console.activeSelf);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        console.SetActive(false);
        text.text = "";
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

        if (messages.Count > maxMessages && maxMessages > 0) messages.Dequeue();

        sb.Clear();

        foreach (string item in messages)
        {
            sb.Append(item);
            sb.Append("<br>");
        }

        text.text = sb.ToString();

        if (console.activeSelf) OnMessageAdded.Invoke();
    }


    public void AddCommand(string command) // Placeholder for testing input field and for the future use.
    {
        AddMessage(command);
    }
}
