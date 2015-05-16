using UnityEngine;
using System.Collections;

public class EquipItem : MonoBehaviour {

    private UISprite sprite;
    public int id;
    private bool isHover = false;

    void Awake() {
        sprite = this.GetComponent<UISprite>();
    }

    public void SetId(int id) {
        this.id = id;
        ObjectInfo info = ObjectsInfo.m_instance.GetObjectInfoById(id);
        SetInfo(info);
    }

    void Update() {
        if (isHover) {//当鼠标在装备栏上，检测鼠标右键的点击
            InfoName.m_instance.Show(id);
            Debug.Log("aa");
            if (Input.GetMouseButtonDown(1)) {//鼠标右键点击，表示卸下
                EquipmentUi.m_instance.TakeOff(id,this.gameObject);

            }
        }
    }

    public void SetInfo(ObjectInfo info) {
        this.id = info.id;
        sprite.spriteName = info.icon_name;
    }

    public void OnHover(bool isOver) {
        isHover = isOver;
    }
}
