using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class CreateOrEnterRoom : MonoBehaviourPunCallbacks{

    public string roomNameValue;
    public Text roomNameField;
    public RoomOptions roomOptions;

    void Start(){
        roomNameValue="";
    }

    public void onClick(){
        roomNameValue = roomNameField.text;
        if(!roomNameValue.Equals("")){
            roomOptions = new RoomOptions(){MaxPlayers=4};
            PhotonNetwork.JoinOrCreateRoom(roomNameValue,roomOptions,TypedLobby.Default);
        }
    }
    

}
