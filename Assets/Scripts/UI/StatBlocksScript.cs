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

        // Start is called before the first frame update
        void Start()
        {
            UpdateStats();
        }

        private void UpdateStats()
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(i < _stat ? _gameObjectTrue : _gameObjectFalse, _mainStatObject.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {
            _stat = Convert.ToInt32(_actualStats.text);
            _info.text = _stat + "/5";
            //UpdateStats();
        }
    }
}
