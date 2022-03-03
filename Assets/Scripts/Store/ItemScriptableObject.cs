using Etienne;
using UnityEngine;

namespace BeeClicker.Store
{
    [CreateAssetMenu(fileName = "StoreItem", menuName = "Beeclicker/Store/Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string Name => _Name;
        public string Description => _Description;
        public AnimationCurve Prices => _Prices;
        public Sprite Icon => _Icon;
        public int[] Upgrades => _Upgrades;

        [SerializeField] private string _Name = "Name";
        [SerializeField] private string _Description = "Description";
        [SerializeField] private AnimationCurve _Prices = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 0), new Keyframe(300, 3000000) });
        [SerializeField, PreviewSprite] private Sprite _Icon;
        [Space(10)]
        [SerializeField, ReadOnly] private int[] _Upgrades;
    }
}
