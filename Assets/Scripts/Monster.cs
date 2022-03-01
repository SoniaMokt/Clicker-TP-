using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour {
    public bool IsAlive => life > 0;
    public Canvas Canvas => canvas;

    [SerializeField] private int life;
    [SerializeField] private TMPro.TextMeshProUGUI textLife;
    [SerializeField] private Slider lifeBar;

    private Canvas canvas;
    private int maxLife;
    private Transform visual;
    private SpriteRenderer visualRenderer;

    private void Awake() {
        visual = transform.GetChild(0);
        visualRenderer = visual.GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        maxLife = life;
    }

    public void SetMonster(MonsterData data) {
        maxLife = data.Life;
        life = maxLife;
        visualRenderer.sprite = data.Sprite;
        UpdateLife(true);
    }

    public void Hit(int amount) {
        life -= amount;
        UpdateLife();
        visual.DOComplete();
        if(!IsAlive) return;
        visual.DOPunchScale(Vector2.one * .2f, .3f);
    }

    private void UpdateLife(bool fast = false) {
        textLife.text = $"{life} / {maxLife}";
        lifeBar.DOComplete();
        lifeBar.DOValue(life / (float)maxLife, .3f);
        if(fast) lifeBar.DOComplete();
    }
}
