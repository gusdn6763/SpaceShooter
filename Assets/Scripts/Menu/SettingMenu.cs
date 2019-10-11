using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
  //  public static SettingMenu instance;

    public GameObject menu;
   // public GameObject background;
  //  public GameObject title;
   // public GameObject start;
   // public GameObject player;

    private AudioSource audioSound;

    private void Awake()
    {
        audioSound = GetComponent<AudioSource>();

        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != null)
        //{
        //    Destroy(this.gameObject);
        //}

        DontDestroyOnLoad(this.gameObject);

    //    player.SetActive(false);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("Game");
//background.SetActive(false);
   //     title.SetActive(false);
       // start.SetActive(false);
      //  player.SetActive(true);
    }

    public void OpenSettings()
    {
        menu.SetActive(true);
    }

    public void CloseSettings()
    {
        menu.SetActive(false);
    }

    public void MusicStop()
    {
        audioSound.Stop();
    }

    public void MusicStart()
    {
        audioSound.Play();
    }

    public void EffectStop()
    {
    //    title.SetActive(false);
    }

    public void EffectStart()
    {
   //     title.SetActive(true);
    }


    public void ChangerSwitchStatus(bool isOn)
    {
        if(isOn == true)
        {
            audioSound.Play();
        }
        else
        {
            audioSound.Stop();
        }
    }



}
