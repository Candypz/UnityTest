using UnityEngine;
using System.Collections;

public class MyNetwork : MonoBehaviour {

    public GameObject playerPrefab;

    private int coonetions = 10;
    private int listenPort = 8392;
    private string ipAddress = "127.0.0.1";
    private bool useNat = false;

    public void OnButtonCreateSeverClick() {
        Network.InitializeServer(coonetions, listenPort, useNat);
    }

    public void OnButtonConnectSeverClick() {
        Network.Connect(ipAddress, listenPort);
    }

    //服务端
    void OnServerInitialized() {
        print("完成初始化");
        //int group = int.Parse(Network.player + "");
        //Network.Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity, group);
    }

    void OnPlayerConnected(NetworkPlayer player) {
        print("一个客户端连接进来，index Number:" + player);
    }

    //客户端
    void OnConnectedToServer() {
        print("成功连上了服务器");
        //int group = int.Parse(Network.player + "");
        //GameObject go = Network.Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity, group)as GameObject;
        //go.networkView.RPC("", RPCMode.AllBuffered, Network.player);
    }
}
