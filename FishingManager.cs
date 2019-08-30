using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public MasterManager masterManager;

    public UISprite LureSprite;

    public GameObject BaitedZone;

    public GameObject BaitedTarget;

    Vector3 LureNextPos;

    int LureMovCase;

    public bool nowVibing;

    int VibCount, VibRange, downCount, upCount;

    int FishNum;

    float GetPercentage;

    public float PlusPercentage;

    //---------------------------------------

    int GetFishNum;
    
    public GameObject ItemEffect;

    public GameObject OutUIFish1;

    public GameObject OutUIFish2;

    public GameObject OutUIFish3;

    public GameObject OutUIFish4;

    public GameObject DropTarget;

    public Transform DropTargetPos;

    public Transform StopTargetPos;

    private void Start()
    {
        FishNum = 0;
        VibCount = 0;
        downCount = 0;
        upCount = 0;

        LureNextPos = LureSprite.transform.localPosition;
        nowVibing = false;
    }

    public void BaitingZoneSpone(Transform target)
    {
        Destroy(GameObject.FindGameObjectWithTag("Baited"));

       // Debug.Log(target);

        BaitedTarget.transform.position = target.position;

        Instantiate(BaitedZone, BaitedTarget.transform);
    }

    public void StartFishing()
    {
        LureNextPos = new Vector3(1.3f, 35f, 0f);
        LureSprite.transform.localPosition = LureNextPos;


        downCount = 0;
        upCount = 0;

        nowVibing = false;
        VibCount = 0;

        StopCoroutine("LureVibrating");
        StopCoroutine("LureDiving");

        StartCoroutine("LureDownMoving");
    }

    public void StopFishing()
    {
        StopCoroutine("LureUpMoving");
        StopCoroutine("LureDownMoving");
        StopCoroutine("LureVibrating");
        StopCoroutine("LureDiving");
    }

    public void CheckCatch()
    {
        if (nowVibing)
        {
            FishNum = Random.Range(0, 7);

            int FishPercent;
            switch (FishNum)
            {
                case 0:
                    GetFishNum = 101;
                    FishPercent = FishNum;
                    break;
                case 1:
                    GetFishNum = 101;
                    FishPercent = FishNum;
                    FishPercent = 0;
                    break;
                case 2:
                    GetFishNum = 101;
                    FishPercent = FishNum;
                    FishPercent = 0;
                    break;
                case 3:
                    GetFishNum = 102;
                    FishPercent = FishNum;
                    FishPercent = 1;
                    break;
                case 4:
                    GetFishNum = 103;
                    FishPercent = FishNum;
                    FishPercent = 2;
                    break;
                default:
                    GetFishNum = 104;
                    FishPercent = 0;
                    break;
                
            }
            GetPercentage = Random.Range(1f, 100f);
            GetPercentage = (GetPercentage / (FishPercent + 1)) + PlusPercentage;
            if (GetPercentage > 15)
            {
                masterManager.soundCheck.SFXPlay("GetFishSound");
                masterManager.PlayerCheck.RodDurability -= 2f;
                masterManager.UiCheck.RodUpdateDur();
                Dropfishs();
            }
            else
            {
                masterManager.PlayerCheck.RodDurability -= 4f;
                masterManager.soundCheck.SFXPlay("FailSound");
                masterManager.UiCheck.RodUpdateDur();
            }
        }
        else
        {
            masterManager.PlayerCheck.RodDurability -= 2f;
            masterManager.soundCheck.SFXPlay("FailSound");
            masterManager.UiCheck.RodUpdateDur();
        }
    }

    IEnumerator LureDownMoving()
    {
        yield return new WaitForSeconds(0.1f);
        if (downCount < Random.Range(40, 110))
        {
            LureNextPos.y -= 0.5f;
            LureSprite.transform.localPosition = LureNextPos;
            downCount += 1;
            StartCoroutine("LureDownMoving");
        }
        else
        {
            StartCoroutine("LureUpMoving");
            StopCoroutine("LureDownMoving");
            yield break;
        }

    }

    IEnumerator LureUpMoving()
    {
        yield return new WaitForSeconds(0.1f);
        if (upCount < Random.Range(40, 110))
        {
            LureNextPos.y += 0.5f;
            LureSprite.transform.localPosition = LureNextPos;
            upCount += 1;
            StartCoroutine("LureUpMoving");
        }
        else
        {
            CheckFishingCase();
            StopCoroutine("LureUpMoving");
            yield break;
        }

    }

    public void CheckFishingCase()
    {
        int CaseNum = 8 - masterManager.PlayerCheck.FishingTime;

        if (CaseNum <= 0)
        {
            LureMovCase = 2;
        }
        else
        {
            LureMovCase = Random.Range(0, CaseNum);
        }
      

        switch (LureMovCase)
        {
            case 1:
                VibCount = Random.Range(0, 21);
                nowVibing = false;
                StopCoroutine("LureVibrating");
                StopCoroutine("LureUpMoving");
                StartCoroutine("LureDownMoving");
                break;

            case 2:
                nowVibing = true;
                VibRange = Random.Range(39, 81);
                StartCoroutine("LureVibrating");
                StopCoroutine("LureUpMoving");
                StopCoroutine("LureDownMoving");
                break;

            default:
                VibCount = Random.Range(0, 21);
                nowVibing = false;
                StopCoroutine("LureVibrating");
                StopCoroutine("LureUpMoving");
                StartCoroutine("LureDownMoving");
                break;
        }
    }

    IEnumerator LureVibrating()
    {
        yield return new WaitForSeconds(0.1f);

        LureNextPos.y -= 5f;

        LureSprite.transform.localPosition = LureNextPos;

        yield return new WaitForSeconds(0.1f);

        LureNextPos.y += 5f;

        LureSprite.transform.localPosition = LureNextPos;

        if (VibCount < VibRange)
        {
            VibCount += 1;
            StartCoroutine("LureVibrating");
        }
        else
        {
            LureMovCase = 0;
            StartCoroutine("LureDownMoving");
            StopCoroutine("LureVibrating");

            yield break;
        }

    }


    IEnumerator LureDiving()
    {
        yield return new WaitForSeconds(0.1f);

        LureNextPos.y -= 20f;
        LureSprite.transform.localPosition = LureNextPos;
        yield return new WaitForSeconds(5f);
        //StartCoroutine("LureDiving");
        StartCoroutine("LureUpMoving");
    }


    public void Dropfishs()
    {
        StartCoroutine(PopUpfish(GetFishNum));
        masterManager.InvenCheck.GetItems(GetFishNum, 1);
        masterManager.HazeCheck.HazeMatNumUpdate();
    }

    IEnumerator PopUpfish(int FishNum)
    {
        switch (FishNum)
        {
            case 101:
                masterManager.soundCheck.SFXPlay("ItemPopUp");
                DropTarget = Instantiate(OutUIFish1, DropTargetPos);
                break;
            case 102:
                masterManager.soundCheck.SFXPlay("ItemPopUp");
                DropTarget = Instantiate(OutUIFish2, DropTargetPos);
                break;
            case 103:
                masterManager.soundCheck.SFXPlay("SpecialItemPop");
                DropTarget = Instantiate(OutUIFish3, DropTargetPos);
                break;
            case 104:
                masterManager.soundCheck.SFXPlay("ItemPopUp");
                DropTarget = Instantiate(OutUIFish4, DropTargetPos);
                break;
        }

        Instantiate(ItemEffect, DropTargetPos);
        StartCoroutine(PopUpfishMov(DropTarget));
        yield return new WaitForSeconds(1f);

    }

    IEnumerator PopUpfishMov(GameObject Item)
    {
        yield return new WaitForSeconds(0.01f);
        if (Item.transform.localPosition.x < 536)
        {
            Item.transform.localPosition = Vector3.Lerp(Item.transform.localPosition, StopTargetPos.localPosition, 0.1f);
            StartCoroutine(PopUpfishMov(Item));
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("ItemEffect"));
            Item.transform.localPosition = StopTargetPos.localPosition;
            StopCoroutine(PopUpfishMov(Item));
            Destroy(Item);
            yield break;
        }
    }
}
