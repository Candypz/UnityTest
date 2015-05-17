using UnityEngine;
using System.Collections;

public class InventoryItemGrid : UIDragDropItem {
    private UISprite sprite;
    private bool isHover = false;
    private int id;

    void Awake() {
        sprite = this.GetComponent<UISprite>();
    }

    void Update() {
        if (isHover) {
            //显示提示信息
            InfoName.m_instance.Show(id);
            if (Input.GetMouseButtonDown(1)) {//卸下
                //处理穿戴功能
                bool success = EquipmentUi.m_instance.Dress(id);
                if (success) {
                    transform.parent.GetComponent<InventoryItem>().MinusNumber();
                }
            }
        }
    }

    protected override void OnDragDropRelease(GameObject surface) {
        base.OnDragDropRelease(surface);
        if (surface != null) {
            if (surface.tag == Tags.inventoryItem) {//当拖放到一个空的盒子
                if (surface == this.transform.parent.gameObject) {//拖放到自己的格子
                    ResetPosition();
                }
                else {
                    InventoryItem oldParent = this.transform.parent.GetComponent<InventoryItem>();

                    this.transform.parent = surface.transform;
                    ResetPosition();
                    InventoryItem newParent = surface.GetComponent<InventoryItem>();
                    newParent.SetId(oldParent.id, oldParent.num);

                    oldParent.ClearInfo();

                }
            }
            else if (surface.tag == Tags.inventoryItemGrid) {//当拖放到一个空的盒子有物体的格子
                ResetPosition();
                InventoryItem grid1 = this.transform.parent.GetComponent<InventoryItem>();
                InventoryItem grid2 = surface.transform.parent.GetComponent<InventoryItem>();
                int id = grid1.id; int num = grid1.num;
                grid1.SetId(grid2.id, grid2.num);
                grid2.SetId(id, num);
            }
            else if(surface.tag==Tags.shortCut){//拖到了快捷方式里面
                surface.GetComponent<ShortCut>().SetInventory(id);
            }
            else {//拖到inventory上
                ResetPosition();
            }
        }
        else {
            ResetPosition();
        }

    }

    void ResetPosition() {
        transform.localPosition = Vector3.zero;
    }

    public void SetId(int id) {
        ObjectInfo info = ObjectsInfo.m_instance.GetObjectInfoById(id);
        sprite.spriteName = info.icon_name;
    }

    public void SetIconName(int id, string icon_name) {
        sprite.spriteName = icon_name;
        this.id = id;
    }

    public void OnHoverOver() {//鼠标移上
        isHover = true;
    }

    public void OnHoverOut() {//鼠标移出
        isHover = false;
    }


}
