using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public UIInput ipAddersInput;
    public int coonections = 10;
    public int listenPort = 4323;
    public GameObject uiRoot;
    public Transform pos1, pos2;
    public GameObject soldierPrefabs;
    //当创建服务器按钮被按下执行
    public void OnButtonCreatSeverClick() {
        Network.InitializeServer(coonections, listenPort, false);
        uiRoot.SetActive(false);//隐藏
    }

    //当链接服务器按钮被按下执行
    public void OnButtonConnectSeverClick() {
        Network.Connect(ipAddersInput.value, listenPort);
        uiRoot.SetActive(false);
    }

    void OnServerInitialized() {//服务器初始化
        NetworkPlayer player = Network.player;
        int group = int.Parse(Network.player + "");
        GameObject go = Network.Instantiate(soldierPrefabs, pos1.position, Quaternion.identity, group)as GameObject;
        //这个代码只设置了当前创建者中的战士ownerPlayer的属性，在其他客户端为NULL;
        //go.GetComponent<PlayerController>().setOwnerPlayer(Network.player);

        //RPC 远程调用
        //完成一个远程调用会执行，会执行所有客户端上的setOwnerPlayer方法
        go.networkView.RPC("setOwnerPlayer", RPCMode.AllBuffered, Network.player);
        Screen.showCursor = false;
    }

    void OnConnectedToServer() {//链接上服务器
        NetworkPlayer player = Network.player;
        int group = int.Parse(Network.player + "");
        GameObject go = Network.Instantiate(soldierPrefabs, pos2.position, Quaternion.identity, group)as GameObject;
        go.networkView.RPC("setOwnerPlayer", RPCMode.AllBuffered, Network.player);
        Screen.showCursor = false;
    }

    //void LateUpdate() {
    //    if (Input.GetKeyDown(KeyCode.Escape)) {
    //        Screen.showCursor = false;
    //    }
    //}
    
}
