using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using Photon.Realtime;
using TMPro;

public class LobbyScene : MonoBehaviourPunCallbacks
{   

    [SerializeField]
    InputField inputRoomName;

    public TextMeshProUGUI textRoomList;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnected == false){
            SceneManager.LoadScene("StartScene");
        }
        else{
            PhotonNetwork.JoinLobby();
        }
        
    }

    public override void OnJoinedLobby(){
        print("Lobby Joined");
    }

    public string GetRoomName(){
        string roomName = inputRoomName.text;
        return roomName.Trim();
    }

    public void OnClickCreateRoom(){
        string roomName = GetRoomName();

        if(roomName.Length > 0){
            PhotonNetwork.CreateRoom(roomName);
        }
        else{
            print("Invalid RoomName");
        }
        
    }

    public override void OnJoinedRoom(){
        print("Room Joined");
        SceneManager.LoadScene("RoomScene");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        StringBuilder sb = new StringBuilder(); 
        foreach(RoomInfo roomInfo in roomList){
            sb.AppendLine(roomInfo.Name);
        }

        textRoomList.text = sb.ToString();
    }
}
