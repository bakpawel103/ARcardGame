﻿using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RpcManager : MonoBehaviourPunCallbacks
{ 
    public static RpcManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PhotonPeer.RegisterType(typeof(Package), (byte) 'M', Package.Serialize, Package.Deserialze);
    }
    
    public void SendStringMessage()
    {
        photonView.RPC("RpcWithString", RpcTarget.Others, "jup", "and jup.");
    }
    
    [PunRPC]
    void RpcWithString(string a, string b, PhotonMessageInfo info)
    {
        GameManager.instance.AddLog($"Chat message from {info.Sender.NickName}: {a} {b}");
    }
    
    public void SendArrayObjectMessage()
    {
        object[] objectArray = { 1, 2, 3, 4, 5, 6 };
        photonView.RPC("RpcWithObjectArray", RpcTarget.Others, objectArray as object);
    }
    
    [PunRPC]
    void RpcWithObjectArray(object[] objectArray, PhotonMessageInfo info)
    {
        GameManager.instance.AddLog($"RpcWithObjectArray from {info.Sender.NickName}: {objectArray}");
    }

    public void SendCustomSerialization()
    {
        Package testData = new Package(1, 12);
        photonView.RPC("RPC_ReceiveMyCustomSerialization", RpcTarget.Others, Package.Serialize(testData));
    }

    [PunRPC]
    void RPC_ReceiveMyCustomSerialization(byte[] datas)
    {
        Package result = (Package) Package.Deserialze(datas);
        GameManager.instance.AddLog($"RPC_ReceiveMyCustomSerialization : {result.packageType}, {result.rollSum}");
    }
}