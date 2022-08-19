using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    private PhotonView _pv;
    GameObject controller;

    // Start is called before the first frame update
    void Awake()
    {
        _pv = GetComponent<PhotonView>();

        if(_pv.IsMine)
        {
            CreatePlayer();
        }
    }

    public void CreatePlayer(){
        float spawnPointX = Random.Range(-3, 3);
        float spawnPointY = 2;

        controller = PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity, 0, new object[]{_pv.ViewID});

    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreatePlayer();
        Debug.Log("Die and Create a new Player");
    }
}
