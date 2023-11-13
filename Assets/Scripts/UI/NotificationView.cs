using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NotificationView : MonoBehaviour
    {
        [SerializeField] private GameObject _statIncreasePrefab;
        [SerializeField] private TextMeshProUGUI _statIncreaseName;

        public void StatIncrease(string statName)
        {
            switch (statName)
            {
                case "PhysicalAbilities": ShowStatIncreaseMessage("Телосложение"); break;
                case "Perception": ShowStatIncreaseMessage("Восприятие"); break;
                case "Intellect": ShowStatIncreaseMessage("Интеллект"); break;
            }
        }

        private void ShowStatIncreaseMessage(string statName)
        {
            var pos = _statIncreasePrefab.transform.position;
            _statIncreasePrefab.SetActive(true);
            _statIncreaseName.text = $"Ваша характеристика \"{statName}\" повышена";
            
            var image = _statIncreasePrefab.GetComponentInChildren<Image>();
            var text = _statIncreasePrefab.GetComponentInChildren<TextMeshProUGUI>();
            
            // Поднимаем объект вверх по оси Y
            _statIncreasePrefab.transform.DOMoveY(_statIncreasePrefab.transform.position.y + 20f, 2f);
            
            // Прозрачность увеличивается до 1 за 2 секунды
            image.DOFade(1, 1.5f); 
            text.DOFade(1, 1.5f);
            
            _statIncreasePrefab.transform.DOMoveY(_statIncreasePrefab.transform.position.y, 2f).SetDelay(3f);
            text.DOFade(0, 1f).SetDelay(3f);
            image.DOFade(0, 1f).SetDelay(3f).OnComplete(() => _statIncreasePrefab.SetActive(false));
        }
    }
}
