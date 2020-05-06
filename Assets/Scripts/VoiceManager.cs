using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Realtime;
using Photon.Pun;

public class VoiceManager : MonoBehaviourPunCallbacks
{

    [Tooltip("Maximum length of status message in characters")]
    public int statusMaxLength = 100;

    [Tooltip("Controller input used to toggle microphone")]
    public OVRInput.RawButton button = OVRInput.RawButton.LThumbstickUp;
    public GameObject microphoneIndicator;
    public Material microphoneOn;
    public Material microphoneOff;

    private string previousMessage = "";

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.Client.AddCallbackTarget(this);
        vc.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        RoomOptions roomopt = new RoomOptions();
        TypedLobby lobby = new TypedLobby("ApplicationLobby", LobbyType.Default);
        vc.Client.OpJoinOrCreateRoom(new EnterRoomParams
        {
            RoomName = "ApplicationRoom",
            RoomOptions =
            roomopt,
            Lobby = lobby
        });
    }

    public override void OnJoinedRoom()
    {
        
        Debug.Log("joined room");
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("disconnected");
        
    }

    void switchMicrophone()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        if ((OVRInput.Get(button)) || (Input.GetAxis("Fire1") > 0.0f))
        {
            vc.PrimaryRecorder.TransmitEnabled = !vc.
            PrimaryRecorder.TransmitEnabled;
        }

        if (microphoneIndicator != null)
        {
            if (vc.PrimaryRecorder.TransmitEnabled)
            {
                microphoneIndicator.GetComponent<MeshRenderer>().material = microphoneOn;
            }
            else
            {
                microphoneIndicator.GetComponent<MeshRenderer>().material = microphoneOff;
            }
        }
    }

    void Update()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        string otherParticipants = "";
        if (vc.Client.InRoom)
        {
            Dictionary<int, Player>.ValueCollection pts = vc.Client.CurrentRoom.Players.Values;
            foreach (Player p in pts)
            {
                otherParticipants += p.ToStringFull();
            }
        }
        string room = "not in room";
        if (vc.Client.CurrentRoom != null)
        {
            room = vc.Client.CurrentRoom.Name;
        }
        //setStatusText(vc.Client.State.ToString() + "server: " + vc.Client.CloudRegion + ":" + vc.Client.CurrentServerAddress + " room: " + room + " participants: " + otherParticipants);
        switchMicrophone();
    }
}



