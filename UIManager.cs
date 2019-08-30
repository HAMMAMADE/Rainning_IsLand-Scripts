using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MasterManager masterManager;

    public UISprite HealthBar;
    Vector3 nextHealthScale;
    Vector3 TargetHealth;

    public UISprite StaminaBar;
    public GameObject[] BreathBob;

    Color PopUpBreath;
    Color PopDownBreath;

    Vector3 nextScale;
    Vector3 TargetScale;
    //------------------------------

    public GameObject SoundSettingUI;

    //------------------------------
    public UILabel DayUI;

    public Transform TimeClock;
    Vector3 nextTimeAng;
    //------------------------------

    public Transform Thermometer;
    Vector3 nextThermoScale;
    Vector3 MoveThermoScale;

    //------------------------------

    public Transform InteractWindow;
    Vector3 nextWindow;
    public UILabel WindowText;

    //------------------------------

    public UISprite InterButton;
    public UILabel ButtonText;

    public UISprite CencelButton;
    public UILabel CencelText;

    public UILabel BuildText;
    public UILabel InvText;
    //------------------------------

    public Transform FireUI;
    public Transform FireTimeGuage;

    public float GetTime;

    Vector3 nextFireUI;
    Vector3 nextTimeGuage;


    public Transform FirebaseMainPos;
    Vector3 nextBasePos;

    public Transform FishGrillPos;
    Vector3 nextFishPos;

    bool OpenGrillMenu;

    //------------------------------
    public Transform FellGuage;
    Vector3 nextFell;

    //------------------------------
    public Transform FishingUi;
    Vector3 nextFishUi;

    //------------------------------
    public Transform BuildingUi;
    Vector3 nextBuildUi;

    //------------------------------
    public Transform InvIcon;
    public Transform InvUi;

    Vector3 nextInvIcon;
    Vector3 nextInvUi;
    //------------------------------

    public GameObject BuffUI1;
    public UILabel CheckTime1;

    public GameObject BuffUI2;
    public UILabel CheckTime2;

    public GameObject BuffUI3;
    public UILabel CheckTime3;

    public GameObject DeBuffUI1;

    public GameObject DeBuffUI2;

    //-------------------------------
    public GameObject AxeSet;
    public GameObject HamSet;
    public GameObject RodSet;

    public Transform AxeGuage;
    Vector3 nextAxe;

    public Transform HamGuage;
    Vector3 nextHam;

    public Transform RodGuage;
    Vector3 nextRod;

    //-------------------------------
    public GameObject MakingGuage;
    public Transform MakePerBar;
    public UILabel MakePersent;
    public float Waittime;

    Vector3 nextMakePer;

    // Start is called before the first frame update
    void Start()
    {
        //BreathBob = new GameObject[8];
        TargetScale = StaminaBar.transform.localScale;
        nextScale = StaminaBar.transform.localScale;

        TargetHealth = HealthBar.transform.localScale;
        nextHealthScale = HealthBar.transform.localScale;

        PopUpBreath = new Color(1f, 1f, 1f, 1f);
        PopDownBreath = new Color(1f, 1f, 1f, 0f);
        nextTimeAng = TimeClock.localEulerAngles;

        nextFell = FellGuage.localPosition;

        nextFishUi = FishingUi.localPosition;

        nextBuildUi = BuildingUi.localPosition;

        nextInvIcon = InvIcon.localPosition;
        nextInvUi = InvUi.localPosition;

        nextFireUI = FireUI.localPosition;
        nextTimeGuage = FireTimeGuage.localScale;

        nextMakePer = MakePerBar.localScale;

        nextAxe = AxeGuage.localScale;
        nextHam = HamGuage.localScale;
        nextRod = RodGuage.localScale;
    }
    //----------------------------------------------------
    public void CallSoundSetting()
    {
        masterManager.PlayerCheck.PauseSet.SetActive(false);
        masterManager.soundCheck.StartCoroutine("SoundSetting");
        SoundSettingUI.SetActive(true);
    }
    public void BackSoundSetting()
    {
        masterManager.PlayerCheck.isSetting = false;
        masterManager.PlayerCheck.PauseSet.SetActive(true);
        masterManager.soundCheck.StopCoroutine("SoundSetting");
        SoundSettingUI.SetActive(false);
    }

    public void ShowDurability(int DurabilityType)
    {
        switch (DurabilityType)
        {
            case 1:
                AxeSet.SetActive(true);
                break;
            case 2:
                HamSet.SetActive(true);
                break;
            case 3:
                RodSet.SetActive(true);
                break;
        }
    }

    public void HideDurability()
    {
        AxeSet.SetActive(false);
        HamSet.SetActive(false);
        RodSet.SetActive(false);
    }

    public void AxeUpdateDur()
    {
        nextAxe.y = masterManager.PlayerCheck.AxeDurability / 100f;
        AxeGuage.localScale = nextAxe;
    }

    public void HamUpdateDur()
    {
        nextHam.y = masterManager.PlayerCheck.HammerDurability / 100f;
        HamGuage.localScale = nextHam;
    }

    public void RodUpdateDur()
    {
        nextRod.y = masterManager.PlayerCheck.RodDurability / 100f;
        RodGuage.localScale = nextRod;
    }

    //----------------------------------------------------
    public void SetMakingBar(float time, float MaxTime)
    {
        MakingGuage.SetActive(true);
        masterManager.BuildCheck.nowMaking = true;
        StartCoroutine(MakeTime(time, MaxTime));
    }

    IEnumerator MakeTime(float time, float MaxTime)
    {
        MakePersent.text = Mathf.Floor((time / MaxTime * 100)).ToString();

        nextMakePer.x = time / MaxTime;

        MakePerBar.localScale = nextMakePer;

        //MakePerBar.localScale = Vector3.Lerp(MakePerBar.localScale,nextMakePer,0.1f);

        yield return new WaitForSecondsRealtime(1f);

        time += 1f;

        if(time >= MaxTime)
        {
            MakingGuage.SetActive(false);
            masterManager.BuildCheck.nowMaking = false;
            nextMakePer.x = 0;
            MakePerBar.localScale = nextMakePer;
            StopCoroutine(MakeTime(time, MaxTime));
            yield break;
        }
        else
        {
            StartCoroutine(MakeTime(time,MaxTime));
        }

    }

    //----------------------------------------------------

    public void DeBuff2On()
    {
        DeBuffUI2.SetActive(true);
    }

    public void DeBuff2Off()
    {
        DeBuffUI2.SetActive(false);
    }

    public void DeBuff1On()
    {
        DeBuffUI1.SetActive(true);
    }

    public void DeBuff1Off()
    {
        DeBuffUI1.SetActive(false);
    }

    public void Buff1On()
    {
        BuffUI1.SetActive(true);
    }
    public void Buff1Time()
    {
        CheckTime1.text = masterManager.BuffCheck.BuffTime1.ToString();
    }
    public void Buff1Off()
    {
        BuffUI1.SetActive(false);
    }

    public void Buff2On()
    {
        BuffUI2.SetActive(true);
    }
    public void Buff2Time()
    {
        CheckTime2.text = masterManager.BuffCheck.BuffTime2.ToString();
    }
    public void Buff2Off()
    {
        BuffUI2.SetActive(false);
    }

    public void Buff3On()
    {
        BuffUI3.SetActive(true);
    }
    public void Buff3Time()
    {
        CheckTime3.text = masterManager.BuffCheck.BuffTime3.ToString();
    }
    public void Buff3Off()
    {
        BuffUI3.SetActive(false);
    }
    //----------------------------------------------------
    public void BreathCheck()
    {
        switch (masterManager.PlayerCheck.BreathPoint)
        {
            case 0:
                for(int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                break;
            case 1:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 2:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 3:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 4:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 5:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 6:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 7:
                for (int i = 7; i >= masterManager.PlayerCheck.BreathPoint; i--)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopDownBreath;
                }
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
            case 8:
                for (int i = 0; i < masterManager.PlayerCheck.BreathPoint; i++)
                {
                    BreathBob[i].GetComponent<UISprite>().color = PopUpBreath;
                }
                break;
        }
    }
    //----------------------------------------------------
    public void StaminaCheck(float num)
    {
        TargetScale.x = masterManager.PlayerCheck.Stamina / 100f;
        if(num > 0)
        {
            StartCoroutine("StaminaInc");
        }
        else
        {
            StartCoroutine("StaminaDec");
        }
    }

    IEnumerator StaminaInc()
    {
        yield return new WaitForSeconds(0.1f);
        if (StaminaBar.transform.localScale.x < TargetScale.x)
        {
            nextScale.x += 0.01f;
            StaminaBar.transform.localScale = nextScale;
            StartCoroutine("StaminaInc");
        }
        else if(StaminaBar.transform.localScale.x > 1f)
        {
            nextScale.x = 1f;
            StaminaBar.transform.localScale = nextScale;
            StopCoroutine("StaminaInc");
        }
        else
        {
            StaminaBar.transform.localScale = TargetScale;
            StopCoroutine("StaminaInc");
        }
    }

    IEnumerator StaminaDec()
    {
        yield return new WaitForSeconds(0.1f);
        if (StaminaBar.transform.localScale.x > TargetScale.x)
        {
            nextScale.x -= 0.01f;
            StaminaBar.transform.localScale = nextScale;
            StartCoroutine("StaminaDec");
        }
        else if (StaminaBar.transform.localScale.x < 0f)
        {
            nextScale.x = 0f;
            StaminaBar.transform.localScale = nextScale;
            StopCoroutine("StaminaDec");
        }
        else
        {
            StaminaBar.transform.localScale = TargetScale;
            StopCoroutine("StaminaDec");
        }
    }
    //----------------------------------------------------
    //----------------------------------------------------
    public void HealthCheck()
    {
        TargetHealth.x = masterManager.PlayerCheck.HealthPoint / 100f;
        if (masterManager.PlayerCheck.HealthPoint >= 100)
        {
            TargetHealth.x = 1f;
        }
        HealthBar.transform.localScale = TargetHealth;
    }
    //---------------------------------
    IEnumerator TimeFlow()
    {
        yield return new WaitForSeconds(0.1f);
        nextTimeAng.z += 0.25f;
        TimeClock.localEulerAngles = nextTimeAng;

        StartCoroutine("TimeFlow");
    }

    public void DayCheck(int Day)
    {
        DayUI.text = Day.ToString() + " 일 차";
    }

    //---------------------------------
    //Temperature Check

    public void SetTemperature(float num)
    {
        StopCoroutine("TempMoveUp");
        StopCoroutine("TempMoveDown");

        MoveThermoScale = Thermometer.localScale;
        nextThermoScale = Thermometer.localScale;
        //nextThermoScale.y = masterManager.PlayerCheck.Temper;
       
        //masterManager.PlayerCheck.Temper = num;
        if (num >= masterManager.PlayerCheck.Temper)
        {
            nextThermoScale.y = num;
            StartCoroutine(TempMoveUp(num));
        }
        else
        {
            nextThermoScale.y = num;
            //nextThermoScale.y = num * -1;
            StartCoroutine(TempMoveDown(num));   
        }
    }

    IEnumerator TempMoveUp(float num)
    {
        yield return new WaitForSeconds(0.001f);
        //Debug.Log(num);
        if (Thermometer.localScale.y < nextThermoScale.y)
        {
            MoveThermoScale.y += 0.0005f;
            Thermometer.localScale = MoveThermoScale;
            masterManager.PlayerCheck.Temper = MoveThermoScale.y;
            StartCoroutine(TempMoveUp(num));
        }
        else
        {
            Thermometer.localScale = nextThermoScale;
            masterManager.PlayerCheck.Temper = num;
            StopCoroutine(TempMoveUp(num));
        }
    }

    IEnumerator TempMoveDown(float num)
    {
        yield return new WaitForSeconds(0.001f);

        if (Thermometer.localScale.y > nextThermoScale.y)
        {
            MoveThermoScale.y -= 0.0005f;
            Thermometer.localScale = MoveThermoScale;
            masterManager.PlayerCheck.Temper = MoveThermoScale.y;
            StartCoroutine(TempMoveDown(num));
        }
        else
        {
            Thermometer.localScale = nextThermoScale;
            masterManager.PlayerCheck.Temper = num;
            StopCoroutine(TempMoveDown(num));
        }
    }

    //---------------------------------
    public void InvOpen()
    {
        InvText.text = "인벤토리 열기";
    }

    public void InvClose()
    {
        InvText.text = "인벤토리 닫기";
    }

    public void OnBaitText()
    {
        BuildText.text = "미끼뿌리기";
    }

    public void BuildReady()
    {
        BuildText.text = "제작 리스트";
    }
    public void BuildSelect()
    {
        BuildText.text = "제작취소";
    }
    public void Building()
    {
        BuildText.text = "제작중";
    }
    public void BuildStart()
    {
        BuildText.text = "제작하기";
    }

    public void EndingReady()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "탈출한다";
    }


    public void FishingReady()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "낚시하기";
    }

    public void DoFishing()
    {
        masterManager.soundCheck.SFXPlay("OpenMenu");

        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "낚시시작";
    }

    public void CatchFish()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "낚기";
    }

    public void WaitInteract()
    {
        Color InternextCol = new Color(0f,0f,0f,0f);
        InterButton.color = InternextCol;
        ButtonText.text = "";
        masterManager.ObjectCheck.TargetObeject = null;
    }

    public void FellingReady()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "채집하기";
    }

    public void FireBaseReady()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "모닥불 메뉴";
    }

    public void HazeReady()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "건조대 메뉴";
    }

    public void FireBaseCloseReady()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        InterButton.color = InternextCol;
        ButtonText.text = "메뉴 닫기";
    }

    public void showCencel()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0.75f);
        CencelButton.color = InternextCol;
        CencelText.text = "취소하기";
    }

    public void EndCencel()
    {
        Color InternextCol = new Color(1f, 1f, 1f, 0f);
        CencelButton.color = InternextCol;
        CencelText.text = " ";
    }

    public void InterActObject(Collider Target)
    {
        FellingReady();
        WindowText.text = masterManager.ObjectCheck.ShowName(Target);
    }

    public void SmallInterActObject(Collider Target)
    {
        FellingReady();
        WindowText.text = masterManager.ObjectCheck.ShowName(Target);
    }

    public void OutInterAct()
    {
        WaitInteract();
        WindowText.text = "";
    }
    //--------------------------------------------------------------------------

    public void StartInvIcon()
    {
        nextInvIcon = new Vector3(800f, -231f, 0f);
        StopCoroutine("InvIconMov");
        StartCoroutine("InvIconMov");
    }

    IEnumerator InvIconMov()
    {
        yield return new WaitForSeconds(0.01f);
        if (InvIcon.localPosition.x < 790f)
        {
            InvIcon.localPosition = Vector3.Lerp(InvIcon.localPosition, nextInvIcon, 0.1f);
            StartCoroutine("InvIconMov");
        }
        else
        {
            StartInvMain();
            InvIcon.localPosition = new Vector3(800f, -231f, 0f);
            StopCoroutine("InvIconMov");
            yield break;
        }
    }

    public void StartInvMain()
    {
        nextInvUi = new Vector3(660f,-75f,0f);
        StopCoroutine("InvUiMov");
        StartCoroutine("InvUiMov");
    }

    IEnumerator InvUiMov()
    {
        yield return new WaitForSeconds(0.01f);
        if (InvUi.localPosition.x > 650f)
        {
            InvUi.localPosition = Vector3.Lerp(InvUi.localPosition, nextInvUi, 0.1f);
            StartCoroutine("InvUiMov");
        }
        else
        {
            InvUi.localPosition = new Vector3(660f, -75f, 0f);
            StopCoroutine("InvUiMov");
            yield break;
        }
    }

    //-----------------------------------------------------

    public void EndInvIcon()
    {
        nextInvIcon = new Vector3(600f, -195f, 0f);
        StopCoroutine("InvIconBackMov");
        StartCoroutine("InvIconBackMov");
    }

    IEnumerator InvIconBackMov()
    {
        yield return new WaitForSeconds(0.01f);
        if (InvIcon.localPosition.x > 590f)
        {
            InvIcon.localPosition = Vector3.Lerp(InvIcon.localPosition, nextInvIcon, 0.1f);
            StartCoroutine("InvIconBackMov");
        }
        else
        {
            InvIcon.localPosition = new Vector3(600f, -195f, 0f);
            StopCoroutine("InvIconBackMov");
            yield break;
        }
    }

    public void EndInvMain()
    {
        nextInvUi = new Vector3(1100f, -75f, 0f);
        StopCoroutine("InvUiBackMov");
        StartCoroutine("InvUiBackMov");
    }

    IEnumerator InvUiBackMov()
    {
        yield return new WaitForSeconds(0.01f);
        if (InvUi.localPosition.x < 1090f)
        {
            InvUi.localPosition = Vector3.Lerp(InvUi.localPosition, nextInvUi, 0.1f);
            StartCoroutine("InvUiBackMov");
        }
        else
        {
            EndInvIcon();
            InvUi.localPosition = new Vector3(1100f, -75f, 0f);
            StopCoroutine("InvUiBackMov");
            yield break;
        }
    }

    //-----------------------------------------------------

    public void StartBuildUI()
    {
        StopCoroutine("PopUpBuildUi");
        StartCoroutine("PopUpBuildUi");
        nextBuildUi = new Vector3(-460f, 100f, 0f);
    }

    public void EndBuildUI()
    {
        StopCoroutine("PopDownBuildUi");
        StartCoroutine("PopDownBuildUi");
        nextBuildUi = new Vector3(-460f, 600f, 0f);
    }

    IEnumerator PopUpBuildUi()
    {
        yield return new WaitForSeconds(0.01f);
        if (BuildingUi.localPosition.y != 100f)
        {
            BuildingUi.localPosition = Vector3.Lerp(BuildingUi.localPosition, nextBuildUi, 0.1f);
            StartCoroutine("PopUpBuildUi");
        }
        else
        {
            BuildingUi.localPosition = new Vector3(-460f, 100f, 0f);
            StopCoroutine("PopUpBuildUi");
            yield break;
        }

    }

    IEnumerator PopDownBuildUi()
    {
        yield return new WaitForSeconds(0.01f);
        if (BuildingUi.localPosition.y != 600f)
        {
            BuildingUi.localPosition = Vector3.Lerp(BuildingUi.localPosition, nextBuildUi, 0.1f);
            StartCoroutine("PopDownBuildUi");
        }
        else
        {
            BuildingUi.localPosition = new Vector3(-460f, 600f, 0f);
            StopCoroutine("PopDownBuildUi");
            yield break;
        }

    }

    public void StartFellUI()
    {
        StopCoroutine("PopUpFellUI");
        StartCoroutine("PopUpFellUI");
        masterManager.soundCheck.SFXPlay("OpenMenu");
        nextFell = new Vector3(0f, -175f, 0f);
    }

    public void EndFellUI()
    {
        StopCoroutine("PopDownFellUI");
        StartCoroutine("PopDownFellUI");
        nextFell = new Vector3(0f, -410f, 0f);
    }

    IEnumerator PopUpFellUI()
    {
        yield return new WaitForSeconds(0.01f);
        if (FellGuage.localPosition.y != -175f) {
            FellGuage.localPosition = Vector3.Lerp(FellGuage.localPosition, nextFell, 0.1f);
            StartCoroutine("PopUpFellUI");
        }
        else
        {
            FellGuage.localPosition = new Vector3(0f, -175f, 0f);
            StopCoroutine("PopUpFellUI");
            yield break;
        }
    }

    IEnumerator PopDownFellUI()
    {
        yield return new WaitForSeconds(0.01f);
        if (FellGuage.localPosition.y != -410f)
        {
            FellGuage.localPosition = Vector3.Lerp(FellGuage.localPosition, nextFell, 0.1f);
            StartCoroutine("PopDownFellUI");
        }
        else
        {
            FellGuage.localPosition = new Vector3(0f, -410f, 0f);
            StopCoroutine("PopDownFellUI");
            yield break;
        }
    }

    //-----------------------------------------------------------------------

    public void StartFishUI()
    {
        StopCoroutine("PopUpFishUI");
        StartCoroutine("PopUpFishUI");
        masterManager.soundCheck.SFXPlay("OpenMenu");
        nextFishUi = new Vector3(-275f, 170f, 0f);
    }

    public void EndFishUI()
    {
        StopCoroutine("PopDownFishUI");
        StartCoroutine("PopDownFishUI");
        nextFishUi = new Vector3(-275f, 630f, 0f);
    }

    IEnumerator PopUpFishUI()
    {
        yield return new WaitForSeconds(0.01f);
        if (FishingUi.localPosition.y != 170f)
        {
            FishingUi.localPosition = Vector3.Lerp(FishingUi.localPosition, nextFishUi, 0.1f);
            StartCoroutine("PopUpFishUI");
        }
        else
        {
            FishingUi.localPosition = new Vector3(-275f, 170f, 0f);
            StopCoroutine("PopUpFishUI");
            yield break;
        }
    }

    IEnumerator PopDownFishUI()
    {
        yield return new WaitForSeconds(0.01f);
        if (FishingUi.localPosition.y != 630f)
        {
            FishingUi.localPosition = Vector3.Lerp(FishingUi.localPosition, nextFishUi, 0.1f);
            StartCoroutine("PopDownFishUI");
        }
        else
        {
            FishingUi.localPosition = new Vector3(-275f, 630f, 0f);
            StopCoroutine("PopDownFishUI");
            yield break;
        }
    }
    //----------------------------------------------------------------------------------------------
    public void StartFireUI()
    {
        StopCoroutine("PopUpFireUi");
        StartCoroutine("PopUpFireUi");
        nextFireUI = new Vector3(-305f, 100f, 0f);
    }

    public void EndFireUI()
    {
        StopCoroutine("PopDownFireUi");
        StartCoroutine("PopDownFireUi");
        nextFireUI = new Vector3(-305f, 600f, 0f);
    }

    IEnumerator PopUpFireUi()
    {
        yield return new WaitForSeconds(0.01f);
        if (FireUI.localPosition.y != 100f)
        {
            FireUI.localPosition = Vector3.Lerp(FireUI.localPosition, nextFireUI, 0.1f);
            StartCoroutine("PopUpFireUi");
        }
        else
        {
            FireUI.localPosition = new Vector3(-305f, 100f, 0f);
            StopCoroutine("PopUpFireUi");
            yield break;
        }

    }

    IEnumerator PopDownFireUi()
    {
        yield return new WaitForSeconds(0.01f);
        if (FireUI.localPosition.y != 600f)
        {
            FireUI.localPosition = Vector3.Lerp(FireUI.localPosition, nextFireUI, 0.1f);
            StartCoroutine("PopDownFireUi");
        }
        else
        {
            FireUI.localPosition = new Vector3(-305f, 600f, 0f);
            StopCoroutine("PopDownFireUi");
            yield break;
        }

    }

    public void FireTimeCheck()
    {
        GetTime /= 100;

        if (GetTime >= 1f) GetTime = 1f;

        FireTimeGuage.localScale = new Vector3(GetTime, 1f, 1f);

        //nextTimeGuage = FireTimeGuage.localScale;
    }

    public void GrillMenu()
    {
        masterManager.soundCheck.SFXPlay("OpenMenu");

        if (!OpenGrillMenu)
        {
            nextBasePos = new Vector3(-100f, 0f, 0f);
            nextFishPos = new Vector3(100f, 0f, 0f);

            StartCoroutine("GrillOpenMoving");
            OpenGrillMenu = true;
        }
        else
        { 
            nextBasePos = new Vector3(0f, 0f, 0f);
            nextFishPos = new Vector3(0f, 0f, 0f);

            StartCoroutine("GrillCloseMoving");
            OpenGrillMenu = false;
        }
    }

    IEnumerator GrillOpenMoving()
    {
        yield return new WaitForSeconds(0.01f);
        if (FirebaseMainPos.localPosition.x > -100f && FishGrillPos.localPosition.x < 100f)
        {
            FirebaseMainPos.localPosition = Vector3.Lerp(FirebaseMainPos.localPosition, nextBasePos, 0.1f);
            FishGrillPos.localPosition = Vector3.Lerp(FishGrillPos.localPosition, nextFishPos, 0.1f);
            StartCoroutine("GrillOpenMoving");
        }
        else
        {
            FirebaseMainPos.localPosition = new Vector3(-100f, 0f, 0f);
            FishGrillPos.localPosition = new Vector3(100f, 0f, 0f);
            StopCoroutine("GrillOpenMoving");
            yield break;
        }
    }

    IEnumerator GrillCloseMoving()
    {
        yield return new WaitForSeconds(0.01f);
        if (FirebaseMainPos.localPosition.x < 0f && FishGrillPos.localPosition.x > 0f)
        {
            FirebaseMainPos.localPosition = Vector3.Lerp(FirebaseMainPos.localPosition, nextBasePos, 0.1f);
            FishGrillPos.localPosition = Vector3.Lerp(FishGrillPos.localPosition, nextFishPos, 0.1f);
            StartCoroutine("GrillCloseMoving");
        }
        else
        {
            FirebaseMainPos.localPosition = new Vector3(0f, 0f, 0f);
            FishGrillPos.localPosition = new Vector3(0f, 0f, 0f);
            StopCoroutine("GrillCloseMoving");
            yield break;
        }
    }
}
