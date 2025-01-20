using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatBlocksScript : MonoBehaviour
    {
        [SerializeField] private GameObject _mainStatObject;
        [SerializeField] private GameObject _gameObjectTrue;
        [SerializeField] private GameObject _gameObjectFalse;

        private int _stat = 0;
        [SerializeField] private TextMeshProUGUI _actualStats;
        [SerializeField] private TextMeshProUGUI _info;

        void Start()
        {
            _stat = Convert.ToInt32(_actualStats.text);
            _info.text = _stat + "/5";
            UpdateStats();
        }
        
        private void UpdateStats()
        {
            foreach (Transform child in _mainStatObject.transform)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(i < _stat ? _gameObjectTrue : _gameObjectFalse, _mainStatObject.transform);
            }
        }
        
        void Update()
        {
            if (_stat != Convert.ToInt32(_actualStats.text))
            {
                _stat = Convert.ToInt32(_actualStats.text);
                _info.text = _stat + "/5";
                UpdateStats();
            }
        }

    }
}
