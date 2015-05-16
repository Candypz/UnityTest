using UnityEngine;
using System.Collections;

//技能图标拖到
public class SkillitemIcon : UIDragDropItem {
    private int skillId;
    protected override void OnDragDropStart() {//在克隆的icon上调用
        skillId = transform.parent.GetComponent<SkillItem>().id;
        base.OnDragDropStart();
        transform.parent = transform.root;
        this.GetComponent<UISprite>().depth = 200;
    }

    protected override void OnDragDropRelease(GameObject surface) {
        base.OnDragDropRelease(surface);
        if (surface != null && surface.tag == Tags.shortCut) {//当一个技能拖到了快捷方式上
            surface.GetComponent<ShortCut>().SetSkill(skillId);
        }
    }

}
