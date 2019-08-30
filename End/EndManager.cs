using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public MasterManager masterManager;

    public UISprite Screen;

    Color nextColor;

    bool inGameOver;

    bool inGameClear;

    bool BgmFadeOut;
    //----------------------------------
    public UILabel ScoreText1;

    public UILabel ScoreText2;

    public UILabel ScoreText2_5;

    public UILabel ScoreText3;

    public UILabel ScoreText4;

    public UILabel ScoreText5;

    public UILabel ScoreText6;

    public GameObject Credits;

    Vector3 nextCreditPos;

    public AudioSource EndBgm;

    float TimeNum;

    private void Start()
    {
        inGameOver = false;
        inGameClear = false;

        nextColor = Screen.color;
    }

    public void StartGameOver()
    {
        nextColor.a = 0f;
        Screen.color = nextColor;
        StartCoroutine("ScreenFade");
    }

    public void StartGameClear()
    {
        inGameClear = true;
        nextColor.a = 0f;
        Screen.color = nextColor;
        StartCoroutine("ScreenFade");
    }

    public void StartBackTitle()
    {
        inGameOver = true;

        MainArchiveManager.StaticDay = 0;

        MainArchiveManager.StaticFishingCount = 0;

        MainArchiveManager.StaticFellingCount = 0;

        MainArchiveManager.StaticHaveDryFish = 0;

        MainArchiveManager.StaticWolfCount = 0;

        MainArchiveManager.RunningTime = 0;

        MainArchiveManager.IsLandHeight = 0;

        StartCoroutine("ScreenFade");
    }

    IEnumerator ScreenFade()
    {
        yield return new WaitForSeconds(0.01f);
        if (nextColor.a >= 1f)
        {
            if (inGameOver)
            {
                SceneManager.LoadScene("Title");
                yield break;
            }
            if (inGameClear)
            {
                SceneManager.LoadScene("GoodEnding");
                yield break;
            }
            else SceneManager.LoadScene("Ending");

            StopCoroutine("ScreenFade");
            yield break;
        }
        else
        {
            nextColor.a += 0.02f;
            Screen.color = nextColor;
            StartCoroutine("ScreenFade");
        }
    }

    public void ScoreLoad()
    {
        ScoreText1.text = MainArchiveManager.StaticDay.ToString() + " 일";
        ScoreText2.text = MainArchiveManager.StaticFishingCount.ToString() + " 번";
        ScoreText2_5.text = MainArchiveManager.StaticHaveDryFish.ToString() + " 개";
        ScoreText3.text = MainArchiveManager.StaticFellingCount.ToString() + " 번";
        ScoreText4.text = MainArchiveManager.StaticWolfCount.ToString() + " 마리";
        TimeNum = Mathf.Floor(MainArchiveManager.RunningTime);
        ScoreText5.text = TimeNum.ToString() + " 걸음";
        ScoreText6.text = MainArchiveManager.IsLandHeight.ToString() + " 미터";
    }

    public void MoveUpCredit()
    {
        ScoreLoad();
       // EndBgm.Play();
        nextCreditPos = Credits.transform.localPosition;
        StartCoroutine("BGMPlay");
        StartCoroutine("CreditMoving");
    }

    IEnumerator BGMPlay()
    {
        yield return new WaitForSeconds(0.1f);
        EndBgm.volume += 0.01f;
        if (EndBgm.volume < 1f)
        {
            StartCoroutine("BGMPlay");
        }
        else
        {
            StopCoroutine("BGMPlay");
        }
    }

    IEnumerator BGMFadeOut()
    {
        yield return new WaitForSeconds(0.1f);
        EndBgm.volume -= 0.01f;
        if (EndBgm.volume > 0f)
        {
            StartCoroutine("BGMFadeOut");
        }
        else
        {
            StopCoroutine("BGMFadeOut");
        }
    }

    IEnumerator CreditMoving()
    {
        yield return new WaitForSeconds(0.01f);

        if (Credits.transform.localPosition.y > 2100f && !BgmFadeOut)
        {
            StartCoroutine("BGMFadeOut");
            BgmFadeOut = true;
        }

        if (Credits.transform.localPosition.y < 2380f)
        {
            nextCreditPos.y += 0.75f;
            Credits.transform.localPosition = nextCreditPos;
            StartCoroutine("CreditMoving");
        }
        else
        {
            inGameOver = true;
            StartCoroutine("ScreenFade");
            StopCoroutine("CreditMoving");
        }
    }
}
