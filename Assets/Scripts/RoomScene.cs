using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using Photon.Realtime;
using TMPro;

public class RoomScene : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI textRoomName;
    public TextMeshProUGUI playerList;
    public Button buttonStartGame;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null){
            SceneManager.LoadScene("LobbyScene");
        }
        else{
            textRoomName.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
    }

    public override void OnMasterClientSwitched(Player newMasterClient){
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
    }

    public void UpdatePlayerList(){
        
        StringBuilder sb = new StringBuilder(); 
        foreach(var kvp in PhotonNetwork.CurrentRoom.Players){
            sb.AppendLine(kvp.Value.NickName);
        }

        playerList.text = sb.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        UpdatePlayerList();
    }

    public void OnClickStartGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickLeaveRoom(){
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom(){
        SceneManager.LoadScene("LobbyScene");
    }
}
