using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public static Shop m_instance;
    private TweenPosition tween;
    private bool isShow = false;

    private GameObject number;
    private UIInput numberInput;

    private int buyId = 0;

    void Awake() {
        m_instance = this;
        tween = this.GetComponent<TweenPosition>();
        number = GameObject.Find("Number");
        numberInput = GameObject.Find("NumberInput").GetComponent<UIInput>();
        number.SetActive(false);
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

    public void OnBuyId1001() {
        Buy(1001);
    }

    public void OnBuyId1002() {
        Buy(1002);
    }

    public void OnBuyId1003() {
        Buy(1003);
    }

    public void OnOkButton() {
        int count = int.Parse(numberInput.value);
        ObjectInfo info = ObjectsInfo.m_instance.GetObjectInfoById(buyId);
        int price = info.price_buy;
        int priceTotal = price * count;
        bool success = Inventory.m_instance.GetCoint(priceTotal);
        if (success) {//取款成功可以购买
            if (count > 0) {
                Inventory.m_instance.GetId(buyId, count);
            }
        }
        number.SetActive(false);
    }

    public void Buy(int id) {
        ShowNumber();
        buyId = id;
    }

    void ShowNumber() {
        number.SetActive(true);
        numberInput.value = "0";
    }
}
