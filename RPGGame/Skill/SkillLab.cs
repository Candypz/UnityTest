using UnityEngine;
using System.Collections;

public class SkillLab : MonoBehaviour {
    public static SkillLab m_instance;
    private Animator animator;

    void Awake() {
        m_instance = this;
    }

    void Start() {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void OnButton1Down() {
        animator.Play("attack1");
    }

    public void OnButton2Down() {
        animator.Play("attack2");
    }

    public void OnButton3Down() {
        animator.Play("attack3");
    }
}
