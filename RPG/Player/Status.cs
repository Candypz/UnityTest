using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {
    public static Status m_instance;
    private TweenPosition tween;
    private bool isShow = false;
    private UILabel attackLabel;
    private UILabel defLabel;
    private UILabel speedLaberl;
    private UILabel pointLaberl;
    private UILabel summaryLabel;

    private GameObject attckButtonGo;
    private GameObject defButtonGo;
    private GameObject speedButtonGo;

    private PlayerStatus playerStatus;

    void Awake() {
        m_instance = this;
        tween = this.GetComponent<TweenPosition>();

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

        attackLabel = transform.Find("Attack").GetComponent<UILabel>();
        speedLaberl = transform.Find("Speed").GetComponent<UILabel>();
        defLabel = transform.Find("Def").GetComponent<UILabel>();
        pointLaberl = transform.Find("Point").GetComponent<UILabel>();
        summaryLabel = transform.Find("Summary").GetComponent<UILabel>();

        attckButtonGo = GameObject.Find("AttackPlus");
        defButtonGo = GameObject.Find("DefPlus");
        speedButtonGo = GameObject.Find("SpeedPlus");


    }

    public void TransformState() {
        if (isShow == false) {
            UpdateShow();
            tween.PlayForward();
            isShow = true;
        }
        else {
            tween.PlayReverse();
            isShow = false;
        }
    }

    void UpdateShow() {//根据playerStatus的属性更新显示
        attackLabel.text = playerStatus.attack + "+" + playerStatus.attackPlus;
        defLabel.text = playerStatus.def + "+" + playerStatus.defPlus;
        speedLaberl.text = playerStatus.speed + "+" + playerStatus.speedPlus;

        pointLaberl.text = playerStatus.pointRemain.ToString();

        summaryLabel.text = "伤害:" + (playerStatus.attack + playerStatus.attackPlus)
            + " " + "防御:" + (playerStatus.def + playerStatus.defPlus)
            + " " + "速度:" + (playerStatus.speed + playerStatus.speedPlus);

        if (playerStatus.pointRemain > 0) {
            attckButtonGo.SetActive(true);
            defButtonGo.SetActive(true);
            speedButtonGo.SetActive(true);
        }
        else {
            attckButtonGo.SetActive(false);
            defButtonGo.SetActive(false);
            speedButtonGo.SetActive(false);
        }
    }

    public void OnAttackButton() {
        bool success = playerStatus.GetPoint();
        if (success) {
            playerStatus.attackPlus++;
            UpdateShow();
        }
    }

    public void OnSpeedButton() {
        bool success = playerStatus.GetPoint();
        if (success) {
            playerStatus.speedPlus++;
            UpdateShow();
        }
    }

    public void OnDefButton() {
        bool success = playerStatus.GetPoint();
        if (success) {
            playerStatus.defPlus++;
            UpdateShow();
        }
    }
}
