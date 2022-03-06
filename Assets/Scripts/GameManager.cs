using BeeClicker.Store;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BeeClicker
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : Etienne.Singleton<GameManager>
    {
        public System.Action<string, int> OnBeePurchased;

        public Hive Hive => _Hive;
        public Monster.Monster Monster => _Monster;
        public StatsHandeler StatsHandeler => _StatsHandeler;

        [SerializeField] private InputActionReference _MouseClick;
        [SerializeField] private Hive _Hive;
        [SerializeField] private Monster.Monster _Monster;
        [SerializeField] private StatsHandeler _StatsHandeler;

        private Camera _MainCamera;

        protected override void Awake()
        {
            base.Awake();
            _MainCamera = Camera.main;
            _MouseClick.action.performed += MouseClick;
        }

        private void MouseClick(InputAction.CallbackContext obj)
        {
            Vector3 mouseWorldPosition = _MainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
            if(hit.collider is null || !hit.collider.TryGetComponent(out IClickable clickable)) return;
            clickable.Click();
        }

        private void OnEnable()
        {
            _MouseClick.action.Enable();
        }

        private void OnDisable()
        {
            _MouseClick.action.Disable();
        }

        public void GainCurrency(int amount)
        {
            StatsHandeler.GainCurrency(amount);
        }

        public bool TryToBuy(int price)
        {
            return StatsHandeler.TryToPay(price);
        }

        public void IncreaseDamage(ItemScriptableObject item, int amount)
        {
            StatsHandeler.IncreaseDamage(item, amount);
        }

        public void BuyBee(string bee, int amount)
        {
            OnBeePurchased?.Invoke(bee, amount);
        }
    }
}
