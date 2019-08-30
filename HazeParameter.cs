using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazeParameter : MonoBehaviour
{
    MasterManager masterManager;

    //----------------------------
    public string SlotImg1;

    public string SlotImg2;

    public string SlotImg3;

    public float Slot1TimeFirst;

    public float Slot2TimeFirst;

    public float Slot3TimeFirst;

    public float Slot1Time;

    public float Slot2Time;

    public float Slot3Time;
    //-----------------------------
    public bool CompFish1;

    public bool CompFish2;

    public bool CompFish3;

    public bool InFish1;

    public bool InFish2;

    public bool InFish3;
    //-----------------------------
    private void Start()
    {
        SlotImg1 = "Fish-Null";
        SlotImg2 = "Fish-Null";
        SlotImg3 = "Fish-Null";

        Slot1TimeFirst = 1;
        Slot2TimeFirst = 1;
        Slot3TimeFirst = 1;

        masterManager = GameObject.Find("MasterManager").GetComponent<MasterManager>();
    }

    public void SetTime1(float Time)
    {
        Slot1TimeFirst = Time;
        Slot1Time = Time;
        InFish1 = true;
        StartCoroutine("Time1Flow");
    }

    IEnumerator Time1Flow()
    {
        yield return new WaitForSecondsRealtime(1f);
        Slot1Time -= 1f;
        if (Slot1Time > 0)
        {
            StartCoroutine("Time1Flow");
        }
        else
        {
            SlotImg1 = "Fish-Z";
            masterManager.HazeCheck.UpdateSlot();
            CompFish1 = true;
            StopCoroutine("Time1Flow");
        }
    }

    public void SetTime2(float Time)
    {
        Slot2TimeFirst = Time;
        Slot2Time = Time;
        InFish2 = true;
        StartCoroutine("Time2Flow");
    }

    IEnumerator Time2Flow()
    {
        yield return new WaitForSecondsRealtime(1f);
        Slot2Time -= 1f;
        if (Slot2Time > 0)
        {
            StartCoroutine("Time2Flow");
        }
        else
        {
            SlotImg2 = "Fish-Z";
            masterManager.HazeCheck.UpdateSlot();
            CompFish2 = true;
            StopCoroutine("Time2Flow");
        }
    }

    public void SetTime3(float Time)
    {
        Slot3TimeFirst = Time;
        Slot3Time = Time;
        InFish3 = true;
        StartCoroutine("Time3Flow");
    }

    IEnumerator Time3Flow()
    {
        yield return new WaitForSecondsRealtime(1f);
        Slot3Time -= 1f;
        if (Slot3Time > 0)
        {
            StartCoroutine("Time3Flow");
        }
        else
        {
            SlotImg3 = "Fish-Z";
            masterManager.HazeCheck.UpdateSlot();
            CompFish3 = true;
            StopCoroutine("Time3Flow");
        }
    }

}
