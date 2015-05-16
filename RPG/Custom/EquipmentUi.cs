using UnityEngine;
using System.Collections;

public class EquipmentUi : MonoBehaviour {

    public static EquipmentUi m_instance;
    public GameObject equipment;

    private TweenPosition tween;
    private bool isShow = false;

    private GameObject headgear;
    private GameObject armor;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject shoe;
    private GameObject accessory;
    private PlayerStatus playerStatus;

    public int attack = 0;
    public int def = 0;
    public int speed = 0;
    void Awake() {
        m_instance = this;
        tween = this.GetComponent<TweenPosition>();

        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        rightHand = transform.Find("RightHand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        leftHand = transform.Find("LeftHand").gameObject;
        accessory = transform.Find("Accessory").gameObject;

        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
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

    //处理装备穿戴
    public bool Dress(int id) {
        ObjectInfo info = ObjectsInfo.m_instance.GetObjectInfoById(id);
        if (info.type != ObjectType.Equip) {
            return false;//穿戴不成功
        }

        if (playerStatus.heroType == HeroType.Magician) {
            if (info.applicationType == ApplicationType.Swordman) {
                return false;
            }
        }

        if (playerStatus.heroType == HeroType.Swordman) {
            if (info.applicationType == ApplicationType.Magician) {
                return false;
            }
        }

        GameObject parent = null;

        switch (info.dressType) {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }
        EquipItem item = parent.GetComponentInChildren<EquipItem>();
        if (item != null) {//说明已经穿戴了同样类型的装备
            Inventory.m_instance.GetId(item.id);//把已经穿戴的装备卸下
            ReduceAttribute();
            item.SetInfo(info);
        }
        else {//没有穿戴
            GameObject go = NGUITools.AddChild(parent, equipment);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<EquipItem>().SetInfo(info);
        }
        UpdateProperty();
        return true;
    }

    void UpdateProperty() {//更新穿戴属性
        this.attack = 0;
        this.def = 0;
        this.speed = 0;
        EquipItem headgearItem = headgear.GetComponentInChildren<EquipItem>();
        PlusProperty(headgearItem);
        EquipItem armorItem = armor.GetComponentInChildren<EquipItem>();
        PlusProperty(armorItem);
        EquipItem leftHandItem = leftHand.GetComponentInChildren<EquipItem>();
        PlusProperty(leftHandItem);
        EquipItem rightHandItem = rightHand.GetComponentInChildren<EquipItem>();
        PlusProperty(rightHandItem);
        EquipItem shoeItem = shoe.GetComponentInChildren<EquipItem>();
        PlusProperty(shoeItem);
        EquipItem accessortItem = accessory.GetComponentInChildren<EquipItem>();
        PlusProperty(accessortItem);
        AddAttribute();
    }

    void PlusProperty(EquipItem item) {
        if (item != null) {
            ObjectInfo info = ObjectsInfo.m_instance.GetObjectInfoById(item.id);
            this.attack += info.attack;
            this.def += info.def;
            this.speed += info.speed;
        }
    }

    public void TakeOff(int id,GameObject go) {//装备卸下
        Inventory.m_instance.GetId(id);
        GameObject.Destroy(go);
        UpdateProperty();
        ReduceAttribute();
    }

    void AddAttribute() {
        PlayerStatus.m_instance.attack += this.attack;
        PlayerStatus.m_instance.speed += this.speed;
        PlayerStatus.m_instance.def += this.def;
    }

    void ReduceAttribute() {
        PlayerStatus.m_instance.attack -= this.attack;
        PlayerStatus.m_instance.speed -= this.speed;
        PlayerStatus.m_instance.def -= this.def;
    }
}
