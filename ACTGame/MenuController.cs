using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
    public Color Purple;
    public SkinnedMeshRenderer headRenderer;
    public SkinnedMeshRenderer handRenderer;
    public Mesh[] headMeshArray;
    public Mesh[] handMeshArray;
    public SkinnedMeshRenderer[] bodyArry;
    private Color[] colorArray;
    private int headMeshIndex = 0;
    private int handMeshIndex = 0;
    private int colorIndex = -1;

    void Start() {
        colorArray = new Color[]{
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
        colorIndex = 0;
        OnChangColor(Color.blue);
    }

    public void OnChangColorCyan() {
        colorIndex = 1;
        OnChangColor(Color.cyan);
    }

    public void OnChangColorGreen() {
        colorIndex = 2;
        OnChangColor(Color.green);
    }

    public void OnChangColorPurple() {
        colorIndex = 3;
        OnChangColor(Purple);
    }

    public void OnChangColorRed() {
        colorIndex = 4;
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
