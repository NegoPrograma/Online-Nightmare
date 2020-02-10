using UnityEngine;
using System.Collections;
using String = System.String;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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

        /*Esse trecho de código define as características iniciais de todo player.
        eu acredito que você poderia ter feito isso via script próprio e obter os mesmos resultados
        mas também acho que por uma questão de segurança deve ser melhor alterar valores via rede mesmo.*/
        foreach(var player in PhotonNetwork.PlayerList){
         Hashtable playerCustomProprierties = new Hashtable();
         playerCustomProprierties.Add("Lives",3);
         playerCustomProprierties.Add("Score",0);
         player.SetScore(0);
         

         player.SetCustomProperties(playerCustomProprierties,null,null);   
        }
        
    }


//esse método verifica a existência de novas salas a cada 5 segundos e só executa evidentimente se houverem novas salas
    public override void OnRoomListUpdate(System.Collections.Generic.List<RoomInfo> roomList){
        foreach(var room in roomList){
            object CustomProperties;
            room.CustomProperties.TryGetValue(GameModeController.gameModeKey,out CustomProperties);
            Debug.Log(String.Format("Room info:\n Room name: {0} isOpen:{1}\n isVisible:{2}\n MaxPlayers:{3}\n CustomRoomProprierties:{4}",room.Name,room.IsOpen,room.IsVisible,room.MaxPlayers,CustomProperties.ToString()));
        }
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
