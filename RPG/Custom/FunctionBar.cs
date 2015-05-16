using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

    public void OnStatusButton() {
        Status.m_instance.TransformState();
    }

    public void OnBagButton() {
        Inventory.m_instance.TransformSate();
    }

    public void OnEquipButton() {
        EquipmentUi.m_instance.TransformState();
    }

    public void OnSkillButton() {
        SkillUI.m_instance.TransformState();
    }

    public void OnSettingButton() {

    }
}
