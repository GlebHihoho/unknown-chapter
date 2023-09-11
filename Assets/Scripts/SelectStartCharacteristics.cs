using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SelectStartCharacteristics : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private string _nameCharacteristic;
        [SerializeField] private MainCharacteristics _mainCharacteristics;

        public void SelectCurrentStat(string nameCharacteristic)
        {
            _nameCharacteristic = nameCharacteristic;
        }

        public void AddCharacteristic()
        {
            if (_nameCharacteristic != "")
            {
                _mainCharacteristics.CharacteristicIncrease(_nameCharacteristic);
                Destroy(gameObject);
            }
            else
            {
                print("Характеристика не выбрана");
            }
        }
    }
}