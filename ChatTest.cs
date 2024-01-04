using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChatManager;

public class ChatTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChatManager cm = GameObject.FindAnyObjectByType<ChatManager>();

        if (cm == null)
        {
            Debug.LogError("Could not find an instance of Chat Manager");
            return;
        }

        cm.ChatMessageListeners += ChatMessageListener;
    }

    void ChatMessageListener(string username, string message)
    {
        string combinedString = username + ": " + message;
        //Debug.Log(combinedString);
        if(message == "!HUH")
        {
            Debug.Log("Huh???");
        }
    }

    void CheckToSeeUserNameHere()
    {
        string[] userNames = {}; 
        for (int i = 0; i < userNames.Length; i++)
        {
            //Test 
        }
    }
}

