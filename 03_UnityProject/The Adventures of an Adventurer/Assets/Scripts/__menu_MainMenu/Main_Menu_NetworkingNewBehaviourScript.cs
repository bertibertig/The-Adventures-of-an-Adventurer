using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Main_Menu_NetworkingNewBehaviourScript : MonoBehaviour {

	NetworkClient myClient;
	public Text ipAdress;

	public void SetupServer()
	{
		NetworkServer.Listen (4444);
		print ("Started Server");
	}

	public void SetupClient()
	{
		myClient = new NetworkClient ();
		myClient.RegisterHandler (MsgType.Connect, OnConnect);
		myClient.Connect (ipAdress.text, 4444);
	}

	public void BackToMainMenu()
	{
		Application.LoadLevel (0);
	}

	public void OnConnect(NetworkMessage netMsg)
	{
		print ("Connected to Server");
	}
}
