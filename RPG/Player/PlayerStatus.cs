using UnityEngine;
using System.Collections;

public enum HeroType {
    Swordman,
    Magician
}

public class PlayerStatus : MonoBehaviour {

    public HeroType heroType;
    public static PlayerStatus m_instance;

    public int level = 1;//等级
    public string name = "Candy_pz";//默然名字
    public int hp = 100;//hp最大值
    public int mp = 100;

    public float hpRemain = 100;
    public float mpRemain = 100;

    public int coin = 200;//金币

    public int attack = 20;//攻击力
    public int attackPlus = 0;//加的属性
    public int def = 20;//防御力
    public int defPlus = 0;
    public int speed = 20;//速度
    public int speedPlus = 0;
    public int pointRemain = 0;//剩余的点数

    void Awake() {
        m_instance = this;
    }

    public void GetCoint(int count) {
        coin += count;
    }

    public bool GetPoint(int point = 1) {
        if (pointRemain >= point) {
            pointRemain -= point;
            return true;
        }
        return false;
    }

    public void GetDrug(int hp,int mp) {
        hpRemain += hp;
        mpRemain += mp;
        if (hpRemain > this.hp) {
            hpRemain = this.hp;
        }
        if (mpRemain > this.mp) {
            mpRemain = this.mp;
        }
    }
}
