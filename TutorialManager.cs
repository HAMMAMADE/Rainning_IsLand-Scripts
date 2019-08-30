using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public MasterManager masterManager;

    public UISprite Screen;

    public GameObject TutorialImg;

    Vector3 ImgNextPos;

    public GameObject TutorialSet;

    public UILabel TutorialName;

    Vector3 nextTutorialPos;

    int TutorialNum = 0;
    bool ImgSliding;

    Color nextFade;

    public static bool EndTutorial;
    // Start is called before the first frame update
    void Awake()
    {
        nextFade = Screen.color;
        EndTutorial = false;
        StartCoroutine("ScreenFadeIn");
    }

    IEnumerator ScreenFadeIn()
    {
        yield return new WaitForSeconds(0.01f);
        if (nextFade.a > 0)
        {
            nextFade.a -= 0.02f;
            Screen.color = nextFade;
            StartCoroutine("ScreenFadeIn");
        }
        else
        {
            yield return new WaitForSeconds(1.8f);
            PopUpTutorial();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StopCoroutine("ScreenFadeIn");
        }
    }


    IEnumerator ScreenFadeout()
    {
        yield return new WaitForSeconds(0.01f);
        if (nextFade.a < 1f)
        {
            nextFade.a += 0.02f;
            Screen.color = nextFade;
            StartCoroutine("ScreenFadeout");
        }
        else
        {
            SceneManager.LoadScene("Title");
            EndTutorial = false;
            StopCoroutine("ScreenFadeout");
        }
    }
    //------------------------------------------------------
    public void PopUpTutorial()
    {
        nextTutorialPos = TutorialSet.transform.localPosition;
        nextTutorialPos.y = 10f;
        masterManager.soundCheck.SFXPlay("AchiveSound");
        StartCoroutine("PopTuto");
    }

    IEnumerator PopTuto()
    {
        yield return new WaitForSeconds(0.01f);

        if(TutorialSet.transform.localPosition.y < 10f)
        {
            TutorialSet.transform.localPosition = Vector3.Lerp(TutorialSet.transform.localPosition, nextTutorialPos, 0.1f);
            StartCoroutine("PopTuto");
        }
        else
        {
            TutorialSet.transform.localPosition = new Vector3(0f, 10f, 0f);
            StopCoroutine("PopTuto");
            yield break;
        }
    }

    public void SkipTutorial()
    {
        masterManager.soundCheck.SFXPlay("ClickSound");
        EndTutorial = true;
        masterManager.TimeCheck.StartTime();
        nextTutorialPos = TutorialSet.transform.localPosition;
        nextTutorialPos.y = -600f;
        StartCoroutine("CloseTuto");
    }

    public void CloseTutorial()
    {
        //masterManager.soundCheck.SFXPlay("ClickSound");
        EndTutorial = true;
        masterManager.TimeCheck.StartTime();
        nextTutorialPos = TutorialSet.transform.localPosition;
        nextTutorialPos.y = -600f;
        StartCoroutine("CloseTuto");
    }

    IEnumerator CloseTuto()
    {
        yield return new WaitForSeconds(0.01f);

        if (TutorialSet.transform.localPosition.y > -600f)
        {
            TutorialSet.transform.localPosition = Vector3.Lerp(TutorialSet.transform.localPosition, nextTutorialPos, 0.1f);
            StartCoroutine("CloseTuto");
        }
        else
        {
            TutorialSet.transform.localPosition = new Vector3(0f, -600f, 0f);

            TutorialName.text = "1. 캐릭터 이동";
            TutorialNum = 0;
            ImgNextPos = TutorialImg.transform.localPosition;
            ImgNextPos.x = 0f;
            ImgSliding = true;
            StartCoroutine("BackSlideTuto");

            StopCoroutine("CloseTuto");
            yield break;
        }
    }
    //--------------------------------------------------
    public void PushNextBtn()
    {
        if (!ImgSliding) {
            masterManager.soundCheck.SFXPlay("ClickSound");
            switch (TutorialNum)
            {
                case 0:
                    TutorialName.text = "2. 공격 하기";
                    TutorialNum += 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -300f;
                    ImgSliding = true;
                    StartCoroutine("SlideTuto");
                    break;

                case 1:
                    TutorialName.text = "3. 채집 하기";
                    TutorialNum += 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -600f;
                    ImgSliding = true;
                    StartCoroutine("SlideTuto");
                    break;
                case 2:
                    TutorialName.text = "4. 낚시 하기";
                    TutorialNum += 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -900f;
                    ImgSliding = true;
                    StartCoroutine("SlideTuto");
                    break;
                case 3:
                    TutorialName.text = "5. 제작 하기";
                    TutorialNum += 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -1200f;
                    ImgSliding = true;
                    StartCoroutine("SlideTuto");
                    break;
                case 4:
                    TutorialName.text = "6. 게임 목표";
                    TutorialNum += 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -1500f;
                    ImgSliding = true;
                    StartCoroutine("SlideTuto");
                    break;
                case 5:
                    CloseTutorial();
                    break;
            }
        }
       // Debug.Log(TutorialNum);
    }


    IEnumerator SlideTuto()
    {
        yield return new WaitForSeconds(0.01f);

        if (TutorialImg.transform.localPosition.x > ImgNextPos.x + 1f)
        {
            TutorialImg.transform.localPosition = Vector3.Lerp(TutorialImg.transform.localPosition, ImgNextPos, 0.1f);
            StartCoroutine("SlideTuto");
        }
        else
        {
            TutorialImg.transform.localPosition = ImgNextPos;
            ImgSliding = false;
            StopCoroutine("SlideTuto");
            yield break;
        }
    }

    public void PushBackBtn()
    {
        if (!ImgSliding) {
            masterManager.soundCheck.SFXPlay("ClickSound");
            switch (TutorialNum)
            {
                case 1:
                    TutorialName.text = "1. 캐릭터 이동";
                    TutorialNum = 0;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = 0f;
                    ImgSliding = true;
                    StartCoroutine("BackSlideTuto");
                    break;
                case 2:
                    TutorialName.text = "2. 공격 하기";
                    TutorialNum -= 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -300f;
                    ImgSliding = true;
                    StartCoroutine("BackSlideTuto");
                    break;
                case 3:
                    TutorialName.text = "3. 채집 하기";
                    TutorialNum -= 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -600f;
                    ImgSliding = true;
                    StartCoroutine("BackSlideTuto");
                    break;
                case 4:
                    TutorialName.text = "4. 낚시 하기";
                    TutorialNum -= 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -900f;
                    ImgSliding = true;
                    StartCoroutine("BackSlideTuto");
                    break;
                case 5:
                    TutorialName.text = "5. 제작 하기";
                    TutorialNum -= 1;
                    ImgNextPos = TutorialImg.transform.localPosition;
                    ImgNextPos.x = -1200f;
                    ImgSliding = true;
                    StartCoroutine("BackSlideTuto");
                    break;
            }
        }
      //  Debug.Log(TutorialNum);
    }

    IEnumerator BackSlideTuto()
    {
        yield return new WaitForSeconds(0.01f);

        if (TutorialImg.transform.localPosition.x < ImgNextPos.x - 1f)
        {
            TutorialImg.transform.localPosition = Vector3.Lerp(TutorialImg.transform.localPosition, ImgNextPos, 0.1f);
            StartCoroutine("BackSlideTuto");
        }
        else
        {
            TutorialImg.transform.localPosition = ImgNextPos;
            ImgSliding = false;
            StopCoroutine("BackSlideTuto");
            yield break;
        }
    }


}
