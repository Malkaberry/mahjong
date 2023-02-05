using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //todo autoreplay the music

    [SerializeField] Image soundIconON;
    [SerializeField] Image soundIconOFF;
    private bool muted = false;

    void Start()
    {

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            load();

        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;

        }

        else
        {
            muted = false;
            AudioListener.pause = false;

        }

        save();
        UpdateButtonIcon();

    }

    private void UpdateButtonIcon()
    {

        if (muted == false)
        {
            soundIconON.enabled = true;
            soundIconOFF.enabled = false;
        }

        else
        {
            soundIconON.enabled = false;
            soundIconOFF.enabled = true;

        }
    }

    private void load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);

    }



}
