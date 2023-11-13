using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SelectStartCharacteristics : MonoBehaviour
    {
        [SerializeField] private string _nameCharacteristic;
        [SerializeField] private MainCharacteristics _mainCharacteristics;
        [SerializeField] private Button _addCharateristicButton;

        void Start()
        {
            _mainCharacteristics = FindObjectOfType<MainCharacteristics>();
        }

        // TODO: Stat??? naming
        public void SelectCurrentStat(string nameCharacteristic)
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
                _mainCharacteristics.CharacteristicIncrease(_nameCharacteristic);
                Destroy(gameObject);
            }
        }
    }
}
