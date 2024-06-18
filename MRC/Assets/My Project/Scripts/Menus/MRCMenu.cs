using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MRCMenu : MonoBehaviour
{
    public GameObject mRCMenu;

    public Scene[] levels;
    public Scene selectedLevel;

    private void Start()
    {
        mRCMenu = GetComponent<GameObject>();
    }


    public void ButtonLojasAmericanas()
    {
        if (selectedLevel == levels[0])
        {
            return;
        }
        else
        {
            selectedLevel = levels[0];
        }
    }
    public void ButtonAçougue()
    {
        if (selectedLevel == levels[1])
        {
            return;
        }
        else
        {
            selectedLevel = levels[1];
        }
    }

    public void ButtonStartGame()
    {
        loadMatch();
    }

    public void loadMatch()
    {
        if (selectedLevel == levels[0])
        {
            SceneManager.LoadScene("MRC");
        }

        if (selectedLevel == levels[1])
        {
            SceneManager.LoadScene("MRC");
        }


    }


}
