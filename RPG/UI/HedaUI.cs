using UnityEngine;
using System.Collections;

public class HedaUI : MonoBehaviour {

    public HedaUI m_instance;

    private UILabel name;
    private UISlider hpBar;
    private UISlider mpBar;
    private UILabel hpLabel;
    private UILabel mpLabel;
    private PlayerStatus playerStatus;

    void Awake() {
        m_instance = this;
        name = transform.Find("Name").GetComponent<UILabel>();
        hpBar = transform.Find("HP").GetComponent<UISlider>();
        mpBar = transform.Find("MP").GetComponent<UISlider>();
        hpLabel = transform.Find("HP/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("MP/Thumb/Label").GetComponent<UILabel>();
    }

    void Start() {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow();
    }

    void UpdateShow() {
        name.text = "Lv." + playerStatus.level + " " + playerStatus.name;
        hpBar.value = playerStatus.hpRemain / playerStatus.hp;
        mpBar.value = playerStatus.mpRemain / playerStatus.mp;

        hpLabel.text = playerStatus.hpRemain+"/"+playerStatus.hp;
        mpLabel.text = playerStatus.mpRemain+"/"+playerStatus.mp;
    }

}
