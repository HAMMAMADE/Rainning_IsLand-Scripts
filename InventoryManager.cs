using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public MasterManager masterManager;

    public Transform DropPos;
    public Dictionary<int, int> HaveItems;

    public Transform TextPos;

    public GameObject TextSet;

    public GameObject TextPrefeb;

    //---------------------------------------

    public GameObject DropItem1;

    public GameObject DropItem2;

    public GameObject DropItem3;

    public GameObject DropItem4;

    public GameObject DropItem5;

    public GameObject DropItem6;

    public GameObject DropItem7;

    public GameObject DropItem8;

    public GameObject DropItem9;

    public GameObject DropItem10;

    public GameObject DropItem11;

    public GameObject DropItem12;

    public GameObject DropItem13;
    //---------------------------------------
    public GameObject DropFish0;

    public GameObject DropFish1;

    public GameObject DropFish2;

    public GameObject DropFish3;

    public GameObject DropFish4;

    public GameObject EndFish;

    //---------------------------------------

    public GameObject DropLeather;

    //---------------------------------------
    int useMatCount, AllItems;

    private void Start()
    {
        AllItems = 0;
        HaveItems = new Dictionary<int, int>();
        useMatCount = 0;
    }

    public void GetItems(int TypeNum, int GetNum)
    {
        switch (TypeNum)
        {
            case 1:
                StartCoroutine(DropItems(DropItem1, GetNum));

                if (HaveItems.ContainsKey(1))
                {
                    HaveItems[1] += GetNum;
                }
                else
                {
                    HaveItems.Add(1, GetNum);
                }
                AllItems += GetNum;
                break;

            case 2:
                StartCoroutine(DropItems(DropItem2, GetNum));

                if (HaveItems.ContainsKey(2))
                {
                    HaveItems[2] += GetNum;
                }
                else
                {
                    HaveItems.Add(2, GetNum);
                }
                AllItems += GetNum;
                break;

            case 3:
                StartCoroutine(DropItems(DropItem3, GetNum));

                if (HaveItems.ContainsKey(3))
                {
                    HaveItems[3] += GetNum;
                }
                else
                {
                    HaveItems.Add(3, GetNum);
                }
                AllItems += GetNum;
                break;

            case 4:
                StartCoroutine(DropItems(DropItem4, GetNum));

                if (HaveItems.ContainsKey(4))
                {
                    HaveItems[4] += GetNum;
                }
                else
                {
                    HaveItems.Add(4, GetNum);
                }
                AllItems += GetNum;
                break;
            case 5://헝겊
                StartCoroutine(DropItems(DropItem5, GetNum));

                if (HaveItems.ContainsKey(5))
                {
                    HaveItems[5] += GetNum;
                }
                else
                {
                    HaveItems.Add(5, GetNum);
                }
                AllItems += GetNum;
                break;
            case 6://갈댓잎
                StartCoroutine(DropItems(DropItem6, GetNum));

                if (HaveItems.ContainsKey(6))
                {
                    HaveItems[6] += GetNum;
                }
                else
                {
                    HaveItems.Add(6, GetNum);
                }
                AllItems += GetNum;
                break;

            case 7://꽃잎
                StartCoroutine(DropItems(DropItem7, GetNum));

                if (HaveItems.ContainsKey(7))
                {
                    HaveItems[7] += GetNum;
                }
                else
                {
                    HaveItems.Add(7, GetNum);
                }
                AllItems += GetNum;
                break;

            case 8://꽃잎
                StartCoroutine(DropItems(DropItem8, GetNum));

                if (HaveItems.ContainsKey(8))
                {
                    HaveItems[8] += GetNum;
                }
                else
                {
                    HaveItems.Add(8, GetNum);
                }
                AllItems += GetNum;
                break;

            case 9:
                StartCoroutine(DropItems(DropItem9, GetNum));

                if (HaveItems.ContainsKey(9))
                {
                    HaveItems[9] += GetNum;
                }
                else
                {
                    HaveItems.Add(9, GetNum);
                }
                AllItems += GetNum;
                break;

            case 10:
                StartCoroutine(DropItems(DropItem10, GetNum));

                if (HaveItems.ContainsKey(10))
                {
                    HaveItems[10] += GetNum;
                }
                else
                {
                    HaveItems.Add(10, GetNum);
                }
                AllItems += GetNum;
                break;

            case 11:
                StartCoroutine(DropItems(DropItem11, GetNum));

                if (HaveItems.ContainsKey(11))
                {
                    HaveItems[11] += GetNum;
                }
                else
                {
                    HaveItems.Add(11, GetNum);
                }
                AllItems += GetNum;
                break;

            case 12:
                StartCoroutine(DropItems(DropItem12, GetNum));

                if (HaveItems.ContainsKey(12))
                {
                    HaveItems[12] += GetNum;
                }
                else
                {
                    HaveItems.Add(12, GetNum);
                }
                AllItems += GetNum;
                break;

            case 13:
                StartCoroutine(DropItems(DropItem13, GetNum));

                if (HaveItems.ContainsKey(13))
                {
                    HaveItems[13] += GetNum;
                }
                else
                {
                    HaveItems.Add(13, GetNum);
                }
                AllItems += GetNum;
                break;


            case 50:
                StartCoroutine(DropItems(DropLeather, GetNum));

                if (HaveItems.ContainsKey(50))
                {
                    HaveItems[50] += GetNum;
                }
                else
                {
                    HaveItems.Add(50, GetNum);
                }
                AllItems += GetNum;
                break;

            case 100:
                StartCoroutine(DropItems(DropFish0, GetNum));

                if (HaveItems.ContainsKey(100))
                {
                    HaveItems[100] += GetNum;
                }
                else
                {
                    HaveItems.Add(100, GetNum);
                }
                AllItems += GetNum;
                break;

            case 101:
                StartCoroutine(DropItems(DropFish1, GetNum));

                if (HaveItems.ContainsKey(101))
                {
                    HaveItems[101] += GetNum;
                }
                else
                {
                    HaveItems.Add(101, GetNum);
                    masterManager.fireCheck.UpdateFishList(101);
                    masterManager.HazeCheck.UpdateFishHazeList(101);
                }
                AllItems += GetNum;
                break;

            case 102:
                StartCoroutine(DropItems(DropFish2, GetNum));

                if (HaveItems.ContainsKey(102))
                {
                    HaveItems[102] += GetNum;   
                }
                else
                {
                    HaveItems.Add(102, GetNum);
                    masterManager.fireCheck.UpdateFishList(102);
                    masterManager.HazeCheck.UpdateFishHazeList(102);
                }
                AllItems += GetNum;
                break;

            case 103:
                StartCoroutine(DropItems(DropFish3, GetNum));

                if (HaveItems.ContainsKey(103))
                {
                    HaveItems[103] += GetNum;
                }
                else
                {
                    HaveItems.Add(103, GetNum);
                    masterManager.fireCheck.UpdateFishList(103);
                    masterManager.HazeCheck.UpdateFishHazeList(103);
                }
                AllItems += GetNum;
                break;

            case 104:
                StartCoroutine(DropItems(DropFish4, GetNum));

                if (HaveItems.ContainsKey(104))
                {
                    HaveItems[104] += GetNum;
                }
                else
                {
                    HaveItems.Add(104, GetNum);
                    //masterManager.fireCheck.UpdateFishList(104);
                }
                AllItems += GetNum;
                break;

            case 199:
                StartCoroutine(DropItems(EndFish, GetNum));

                if (HaveItems.ContainsKey(199))
                {
                    HaveItems[199] += GetNum;
                }
                else
                {
                    HaveItems.Add(199, GetNum);
                    //masterManager.fireCheck.UpdateFishList(104);
                }
                AllItems += GetNum;
                break;
        }
    }

    IEnumerator DropItems(GameObject DropItem, int GetNum)
    {
        for (int i = 0; i < GetNum; i++)
        {
            yield return new WaitForSeconds(0.1f);

            Instantiate(DropItem, DropPos);
            GameObject FindAddItems = GameObject.FindGameObjectWithTag("Material");

            Rigidbody rigid = FindAddItems.GetComponent<Rigidbody>();
            rigid.AddTorque(0f,0f,90f);
        }
    }

    public void DecreaseItems(int TypeNum, int GetNum)
    {
       // Debug.Log("AllItems: " + AllItems);
        useMatCount = 0;
        ShowItemText(TypeNum,GetNum);
        StartCoroutine(DeleteItems(TypeNum, GetNum));
        EndDelItem(TypeNum, GetNum);
    }

    IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(TextSet);
    }

    public void ShowItemText(int TypeNum, int GetNum)
    {
        TextSet = Instantiate(TextPrefeb, TextPos);
        TextSet.GetComponentInChildren<UILabel>().text = " -" + GetNum;
        switch (TypeNum)
        {
            case 1:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon";
                break;

            case 2:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-B";
                break;

            case 3:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-D";
                break;

            case 4:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-C";
                break;
            case 5://헝겊
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-F";
                break;
            case 6://갈댓잎
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-G";
                break;

            case 7://꽃잎
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-E";
                break;

            case 8://미끼 
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-H";
                break;

            case 9:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-J";
                break;

            case 10:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-I";
                break;

            case 11:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-K";
                break;

            case 12:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-L";
                break;

            case 13:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "MatIcon-M";
                break;

            case 50:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Leather";
                break;

            case 100:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Fish-Bone";
                break;

            case 101:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Fish-A";
                break;

            case 102:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Fish-B";
                break;

            case 103:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Fish-C";
                break;

            case 104:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Fish-D";
                break;

            case 199:
                TextSet.GetComponentInChildren<UISprite>().spriteName = "Fish-Z";
                break;
        }

        StartCoroutine("TextDelay");
    }

    public IEnumerator DelayDelete(int TypeNum1, int GetNum1, int TypeNum2, int GetNum2)
    {
        useMatCount = 0;
        StartCoroutine(DeleteItems(TypeNum1, GetNum1));
        ShowItemText(TypeNum1, GetNum1);
        EndDelItem(TypeNum1, GetNum1);
        yield return new WaitForSeconds(4f);
        useMatCount = 0;
        StartCoroutine(DeleteItems(TypeNum2, GetNum2));
        ShowItemText(TypeNum2, GetNum2);
        EndDelItem(TypeNum2, GetNum2);
    }

    IEnumerator DeleteItems(int TypeNum, int GetNum)
    {
        GameObject[] FindDeleteItems = GameObject.FindGameObjectsWithTag("Material");

        for (int i = 0; i <= AllItems; i++)
        {
            if (useMatCount < GetNum)
            {
                if (TypeNum == FindDeleteItems[i].GetComponent<MaterialManager>().ItemType)
                {
                    useMatCount += 1;
                    Destroy(FindDeleteItems[i]);
                }
            }
            else
            {
                yield return new WaitForSeconds(1f);
                StopCoroutine(DeleteItems(TypeNum, GetNum));
            }
            //yield return null;
        }
    }

    public void EndDelItem(int typeNum, int Num)
    {
        HaveItems[typeNum] -= Num;
        AllItems -= Num;
    }
    
}
