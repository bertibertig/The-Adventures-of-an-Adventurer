using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Main : MonoBehaviour {

    public bool spawnAsSpirit = false;
    public Text ConnectionState;
    public Button CreateButton;
    public InputField RoomName;

    private GameObject Player;
    private bool waitingForSecondPlayer = false;

    public GameObject MultiplayerComplete { get; set; }
    public bool RoomCreated { get; set; }

    void Start () {
        PhotonNetwork.ConnectUsingSettings("v1.0");
        MultiplayerComplete = GameObject.FindGameObjectWithTag("MultiplayerGUI");
        MultiplayerComplete.SetActive(false);
        //StartCoroutine("InizialiseServer");
        Player = GameObject.FindGameObjectWithTag("Player");
        RoomCreated = false;
    }

    public void ConnectAfterOpening()
    {
        if (ConnectionState != null)
        {
            StartCoroutine("InizialiseServer");
        }
    }

    void OnJoinedRoom()
    {
        //NOTE: Check if Player is on Mobile => SystemInfo.operatingSystemFamily == Windows/Android
        if (SystemInfo.operatingSystemFamily.ToString() == "Windows" && !spawnAsSpirit)
        {
            //TODO: Implement properly, so that the State of the Player is saved
            if (Player != null)
            {
                Player.SetActive(false);
                PhotonNetwork.Instantiate("Player", new Vector2(Player.transform.position.x, Player.transform.position.y), Quaternion.identity, 0);
            }
            else
                PhotonNetwork.Instantiate("Player", new Vector2(-38.114f, 5), Quaternion.identity, 0);
            print("Joined as Player");
        }
        else
        {
            PhotonNetwork.Instantiate("Spirit", new Vector2(-35.114f, 3), Quaternion.identity, 0);
            print("Joined as Spirit");
        }
    }

    public void CreateButtonEvent()
    {
        if (RoomName.text != "")
        {
            CreateButton.interactable = false;
            RoomName.interactable = false;
            StartCoroutine("CheckForRoom");
        }
        else
        {
            ConnectionState.text = "Please enter a Roomname.";
        }
    }

    public void CloseButtonEvent()
    {
        MultiplayerComplete.SetActive(false);
    }

    IEnumerator InizialiseServer()
    {
        do
        {
            yield return null;
        } while (PhotonNetwork.connectionStateDetailed.ToString() != "ConnectedToMaster");

        ConnectionState.text = "Sucessfully connected to Server.";
        CreateButton.interactable = true;
        RoomName.interactable = true;

        if (PhotonNetwork.inRoom)
            ConnectionState.text = "Sucessfully created and joined room " + PhotonNetwork.room.Name;
    }

    IEnumerator CheckForRoom()
    {
        //TODO: Reserch, why rInfos is empty
        RoomInfo[] rInfos = PhotonNetwork.GetRoomList();
        bool roomExists = false;
        foreach(RoomInfo r in rInfos)
        {
            if (RoomName.text == r.Name)
                roomExists = true;
            yield return null;
        }

        if(!roomExists)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(RoomName.text, options, TypedLobby.Default);
            RoomCreated = true;
            waitingForSecondPlayer = true;
            StartCoroutine("WaitingText");
        }
        else
        {
            CreateButton.interactable = true;
            RoomName.interactable = true;
            ConnectionState.text = "Room already exists. Please use another name";
        }
    }

    private IEnumerator WaitingText()
    {
        int counter = 0;
        do
        {
            switch (counter)
            {
                case 0:
                    ConnectionState.text = "Room created and Joined, Waiting for Second Player";
                    break;
                case 1:
                    ConnectionState.text = "Room created and Joined, Waiting for Second Player .";
                    break;
                case 2:
                    ConnectionState.text = "Room created and Joined, Waiting for Second Player ..";
                    break;
                case 3:
                    ConnectionState.text = "Room created and Joined, Waiting for Second Player ...";
                    counter = 0;
                    break;
            }
            counter++;
            yield return new WaitForSeconds(0.5f);

        } while (waitingForSecondPlayer);
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        waitingForSecondPlayer = false;
        Debug.Log("Player Connected " + player.NickName);
        StartCoroutine("WaitForSpirit");
        MultiplayerComplete.SetActive(false);
    }

    IEnumerator WaitForSpirit()
    {
        do
        {
            yield return null;
        } while (GameObject.FindGameObjectWithTag("Spirit") == null && GameObject.FindGameObjectWithTag("Player") == null);
        Player = GameObject.FindGameObjectWithTag("Player");
        //GameObject.FindGameObjectWithTag("Spirit").transform.position = new Vector2(Player.transform.position.x + 2, Player.transform.position.y + 2);
    }

    //Old GUI
    /*private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if(PhotonNetwork.inRoom)
        {
            GUILayout.Label("Sucessfully joined room " + PhotonNetwork.room.Name);
        }
        else
        {
            roomName = GUILayout.TextField(roomName);
            if (GUILayout.Button("Join"))
            {
                RoomOptions options = new RoomOptions();
                options.MaxPlayers = 2;
                PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
            }
        }
    }*/
}
