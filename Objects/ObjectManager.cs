using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public MasterManager masterManager;

    public ObjectParmeter TargetObeject;

    string objectName;

    //-------------------------------------

    public GameObject FellPowerGuage;

    Vector3 nextPower;

    float Power;

    bool MoveL;

    private void Start()
    {
        nextPower = FellPowerGuage.transform.localPosition;
        MoveL = false;
    }

    public string ShowName(Collider Target)
    {
        objectName = Target.GetComponent<ObjectParmeter>().ObjectName;

        TargetObeject = Target.GetComponentInParent<ObjectParmeter>();

        return objectName;
    }

    public void StartFelling()
    {
        Power = Random.Range(4, 7);
        nextPower.x = Random.Range(-77, 77);
        FellPowerGuage.transform.localPosition = nextPower;

        StopCoroutine("FellingPower");
        StartCoroutine("FellingPower");
    }
    public void StopFelling()
    {
        if ((FellPowerGuage.transform.localPosition.x > -35f && FellPowerGuage.transform.localPosition.x < -12f)||(FellPowerGuage.transform.localPosition.x > 12f && FellPowerGuage.transform.localPosition.x < 35f))
        {
            TargetObeject.ObjectHealth -= (3+masterManager.PlayerCheck.AddPower);
            if (TargetObeject.ObjectHealth <= 0)
            {
                TargetObeject.ObjectAnim.SetTrigger("ObjectDestroy");
                TargetObeject.ObjectDestroy();
                TargetObeject.DropItems();
                StopCoroutine("FellingPower");
                TargetObeject.OffColider();

                masterManager.ArchiveCheck.FellingCount += 1;
                MainArchiveManager.StaticFellingCount += 1;

                masterManager.ArchiveCheck.CheckArchive();
                masterManager.PlayerCheck.keyCount = 0;
                masterManager.PlayerCheck.isInterAct = false;

                masterManager.UiCheck.EndFellUI();

            }
        }
        else if (FellPowerGuage.transform.localPosition.x >= -12f && FellPowerGuage.transform.localPosition.x <= 12f)
        {
            TargetObeject.ObjectHealth -= (5+masterManager.PlayerCheck.AddPower);
            if (TargetObeject.ObjectHealth <= 0)
            {
                TargetObeject.ObjectAnim.SetTrigger("ObjectDestroy");
                TargetObeject.DropItems();
                TargetObeject.ObjectDestroy();
                StopCoroutine("FellingPower");
                TargetObeject.OffColider();

                masterManager.ArchiveCheck.FellingCount += 1;
                MainArchiveManager.StaticFellingCount += 1;


                masterManager.ArchiveCheck.CheckArchive();
                masterManager.PlayerCheck.keyCount = 0;
                masterManager.PlayerCheck.isInterAct = false;
                masterManager.UiCheck.EndFellUI();
            }
        }
        else
        {
            TargetObeject.ObjectHealth -= (1+masterManager.PlayerCheck.AddPower);
            if (TargetObeject.ObjectHealth <= 0)
            {
                TargetObeject.ObjectAnim.SetTrigger("ObjectDestroy");
                TargetObeject.ObjectDestroy();
                TargetObeject.DropItems();
                StopCoroutine("FellingPower");
                TargetObeject.OffColider();

                masterManager.ArchiveCheck.FellingCount += 1;
                MainArchiveManager.StaticFellingCount += 1;

                masterManager.ArchiveCheck.CheckArchive();
                masterManager.PlayerCheck.keyCount = 0;
                masterManager.PlayerCheck.isInterAct = false;
                masterManager.UiCheck.EndFellUI();
            }
        }

        StopCoroutine("FellingPower");
    }

    IEnumerator FellingPower()
    {
        yield return new WaitForSeconds(0.01f);

        if(!MoveL)
        {
            nextPower.x += Power;
            FellPowerGuage.transform.localPosition = nextPower;
            if (FellPowerGuage.transform.localPosition.x >= 77f)
            {
                MoveL = true;
            }
        }
        else
        {
            nextPower.x -= Power;
            FellPowerGuage.transform.localPosition = nextPower;
            if (FellPowerGuage.transform.localPosition.x <= -77f)
            {
                MoveL = false;
            }
        }
        StartCoroutine("FellingPower");
    }
}
