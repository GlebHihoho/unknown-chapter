using UI.InventoryScripts;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryBackGround;

        [SerializeField] private GameObject _playerBackGround;
        [SerializeField] private GameObject _miniPlayerBackGround;

        private void Awake()
        {
            PlayerInputActions inputActions = new PlayerInputActions();
            inputActions.Player.Enable();
            inputActions.Player.Inventory.performed += Inventory;
            inputActions.Player.CharacterTab.performed += CharacterTab;
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
