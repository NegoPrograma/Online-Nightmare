
using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnectCycle : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnected(){
        Debug.Log("onConnected stage achieved: next one should be ConnectedToMaster");
    }

    public override void OnConnectedToMaster(){
        Debug.Log("OnConnectedToMaster stage achieved, you're successfully connected to the server. you may enter the lobby now.");
        Debug.Log("Server hosted at: "+ PhotonNetwork.CloudRegion + ", your ping on connection:" + PhotonNetwork.GetPing());
    }


}
