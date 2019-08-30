using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public MasterManager masterManager;

    public AudioClip ItemPopUp;

    public AudioClip OpenMenu;

    public AudioClip ClickSound;

    public AudioClip CancelSound;

    public AudioClip FailSound;

    public AudioClip HitSound;

    public AudioClip GetFishSound;

    public AudioClip HitObjectSound;

    public AudioClip SpecialItemPop;

    public AudioClip ShootSound;

    public AudioClip PauseSound;

    public AudioClip OpenInvenSound;

    public AudioClip AchiveSound;

    public AudioClip SlideSound;
    //-------------------------------------

    public AudioClip BGM1;

    public AudioClip BGM2;

    public AudioClip BeachBGM;

    public AudioSource[] SFXPlayer;

    public AudioSource BGMPlayer;

    public AudioSource BGMPlayer2;

    int sfxIndex;
    //-------------------------------------

    public UISlider BGMValue;

    public UISlider SFXValue;

    private void Start()
    {
        StartCoroutine("PlayBeachSound");
    }

    IEnumerator SoundSetting()
    {
        yield return null;

        BGMPlayer.volume = BGMValue.value;
        BGMPlayer2.volume = BGMValue.value * 0.3f;

        for(int i = 0; i <= 2; i++)
        {
            SFXPlayer[i].volume = SFXValue.value;
        }

        StartCoroutine("SoundSetting");
    }

    IEnumerator PlayBeachSound()
    {
        BGMPlay("BeachBGM");
        yield return new WaitForSecondsRealtime(150f);
        StartCoroutine("StopBeachSound");
        yield return new WaitForSecondsRealtime(350f);

        StartCoroutine("StartBeachSound");
        StartCoroutine("PlayBeachSound");
    }
    IEnumerator StartBeachSound()
    {
        yield return new WaitForSeconds(0.1f);

        BGMPlayer2.volume += 0.015f;

        if (BGMPlayer2.volume >= 0.3)
        {
            BGMPlayer2.volume = 0.3f;
            yield return new WaitForSecondsRealtime(2f);
            StopCoroutine("StartBeachSound");

        }
        else
        {
            StartCoroutine("StartBeachSound");
        }
    }

    IEnumerator StopBeachSound()
    {
        yield return new WaitForSeconds(0.1f);

        BGMPlayer2.volume -= 0.05f;

        if (BGMPlayer2.volume <= 0)
        {
            BGMPlayer2.volume = 0;
            yield return new WaitForSecondsRealtime(2f);
            StopCoroutine("StopBeachSound");

        }
        else
        {
            StartCoroutine("StopBeachSound");
        }
    }

    public void SFXPlay(string name)
    {
        sfxIndex = (sfxIndex + 1) % 3;
        switch (name)
        {
            case "ItemPopUp":
                SFXPlayer[sfxIndex].clip = ItemPopUp;
                break;
            case "OpenMenu":
                SFXPlayer[sfxIndex].clip = OpenMenu;
                break;
            case "ClickSound":
                SFXPlayer[sfxIndex].clip = ClickSound;
                break;
            case "CancelSound":
                SFXPlayer[sfxIndex].clip = CancelSound;
                break;
            case "FailSound":
                SFXPlayer[sfxIndex].clip = FailSound;
                break;
            case "HitSound":
                SFXPlayer[sfxIndex].clip = HitSound;
                break;
            case "GetFishSound":
                SFXPlayer[sfxIndex].clip = GetFishSound;
                break;
            case "HitObjectSound":
                SFXPlayer[sfxIndex].clip = HitObjectSound;
                break;
            case "SpecialItemPop":
                SFXPlayer[sfxIndex].clip = SpecialItemPop;
                break;
            case "ShootSound":
                SFXPlayer[sfxIndex].clip = ShootSound;
                break;
            case "PauseSound":
                SFXPlayer[sfxIndex].clip = PauseSound;
                break;
            case "OpenInvenSound":
                SFXPlayer[sfxIndex].clip = OpenInvenSound;
                break;
            case "AchiveSound":
                SFXPlayer[sfxIndex].clip = AchiveSound;
                break;
            case "SlideSound":
                SFXPlayer[sfxIndex].clip = SlideSound;
                break;
        }
        SFXPlayer[sfxIndex].Play();
    }

    public void BGMPlay(string name)
    {
        switch (name)
        {
            case "BeachBGM":
                BGMPlayer2.clip = BeachBGM;
                BGMPlayer2.Play();
                break;

            case "DayBGM":
                BGMPlayer.clip = BGM1;
                BGMPlayer.Play();
                break;

            case "NightBGM":
                BGMPlayer.clip = BGM2;
                BGMPlayer.Play();
                break;
        }

    }

    public void ChangeBGM(string BGMname)
    {
        StartCoroutine(FadeoutBGM(BGMname));
    }

    IEnumerator FadeoutBGM(string BGMname)
    {
        yield return new WaitForSeconds(0.1f);

        BGMPlayer.volume -= 0.015f;
    
        if (BGMPlayer.volume <= 0)
        {
            BGMPlayer.volume = 0;
            BGMPlay(BGMname);
            yield return new WaitForSecondsRealtime(2f);
            StartCoroutine("FadeinBGM");

            StopCoroutine(FadeoutBGM(BGMname));

        }
        else
        {
            StartCoroutine(FadeoutBGM(BGMname));
        }
    }

    IEnumerator FadeinBGM()
    {
        yield return new WaitForSeconds(0.1f);

        BGMPlayer.volume += 0.015f;

        if (BGMPlayer.volume >= BGMValue.value - 0.1f)
        {
            BGMPlayer.volume = BGMValue.value;
            StopCoroutine("FadeinBGM");
        }
        else
        {
            StartCoroutine("FadeinBGM");
        }
    }
}
