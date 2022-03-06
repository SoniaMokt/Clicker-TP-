using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Image _Tree;
        [SerializeField] private Sprite[] _TreeSprites;
        [SerializeField] private Sprite[] _Sprites;
        private Image _Renderer;
        private void Awake()
        {
            GameManager.Instance.Hive.OnEnable += ChangeBackground;
            GameManager.Instance.Hive.OnCompleted += HideTree;
            _Renderer = GetComponent<Image>();
            ChangeBackground(true);
        }

        private void HideTree(int _, bool __)
        {
            _Tree.gameObject.SetActive(false);
        }

        private void ChangeBackground(bool completed)
        {
            _Tree.gameObject.SetActive(true);
            if(!completed) return;
            _Renderer.sprite = _Sprites[Random.Range(0, _Sprites.Length)];
            _Tree.sprite = _TreeSprites[Random.Range(0, _TreeSprites.Length)];
        }
    }
}
