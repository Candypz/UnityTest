using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {

    public static SkillUI m_instance;
    public UIGrid grid;
    public GameObject skillItemPrefab;

    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;

    private TweenPosition tween;
    private bool isShow = false;
    private PlayerStatus playerStatus;

    void Awake() {
        m_instance = this;
        tween = this.GetComponent<TweenPosition>();
    }

    void Start() {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        int[] idList = null;
        switch (playerStatus.heroType) {
            case HeroType.Magician:
                idList = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idList = swordmanSkillIdList;
                break;
        }
        foreach (int id in idList) {
            GameObject go = NGUITools.AddChild(grid.gameObject, skillItemPrefab);
            grid.AddChild(go.transform);
            go.GetComponent<SkillItem>().SetId(id);
        }
    }


    public void TransformState() {
        if (isShow) {
            tween.PlayReverse();
            isShow = false;
        }
        else {
            tween.PlayForward();
            UpdateShow();
            isShow = true;
        }
    }

    public void UpdateShow() {
        SkillItem[] items = this.GetComponentsInChildren<SkillItem>();
        foreach (SkillItem item in items) {
            item.UpdateShow(playerStatus.level);
        }
    }
}
