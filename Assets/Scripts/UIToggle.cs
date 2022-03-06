using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BeeClicker
{
    [RequireComponent(typeof(Image))]
    public class UIToggle : MonoBehaviour
    {
        [SerializeField] private AudioMixer _Mixer;
        [SerializeField] private string _FloatName;
        [SerializeField] private Sprite _SpriteON;
        [SerializeField] private Sprite _SpriteOFF;
        private bool _Toggle = true;
        private Image _Image;
        private Button _Button;

        private void Awake()
        {
            _Image = GetComponent<Image>();
            _Button = GetComponent<Button>();
            _Button.onClick.AddListener(ToggleSprite);
        }

        private void ToggleSprite()
        {
            _Toggle = !_Toggle;
            _Image.sprite = _Toggle ? _SpriteON : _SpriteOFF;
            _Mixer.SetFloat(_FloatName, _Toggle ? 0.0f : -80f);
        }
    }
}
