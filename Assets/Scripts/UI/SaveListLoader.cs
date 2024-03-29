using System;
using DefaultNamespace.SaveLoadData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SaveListLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _saveButtonPrefab;
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private SaveManager_old _saveManager;
        [SerializeField] private string _currentSaveFile;

        private void OnEnable()
        {
            foreach (var file in _saveManager.AllSaveName)
            {
                var obj = Instantiate(_saveButtonPrefab, _contentContainer);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = file;
                obj.name = file;
                // Добавление обработчика нажатия на кнопку
                obj.GetComponent<Button>().onClick.AddListener(() => OnSaveButtonClick(file));
            }
        }
        
        private void OnSaveButtonClick(string saveName)
        {
            // Вызывайте метод SaveManager.Load и передавайте имя сохранения
            _currentSaveFile = saveName;
        }

        public void LoadSaveFile()
        {
            if (_currentSaveFile != "")
            {
                SaveManager_old.Instance.LoadGameData(_currentSaveFile);
            }
            else
            {
                print("Вы не выбрали сохранение");
            }
        }
    }
}