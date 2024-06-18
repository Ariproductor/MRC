using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopScreen : MonoBehaviour
{
    public GameObject desktopScreen;
    public GameObject chatBox;
     public bool chatBoxToggle = false;

    private void Start()
    {
        desktopScreen = GetComponent<GameObject>();
    }

    public void ButtonChat()
    {
        chatBoxToggle = !chatBoxToggle;

        if (chatBoxToggle)
        {
            chatBox.SetActive(true);
        }
        else
        {
            chatBox.SetActive(false);
        }
    }

    public void ButtonMapSelectMenu()
    {

    }

    public void StartGame()
    {

    }


}
