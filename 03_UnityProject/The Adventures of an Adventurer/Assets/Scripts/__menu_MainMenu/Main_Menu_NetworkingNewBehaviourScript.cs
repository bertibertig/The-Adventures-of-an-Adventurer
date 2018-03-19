using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_NetworkingNewBehaviourScript : Photon.MonoBehaviour {

    public InputField RoomName;
    public Button JoinButton;
    public Text ConnectionState;


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
        StartCoroutine("InizialiseServer");
    }

    public void JoinButtonEvent()
    {
        if (RoomName.text != "")
        {
            JoinButton.interactable = false;
            RoomName.interactable = false;
            StartCoroutine("CheckForRoom");
        }
        else
        {
            ConnectionState.text = "Please enter a Roomname.";
        }
    }
    IEnumerator InizialiseServer()
    {
        do
        {
            yield return null;
        } while (PhotonNetwork.connectionStateDetailed.ToString() != "ConnectedToMaster");

        ConnectionState.text = "Sucessfully connected to Server.";
        JoinButton.interactable = true;
        RoomName.interactable = true;

        if (PhotonNetwork.inRoom)
            ConnectionState.text = "Sucessfully created and joined room " + PhotonNetwork.room.Name;
    }

    IEnumerator CheckForRoom()
    {
        RoomInfo[] rInfos = PhotonNetwork.GetRoomList();
        bool roomExists = false;
        foreach (RoomInfo r in rInfos)
        {
            if (RoomName.text == r.Name)
                roomExists = true;
            yield return null;
        }

        //TODO: Reserch why rInfos is empty
        roomExists = true;

        if (roomExists)
        {
            ConnectionState.text = "Connecting to Room ...";
            PhotonNetwork.JoinRoom(RoomName.text);
            //PhotonNetwork.LoadLevel("0_Level_Tutorial");
            //SceneManager.LoadScene("0_Level_Tutorial");
        }
        else
        {
            JoinButton.interactable = true;
            RoomName.interactable = true;
            ConnectionState.text = "The room does not exist.";
        }
    }

    void OnJoinedRoom()
    {
        print("Joined");
        PhotonNetwork.Instantiate("Spirit", new Vector2(-100, -100), Quaternion.identity, 0);
        print("Joined as Spirit");
        PhotonNetwork.LoadLevel("Level_T1_MP");
    }



    public void BackToMainMenu()
	{
		Application.LoadLevel (0);
	}
}
