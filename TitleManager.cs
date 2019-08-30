using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public UISprite UIScreen;

    Color nextFade;

    public GameObject AchiveSetUI;

    Vector3 nextAchiveSet;

    bool isOpenAchive;

    public AudioSource TitleBGM;

    void Start()
    {
        Screen.SetResolution(1280,720,false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        nextFade = UIScreen.color;
        StartCoroutine("BGMPlaying");
    }

    IEnumerator BGMPlaying()
    {
        yield return new WaitForSeconds(0.1f);

        if(TitleBGM.volume <= 1)
        {
            TitleBGM.volume += 0.05f;
            StartCoroutine("BGMPlaying");
        }
        else
        {
            TitleBGM.volume = 1f;
            StopCoroutine("BGMPlaying");
        }
    }

    IEnumerator BGMStop()
    {

        yield return new WaitForSeconds(0.01f);

        if (TitleBGM.volume >= 0)
        {
            TitleBGM.volume -= 0.05f;
            StartCoroutine("BGMStop");
        }
        else
        {
            TitleBGM.volume = 0f;
            StopCoroutine("BGMStop");
        }
    }

    public void StartGame()
    {
        StartCoroutine("BGMStop");
        StartCoroutine("ScreenFadeOut");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    IEnumerator ScreenFadeOut()
    {
        yield return new WaitForSeconds(0.02f);
        if(nextFade.a >= 1f)
        {
            SceneManager.LoadScene("Main");
            StopCoroutine("ScreenFadeOut");
            yield break;
        }
        else
        {
            nextFade.a += 0.02f;
            UIScreen.color = nextFade;
            StartCoroutine("ScreenFadeOut");
        }
    }

    public void OpenAchive()
    {
        StopCoroutine("AchiveUIopen");
        StopCoroutine("AchiveUIclose");

        if (!isOpenAchive)
        {
            nextAchiveSet = new Vector3(350f, 108f, 0f);
            StartCoroutine("AchiveUIopen");
            isOpenAchive = true;
        }
        else
        {
            nextAchiveSet = new Vector3(1000f, 108f, 0f);
            StartCoroutine("AchiveUIclose");
            isOpenAchive = false;
        }
    }

    IEnumerator AchiveUIopen()
    {
        yield return new WaitForSeconds(0.01f);

        if (AchiveSetUI.transform.localPosition.x > 355f)
        {
            AchiveSetUI.transform.localPosition = Vector3.Lerp(AchiveSetUI.transform.localPosition, nextAchiveSet, 0.1f);
            StartCoroutine("AchiveUIopen");
        }
        else
        {
            AchiveSetUI.transform.localPosition = new Vector3(350f, 108f, 0f);
            StopCoroutine("AchiveUIopen");
        }
    }

    IEnumerator AchiveUIclose()
    {
        yield return new WaitForSeconds(0.01f);

        if (AchiveSetUI.transform.localPosition.x < 990f)
        {
            AchiveSetUI.transform.localPosition = Vector3.Lerp(AchiveSetUI.transform.localPosition, nextAchiveSet, 0.1f);
            StartCoroutine("AchiveUIclose");
        }
        else
        {
            AchiveSetUI.transform.localPosition = new Vector3(1000f, 108f, 0f);
            StopCoroutine("AchiveUIclose");
        }
    }

}
