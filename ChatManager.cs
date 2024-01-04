using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    string username = "justinfan1234";
    string password = "pass";
    string channelName = "Dark_Zelda92";

    public delegate void ChatMessageListener(string source, string parameters);
    public event ChatMessageListener ChatMessageListeners;

    // Start is called before the first frame update
    void Start()
    {
        Connected();
        StartCoroutine(Timer(250));
    }

    private void Connected()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();

        Debug.Log("Connected to Twitch IRC");
    }

    // Update is called once per frame
    void Update()
    {
        if(twitchClient == null || !twitchClient.Connected)
        {
            Connected();
        }
        ReadChat();
    }

    void ReadChat()
    {
        if(twitchClient.Available > 0)
        {
            string message = reader.ReadLine();
           if(message.Contains("PRIVMSG"))
            {
                //Debug.Log(message);
                int splitPoint = message.IndexOf("!", 1);
                string chatName = message.Substring(1, splitPoint - 1);

                splitPoint = message.IndexOf(":", 1);
                string chatMessage = message.Substring(splitPoint + 1);
                ChatMessageListeners?.Invoke(chatName, chatMessage);
            }
        }
    }
    IEnumerator Timer(int _timer)
    {
        int i = 0;
        while(i < _timer)
        {
            if(i == 299)
            {
                Connected();
            }
            i++;
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(Timer(299));
    }

}
