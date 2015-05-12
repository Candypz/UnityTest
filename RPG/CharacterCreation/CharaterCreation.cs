using UnityEngine;
using System.Collections;

public class CharaterCreation : MonoBehaviour {
    public GameObject[] charaterPrefabs;
    public UIInput nameInput;//名字输入
    private GameObject[] charateGameObjercts;
    private int m_index = 0;
    private int length;

    // Use this for initialization
    void Start() {
        length = charaterPrefabs.Length;
        charateGameObjercts = new GameObject[length];
        for (int i = 0; i < length; ++i) {
            charateGameObjercts[i] = GameObject.Instantiate(charaterPrefabs[i], transform.position,transform.rotation) as GameObject;
        }
        CharacterShow();
    }

    // Update is called once per frame
    void Update() {

    }

    void CharacterShow() {//更新角色的显示
        charateGameObjercts[m_index].SetActive(true);
        for (int i = 0; i < length; ++i) {
            if (i != m_index) {
                charateGameObjercts[i].SetActive(false);//把没选择的角色隐藏
            }
        }
    }

    public void OnNextButtonClick() {
        m_index++;
        m_index %= length;
        CharacterShow();
    }

    public void OnPrevButtonClick() {
        m_index--;
        if (m_index == -1) {
            m_index = length - 1;
        }
        CharacterShow();
    }

    public void OnOkButtonClick() {
        PlayerPrefs.SetInt("SelectedCharacterIndex", m_index);//存储选择的角色
        PlayerPrefs.SetString("Name", nameInput.value);//存储名字
        //加载下一个场景
    }
}
