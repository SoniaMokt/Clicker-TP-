using BeeClicker.Store;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BeeClicker
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : Etienne.Singleton<GameManager>
    {
        public Hive Hive => _Hive;
        public StatsHandeler StatsHandeler => _StatsHandeler;

        [SerializeField] private InputActionReference _MouseClick;
        [SerializeField] private Hive _Hive;
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

        public bool Buy(int price)
        {
            return StatsHandeler.Pay(price);
        }

        public void IncreaseDamage(ItemScriptableObject item, int amount)
        {
            StatsHandeler.IncreaseDamage(item, amount);
        }
    }
}
