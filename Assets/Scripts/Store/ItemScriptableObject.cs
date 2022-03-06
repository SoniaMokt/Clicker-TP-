using Etienne;
using UnityEngine;

namespace BeeClicker.Store
{
    [System.Flags]
    public enum DamageType { None, ClickDamage, DPS, HPS = 4 }
    [CreateAssetMenu(fileName = "StoreItem", menuName = "Beeclicker/Store/Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string Name => _Name;
        public string Description => _Description;
        public AnimationCurve Prices => _Prices;
        public AnimationCurve Gain => _Gain;
        public Sprite Icon => _Icon;
        public int[] Upgrades => _Upgrades;
        public DamageType DamageType => _DamageType;

        [SerializeField] private string _Name = "Name";
        [SerializeField] private string _Description = "Description";
        [SerializeField] private AnimationCurve _Prices = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 0), new Keyframe(300, 3000000) });
        [SerializeField] private AnimationCurve _Gain = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1), new Keyframe(300, 3000000) });
        [SerializeField] private DamageType _DamageType;
        [SerializeField, PreviewSprite] private Sprite _Icon;
        [Space(10)]
        [SerializeField, ReadOnly] private int[] _Upgrades;
    }
}
