using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class SettingButton : MonoBehaviour
{
    private  bool isOn = false;

    public RectTransform trans;

    public SettingMenu menusound;

    public void SetSound()
    {
        isOn = !isOn;

        if(isOn==true)
        {
            trans.anchoredPosition = new Vector3(8, 0, 0);
            menusound.MusicStop();
        }
        else
        {
            trans.anchoredPosition = new Vector3(-8, 0, 0);
            menusound.MusicStart();
        }
    }

    public void SetEffect()
    {
        isOn = !isOn;

        if (isOn == true)
        {
            trans.anchoredPosition = new Vector3(8, 0, 0);
            menusound.EffectStart();
        }
        else
        {
            trans.anchoredPosition = new Vector3(-8, 0, 0);
            menusound.EffectStop();
        }
    }

    public void SetSound(int number)
    {
        isOn = !isOn;

        if (isOn == true)
        {
            trans.anchoredPosition = new Vector3(8, 0, 0);
        }
        else
        {
            trans.anchoredPosition = new Vector3(-8, 0, 0);
        }

        switch(number)
        {
            case 1:
                menusound.MusicStop();
                break;
            case 2:
                menusound.MusicStart();
                break;
            case 3:
                menusound.EffectStart();
                break;
            case 4:
                menusound.EffectStop();
                break;
        }
    }
}
*/

class SettingButton : MonoBehaviour
{
    public RectTransform trans;
    public SettingMenu settingMenu;

    bool isOn;

    public void onClick()
    {
        isOn = !isOn;

        if (isOn == true)
        {
            trans.anchoredPosition = new Vector3(8, 0, 0);
        }
        else
        {
            trans.anchoredPosition = new Vector3(-8, 0, 0);
        }
        settingMenu.ChangerSwitchStatus(isOn);
    }


    //껏다 켯다 하는 동작
    //눈에 보이지 않는 동작
    class control
    {

    }

    //보이는 동작
    class model
    {

    }

    //클래스 3가지?

    //1.눈에 보이는 동작
    //2.눈에 보이지 않는 동작 ex)효과음 꺼짐
    //3.데이터 저장
    //mvc 패턴
}
