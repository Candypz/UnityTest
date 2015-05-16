using UnityEngine;
using System.Collections;

public class SkillItem : MonoBehaviour {

    public int id;
    private SkillInfo info;
    private UISprite iconNameSprite;
    private UILabel nameLabel;
    private UILabel applytypeLabel;
    private UILabel desLabel;
    private UILabel mpLael;

    private GameObject iconMask;

    void InitProperty() {
        iconNameSprite = transform.Find("Icon").GetComponent<UISprite>();
        nameLabel = transform.Find("Sprite/Name").GetComponent<UILabel>();
        applytypeLabel = transform.Find("Sprite/Applytype").GetComponent<UILabel>();
        desLabel = transform.Find("Sprite/Des").GetComponent<UILabel>();
        mpLael = transform.Find("Sprite/MP").GetComponent<UILabel>();
        iconMask = transform.Find("IconMask").gameObject;
        iconMask.SetActive(false);
    }

    public void UpdateShow(int level) {
        if (info.level <= level) {//技能可用
            iconMask.SetActive(false);
            iconNameSprite.GetComponent<SkillitemIcon>().enabled = true;//可以拖拽
        }
        else {
            iconMask.SetActive(true);
            iconNameSprite.GetComponent<SkillitemIcon>().enabled = false;//不可以拖拽
        }
    }


    public void SetId(int id) {
        InitProperty();
        this.id = id;
        info = SkillsInfo.m_instance.GetSkillInfoById(id);
        iconNameSprite.spriteName = info.iconName;
        nameLabel.text = info.name;

        switch (info.applyType) {
            case ApplyType.Passive:
                applytypeLabel.text = "增益";
                break;
            case ApplyType.Buff:
                applytypeLabel.text = "增强";
                break;
            case ApplyType.SingleTarget:
                applytypeLabel.text = "单个目标";
                break;
            case ApplyType.MultiTarget:
                applytypeLabel.text = "全体技能";
                break;
        }
        desLabel.text = info.des;
        mpLael.text = info.mp + "MP";
    }
}
