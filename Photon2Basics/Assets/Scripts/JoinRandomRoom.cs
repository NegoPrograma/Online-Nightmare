using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JoinRandomRoom : MonoBehaviourPunCallbacks{


    public void onClick(){
        PhotonNetwork.JoinRandomRoom();
    }

     public override void OnJoinRandomFailed(short returnCode, string message){
        string randomRoomString = "Room"+Random.Range(100,10000).ToString()+Random.Range(100,10000).ToString();
        PhotonNetwork.CreateRoom(randomRoomString);
    }

}
