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
        
        object gameModeSelected;
        if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(GameModeController.gameModeKey, out gameModeSelected)){
            Debug.Log("GameMode Selected:" + gameModeSelected.ToString());
        }
        PanelSwitch.lobbyPanel.SetActive(false);
        PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(0,0,0),Quaternion.Euler(0,0,90),0);
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        string randomGeneratedRoomName ="Room"+Random.Range(1,5000).ToString()+Random.Range(1,5000).ToString();
        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = GameModeController.playerLimitPerRoom;
        options.CustomRoomProperties = GameModeController.gameMode;
        //todas as opçoes acima são visiveis apenas pela sala, logo o searching do join random room não funcionaria
        //para deixar as salas visíveis pelo lobby, é necessário setar quais infos sobre sua sala são visíveis, vide linha abaixo 
        options.CustomRoomPropertiesForLobby = new string[] { GameModeController.gameModeKey};
        PhotonNetwork.CreateRoom(randomGeneratedRoomName,options);
    }


}
