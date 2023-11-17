using TMPro;
using UnityEngine;

namespace DefaultNamespace.TestTools
{
    public class Printing : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _countText;

        private void Update()
        {
            print(_countText.text);
        }
    }
}