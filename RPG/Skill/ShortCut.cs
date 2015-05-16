using UnityEngine;
using System.Collections;

public enum ShortCutType {//技能图标类型
    Skill,
    Drug,
    None
}

public class ShortCut : MonoBehaviour {

    public KeyCode keyCode;
    private UISprite icon;
    private ShortCutType shortType = ShortCutType.None;
    private int id;
    private SkillInfo info;

    void Awake() {
        icon = transform.Find("Icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }

    public void SetSkill(int id) {//技能
        this.id = id;
        this.info = SkillsInfo.m_instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.iconName;
        shortType = ShortCutType.Skill;
    }

    public void SetInventory(int id) {

    }

    void Update() {

    }
}
