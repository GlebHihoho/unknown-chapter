using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class SelectStartCharacteristics : MonoBehaviour
    {
        [SerializeField] private string _nameCharacteristic;
        [FormerlySerializedAs("_mainCharacteristics")] [SerializeField] private Characteristics characteristics;
        [SerializeField] private Button _addCharateristicButton;

        void Start()
        {
            characteristics = FindObjectOfType<Characteristics>();
        }

        public void SelectCurrentCharacteristics(string nameCharacteristic)
        {
            _nameCharacteristic = nameCharacteristic;
            _addCharateristicButton.interactable = true;
            var colorText = _addCharateristicButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            colorText.color = new Color(0.8705883f, 0.8078432f, 0.6431373f);
        }

        public void AddCharacteristic()
        {
            if (_nameCharacteristic != "")
            {
                characteristics.CharacteristicIncrease(_nameCharacteristic);
                Destroy(gameObject);
            }
        }
    }
}
