using UnityEngine;
using System.Collections;

public class MyNetwork : MonoBehaviour {
    public int connections = 10;
    public int listenPort = 8899;
    public bool useNat = false;
    public string ip = "127.0.0.1";
    public GameObject playerPrefab;

    void OnGUI() {
        //networkpeertype 
        //No client connection running. Server not initialized.
        if (Network.peerType == NetworkPeerType.Disconnected) {
            if (GUILayout.Button("创建服务器")) {
                NetworkConnectionError error = Network.InitializeServer(connections, listenPort, useNat);
                print(error);
            }

            if (GUILayout.Button("联机服务器")) {
                NetworkConnectionError error = Network.Connect(ip, listenPort);
                print(error);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server) {
            GUILayout.Label("服务器创建成功");
        }
        else if (Network.peerType == NetworkPeerType.Client) {
            GUILayout.Label("客户端已经连入");
        }
    }

    //两个都是在服务器端调用
    void OnServerInitialized() {
        print("完成初始");
        //Network.player;访问到当前的客户端
        int group = int.Parse(Network.player + "");//直接访问Network会得到当前客户端的索引 唯一
        Network.Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
    }

    void OnPlayerConnected(NetworkPlayer player) {
        print("一个客户端连接进来，index Number:" + player);
    }

    //客户端才调用
    void OnConnectedToServer() {
        print("我成功联机上了服务器");
        int group = int.Parse(Network.player + "");//直接访问Network会得到当前客户端的索引 唯一
        Network.Instantiate(playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
    }
    //network view 组件用来在局域网之内同步一个游戏物体的组件属性
    //network view 会把创建出来它的客户端作为主人，其他的客户端都会以主客户端为主

}
