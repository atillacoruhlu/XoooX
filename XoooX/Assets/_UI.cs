using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class _UI : NetworkRoomManager
{   
    NetworkManager manager;
    public InputField field;
    public Text ConnectingText;



    new void Awake()
    {   
        manager = GetComponent<NetworkManager>();
        field.text = "localhost";
    }
    /*void Update(){
        if (NetworkClient.active)
        {   
            ConnectingText.text="Connecting to " + manager.networkAddress ;
        }
        else
        {
            ConnectingText.text="";
        }
    }*/
    public void StartHosting()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
        manager.StartHost();
        }
    }
    public void StartNewClient()
    {
        Debug.Log("startnewcliente girdi");
        ConnectingText.text = "Trying to Connect";
        manager.StartClient();
        manager.networkAddress = field.text;
    }
    public void ReturnButton()
    {
        Initiate.Fade("Menu_Scene", Color.black, 2); //Loads menu
    }
    public void StopHostButton()
    {   
        if (NetworkServer.active && NetworkClient.isConnected)
        {
        manager.StopHost();
        }
    }


}
