using UnityEngine;
using System.Collections;

//游戏开始菜单

public class MenuController : MonoBehaviour {
    public static MenuController m_instance;
    public Color Purple;
    public SkinnedMeshRenderer headRenderer;
    public SkinnedMeshRenderer handRenderer;
    public Mesh[] headMeshArray;
    public Mesh[] handMeshArray;
    public SkinnedMeshRenderer[] bodyArry;

    [HideInInspector]
    public Color[] colorArray;

    private int headMeshIndex = 0;
    private int handMeshIndex = 0;
    private int colorIndex = 0;

    void Awake() {
        m_instance = this;
    }

    void Start() {
        colorArray = new Color[]{
            Color.white,
            Color.blue,
            Color.cyan,
            Color.green,
            Purple,
            Color.red,
        };
        //禁止销毁
        DontDestroyOnLoad(this.gameObject);
    }

    void Save() {
        PlayerPrefs.SetInt("HeadMeshIndex", headMeshIndex);
        PlayerPrefs.SetInt("HandMeshIndex", handMeshIndex);
        PlayerPrefs.SetInt("ColorIndex", colorIndex);
    }

    public void OnHeadMeshNext() {
        headMeshIndex++;
        headMeshIndex %= headMeshArray.Length;
        headRenderer.sharedMesh = headMeshArray[headMeshIndex];
    }

    public void OnHandMeshNext() {
        handMeshIndex++;
        handMeshIndex %= handMeshArray.Length;
        handRenderer.sharedMesh = handMeshArray[handMeshIndex];
    }

    public void OnChangColorBlue() {
        colorIndex = 1;
        OnChangColor(Color.blue);
    }

    public void OnChangColorCyan() {
        colorIndex = 2;
        OnChangColor(Color.cyan);
    }

    public void OnChangColorGreen() {
        colorIndex = 3;
        OnChangColor(Color.green);
    }

    public void OnChangColorPurple() {
        colorIndex = 4;
        OnChangColor(Purple);
    }

    public void OnChangColorRed() {
        colorIndex = 5;
        OnChangColor(Color.red);
    }

    void OnChangColor(Color color) {
        foreach (SkinnedMeshRenderer renderer in bodyArry) {
            renderer.material.color = color;
        }
    }

    public void OnPlay() {
        Save();
        Application.LoadLevel(1);
    }
}
