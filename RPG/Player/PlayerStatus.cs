using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
    public int grade = 1;//等级
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;//金币

    public void GetCoint(int count) {
        coin += count;
    }
}
