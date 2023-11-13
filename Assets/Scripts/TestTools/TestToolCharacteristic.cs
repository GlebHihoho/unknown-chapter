using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.TestTools
{
    public class TestToolCharacteristic : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentPhysical;
        [SerializeField] private TextMeshProUGUI _currentPerception;
        [SerializeField] private TextMeshProUGUI _currentIntellect;
        [FormerlySerializedAs("_mainCharacteristics")] [SerializeField] private Characteristics characteristics;

        private void Awake()
        {
            _currentPhysical.text = characteristics.GetSkill("PhysicalAbilities").ToString();
            _currentPerception.text = characteristics.GetSkill("Perception").ToString();
            _currentIntellect.text = characteristics.GetSkill("Intellect").ToString();
        }

        private void Update()
        {
            _currentPhysical.text = characteristics.GetSkill("PhysicalAbilities").ToString();
            _currentPerception.text = characteristics.GetSkill("Perception").ToString();
            _currentIntellect.text = characteristics.GetSkill("Intellect").ToString();
        }
    }
}