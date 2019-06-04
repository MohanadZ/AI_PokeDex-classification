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

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }

    public void MessageToServer(string imagePath)
    {
        _helloRequester.imageMessage = imagePath;
        _helloRequester.Continue();
        _helloRequester.Pause();
    }
}