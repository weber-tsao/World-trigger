using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    //private PhotonView _pv;
    //GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null){
            SceneManager.LoadScene("LobbyScene");
        }
        else{
            InitGame();
        }
    }

    public void InitGame(){
        float spawnPointX = Random.Range(-3, 3);
        float spawnPointY = 2;

        //PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("PlayerManager", new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity);

    }
}
