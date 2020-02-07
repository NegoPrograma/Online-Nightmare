using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnectCycle : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;

    public override void OnConnected(){
        Debug.Log("onConnected stage achieved: next one should be ConnectedToMaster");
    }

    public override void OnDisconnected(DisconnectCause cause){
        Debug.Log("Error! "+ cause);
        Debug.Log("Trying to return you to lobby.");
        PhotonNetwork.Reconnect();
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("OnConnectedToMaster stage achieved, you're successfully connected to the server. you may enter the lobby now.");
        Debug.Log("Server hosted at: "+ PhotonNetwork.CloudRegion + ", your ping on connection:" + PhotonNetwork.GetPing());
        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby(){
        Debug.Log("You are now at the lobby.");
        PanelSwitch.togglePanelState();
    }

    public override void OnJoinedRoom(){
        Debug.Log("You're now inside a room.");
        Debug.Log("Current room name:" + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Current Player Count:" + PhotonNetwork.CurrentRoom.PlayerCount);
        
        //Instantiate(playerPrefab,new Vector3(0,0,0),Quaternion.Euler(0,0,90));
        PanelSwitch.lobbyPanel.SetActive(false);
        PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(0,0,0),Quaternion.Euler(0,0,90),0);
        
    }


}
