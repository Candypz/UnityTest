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
    private ObjectInfo objectinfo;
    private PlayerStatus playerStatus;

    void Awake() {
        icon = transform.Find("Icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }

    void Start() {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    void Update() {
        if (Input.GetKeyDown(keyCode)) {
            if (shortType == ShortCutType.Drug) {
                OnDrugUse();
            }
            else if (shortType == ShortCutType.Skill) {

            }
        }
    }

    public void SetSkill(int id) {//技能
        this.id = id;
        this.info = SkillsInfo.m_instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.iconName;
        shortType = ShortCutType.Skill;
    }

    public void SetInventory(int id) {
        this.id = id;
        objectinfo = ObjectsInfo.m_instance.GetObjectInfoById(id);
        if (objectinfo.type == ObjectType.Drug) {//如果是药品才可拖到
            icon.gameObject.SetActive(true);
            icon.spriteName = objectinfo.icon_name;
            shortType = ShortCutType.Drug;
        }
    }

    public void OnDrugUse() {
        bool success = Inventory.m_instance.MinusId(id, 1);
        if (success) {
            playerStatus.GetDrug(objectinfo.hp, objectinfo.mp);
        }
        else {
            shortType = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            info = null;
            objectinfo = null;
        }
    }
}
