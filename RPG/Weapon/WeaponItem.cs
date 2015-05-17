using UnityEngine;
using System.Collections;

public class WeaponItem : MonoBehaviour {

    private int id;
    private ObjectInfo info;
    private UISprite iconSprite;
    private UILabel nameLabel;
    private UILabel effectLabel;
    private UILabel priceSell;

    void Awake() {
        iconSprite = transform.Find("Icon").GetComponent<UISprite>();
        nameLabel = transform.Find("Name").GetComponent<UILabel>();
        effectLabel = transform.Find("Effect").GetComponent<UILabel>();
        priceSell = transform.Find("PriceSell").GetComponent<UILabel>();
    }

    //通过调用这个方法更新装备的显示
    public void SetId(int id) {
        this.id = id;
        info = ObjectsInfo.m_instance.GetObjectInfoById(id);

        iconSprite.spriteName = info.icon_name;
        nameLabel.text = info.name;
        if (info.attack > 0) {
            effectLabel.text = "+伤害" + info.attack;
        }
        else if(info.def>0) {
            effectLabel.text = "+防御" + info.def;
        }
        else if (info.speed>0) {
            effectLabel.text = "+速度" + info.speed;
        }
        priceSell.text = info.price_sell.ToString();
    }

    public void OnBuyButton() {
        WeaponShop.m_instance.OnBuyButtonClick(id);
    }
}
