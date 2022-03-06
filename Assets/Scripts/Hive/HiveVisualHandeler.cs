using UnityEngine;

namespace BeeClicker
{
    public class HiveVisualHandeler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _Images;
        [Header("Parts")]
        [SerializeField] private HiveParts[] _Parts;

        private void Awake()
        {
            GameManager.Instance.Hive.OnEnable += UpdateVisual;
            UpdateVisual(true);
        }

        private void UpdateVisual(bool completed)
        {
            if(!completed) return;
            for(int i = 0; i < _Images.Length; i++)
            {
                SpriteRenderer image = _Images[i];
                int random = Random.Range(0, _Images.Length);
                image.sprite = _Parts[i].Parts[random];
            }
        }

        [System.Serializable]
        private struct HiveParts
        {
            public Sprite[] Parts => _Parts;
            [SerializeField] private Sprite[] _Parts;
        }
    }
}
