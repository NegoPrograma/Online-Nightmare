using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitch : MonoBehaviour
{
    public static GameObject lobbyPanel;
    public static GameObject loginPanel;
    void Start(){
        lobbyPanel = GameObject.Find("LobbyPanel");
        loginPanel = GameObject.Find("LoginPanel");
        lobbyPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
    public static void togglePanelState(){
        if(!SetNickname.nickname.Equals("")){
            lobbyPanel.SetActive(true);
            loginPanel.SetActive(false);
        }
    }
}
