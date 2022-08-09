using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviourPunCallbacks
{
    public void OnClickStart(){
        PhotonNetwork.ConnectUsingSettings();
        print("Click Start");
    }

    public override void OnConnectedToMaster(){
        print("Connected");
        SceneManager.LoadScene("LobbyScene");
    }
}
