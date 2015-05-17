using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public static Inventory m_instance;
    public List<InventoryItem> inventoryItem = new List<InventoryItem>();
    public UILabel coinNumberLabel;
    public GameObject inventoryItemPrefabs;
    private TweenPosition tween;
    private int coin = 1000;
    private bool isShow = false;

    void Awake() {
        m_instance = this;
        tween = this.GetComponent<TweenPosition>();
        //tween.AddOnFinished(this.OnTweenPlayFinished);
        //this.gameObject.SetActive(false);
    }

    void Show() {
        this.gameObject.SetActive(true);
        tween.PlayForward();
        isShow = true;
    }

    void Hide() {
        tween.PlayReverse();
        //this.gameObject.SetActive(false);
        isShow = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            GetId(Random.Range(2001,2023));
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            TransformSate();
        }
    }

    //拾取到id的物品，并添加到物品栏
    //处理拾取物品的功能
    public void GetId(int id,int count = 1) {
        //查找是否所有物品中是否存在该物品
        //如果存在num+1
        //如果不存在，查找空的方格，把新创建的放进方格
        InventoryItem item = null;
        foreach (InventoryItem temp in inventoryItem) {
            if (temp.id == id) {
                item = temp;
                break;
            }
        }
        if (item != null) {//存在的情况
            item.PlusNumber(count);
        }
        else {//不存在的情况
            foreach (InventoryItem temp in inventoryItem) {
                if (temp.id == 0) {
                    item = temp;
                    break;
                }
            }
            if (item != null) {//查找空的方格，把新创建的放进方格
                GameObject go = NGUITools.AddChild(item.gameObject, inventoryItemPrefabs);
                go.transform.localPosition = Vector3.zero;
                item.SetId(id,count);
            }
        }
    }

    void OnTweenPlayFinished() {
        if (isShow == false) {
            this.gameObject.SetActive(false);
        }
    }

    public void TransformSate() {//转变状态
        if (isShow == false) {
            Show();
        }
        else {
            Hide();
        }
    }

    //取款方法，返回true表示成功
    public bool GetCoint(int conut) {
        if (coin >= conut) {
            coin -= conut;
            coinNumberLabel.text = coin.ToString();//更新金币显示
            return true;
        }
        return false;
    }

    //使用物品
    public bool MinusId(int id, int count = 1) {
        InventoryItem item = null;
        foreach (InventoryItem temp in inventoryItem) {
            if (temp.id == 0) {
                item = temp;
                break;
            }
        }
        if (item == null) {
            return false;
        }
        else {
            bool isSuccess = item.MinusNumber(count);
            return isSuccess;
        }
    }

}
