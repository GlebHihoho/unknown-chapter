using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class MiniPlayerPanelScript : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPlayerPanel;


        [SerializeField] private TextMeshProUGUI _phys;
        [SerializeField] private TextMeshProUGUI _per;
        [SerializeField] private TextMeshProUGUI _int;


        private void Start() => CheckStats();

        private void OnEnable() => Characteristics.OnStatsChanged += CheckStats;
        private void OnDisable() => Characteristics.OnStatsChanged -= CheckStats;

        private void CheckStats()
        {
            if (Characteristics.instance != null)
            {
                _phys.text = Characteristics.instance.PhysicalAbilities.ToString();
                _per.text = Characteristics.instance.Perception.ToString();
                _int.text = Characteristics.instance.Intellect.ToString();
            }
        }
    }
}
