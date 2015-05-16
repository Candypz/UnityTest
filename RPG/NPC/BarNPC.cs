using UnityEngine;
using System.Collections;

public class BarNPC : NPC {
    public TweenPosition tweenQuest;
    public bool isQuest = false;
    public int killMonster = 0;
    public UILabel questLabel;
    public GameObject acceptButton;
    public GameObject okButton;
    public GameObject colosetButton;
    public GameObject cancelButton;

    private PlayerStatus playerStatus;
    private GameObject player;
    private float distance;
    private bool onQuestPanel = false;

    void Start() {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    void Update() {
        PlayerDistance();
    }

    public void PlayerDistance() {
        player = GameObject.Find("Player");
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > 5f && onQuestPanel) {
            HideQuest();
        }
    }

    void OnMouseOver() {//当鼠标位于这个之上每帧都会调用
        if (Input.GetMouseButtonDown(0) && distance < 4f && onQuestPanel != true) {
            if (isQuest) {
                ShowTaskProgress();
            }
            else {
                ShowTaskDes();
            }
            ShowQuest();
        }
    }

    void ShowQuest() {
        tweenQuest.gameObject.SetActive(true);
        tweenQuest.PlayForward();
    }

    void HideQuest() {
        onQuestPanel = false;
        tweenQuest.gameObject.SetActive(false);
        //tweenQuest.PlayReverse();
        //StartCoroutine(WaitPlayAnimation());
    }

    public void OnButtonCloset() {
        HideQuest();
    }

    void ShowTaskDes() {//显示任务描述
        onQuestPanel = true;
        questLabel.text = "任务：\n杀死10只怪\n\n奖励：\n1000金币";
        okButton.SetActive(false);
        acceptButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    void ShowTaskProgress() {//显示任务进度
        onQuestPanel = true;
        questLabel.text = "任务：\n你已经杀死了" + killMonster + "\\10只怪\n\n奖励：\n1000金币";
        okButton.SetActive(true);
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void OnButtonAccept() {
        ShowTaskProgress();
        isQuest = true;//开始任务
    }

    public void OnButtonOK() {
        if (killMonster >= 10) {
            playerStatus.GetCoint(1000);
            killMonster = 0;
            ShowTaskDes();
        }
        else {
            HideQuest();
        }
    }

    public void OnButtonCancel() {
        HideQuest();
    }

//     IEnumerator WaitPlayAnimation() {//等待UI关闭的动画播放
//         yield return new WaitForSeconds(2f);
//         tweenQuest.gameObject.SetActive(false);
//         onQuestPanel = false;
//     }
}
