using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class MiniPlayerPanelScript : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPlayerPanel;

        [FormerlySerializedAs("_mainStats")] [SerializeField] private Characteristics stats;

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
            _phys.text = stats.GetPhysicalAbilities(0).ToString();
            _per.text = stats.GetPerception(0).ToString();
            _int.text = stats.GetIntellect(0).ToString();
        }
    }
}
