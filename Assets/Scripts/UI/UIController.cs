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


        private void Start()
        {
            GameControls.instance.OnInventory += Inventory;
            GameControls.instance.OnCharacterTab += CharacterTab;
            GameControls.instance.OnMap += Map;

            GameControls.instance.OnMainMenu += HideAll;
        }



        private void OnDestroy()
        {
            GameControls.instance.OnInventory -= Inventory;
            GameControls.instance.OnCharacterTab -= CharacterTab;
            GameControls.instance.OnMap -= Map;

            GameControls.instance.OnMainMenu -= HideAll;
        }

        private void Inventory(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _inventoryBackGround.SetActive(!_inventoryBackGround.activeSelf);
        }

        private void CharacterTab(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _playerBackGround.SetActive(!_playerBackGround.activeSelf);

            _miniPlayerBackGround.SetActive(!_playerBackGround.activeSelf);
        }


        private void Map(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            map.ToggleMap();
        }


        private void HideAll(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _inventoryBackGround.SetActive(false);

            _playerBackGround.SetActive(false);
            _miniPlayerBackGround.SetActive(true);

            map.HideMap();
        }


    }
}
