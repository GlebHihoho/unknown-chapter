using UnityEngine;

namespace UI
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] ModalWindow modalWindow;

        public void ExitGame()
        {
            modalWindow.ShowPromt("", "Выйти из игры?", ExitGame);           
        }

        private void ExitGame(bool confirmed)
        {
            if (confirmed) Application.Quit();
        }
    }
}
