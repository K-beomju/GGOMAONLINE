using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Panels")]
    public GameObject connectToMasterPanel;
    public GameObject lobbyPanel;
    public GameObject joinRandomRoomPanel;
    public GameObject matchingPanel;
    public GameObject optionPanel;

    [Header("Lobby Screen")]
    public InputField PlayerName;
    public Text player1NameText;
    public Text player2NameText;
    public Text gameStartingText;
    public GameObject matchCancleButton;


    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(1920  , 1080, true);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.ConnectUsingSettings();
        
    }


    void Start() 
    {
        SetActivePanel(connectToMasterPanel.name);
    }
        public override void OnJoinedRoom()
    {        
        SetActivePanel(matchingPanel.name);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }

        void TryStartGame()
    {
        // if we have 2 players in the lobby, load the Game scene
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            PhotonNetwork.LoadLevel("TileMap01");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

        }
        else
        {
            gameStartingText.text = "Waiting for Player...";
            PhotonNetwork.LeaveRoom();
            SetActivePanel(lobbyPanel.name);
        }
            
    }

        public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

        public void OnLeaveButton()
    {
        PhotonNetwork.LeaveRoom();
        SetActivePanel(lobbyPanel.name);
    }

    

    [PunRPC]
    void UpdateLobbyUI()
    {
        // set the player name texts
        player1NameText.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        player2NameText.text = PhotonNetwork.PlayerList.Length == 2 ? PhotonNetwork.CurrentRoom.GetPlayer(2).NickName : "...";

        // set the game starting text
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            gameStartingText.text = "게임이 3초후 시작됩니다.";
            matchCancleButton.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
                Invoke("TryStartGame", 3.0f);
        }
    }

     public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions options = new RoomOptions {MaxPlayers = 2};
        PhotonNetwork.CreateRoom(null, options);
    }

    public void OnClickStartButton()
    {
        PhotonNetwork.LocalPlayer.NickName = PlayerName.text;
        SetActivePanel(joinRandomRoomPanel.name);
        PhotonNetwork.JoinRandomRoom();
    }

    private void SetActivePanel(string activePanel)
        {
            connectToMasterPanel.SetActive(activePanel.Equals(connectToMasterPanel.name));
            lobbyPanel.SetActive(activePanel.Equals(lobbyPanel.name));
            joinRandomRoomPanel.SetActive(activePanel.Equals(joinRandomRoomPanel.name));
            matchingPanel.SetActive(activePanel.Equals(matchingPanel.name));
            optionPanel.SetActive(activePanel.Equals(optionPanel.name));    
        }


    
    public override void OnConnectedToMaster()
    {
        SetActivePanel(lobbyPanel.name);
    }

    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) OnLeaveButton(); 
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainLobby");
    }
}
