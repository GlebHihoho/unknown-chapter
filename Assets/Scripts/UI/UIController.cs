using System;
using UI.InventoryScripts;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryBackGround;

        [SerializeField] private GameObject _playerBackGround;
        [SerializeField] private GameObject _miniPlayerBackGround;

        [SerializeField] private Map map;

        [SerializeField] Journal journal;

        public static event Action OnMainMenu;

        bool isMainMenuDisabled = false;

        public static event Action<bool> OnEnableUI;


        private void Start()
        {
            GameControls.instance.OnInventory += Inventory;
            //GameControls.instance.OnCharacterTab += CharacterTab; // Temporarily disable character's window for gamedesing reasons.
            GameControls.instance.OnMap += Map;
            GameControls.instance.OnJournal += Journal;

            GameControls.instance.OnMainMenu += MainMenu;
        }



        private void OnDestroy()
        {
            GameControls.instance.OnInventory -= Inventory;
            //GameControls.instance.OnCharacterTab -= CharacterTab;
            GameControls.instance.OnMap -= Map;
            GameControls.instance.OnJournal -= Journal;

            GameControls.instance.OnMainMenu -= MainMenu;
        }

        private void Inventory()
        {
            _inventoryBackGround.SetActive(!_inventoryBackGround.activeSelf);
        }

        private void CharacterTab()
        {
            _playerBackGround.SetActive(!_playerBackGround.activeSelf);

            _miniPlayerBackGround.SetActive(!_playerBackGround.activeSelf);
        }


        private void Map()
        {
            map.ToggleMap();
        }

        private void Journal()
        {
            journal.ToggleJournal();
        }


        private void MainMenu()
        {
            if (isMainMenuDisabled) return;

            if (!_inventoryBackGround.activeSelf && !map.MapOpened && !_playerBackGround.activeSelf && !journal.JournalOpened) OnMainMenu?.Invoke();
            else HideAll();
        }

        public void DisableMainMenu() => isMainMenuDisabled = true;
        public void EnableMainMenu() => isMainMenuDisabled = false;


        public void HideAll()
        {
            _inventoryBackGround.SetActive(false);

            _playerBackGround.SetActive(false);
            //_miniPlayerBackGround.SetActive(true);

            map.HideMap();

            journal.CloseJournal();
        }


        public void DisableUI()
        {
            HideAll();
            DisableMainMenu();

            OnEnableUI?.Invoke(false);
        }

        public void EnableUI()
        {
            EnableMainMenu();
            OnEnableUI?.Invoke(true);
        }


    }
}
