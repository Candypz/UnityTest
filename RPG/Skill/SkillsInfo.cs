using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsInfo : MonoBehaviour {
    public static SkillsInfo m_instance;
    public TextAsset skillsInfoText;

    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();

    void Awake() {
        m_instance = this;
        InitSkillInfoDict();//初始化技能字典的信息
    }

    void InitSkillInfoDict() {
        string text = skillsInfoText.text;
        string[] skillInfoArray = text.Split('\n');

        foreach (string skillInfoStr in skillInfoArray) {
            string[] proArray = skillInfoStr.Split(',');
            SkillInfo info = new SkillInfo();
            info.id = int.Parse(proArray[0]);
            info.name = proArray[1];
            info.iconName = proArray[2];
            info.des = proArray[3];
            string strApplytype = proArray[4];
            switch (strApplytype) {
                case "Passive":
                    info.applyType = ApplyType.Passive;
                    break;
                case "Buff":
                    info.applyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.applyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.applyType = ApplyType.MultiTarget;
                    break;
            }
            string strApplyPro = proArray[5];
            switch (strApplyPro) {
                case "Attack":
                    info.applyProperty = ApplyProperty.Attack;
                    break;
                case "Def":
                    info.applyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    info.applyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    info.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    info.applyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    info.applyProperty = ApplyProperty.MP;
                    break;
            }
            info.applyValue = int.Parse(proArray[6]);
            info.applyTime = int.Parse(proArray[7]);
            info.mp = int.Parse(proArray[8]);
            info.coldTime = int.Parse(proArray[9]);
            switch (proArray[10]) {
                case "Swordman":
                    info.applicableRole = ApplicableRole.Swordman;
                    break;
                case "Magician":
                    info.applicableRole = ApplicableRole.Magician;
                    break;
            }
            info.level = int.Parse(proArray[11]);
            switch (proArray[12]) {
                case "Self":
                    info.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    info.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.releaseType = ReleaseType.Position;
                    break;
            }
            info.distance = float.Parse(proArray[13]);
            skillInfoDict.Add(info.id, info);
        }
    }

    public SkillInfo GetSkillInfoById(int id) {
        SkillInfo skillInfo = null;
        skillInfoDict.TryGetValue(id, out skillInfo);
        return skillInfo;
    }

}

//适用角色
public enum ApplicableRole {
    Swordman,
    Magician
}

//作用类型
public enum ApplyType {
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}

//作用属性
public enum ApplyProperty {
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}

//释放类型
public enum ReleaseType {
    Self,
    Enemy,
    Position
}

//技能信息
public class SkillInfo {
    public int id;
    public string name;
    public string iconName;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicableRole applicableRole;
    public int level;
    public ReleaseType releaseType;
    public float distance;
}
