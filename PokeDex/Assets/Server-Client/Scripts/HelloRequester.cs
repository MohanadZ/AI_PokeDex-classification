using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Collections.Generic;

public class HelloRequester : RunAbleThread
{
    public string imageMessage;
    string[] messageFromServer;
    public static List<string> whoIsThatPokemon = new List<string>();

    protected override void Run()
    {
        ForceDotNet.Force();

        using (RequestSocket client = new RequestSocket())
        {
            //Port number in the TCP connection
            client.Connect("tcp://localhost:5555");

            while (Running)
            {
                if (Send)
                {
                    //The image file path is sent over the socket
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

                        //Remove unnecessary characters from the received classification result
                        //Then split the result where there is a space, and store it in a string array
                        messageFromServer = message.Replace(":", "").Replace("(", "").Replace(")", "").Split(' ');

                        //Add the string array elements to a string list
                        whoIsThatPokemon.AddRange(messageFromServer);
                    }
                }
            }
        }

        NetMQConfig.Cleanup();
    }
}