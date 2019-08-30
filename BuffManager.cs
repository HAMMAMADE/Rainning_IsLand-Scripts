using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public MasterManager masterManager;

    public bool inWaterBreath;

    public bool itHaveCold;

    public float StaminaCover;

    public float HpCover;

    public float BuffTime1;

    public float BuffTime2;

    public float BuffTime3;

    public float Debuff1Effect1;

    private void Start()
    {
        inWaterBreath = false;

        StaminaCover = 0f;

        HpCover = 0f;
    }

    public void SetDebuff1()
    {
        itHaveCold = true;
        masterManager.UiCheck.DeBuff1On();
        Debuff1Effect1 = 1.5f;
    }

    public void OffDebuff1()
    {
        itHaveCold = false;
        masterManager.UiCheck.DeBuff1Off();
        Debuff1Effect1 = 0f;
    }

    public void SetWaterBuff(float Time)
    {
        inWaterBreath = true;

        BuffTime1 = Time;
        masterManager.UiCheck.Buff1On();
        masterManager.UiCheck.Buff1Time();

        StartCoroutine("WaterTimeCheck");
    }

    IEnumerator WaterTimeCheck()
    {
        yield return new WaitForSecondsRealtime(1f);
        BuffTime1 -= 1f;
        if (BuffTime1 <= 0f)
        {
            inWaterBreath = false;
            masterManager.UiCheck.Buff1Off();
            
            StopCoroutine("WaterTimeCheck");
        }
        else
        {
            masterManager.UiCheck.Buff1Time();
            StartCoroutine("WaterTimeCheck");
        }
    }

    public void SetStaminaBuff(float Time)
    {
        StaminaCover = 0.75f;

        BuffTime2 = Time;
        masterManager.UiCheck.Buff2On();
        masterManager.UiCheck.Buff2Time();

        StartCoroutine("StaminaTimeCheck");
    }

    IEnumerator StaminaTimeCheck()
    {
        yield return new WaitForSecondsRealtime(1f);
        BuffTime2 -= 1f;
        if (BuffTime2 <= 0f)
        {
            StaminaCover = 0f;
            masterManager.UiCheck.Buff2Off();
            
            StopCoroutine("StaminaTimeCheck");
        }
        else
        {
            masterManager.UiCheck.Buff2Time();
            StartCoroutine("StaminaTimeCheck");
        }
    }

    public void SetHpBuff(float Time)
    {
        HpCover = 0.05f;

        BuffTime3 = Time;
        masterManager.UiCheck.Buff3On();
        masterManager.UiCheck.Buff3Time();

        StartCoroutine("HpTimeCheck");
    }

    IEnumerator HpTimeCheck()
    {
        yield return new WaitForSecondsRealtime(1f);
        BuffTime3 -= 1f;
        if (BuffTime3 <= 0f)
        {
            HpCover = 0f;
            masterManager.UiCheck.Buff3Off();
            StopCoroutine("HpTimeCheck");
        }
        else
        {
            masterManager.UiCheck.Buff3Time();
            StartCoroutine("HpTimeCheck");
        }
    }
}
