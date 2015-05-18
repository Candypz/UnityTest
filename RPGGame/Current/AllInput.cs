using UnityEngine;
using System.Collections;

public class AllInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SkillLab.m_instance.OnButton1Down();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SkillLab.m_instance.OnButton2Down();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SkillLab.m_instance.OnButton3Down();
        }
	}
}
