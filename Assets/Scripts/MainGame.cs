using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGame : Etienne.Singleton<MainGame> {

    [SerializeField] private InputActionReference mouseClick;
    [SerializeField] private Hive hive;
    [SerializeField] private GameObject prefabHitPoint;
    [SerializeField] private GameObject prefabUpgradeUI;
    [SerializeField] private List<MonsterData> monsters = new List<MonsterData>();
    [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();

    private Camera mainCamera;
    private int currentMonsterIndex;
    private Transform parentUpgrade;
    private List<Upgrade> unlockedUpgrades = new List<Upgrade>();

    protected override void Awake() {
        base.Awake();
        mainCamera = Camera.main;
        mouseClick.action.performed += MouseClick;
        //parentUpgrade = prefabUpgradeUI.transform.parent;
    }

    private void Start() {
        currentMonsterIndex = 0;
        //hive.SetMonster(monsters[currentMonsterIndex]);

        //prefabUpgradeUI.SetActive(false);
        //foreach(Upgrade upgrade in upgrades) {
        //    GameObject go = GameObject.Instantiate(prefabUpgradeUI, parentUpgrade, false);
        //    go.SetActive(true);
        //    go.GetComponent<UpgradeUI>().Initialize(upgrade);
        //}

    }

    public void AddUpgrade(Upgrade upgrade) {
        lock(unlockedUpgrades) {
            unlockedUpgrades.Add(upgrade);
        }
    }

    private async void DoDPS() {
        while(enabled) {
            await Task.Delay(1000);
            if(!Application.isPlaying || !enabled) return;

            lock(unlockedUpgrades) {
                foreach(Upgrade upgrade in unlockedUpgrades) {
                    HitMonster(hive.transform.position, upgrade.DPS);
                }
            }
        }
    }

    public void NextMonster() {
        //hive.SetMonster(monsters[++currentMonsterIndex]);
    }

    private void MouseClick(InputAction.CallbackContext obj) {
        Vector3 world = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
        if(hit.collider is null || !hit.collider.TryGetComponent(out Hive _)) return;
        HitMonster(hit.point, 1);
    }

    private void HitMonster(Vector2 position, int damage) {
       // hive.Hit(damage);
        SpawnHitPointDamage(position, hive, damage);
        //if(!hive.IsAlive) NextMonster();
    }

    private void SpawnHitPointDamage(Vector2 position, Hive hive, int amount) {
        GameObject go = GameObject.Instantiate(prefabHitPoint, position + Random.insideUnitCircle, Quaternion.identity, hive.Canvas.transform);
        go.transform.DOLocalMoveY(go.transform.localPosition.y + .5f, .8f);
        TMPro.TextMeshProUGUI text = go.GetComponent<TMPro.TextMeshProUGUI>();
        text.text = amount.ToString();
        text.DOFade(0, .8f).OnComplete(() => GameObject.Destroy(go));
    }

    private void OnEnable() {
        DoDPS();
        mouseClick.action.Enable();
    }
    private void OnDisable() {
        mouseClick.action.Disable();
    }
}
