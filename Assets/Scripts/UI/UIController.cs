using UI.InventoryScripts;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryBackGround;

        [SerializeField] private GameObject _playerBackGround;
        [SerializeField] private GameObject _miniPlayerBackGround;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventoryBackGround.SetActive(!_inventoryBackGround.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _playerBackGround.SetActive(!_playerBackGround.activeSelf);
            }
            
            _miniPlayerBackGround.SetActive(!_playerBackGround.activeSelf);
        }
    }
}
