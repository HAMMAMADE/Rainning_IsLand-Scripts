using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public MasterManager masterManager;

    //public FogManager FogCheck;
    public Transform Light;
    Vector3 nextAngle;
    public bool isNight, isNoon;
    bool ChangedWeather;

    public int Day;

    // Start is called before the first frame update
    public void StartTime()
    {
        ChangedWeather = false;
        nextAngle = Light.localEulerAngles;
        masterManager.UiCheck.StartCoroutine("TimeFlow");
        StartCoroutine("DayLight");
        Day = 0;
    }

    IEnumerator DayLight()
    {
        yield return new WaitForSeconds(0.1f);
        nextAngle.x += 0.25f;
        Light.localEulerAngles = nextAngle;
        if(Light.localEulerAngles.x < 350 && Light.localEulerAngles.x > 340 && Light.localEulerAngles.y == 180 && !isNight)
        {
            masterManager.FogCheck.ChangeNightFog();
            masterManager.soundCheck.ChangeBGM("NightBGM");

            isNight = true;
            isNoon = false;
            masterManager.SponeCheck.StartCoroutine("Spone1");
        }
        else if (Light.localEulerAngles.x < 350 && Light.localEulerAngles.x > 340 && Light.localEulerAngles.y == 0 && !ChangedWeather)
        {
            ChangedWeather = true;
            masterManager.Weathercheck.ChangeWeather();
        }
        else if (Light.localEulerAngles.x < 10 && Light.localEulerAngles.x > 0 && Light.localEulerAngles.y == 0 && !isNoon)
        {
            masterManager.SponeCheck.StopCoroutine("Spone1");

            masterManager.FogCheck.ChangeNoonFog();
            masterManager.soundCheck.ChangeBGM("DayBGM");

            ChangedWeather = false;
            isNoon = true;
            isNight = false;
            Day += 1;
            MainArchiveManager.StaticDay += 1; 
            masterManager.UiCheck.DayCheck(Day);

            masterManager.SponeCheck.StopCoroutine("Spone1");
        }
        StartCoroutine("DayLight");
    }

    public void PushBackTitle()
    {
        masterManager.soundCheck.SFXPlay("ClickSound");
        Time.timeScale = 1f;
        masterManager.tutorialCheck.StartCoroutine("ScreenFadeout");
    }

    public void PushBack()
    {
        masterManager.soundCheck.SFXPlay("ClickSound");
        masterManager.PlayerCheck.EndPause();
    }

    public void Setting()
    {
        masterManager.soundCheck.SFXPlay("ClickSound");
        masterManager.PlayerCheck.isSetting = true;
        masterManager.UiCheck.CallSoundSetting();
    }
}
