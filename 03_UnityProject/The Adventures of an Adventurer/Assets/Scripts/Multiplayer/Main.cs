using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private string roomName = "TestRoom";
	
	void Start () {
        PhotonNetwork.ConnectUsingSettings("v1.0");
	}

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.inRoom)
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
    }

    void OnJoinedRoom()
    {
        print("Joined");
        //PhotonNetwork.Instantiate("Player", new Vector2(-38.114f, 5), Quaternion.identity, 0);
        //NOTE: Check if Player is on Mobile => SystemInfo.operatingSystemFamily == Windows/Android
        if (SystemInfo.operatingSystemFamily.ToString() == "Windows")
        {
            PhotonNetwork.Instantiate("Player", new Vector2(-38.114f, 5), Quaternion.identity, 0);
        }
        else
        {
            PhotonNetwork.Instantiate("Spirit", new Vector2(-35.114f, 3), Quaternion.identity, 0);
        }
    }
}
