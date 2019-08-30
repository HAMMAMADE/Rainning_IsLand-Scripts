using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazeManager : MonoBehaviour
{
    public MasterManager masterManager;

    public HazeParameter Hazeparm;

    public GameObject HazeUI;

    Vector3 nextHazePos;

    public GameObject HazeMainUI;

    Vector3 nextHazeMain;

    public GameObject HazeMatUI;

    Vector3 nextHazeMat;

    bool HazeMaton = false;

    bool hazeOn;

    int btnNum;
    //-------------------------------------

    BoxCollider HideMatBtn1;

    BoxCollider HideMatBtn2;

    BoxCollider HideMatBtn3;

    //-------------------------------------

    public UISprite HazeSlot1;

    public UISprite HazeSlot2;

    public UISprite HazeSlot3;


    UILabel Fish1Num;

    UILabel Fish2Num;

    UILabel Fish3Num;
    //-------------------------------------

    public UISprite SlotTimeBar1;

    public UISprite SlotTimeBar2;

    public UISprite SlotTimeBar3;

    Vector3 nextTimeBar1;

    Vector3 nextTimeBar2;

    Vector3 nextTimeBar3;

    //-------------------------------------

    public GameObject HazeListParent;

    public GameObject HazeFishList1;

    public GameObject HazeFishList2;

    public GameObject HazeFishList3;

    float ListWidth;

    //-----------------------------------------------------
    public GameObject OutMaterial;
    public GameObject ItemEffect;
    public GameObject DropTarget;
    public Transform DropTargetPos;
    public Transform StopTargetPos;

    private void Start()
    {
        nextTimeBar1 = SlotTimeBar1.transform.localScale;
        nextTimeBar2 = SlotTimeBar2.transform.localScale;
        nextTimeBar3 = SlotTimeBar3.transform.localScale;
    }
    IEnumerator TimeBarMove()
    {
        yield return null;
        nextTimeBar1.x = Hazeparm.Slot1Time / Hazeparm.Slot1TimeFirst;
        SlotTimeBar1.transform.localScale = nextTimeBar1;

        nextTimeBar2.x = Hazeparm.Slot2Time / Hazeparm.Slot2TimeFirst;
        SlotTimeBar2.transform.localScale = nextTimeBar2;

        nextTimeBar3.x = Hazeparm.Slot3Time / Hazeparm.Slot3TimeFirst;
        SlotTimeBar3.transform.localScale = nextTimeBar3;

        StartCoroutine("TimeBarMove");
    }

    public void HazeMatNumUpdate()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(101))
        {
            Fish1Num = GameObject.Find("Fish1Num").GetComponent<UILabel>();
            Fish1Num.text = masterManager.InvenCheck.HaveItems[101].ToString();

            HideMatBtn1 = GameObject.Find("HazeBtn1(Clone)").GetComponent<BoxCollider>();
        }
        if (masterManager.InvenCheck.HaveItems.ContainsKey(102))
        {
            Fish2Num = GameObject.Find("Fish2Num").GetComponent<UILabel>();
            Fish2Num.text = masterManager.InvenCheck.HaveItems[102].ToString();
            HideMatBtn2 = GameObject.Find("HazeBtn2(Clone)").GetComponent<BoxCollider>();
        }
        if (masterManager.InvenCheck.HaveItems.ContainsKey(103))
        {
            Fish3Num = GameObject.Find("Fish3Num").GetComponent<UILabel>();
            Fish3Num.text = masterManager.InvenCheck.HaveItems[103].ToString();
            HideMatBtn3 = GameObject.Find("HazeBtn3(Clone)").GetComponent<BoxCollider>();
        }
    }

    public void HazeMatUIOff()
    {
        nextHazeMat = new Vector3(0f, -40f, 0f);
        nextHazeMain = new Vector3(0f, 0f, 0f);
        StartCoroutine("HazeMatOffMove");
        HazeMaton = false;
        btnNum = 0;
    }

    public void HazeMatUI1On()
    {
        if (!Hazeparm.InFish1)
        {
            if (!HazeMaton)
            {
                nextHazeMat = new Vector3(0f, -85f, 0f);
                nextHazeMain = new Vector3(0f, 35f, 0f);
                OnMatCollider();
                StartCoroutine("HazeMatOnMove");
                HazeMaton = true;
            }
            else
            {
                nextHazeMat = new Vector3(0f, -40f, 0f);
                nextHazeMain = new Vector3(0f, 0f, 0f);
                HideMatCollider();
                StartCoroutine("HazeMatOffMove");
                HazeMaton = false;
            }
            btnNum = 1;
        }
        if (Hazeparm.CompFish1)
        {
            masterManager.soundCheck.SFXPlay("ClickSound");
            DropItems();
            Hazeparm.CompFish1 = false;
            Hazeparm.InFish1 = false;
            Hazeparm.SlotImg1 = "Fish-Null";
            UpdateSlot();

            masterManager.ArchiveCheck.HaveDryFish += 1;
            MainArchiveManager.StaticHaveDryFish += 1;
            masterManager.ArchiveCheck.CheckArchive();
        }
    }

    public void HazeMatUI2On()
    {
        if (!Hazeparm.InFish2)
        {
            if (!HazeMaton)
            {
                nextHazeMat = new Vector3(0f, -85f, 0f);
                nextHazeMain = new Vector3(0f, 35f, 0f);
                OnMatCollider();
                StartCoroutine("HazeMatOnMove");
                HazeMaton = true;
            }
            else
            {
                nextHazeMat = new Vector3(0f, -40f, 0f);
                nextHazeMain = new Vector3(0f, 0f, 0f);
                HideMatCollider();
                StartCoroutine("HazeMatOffMove");
                HazeMaton = false;
            }
            btnNum = 2;
        }
        if (Hazeparm.CompFish2)
        {
            masterManager.soundCheck.SFXPlay("ClickSound");
            DropItems();
            Hazeparm.CompFish2 = false;
            Hazeparm.InFish2 = false;
            Hazeparm.SlotImg2 = "Fish-Null";
            UpdateSlot();

            masterManager.ArchiveCheck.HaveDryFish += 1;
            MainArchiveManager.StaticHaveDryFish += 1;
            masterManager.ArchiveCheck.CheckArchive();
        }
    }

    public void HazeMatUI3On()
    {
        if (!Hazeparm.InFish3)
        {
            if (!HazeMaton)
            {
                nextHazeMat = new Vector3(0f, -85f, 0f);
                nextHazeMain = new Vector3(0f, 35f, 0f);
                OnMatCollider();
                StartCoroutine("HazeMatOnMove");
                HazeMaton = true;
            }
            else
            {
                nextHazeMat = new Vector3(0f, -40f, 0f);
                nextHazeMain = new Vector3(0f, 0f, 0f);
                HideMatCollider();
                StartCoroutine("HazeMatOffMove");
                HazeMaton = false;
            }
            btnNum = 3;
        }
        if (Hazeparm.CompFish3)
        {
            masterManager.soundCheck.SFXPlay("ClickSound");
            DropItems();
            Hazeparm.CompFish3 = false;
            Hazeparm.InFish3 = false;
            Hazeparm.SlotImg3 = "Fish-Null";
            UpdateSlot();

            masterManager.ArchiveCheck.HaveDryFish += 1;
            MainArchiveManager.StaticHaveDryFish += 1;

            masterManager.ArchiveCheck.CheckArchive();
        }
    }

    IEnumerator HazeMatOnMove()
    {
        yield return new WaitForSeconds(0.01f);

        if (HazeMatUI.transform.localPosition.y > -85f)
        {
            HazeMatUI.transform.localPosition = Vector3.Lerp(HazeMatUI.transform.localPosition, nextHazeMat, 0.1f);
            HazeMainUI.transform.localPosition = Vector3.Lerp(HazeMainUI.transform.localPosition, nextHazeMain, 0.1f);
            StartCoroutine("HazeMatOnMove");
        }
        else
        {
            HazeMatUI.transform.localPosition = new Vector3(0f, -85f, 0f);
            HazeMainUI.transform.localPosition = new Vector3(0f, 35f, 0f);
            StopCoroutine("HazeMatOnMove");
            yield break;
        }
    }

    IEnumerator HazeMatOffMove()
    {
        yield return new WaitForSeconds(0.01f);

        if (HazeMatUI.transform.localPosition.y < -40f)
        {
            HazeMatUI.transform.localPosition = Vector3.Lerp(HazeMatUI.transform.localPosition, nextHazeMat, 0.1f);
            HazeMainUI.transform.localPosition = Vector3.Lerp(HazeMainUI.transform.localPosition, nextHazeMain, 0.1f);
            StartCoroutine("HazeMatOffMove");
        }
        else
        {
            HazeMatUI.transform.localPosition = new Vector3(0f, -40f, 0f);
            HazeMainUI.transform.localPosition = new Vector3(0f, 0f, 0f);
            StopCoroutine("HazeMatOffMove");
            yield break;
        }
    }

    //-----------------------------------------------------
    public void HideMatCollider()
    {
        if (HideMatBtn1)
        {
            HideMatBtn1.enabled = false;
        }
        if (HideMatBtn2)
        {
            HideMatBtn2.enabled = false;
        }
        if (HideMatBtn3)
        {
            HideMatBtn3.enabled = false;
        }
    }

    public void OnMatCollider()
    {
        if (HideMatBtn1)
        {
            HideMatBtn1.enabled = true;
        }
        if (HideMatBtn2)
        {
            HideMatBtn2.enabled = true;
        }
        if (HideMatBtn3)
        {
            HideMatBtn3.enabled = true;
        }
    }

    public void HazeUIOn()
    {
        nextHazePos = new Vector3(-300f, 55f, 0f);
        UpdateSlot();
        HideMatCollider();
        hazeOn = true;
        StartCoroutine("HazeOnMove");
        StartCoroutine("TimeBarMove");
    }

    public void UpdateSlot()
    {
        HazeSlot1.spriteName = Hazeparm.SlotImg1;
        HazeSlot2.spriteName = Hazeparm.SlotImg2;
        HazeSlot3.spriteName = Hazeparm.SlotImg3;
    }


    IEnumerator HazeOnMove()
    {
        yield return new WaitForSeconds(0.01f);
        if(HazeUI.transform.localPosition.y > 55f)
        {
            HazeUI.transform.localPosition = Vector3.Lerp(HazeUI.transform.localPosition, nextHazePos, 0.1f);
            StartCoroutine("HazeOnMove");
        }
        else
        {
            HazeUI.transform.localPosition = new Vector3(-300f, 55f, 0f);
            StopCoroutine("HazeOnMove");
            yield break;
        }
    }

    public void HazeUIOff()
    {
        nextHazePos = new Vector3(-300f, 600f, 0f);
        HazeMatUIOff();
        HideMatCollider();
        hazeOn = false;
        StartCoroutine("HazeOffMove");
        StopCoroutine("TimeBarMove");
    }

    IEnumerator HazeOffMove()
    {
        yield return new WaitForSeconds(0.01f);

        if (HazeUI.transform.localPosition.y < 600f)
        {
            HazeUI.transform.localPosition = Vector3.Lerp(HazeUI.transform.localPosition, nextHazePos, 0.1f);
            StartCoroutine("HazeOffMove");
        }
        else
        {
            HazeUI.transform.localPosition = new Vector3(-300f, 600f, 0f);
            StopCoroutine("HazeOffMove");
            yield break;
        }
    }
    //------------------------------------------------------
    public void UpdateFishHazeList(int CatchFishNum)
    {
        GameObject ListPos = GameObject.Find("HazeListPos");
        switch (CatchFishNum)
        {
            case 101:
                ListPos = Instantiate(HazeFishList1, ListPos.transform);
                ListPos.transform.parent = HazeListParent.transform;

                ListPos.transform.localPosition = new Vector3(ListWidth, 0f, 0f);
                ListWidth += 75f;
                break;
            case 102:
                ListPos = Instantiate(HazeFishList2, ListPos.transform);
                ListPos.transform.parent = HazeListParent.transform;

                ListPos.transform.localPosition = new Vector3(ListWidth, 0f, 0f);
                ListWidth += 75f;
                break;
            case 103:
                ListPos = Instantiate(HazeFishList3, ListPos.transform);
                ListPos.transform.parent = HazeListParent.transform;

                ListPos.transform.localPosition = new Vector3(ListWidth, 0f, 0f);
                ListWidth += 75f;
                break;
        }
    }

    //-------------------------------------------

    public void pushMat1Button()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(101))
        {
            if (masterManager.InvenCheck.HaveItems[101] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                switch (btnNum)
                {
                    case 1:
                        Hazeparm.SlotImg1 = "Fish-A";
                        Hazeparm.SetTime1(20f);
                        break;
                    case 2:
                        Hazeparm.SlotImg2 = "Fish-A";
                        Hazeparm.SetTime2(20f);
                        break;
                    case 3:
                        Hazeparm.SlotImg3 = "Fish-A";
                        Hazeparm.SetTime3(20f);
                        break;
                }
                masterManager.InvenCheck.DecreaseItems(101, 1);
                UpdateSlot();
                HazeMatNumUpdate();
                HazeMatUIOff();
            }
            else masterManager.soundCheck.SFXPlay("CancelSound");
        }
        else masterManager.soundCheck.SFXPlay("CancelSound");

    }

    public void pushMat2Button()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(102))
        {
            if (masterManager.InvenCheck.HaveItems[102] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                switch (btnNum)
                {
                    case 1:
                        Hazeparm.SlotImg1 = "Fish-B";
                        Hazeparm.SetTime1(10f);
                        break;
                    case 2:
                        Hazeparm.SlotImg2 = "Fish-B";
                        Hazeparm.SetTime2(10f);
                        break;
                    case 3:
                        Hazeparm.SlotImg3 = "Fish-B";
                        Hazeparm.SetTime3(10f);
                        break;
                }
                masterManager.InvenCheck.DecreaseItems(102, 1);
                UpdateSlot();
                HazeMatNumUpdate();
                HazeMatUIOff();
            }
            else masterManager.soundCheck.SFXPlay("CancelSound");
        }
        else masterManager.soundCheck.SFXPlay("CancelSound");
    }

    public void pushMat3Button()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(103))
        {
            if (masterManager.InvenCheck.HaveItems[103] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                switch (btnNum)
                {
                    case 1:
                        Hazeparm.SlotImg1 = "Fish-C";
                        Hazeparm.SetTime1(30f);
                        break;
                    case 2:
                        Hazeparm.SlotImg2 = "Fish-C";
                        Hazeparm.SetTime2(30f);
                        break;
                    case 3:
                        Hazeparm.SlotImg3 = "Fish-C";
                        Hazeparm.SetTime3(30f);
                        break;
                }
                masterManager.InvenCheck.DecreaseItems(103, 1);
                UpdateSlot();
                HazeMatNumUpdate();
                HazeMatUIOff();
            }
            else masterManager.soundCheck.SFXPlay("CancelSound");
        }
        else masterManager.soundCheck.SFXPlay("CancelSound");
    }

    public void DropItems()
    {
        StartCoroutine("PopUpItem");
        masterManager.InvenCheck.GetItems(199, 1);
    }


    IEnumerator PopUpItem()
    {
        yield return new WaitForSeconds(1f);
        masterManager.soundCheck.SFXPlay("ItemPopUp");
        DropTarget = Instantiate(OutMaterial, DropTargetPos);
        Instantiate(ItemEffect, DropTargetPos);
        StartCoroutine(PopUpItemMov(DropTarget));
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PopUpItemMov(GameObject Item)
    {
        yield return new WaitForSeconds(0.01f);
        if (Item.transform.localPosition.x < 536)
        {
            Item.transform.localPosition = Vector3.Lerp(Item.transform.localPosition, StopTargetPos.localPosition, 0.1f);
            StartCoroutine(PopUpItemMov(Item));
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("ItemEffect"));
            Item.transform.localPosition = StopTargetPos.localPosition;
            StopCoroutine(PopUpItemMov(Item));
            Destroy(Item);
            yield break;
        }
    }
}
