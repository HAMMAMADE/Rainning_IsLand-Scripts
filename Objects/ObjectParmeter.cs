using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParmeter : MonoBehaviour
{
    public MasterManager masterManager;

    public Animator ObjectAnim;
    //---------------------------------
    public Collider thisCollider;

    public bool DropPlus;
    public float DropPlusPercent;

    public int ObjectKey;
    public int ObjectKey2;
    //---------------------------------
    public string ObjectName;
    public string ObjectRank;
    public int ObjectHealth;
    public int ItemNum;
    public GameObject ItemEffect;
    //---------------------------------
    public GameObject OutMaterial;
    public GameObject OutMaterial2;
    //---------------------------------
    public GameObject[] DropTarget;
    public Transform DropTargetPos;
    public Transform StopTargetPos;

    private void Start()
    {
        if(masterManager == null)
        {
            masterManager = GameObject.Find("MasterManager").GetComponent<MasterManager>();
        }

        for (int i = 0; i < ItemNum; i++) {
            if (DropTarget[i] == null)
            {
                DropTarget[i] = GameObject.Find("PopUpPos");
            }
        }

        if(StopTargetPos == null)
        {
            StopTargetPos = GameObject.Find("TargetPos").transform;
        }

        if(DropTargetPos == null)
        {
            DropTargetPos = GameObject.Find("PopUpPos").transform;
        }

    }

    public void OffColider()
    {
        thisCollider.enabled = false;
        // gameObject.name = " ";
        masterManager.UiCheck.OutInterAct();
        masterManager.PlayerCheck.isSmallFell = false;
        masterManager.PlayerCheck.isbigFell = false;
    }

    public void MoveObjectDestroy()
    {
        StartCoroutine("MovDestroying");
    }

    IEnumerator MovDestroying()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void ObjectDestroy()
    {
        StartCoroutine("Destroying");
    }

    IEnumerator Destroying()
    {
        masterManager.UiCheck.EndFellUI();

        yield return new WaitForSeconds(2f);

        masterManager.PlayerCheck.EndFell();

        yield return new WaitForSeconds(6f);

        Destroy(gameObject);
    }

    public void DropItems()
    {
        StartCoroutine(PopUpItem(ItemNum));
        masterManager.InvenCheck.GetItems(ObjectKey,ItemNum);
    }

    //----------------------------------------------------------------------
    IEnumerator PopUpItem(int num)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < num; i++)
        {
            masterManager.soundCheck.SFXPlay("ItemPopUp");
            DropTarget[i] = Instantiate(OutMaterial, DropTargetPos);
            Instantiate(ItemEffect, DropTargetPos);
            StartCoroutine(PopUpItemMov(DropTarget[i]));
            yield return new WaitForSeconds(1f);
        }
        if (DropPlus)
        {
            float DropPercent = Random.Range(0f, 100f);
           // Debug.Log(DropPercent);
            if (DropPercent <= DropPlusPercent)
            {
                StartCoroutine("PopUpItem2");
                masterManager.InvenCheck.GetItems(ObjectKey2, 1);
            }
        }
    }

    IEnumerator PopUpItem2()
    {
        yield return new WaitForSeconds(1f);
        masterManager.soundCheck.SFXPlay("SpecialItemPop");
        DropTarget[0] = Instantiate(OutMaterial2, DropTargetPos);
        Instantiate(ItemEffect, DropTargetPos);
        StartCoroutine(PopUpItemMov(DropTarget[0]));
        yield return new WaitForSeconds(1f);
    }
    //----------------------------------------------------------------------
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
