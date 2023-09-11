using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.TestTools
{
    public class TestToolCharacteristic : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentPhysical;
        [SerializeField] private TextMeshProUGUI _currentPerception;
        [SerializeField] private TextMeshProUGUI _currentIntellect;
        [SerializeField] private MainCharacteristics _mainCharacteristics;

        private void Awake()
        {
            _currentPhysical.text = _mainCharacteristics.GetSkill("PhysicalAbilities").ToString();
            _currentPerception.text = _mainCharacteristics.GetSkill("Perception").ToString();
            _currentIntellect.text = _mainCharacteristics.GetSkill("Intellect").ToString();
        }

        private void Update()
        {
            _currentPhysical.text = _mainCharacteristics.GetSkill("PhysicalAbilities").ToString();
            _currentPerception.text = _mainCharacteristics.GetSkill("Perception").ToString();
            _currentIntellect.text = _mainCharacteristics.GetSkill("Intellect").ToString();
        }
    }
}