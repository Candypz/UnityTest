using UnityEngine;
using System.Collections;

/*
 * UI中的攻击按钮切换
 */

public class UIAttack : MonoBehaviour {
    public GameObject normalAttack;
    public GameObject rangeAttack;
    public GameObject redAttack;
    public static UIAttack m_instance;

    // Use this for initialization
    void Start() {
        m_instance = this;
        normalAttack = transform.Find("NormalAttack").gameObject;
        rangeAttack = transform.Find("RangeAttack").gameObject;
        redAttack = transform.Find("RedAttack").gameObject;
    }

    public void TurnToOneAttack() {
        normalAttack.SetActive(false);
        rangeAttack.SetActive(false);
        redAttack.SetActive(true);
    }

    public void TurnToTwoAttack() {
        normalAttack.SetActive(true);
        rangeAttack.SetActive(true);
        redAttack.SetActive(false);
    }
}
