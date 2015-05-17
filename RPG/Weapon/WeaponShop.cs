using UnityEngine;
using System.Collections;

public class WeaponShop : MonoBehaviour {

    public static WeaponShop m_instance;
    public int[] weaponId;
    public UIGrid grid;
    public GameObject weaponItem;

    private TweenPosition tween;
    private bool isShow;

    private GameObject number;
    private UIInput numberInput;

    private int buyId = 0;

    void Awake() {
        m_instance = this;
        tween = this.GetComponent<TweenPosition>();
        number = transform.Find("Panel/Number").gameObject;
        numberInput = transform.Find("Panel/Number/NumberInput").GetComponent<UIInput>();
    }

    void Start() {
        number.SetActive(false);
        InitWeaponShop();
    }

    public void TransformState() {
        if (isShow == false) {
            tween.PlayForward();
            isShow = true;
        }
        else {
            tween.PlayReverse();
            isShow = false;
        }
    }

    public void OnCloseButton() {
        TransformState();
    }

    public void OnBuyButtonClick(int id) {
        buyId = id;
        number.SetActive(true);
        numberInput.value = "1";
    }

    public void OnOkButton() {
        int count = int.Parse(numberInput.value);
        if (count <= 0) {
            return;
        }
        int price = ObjectsInfo.m_instance.GetObjectInfoById(buyId).price_buy;
        int totalPrice = price * count;
        bool success = Inventory.m_instance.GetCoint(totalPrice);
        if (success) {
            Inventory.m_instance.GetId(buyId, count);
        }
        buyId = 0;
        numberInput.value = "1";
        number.SetActive(false);
    }

    void InitWeaponShop() {//初始化武器商店信息
        foreach (int id in weaponId) {
            GameObject go = NGUITools.AddChild(grid.gameObject, weaponItem);
            grid.AddChild(go.transform);
            go.GetComponent<WeaponItem>().SetId(id);
        }
    }

    public void OnClick() {
        number.SetActive(false);//点击商店背景隐藏输入
    }
}
