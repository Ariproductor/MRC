using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    [Header("Volume")]
    public int volume = 50;
    public Image volumeBar;

    private void Start()
    {
        if (optionsMenu == null)
        {
            optionsMenu = GetComponent<GameObject>();
        }
        UpdateVolumeBar();
    }

    public void ButtonIncreaseVolume()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            volume += 10;
            if (volume > 100) volume = 100;
            if (volume < 0) volume = 0;
        }
        else
        {
            volume += 1;
            if (volume > 100) volume = 100;
            if (volume < 0) volume = 0;
        }
        UpdateVolumeBar();
    }
    public void ButtonDecreaseVolume()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            volume -= 10;
            if (volume > 100) volume = 100;
            if (volume < 0) volume = 0;
        }
        else
        {
            volume -= 1;
            if (volume > 100) volume = 100;
            if (volume < 0) volume = 0;
        }
        UpdateVolumeBar();
    }
    public void UpdateVolumeBar()
    {
        volumeBar.fillAmount = volume / 100f;
    }

}
