using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public MasterManager masterManager;

    public BuildObjectParmeter objectParm;

    public Transform BuildTargetPos;

    public Transform BuildedTarget;

    public GameObject[] BuildImgObject;

    public GameObject[] Builded;

    public GameObject[] AddBuilded;

    GameObject BuildObject;

    GameObject InstantedObject;

    GameObject InsBuildObject;

    //------------------------------

    public GameObject BuildListParent;

    public GameObject[] BuildAddButtons;

    public float BuildListHeight;

    //------------------------------
    public GameObject ItemEffect;
    public GameObject DropTarget;
    public Transform DropTargetPos;
    public Transform StopTargetPos;
    int keyCount;
    bool BuildButtonClicked, MatBuildClicked;
    public bool openBuildMenu;
    public bool nowMaking;

    bool BuildUpdate1, BuildUpdate3, BuildUpdate4;

    private void Start()
    {
        BuildUpdate1 = false;
        BuildUpdate3 = false;
        BuildUpdate4 = false;

        masterManager.UiCheck.BuildReady();
        keyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&& !masterManager.PlayerCheck.isFelling && !masterManager.PlayerCheck.isFishing && !masterManager.PlayerCheck.isWithFire && !masterManager.PlayerCheck.isPaused && keyCount == 0 && !BuildButtonClicked && !MatBuildClicked)
        {
            masterManager.soundCheck.SFXPlay("OpenMenu");

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            openBuildMenu = true;

            masterManager.UiCheck.BuildSelect();
            masterManager.PlayerCheck.isBuilding = true;
            masterManager.cameraCheck.BuildStart = true;
            masterManager.UiCheck.StartBuildUI();

            keyCount += 1;
        }
        else if(Input.GetKeyDown(KeyCode.F) && keyCount == 1 && !masterManager.PlayerCheck.isPaused && !masterManager.PlayerCheck.isWithFire && !BuildButtonClicked && !MatBuildClicked)
        {
            masterManager.soundCheck.SFXPlay("OpenMenu");

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            openBuildMenu = false;

            masterManager.UiCheck.BuildReady();
            masterManager.PlayerCheck.isBuilding = false;
            masterManager.cameraCheck.BuildStart = false;
            masterManager.UiCheck.EndBuildUI();

            keyCount = 0;
        }

        if (BuildButtonClicked)
        {
            //masterManager.UiCheck.BuildStart();
            masterManager.UiCheck.Building();
            masterManager.PlayerCheck.isBuilding = false;
            masterManager.cameraCheck.BuildStart = false;
            masterManager.UiCheck.EndBuildUI();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (!nowMaking) {
                if (Input.GetMouseButton(0) && objectParm.CanBuild )
                {
                    if (objectParm.CanBuild)
                    {
                        InsBuildObject = Instantiate(BuildObject, BuildTargetPos);
                        InsBuildObject.transform.parent = BuildedTarget;
                    }
                    Destroy(InstantedObject);
                    Destroy(objectParm);
                    BuildButtonClicked = false;
                    openBuildMenu = false;

                    masterManager.UiCheck.BuildReady();
                }
            }
            keyCount = 0;
        }
    }
    public void CloseBuildWindow()
    {
        masterManager.soundCheck.SFXPlay("OpenMenu");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        openBuildMenu = false;

        //masterManager.UiCheck.BuildReady();
        masterManager.PlayerCheck.isBuilding = false;
        masterManager.cameraCheck.BuildStart = false;
        masterManager.UiCheck.EndBuildUI();

        keyCount = 0;
    }

    IEnumerator WaitMaking(float Time, GameObject TargetObject, Transform pos)
    {
        masterManager.UiCheck.SetMakingBar(0, Time);
        yield return new WaitForSecondsRealtime(Time);
        InstantedObject = Instantiate(TargetObject, pos);
       // masterManager.UiCheck.BuildReady();
        objectParm = InstantedObject.GetComponent<BuildObjectParmeter>();
    }

    IEnumerator WaitToolMake(float Time, int ToolType)
    {
        masterManager.UiCheck.SetMakingBar(0, Time);
        yield return new WaitForSecondsRealtime(Time);
        masterManager.UiCheck.BuildReady();
        MatBuildClicked = false;
        switch (ToolType)
        {
            case 1:
                masterManager.PlayerCheck.AxeDurability = 100;
                masterManager.UiCheck.AxeUpdateDur();
                break;
            case 2:
                masterManager.PlayerCheck.HammerDurability = 100;
                masterManager.UiCheck.HamUpdateDur();
                break;
            case 3:
                masterManager.PlayerCheck.RodDurability = 100;
                masterManager.UiCheck.RodUpdateDur();
                break;
        }

    }

    IEnumerator WaitMatMaking(int CompleteType,int CompleteNum, float Time, int num, int key)//얻을아이탬, 얻을개수, 걸린시간, 표시갯수, 빌드매니저표시키
    {
        masterManager.UiCheck.SetMakingBar(0, Time);
        yield return new WaitForSecondsRealtime(Time);
        masterManager.UiCheck.BuildReady();
        MatBuildClicked = false;
        StartCoroutine(PopUpItem(num, key));
        masterManager.InvenCheck.GetItems(CompleteType,CompleteNum);
    }

    public void PushBuildButton1()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(2))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 2&& masterManager.InvenCheck.HaveItems[2] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
               // masterManager.InvenCheck.DecreaseItems(1, 2);
               // masterManager.InvenCheck.DecreaseItems(2, 2);
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 2, 2, 2));
                if (!BuildUpdate1)
                {
                    UpdateBuildList(2);
                    UpdateBuildList(3);
                    UpdateBuildList(10);
                    BuildUpdate1 = true;
                }
                if (!AchiveManager.Achivements2)
                {
                    AchiveManager.Achivements2 = true;
                }

                StartCoroutine(WaitMaking(8, BuildImgObject[0],BuildTargetPos));
                //InstantedObject = Instantiate(BuildImgObject[0], BuildTargetPos);
                
                BuildButtonClicked = true;
                BuildObject = Builded[0];
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

    public void PushBuildButton2()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 10)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                masterManager.InvenCheck.DecreaseItems(1, 10);
                StartCoroutine(WaitMaking(50, BuildImgObject[1], BuildTargetPos));
                BuildButtonClicked = true;
                BuildObject = Builded[1];
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

    public void PushBuildButton3()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(5))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 4 && masterManager.InvenCheck.HaveItems[5] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                //masterManager.InvenCheck.DecreaseItems(1, 4);
                // masterManager.InvenCheck.DecreaseItems(5, 2);
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 4, 5, 2));
                if (!BuildUpdate3)
                {
                    UpdateBuildList(7);
                    UpdateBuildList(0);
                    UpdateBuildList(11);
                    BuildUpdate3 = true;
                }
                StartCoroutine(WaitMaking(15, BuildImgObject[2], BuildTargetPos));
                BuildButtonClicked = true;
                BuildObject = Builded[2];
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

    public void PushBuildButton4()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(4))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 6 && masterManager.InvenCheck.HaveItems[4] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                //masterManager.InvenCheck.DecreaseItems(1, 4);
                // masterManager.InvenCheck.DecreaseItems(5, 2);
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 6, 4, 2));

                if (!BuildUpdate4)
                {
                    UpdateBuildList(4);
                    UpdateBuildList(5);
                    UpdateBuildList(6);
                    UpdateBuildList(8);
                    UpdateBuildList(9);
                    BuildUpdate4 = true;
                }
                StartCoroutine(WaitMaking(25, BuildImgObject[3], BuildTargetPos));
                BuildButtonClicked = true;
                BuildObject = Builded[3];
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
    public void PushBuildButton5()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(6))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 6 && masterManager.InvenCheck.HaveItems[6] >= 3)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                //masterManager.InvenCheck.DecreaseItems(1, 4);
                // masterManager.InvenCheck.DecreaseItems(5, 2);
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 6, 6, 3));

                StartCoroutine(WaitMaking(25, BuildImgObject[4], BuildTargetPos));
                BuildButtonClicked = true;
                BuildObject = Builded[4];
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
    public void PushBuildButton6()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(10) && masterManager.InvenCheck.HaveItems.ContainsKey(11))
        {
            if (masterManager.InvenCheck.HaveItems[10] >= 1 && masterManager.InvenCheck.HaveItems[11] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                //masterManager.InvenCheck.DecreaseItems(1, 4);
                // masterManager.InvenCheck.DecreaseItems(5, 2);
                StartCoroutine(masterManager.InvenCheck.DelayDelete(10, 1, 11, 2));

                StartCoroutine(WaitMaking(30, BuildImgObject[5], BuildTargetPos));
                BuildButtonClicked = true;
                BuildObject = Builded[5];
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

    public void PushBuildButton7()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(12))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 8 && masterManager.InvenCheck.HaveItems[12] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                //masterManager.InvenCheck.DecreaseItems(1, 4);
                // masterManager.InvenCheck.DecreaseItems(5, 2);
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 8, 12, 2));

                StartCoroutine(WaitMaking(35, BuildImgObject[6], BuildTargetPos));
                BuildButtonClicked = true;
                BuildObject = Builded[6];
                UpdateBuildList(1);
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
    //----------------------------------------------------
    IEnumerator PopUpItem(int num, int key)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < num; i++)
        {
            masterManager.soundCheck.SFXPlay("ItemPopUp");
            DropTarget = Instantiate(AddBuilded[key], DropTargetPos);
            Instantiate(ItemEffect, DropTargetPos);
            StartCoroutine(PopUpItemMov(DropTarget));
            yield return new WaitForSeconds(1f);
        }
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
    //------------------------------------------------------------------------
    public void PushAddBuildButton1()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(2) && masterManager.InvenCheck.HaveItems.ContainsKey(104))
        {
            if (masterManager.InvenCheck.HaveItems[2] >= 1 && masterManager.InvenCheck.HaveItems[104] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                StartCoroutine(masterManager.InvenCheck.DelayDelete(2, 1, 104, 1));
                StartCoroutine(WaitMatMaking(8, 2, 5, 2, 0));
                MatBuildClicked = true;
                CloseBuildWindow();
                // StartCoroutine(PopUpItem(2, 0));
                // masterManager.InvenCheck.GetItems(8, 2);
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

        public void PushAddBuildButton2()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(4))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 2 && masterManager.InvenCheck.HaveItems[4] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 2, 4, 1));
                StartCoroutine(WaitToolMake(15, 1));
                MatBuildClicked = true;
                CloseBuildWindow();
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

    public void PushAddBuildButton3()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(4))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 2 && masterManager.InvenCheck.HaveItems[4] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 2, 4, 2));
                //StartCoroutine(WaitMatMaking(8, 2, 15, 2, 0));
                StartCoroutine(WaitToolMake(20, 2));
                MatBuildClicked = true;
                CloseBuildWindow();
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

    public void PushAddBuildButton4()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(6) && masterManager.InvenCheck.HaveItems.ContainsKey(8))
        {
            if (masterManager.InvenCheck.HaveItems[6] >= 2 && masterManager.InvenCheck.HaveItems[8] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                StartCoroutine(masterManager.InvenCheck.DelayDelete(6, 2, 8, 1));
                //StartCoroutine(WaitMatMaking(8, 2, 15, 2, 0));
                StartCoroutine(WaitToolMake(20, 3));
                MatBuildClicked = true;
                CloseBuildWindow();
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

    public void PushAddBuildButton5()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(1) && masterManager.InvenCheck.HaveItems.ContainsKey(13))
        {
            if (masterManager.InvenCheck.HaveItems[1] >= 4 && masterManager.InvenCheck.HaveItems[13] >= 2)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                StartCoroutine(masterManager.InvenCheck.DelayDelete(1, 4, 13, 2));
                StartCoroutine(WaitMatMaking(12, 1, 15, 1, 1));
                MatBuildClicked = true;
                CloseBuildWindow();
                // StartCoroutine(PopUpItem(2, 0));
                // masterManager.InvenCheck.GetItems(8, 2);

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

    public void PushAddBuildButton6()
    {
        if (masterManager.InvenCheck.HaveItems.ContainsKey(2) && masterManager.InvenCheck.HaveItems.ContainsKey(4))
        {
            if (masterManager.InvenCheck.HaveItems[2] >= 4 && masterManager.InvenCheck.HaveItems[4] >= 1)
            {
                masterManager.soundCheck.SFXPlay("ClickSound");
                masterManager.UiCheck.Building();
                StartCoroutine(masterManager.InvenCheck.DelayDelete(2, 4, 4, 1));
                StartCoroutine(WaitMatMaking(13, 1, 20, 1, 2));
                MatBuildClicked = true;
                CloseBuildWindow();
                // StartCoroutine(PopUpItem(2, 0));
                // masterManager.InvenCheck.GetItems(8, 2);

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

    //----------------------------------------------------------

    public void UpdateBuildList(int BuildedNum)
    {
        GameObject BuildListPos = GameObject.Find("BuildButtonPos");

        BuildListPos = Instantiate(BuildAddButtons[BuildedNum], BuildListPos.transform);
        BuildListPos.transform.parent = BuildListParent.transform;

        BuildListPos.transform.localPosition = new Vector3(0f, BuildListHeight, 0f);
        BuildListHeight -= 75f;
    }
}
