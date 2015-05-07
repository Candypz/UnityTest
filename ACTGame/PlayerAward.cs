using UnityEngine;
using System.Collections;

//拾取奖励物品


public class PlayerAward : MonoBehaviour {
    public GameObject singleSword;//单刃剑
    public GameObject dualSword;//双刃剑
    public GameObject gun;//枪
    public float exitTime = 10;//奖励物品存在的时间
    public float dualSwordTimer = 0;//武器的存在时间
    public float gunTimer = 0;//武器的存在时间


    void Update() {
        if (dualSwordTimer > 0) {
            dualSwordTimer -= Time.deltaTime;
            if (dualSwordTimer < 0) {
                TurnToSingleSword();
            }
        }

        if (gunTimer > 0) {
            gunTimer -= Time.deltaTime;
            if (gunTimer < 0) {
                TurnToSingleSword();
            }
        }
    }


    //切换武器
    public void GetAward(AwardType type) {
        if (type == AwardType.DualSword) {
            TurnToDualSword();
        }
        if (type == AwardType.Gun) {
            TurnToGun();
        }
    }

    void TurnToDualSword() {
        singleSword.SetActive(false);
        dualSword.SetActive(true);
        gun.SetActive(false);
        dualSwordTimer = exitTime;
        gunTimer = 0;
        UIAttack.m_instance.TurnToTwoAttack();//切换按钮
    }

    void TurnToGun() {
        singleSword.SetActive(false);
        dualSword.SetActive(false);
        gun.SetActive(true);
        gunTimer = exitTime;
        dualSwordTimer = 0;
        UIAttack.m_instance.TurnToOneAttack();//切换按钮
    }

    void TurnToSingleSword() {
        singleSword.SetActive(true);
        dualSword.SetActive(false);
        gun.SetActive(false);
        dualSwordTimer = 0;
        gunTimer = 0;
        UIAttack.m_instance.TurnToTwoAttack();//切换按钮
    }
}
