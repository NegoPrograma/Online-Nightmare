using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitch : MonoBehaviour
{
    public GameObject lobbyPanel;
    public GameObject loginPanel;
    void Start(){
        lobbyPanel = GameObject.Find("LobbyPanel");
        loginPanel = GameObject.Find("LoginPanel");
        lobbyPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void  InvokeToggleState(){
        Invoke("togglePanelState",5f);
    }
    private void togglePanelState(){
        if(!SetNickname.nickname.Equals("")){
            lobbyPanel.SetActive(true);
            loginPanel.SetActive(false);
        }
    }
}
