using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainArchiveManager : MonoBehaviour
{
    public MasterManager masterManager;

    public GameObject MessageUI;

    Vector3 nextMessage;

    public Transform EffectPos;

    public UILabel Messagetext;

    public GameObject PopEffect;

    //----------------------------------

    public GameObject ArchiveUI;

    Vector3 nextArchive;

    public UILabel AchiveName;

    public UISprite ArchiveIcon;

    string AchiveText;

    //-----------------------------------
    public bool LookedEnd = false;

    public int FishingCount;

    public int FellingCount;

    public int HaveDryFish;

    //-----------------------------------
    public static int StaticDay;

    public static int StaticFishingCount;

    public static int StaticFellingCount;

    public static int StaticHaveDryFish;

    public static int StaticWolfCount;

    public static float RunningTime;

    public static float IsLandHeight;

    public void CheckArchive()
    {
        if (FishingCount > 0 && !AchiveManager.Achivements1)
        {
            PopUpMessage("낚시의시작");
            AchiveManager.UpdateAchive(true, 1);
        }

        if (LookedEnd && !AchiveManager.Achivements2)
        {
            PopUpMessage("케스트어웨이");
            AchiveManager.UpdateAchive(true, 2);
        }

        if (HaveDryFish >= 20 && !AchiveManager.Achivements3)
        {
            PopUpMessage("굴비 한두름");
            AchiveManager.UpdateAchive(true, 3);
        }

        if (FellingCount > 45 && !AchiveManager.Achivements4)
        {
            PopUpMessage("타고난 나무꾼");
            AchiveManager.UpdateAchive(true, 4);
        }

        if (FishingCount > 45 && !AchiveManager.Achivements5)
        {
            PopUpMessage("낚시 익스퍼트");
            AchiveManager.UpdateAchive(true, 5);
        }

        if (FellingCount > 100 && !AchiveManager.Achivements6)
        {
            PopUpMessage("환경 파괴왕");
            AchiveManager.UpdateAchive(true, 6);
        }

    }

    public void PopUpMessage(string name)
    {
        AchiveText = name;

        nextMessage = new Vector3(0f, -500f, 0f);
        MessageUI.transform.localPosition = nextMessage;

        StopCoroutine("MessageClose");
        StopCoroutine("MessagePop");

        nextMessage = new Vector3(0f, -340f, 0f);
        masterManager.soundCheck.SFXPlay("AchiveSound");
        Instantiate(PopEffect, EffectPos);
        StartCoroutine("MessagePop");
    }

    IEnumerator MessagePop()
    {
        yield return new WaitForSeconds(0.01f);

        if (MessageUI.transform.localPosition.y < -345f)
        {
            MessageUI.transform.localPosition = Vector3.Lerp(MessageUI.transform.localPosition, nextMessage, 0.1f);
            StartCoroutine("MessagePop");
        }
        else
        {
            //Debug.Log("창닫기");
            MessageUI.transform.localPosition = nextMessage;
            yield return new WaitForSeconds(2f);
            PopDownMessage();
            StopCoroutine("MessagePop");
        }
    }

    public void PopDownMessage()
    {
        nextMessage = new Vector3(0f, -500f, 0f);
        Destroy(GameObject.FindGameObjectWithTag("ItemEffect"));
        StartCoroutine("MessageClose");
    }

    IEnumerator MessageClose()
    {
        yield return new WaitForSeconds(0.01f);

        if (MessageUI.transform.localPosition.y > -495f)
        {
            MessageUI.transform.localPosition = Vector3.Lerp(MessageUI.transform.localPosition, nextMessage, 0.1f);
            StartCoroutine("MessageClose");
        }
        else
        {
            MessageUI.transform.localPosition = nextMessage;
            PopUpArchive();
            StopCoroutine("MessageClose");
        }
    }

    //----------------------------------------
    public void PopUpArchive()
    {
        AchiveName.text = AchiveText;

        switch (AchiveText)
        {
            case "낚시의시작"://1
                ArchiveIcon.spriteName = "ArchiveIcon1";
                break;

            case "케스트어웨이"://2
                ArchiveIcon.spriteName = "ArchiveIcon2";
                break;

            case "굴비 한두름"://3
                ArchiveIcon.spriteName = "ArchiveIcon3";
                break;

            case "타고난 나무꾼"://4
                ArchiveIcon.spriteName = "ArchiveIcon4";
                break;

            case "낚시 익스퍼트"://5
                ArchiveIcon.spriteName = "ArchiveIcon5";
                break;

            case "환경 파괴왕"://6
                ArchiveIcon.spriteName = "ArchiveIcon6";
                break;



        }

        nextArchive = new Vector3(0f, -500f, 0f);
        ArchiveUI.transform.localPosition = nextArchive;

        StopCoroutine("ArchiveClose");
        StopCoroutine("ArchivePop");

        nextArchive = new Vector3(0f, -300f, 0f);
        masterManager.soundCheck.SFXPlay("AchiveSound");
        //  Instantiate(PopEffect, EffectPos);
        StartCoroutine("ArchivePop");
    }

    IEnumerator ArchivePop()
    {
        yield return new WaitForSeconds(0.01f);

        if (ArchiveUI.transform.localPosition.y < -305f)
        {
            ArchiveUI.transform.localPosition = Vector3.Lerp(ArchiveUI.transform.localPosition, nextArchive, 0.1f);
            StartCoroutine("ArchivePop");
        }
        else
        {
            //Debug.Log("창닫기");
            ArchiveUI.transform.localPosition = nextArchive;
            yield return new WaitForSeconds(3f);
            PopDownArchive();
            StopCoroutine("ArchivePop");
        }
    }
    public void PopDownArchive()
    {
        nextArchive = new Vector3(0f, -500f, 0f);
        //Destroy(GameObject.FindGameObjectWithTag("ItemEffect"));
        StartCoroutine("ArchiveClose");
    }

    IEnumerator ArchiveClose()
    {
        yield return new WaitForSeconds(0.01f);

        if (ArchiveUI.transform.localPosition.y > -495f)
        {
            ArchiveUI.transform.localPosition = Vector3.Lerp(ArchiveUI.transform.localPosition, nextArchive, 0.1f);
            StartCoroutine("ArchiveClose");
        }
        else
        {
            ArchiveUI.transform.localPosition = nextArchive;
            StopCoroutine("ArchiveClose");
        }
    }

}

