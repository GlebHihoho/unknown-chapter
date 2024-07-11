using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ModalWindow : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI promtLabel;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    public delegate void Result(bool isConfirmed);
    private Result result;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => gameObject.SetActive(false);


    private void OnEnable()
    {
        yesButton.onClick.AddListener(Confirm);
        noButton.onClick.AddListener(Cancel);
    }


    private void OnDisable()
    {
        yesButton.onClick.RemoveListener(Confirm);
        noButton.onClick.RemoveListener(Cancel);
    }


    public void ShowPromt(string header, string promt, Result result)
    {
        this.result = result;
        this.header.text = header;
        promtLabel.text = promt;
        gameObject.SetActive(true);
    }


    private void Confirm()
    {
        result(true);
        gameObject.SetActive(false);
    }

    private void Cancel()
    {
        result(false);
        gameObject.SetActive(false);
    }
}
