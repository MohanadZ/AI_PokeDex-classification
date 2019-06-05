using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;

public class HelloClient : MonoBehaviour
{
    private HelloRequester _helloRequester;
    public bool SendPack = true;

    private void Start()
    {
        _helloRequester = new HelloRequester();
        _helloRequester.Start();
    }

    public void MessageToServer(string imagePath)
    {
        _helloRequester.imageMessage = imagePath;   //The image file path is saved in a HelloRequester instance attribute
        _helloRequester.Continue();
        _helloRequester.Pause();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}