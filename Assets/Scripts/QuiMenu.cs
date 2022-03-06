using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace BeeClicker
{
    public class QuiMenu : MonoBehaviour
    {
        [SerializeField] private UnityEngine.InputSystem.InputActionReference _ToggleMenuInput;
        [SerializeField] private Button _ResumeButton;
        [SerializeField] private Button _QuitButton;
        private void Awake()
        {
            _ResumeButton.onClick.AddListener(ResumeGame);
            _QuitButton.onClick.AddListener(QuitGame);
            _ToggleMenuInput.action.performed += ToggleMenu;
            _ToggleMenuInput.action.Enable();
            gameObject.SetActive(false);
        }

        private void ToggleMenu(InputAction.CallbackContext obj)
        {
            if(gameObject.activeInHierarchy)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.volume = .2f;
        }

        private void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f;
            AudioListener.volume = 1f;
            gameObject.SetActive(false);
        }
    }
}
