using UnityEngine;

public class UpgradeUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Image portrait;
    [SerializeField] private TMPro.TextMeshProUGUI nameText, descriptionText, priceText;
    [SerializeField, Tooltip("Replace the \"*\" with the amount")] private string descriptionFormat = "Add * dps";
    [SerializeField] private string currency = "$";
    private Upgrade upgrade;

    public void Initialize(Sprite sprite, string name, int price, int dps) {
        portrait.sprite = sprite;
        this.name = name;
        nameText.text = name;
        priceText.text = $"{price}{currency}";
        descriptionText.text = descriptionFormat.Replace("*", dps.ToString());
    }

    public void Initialize(Upgrade upgrade) {
        this.upgrade = upgrade;
        Initialize(upgrade.Sprite, upgrade.Name, upgrade.Price, upgrade.DPS);
        GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }

    private void OnClick() {
        MainGame.Instance.AddUpgrade(upgrade);
        GameObject.Destroy(gameObject);
    }
}
