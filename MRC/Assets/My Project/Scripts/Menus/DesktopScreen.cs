using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopScreen : MonoBehaviour
{
    public GameObject desktopScreen;


    [Header("ChatBox")]
    public GameObject chatBox;
    public bool chatBoxToggle = false;


    [Header("MRCMenu")]
    public GameObject mRCMenu;
    public bool mRCMenuToggle = false;

    [Header("OptionsMenu")]
    public GameObject optionsMenu;
    public bool optionsMenuToggle = false;




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

    public void ButtonMRCMenu()
    {
        mRCMenuToggle = !mRCMenuToggle;
        if (mRCMenuToggle)
        {
            mRCMenu.SetActive(true);
        }
        else
        {
            mRCMenu.SetActive(false);
        }
    }

    public void ButtonOptionsMenu()
    {
        optionsMenuToggle = !optionsMenuToggle;
        if (!optionsMenuToggle)
        {
            optionsMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(false);
        }
    }



}
