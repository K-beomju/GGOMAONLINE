using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviourPunCallbacks
{

    public bool onClickLButton = false;
    public bool onClickRButton = false;
    PlayerScript Player = null;

	PlayerScript FindPlayer() 
	{
		foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player")) 
			if (Player.GetPhotonView().IsMine) return Player.GetComponent<PlayerScript>();
		return null;
	}

    private void Start() {
        Player = FindPlayer();
    }

    void Update() {
        

        if(onClickLButton)
        {
            Player.axis = -1;
        }
        if(onClickRButton)
        {
            
            Player.axis = 1;
        }
       if(!onClickLButton && !onClickRButton)
        {
            Player.axis = 0;
        }
    }

        public void OnClickUPLeftButton()
    {
        Player = FindPlayer();
        onClickLButton = false;

    }
        public void OnClickDOWNLeftButton()
    {
        Player = FindPlayer();
        onClickLButton = true;

    }

        public void OnClickUPRightButton()
    {
        Player = FindPlayer();
        onClickRButton = false;

        
    }
        public void OnClickDOWNRightButton()
    {
        onClickRButton = true;

        
    }

       public void OnClickJumpButton()
    {
        PlayerScript Player = FindPlayer();
        if (Player.isGround) Player.PV.RPC("JumpRPC", RpcTarget.All);
    }
}
