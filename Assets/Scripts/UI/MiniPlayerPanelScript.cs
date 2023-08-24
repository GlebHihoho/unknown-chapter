using TMPro;
using UnityEngine;

namespace UI
{
    public class MiniPlayerPanelScript : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPlayerPanel;

        [SerializeField] private MainCharacteristics _mainStats;

        [SerializeField] private TextMeshProUGUI _phys;
        [SerializeField] private TextMeshProUGUI _per;
        [SerializeField] private TextMeshProUGUI _int;

        // Update is called once per frame
        void Update()
        {
            CheckStats();
        }

        private void CheckStats()
        {
            _phys.text = _mainStats.GetPhysicalAbilities().ToString();
            _per.text = _mainStats.GetPerception().ToString();
            _int.text = _mainStats.GetIntellect().ToString();
        }
    }
}
