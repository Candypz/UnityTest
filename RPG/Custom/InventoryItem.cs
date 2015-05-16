using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {
    public int id = 0;
    public int num = 0;
    private ObjectInfo info = null;
    public UILabel numLable;


    void Start() {
        numLable = this.GetComponentInChildren<UILabel>();
    }

    public void PlusNumber(int num = 1) {
        this.num += num;
        numLable.text = this.num.ToString();
    }

    //减去数量
    public bool MinusNumber(int num = 1) {
        if (this.num >= num) {
            this.num -= num;
            if (this.num == 0) {
                ClearInfo();//清空
                GameObject.Destroy(this.GetComponentInChildren<InventoryItemGrid>().gameObject);//删除
            }
            numLable.text = this.num.ToString();
            return true;
        }
        return false;
    }

    public void SetId(int id, int num = 1) {
        this.id = id;
        info = ObjectsInfo.m_instance.GetObjectInfoById(id);
        InventoryItemGrid item = this.GetComponentInChildren<InventoryItemGrid>();
        item.SetIconName(info.id, info.icon_name);
        numLable.enabled = true;
        this.num = num;
        numLable.text = num.ToString();
    }

    //清空格子存的物品信息
    public void ClearInfo() {
        id = 0;
        info = null;
        num = 0;
        numLable.enabled = false;//组件是否显示
    }

}
