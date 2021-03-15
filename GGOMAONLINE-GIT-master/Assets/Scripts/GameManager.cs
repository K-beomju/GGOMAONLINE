using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject inGamePanel;
    public GameObject respawnPanel;
    public GameObject winPanel;
    public GameObject losePanel;


    void Awake()
    {
        Screen.SetResolution(1920  , 1080, true);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    private void Start() 
    {
        SetActivePanel(inGamePanel.name);
        Spawn();
    }
        public void ReSpawn()
    {
        SetActivePanel(inGamePanel.name);
        PhotonNetwork.Instantiate("Player", new Vector3(0, -7, 0), Quaternion.identity);
    }
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0, -7, 0), Quaternion.identity);
    }

    public void OnClickGotoMenuButton()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect(); 
        
    }

    void Update() { if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) OnClickGotoMenuButton(); }

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainLobby");
    }

    private void SetActivePanel(string activePanel)
        {
            inGamePanel.SetActive(activePanel.Equals(inGamePanel.name));
            respawnPanel.SetActive(activePanel.Equals(respawnPanel.name));
            winPanel.SetActive(activePanel.Equals(winPanel.name));
 
        }
}
