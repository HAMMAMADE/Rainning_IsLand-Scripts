using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBaseManager : MonoBehaviour
{
    public MasterManager masterManager;

    public BuildObjectParm fireParm;

    float ListHeight;

    //----------------------------------
    public GameObject ListParent;

    public GameObject FishList1;

    public GameObject FishList2;

    public GameObject FishList3;

    //----------------------------------

    public void AddFireWood()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(3))
        {
            if (masterManager.InvenCheck.HaveItems[3] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.InvenCheck.DecreaseItems(3, 1);
                fireParm.Time += 30;
                masterManager.UiCheck.FireTimeCheck();
            }
            else
            {
                masterManager.soundCheck.SFXPlay("CancelSound");
                // Debug.Log("재료가 부족하다");
            }
        }
        else
        {
            masterManager.soundCheck.SFXPlay("CancelSound");
            // Debug.Log("리스트에 없는 재료다.");
        }
    }

    public void UpdateFishList(int CatchFishNum)
    {
        GameObject ListPos = GameObject.Find("GrillPos");
        switch (CatchFishNum)
        {
            case 101:
                ListPos = Instantiate(FishList1, ListPos.transform);
                ListPos.transform.parent = ListParent.transform;

                ListPos.transform.localPosition = new Vector3(0f, ListHeight, 0f);
                ListHeight -= 50f;
                break;
            case 102:
                ListPos = Instantiate(FishList2, ListPos.transform);
                ListPos.transform.parent = ListParent.transform;

                ListPos.transform.localPosition = new Vector3(0f, ListHeight, 0f);
                ListHeight -= 50f;
                break;
            case 103:
                ListPos = Instantiate(FishList3, ListPos.transform);
                ListPos.transform.parent = ListParent.transform;

                ListPos.transform.localPosition = new Vector3(0f, ListHeight, 0f);
                ListHeight -= 50f;
                break;
        }
    }

    public void UseFish1()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(101))
        {
            if (masterManager.InvenCheck.HaveItems[101] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.InvenCheck.DecreaseItems(101, 1);


                masterManager.BuffCheck.SetHpBuff(40f);
                masterManager.PlayerCheck.HealthPoint += 15f;
                masterManager.PlayerCheck.coldstack -= 2;

                if (masterManager.PlayerCheck.coldstack < 0) masterManager.PlayerCheck.coldstack = 0;

                masterManager.UiCheck.HealthCheck();
                masterManager.HazeCheck.HazeMatNumUpdate();
            }
            else
            {
                masterManager.soundCheck.SFXPlay("CancelSound");
                // Debug.Log("재료가 부족하다");
            }
        }
        else
        {
            masterManager.soundCheck.SFXPlay("CancelSound");
            // Debug.Log("리스트에 없는 재료다.");
        }
    }

    public void UseFish2()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(102))
        {
            if (masterManager.InvenCheck.HaveItems[102] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.InvenCheck.DecreaseItems(102, 1);
                masterManager.PlayerCheck.coldstack -= 2;
                if (masterManager.PlayerCheck.coldstack < 0) masterManager.PlayerCheck.coldstack = 0;

                masterManager.BuffCheck.SetStaminaBuff(120f);

                masterManager.HazeCheck.HazeMatNumUpdate();
            }
            else
            {
                masterManager.soundCheck.SFXPlay("CancelSound");
                // Debug.Log("재료가 부족하다");
            }
        }
        else
        {
            masterManager.soundCheck.SFXPlay("CancelSound");
            // Debug.Log("리스트에 없는 재료다.");
        }
    }

    public void UseFish3()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(103))
        {
            if (masterManager.InvenCheck.HaveItems[103] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.InvenCheck.DecreaseItems(103, 1);

                masterManager.PlayerCheck.HealthPoint += 100f;
                masterManager.PlayerCheck.coldstack = 0;

                masterManager.UiCheck.HealthCheck();
                masterManager.BuffCheck.SetWaterBuff(100f);
                masterManager.HazeCheck.HazeMatNumUpdate();

            }
            else
            {
                masterManager.soundCheck.SFXPlay("CancelSound");
                // Debug.Log("재료가 부족하다");
            }
        }
        else
        {
            masterManager.soundCheck.SFXPlay("CancelSound");
            // Debug.Log("리스트에 없는 재료다.");
        }
    }
}
