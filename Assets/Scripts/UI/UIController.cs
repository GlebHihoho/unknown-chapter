using UI.InventoryScripts;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryBackGround;

        [SerializeField] private GameObject _playerBackGround;
        [SerializeField] private GameObject _miniPlayerBackGround;


        private void Start()
        {
            GameControls.instance.OnInventory += Inventory;
            GameControls.instance.OnCharacterTab += CharacterTab;
        }

        private void OnDestroy()
        {
            GameControls.instance.OnInventory -= Inventory;
            GameControls.instance.OnCharacterTab -= CharacterTab;
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


    }
}
