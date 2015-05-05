using UnityEngine;
using System.Collections;

public class PlayerAnimationAttack : MonoBehaviour {
    private Animator animator;
    private bool isCanAttackB;

    void Awake() {
        animator = this.GetComponent<Animator>();
    }

    public void OnNormalAttackClick() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackA") && isCanAttackB) {
            animator.SetTrigger("AttackB");
        }
        else {
            animator.SetTrigger("AttackA");
        }
    }

    public void OnRangeAttackClick() {
        animator.SetTrigger("AttackRange");
    }

    public void OnRedAttackClick() {
        animator.SetTrigger("AttackA");
    }

    public void AttackBEvent1() {
        isCanAttackB = true;
    }

    public void AttackBEvent2() {
        isCanAttackB = false;
    }

    // Use this for initialization
    void Start() {
        EventDelegate NormalAttackEvent = new EventDelegate(this, "OnNormalAttackClick");
        GameObject.Find("NormalAttack").GetComponent<UIButton>().onClick.Add(NormalAttackEvent);

        EventDelegate RangeAttackEvent = new EventDelegate(this, "OnRangeAttackClick");
        GameObject.Find("RangeAttack").GetComponent<UIButton>().onClick.Add(RangeAttackEvent);

        EventDelegate RedAttackEvent = new EventDelegate(this, "OnRedAttackClick");
        GameObject redAttack = GameObject.Find("RedAttack");
        redAttack.GetComponent<UIButton>().onClick.Add(RedAttackEvent);
        redAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
}
