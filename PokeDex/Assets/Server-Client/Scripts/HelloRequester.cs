using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Collections.Generic;

///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.


public class HelloRequester : RunAbleThread
{
    ///     Stop requesting when Running=false.

    public string imageMessage;
    string[] messageFromServer;
    public static List<string> whoIsThatPokemon = new List<string>();

    protected override void Run()
    {
        ForceDotNet.Force();

        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            while (Running)
            {
                if (Send)
                {
                    //string message = client.ReceiveFrameString();
                    client.SendFrame(imageMessage);

                    string message = null;
                    bool gotMessage = false;

                    while (Running)
                    {
                        gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                        if (gotMessage) break;
                    }
                    if (gotMessage)
                    {
                        Debug.Log("Received " + message);

                        messageFromServer = message.Replace(":", "").Replace("(", "").Replace(")", "").Split(' ');

                        whoIsThatPokemon.AddRange(messageFromServer);
                    }
                }
            }
        }

        NetMQConfig.Cleanup();
    }
}