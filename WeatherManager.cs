using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
  //public MasterManager masterManager;

    GameObject MainCamera;

    public GameObject WaterHeight;

    Vector3 nextHeight;

    public GameObject RainEffect;

    public Light WeatherLight;

    public float FogDesity;

    public Color SkyColor;

    public Material DefaultSky;

    public Material RainSky;

    //---------------------------

    public bool isRaining;

    float WeatherCase;

    private void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        isRaining = false;
        WeatherLight.color = SkyColor;
        WeatherCase = 0;
    }

    public void OffRainEffect()
    {
        RainEffect.SetActive(false);
    }

    public void OnRainEffect()
    {
        RainEffect.SetActive(true);
    }

    public void ChangeWeather()
    {
        WeatherCase = Random.Range(0, 2);
        //WeatherCase = 1;

        switch (WeatherCase)
        {
            case 0:
                MainCamera.GetComponent<Skybox>().material = DefaultSky;
                SkyColor = new Color(1f, 1f, 1f);
                WeatherLight.color = SkyColor;
                isRaining = false;
                OffRainEffect();
                break;
            case 1:
                MainCamera.GetComponent<Skybox>().material = RainSky;
                SkyColor = new Color(0.5f, 0.5f, 0.5f);
                WeatherLight.color = SkyColor;
                isRaining = true;
                WaterHeightUP();
                OnRainEffect();
                break;
            default:
                MainCamera.GetComponent<Skybox>().material = RainSky;
                SkyColor = new Color(0.5f, 0.5f, 0.5f);
                WeatherLight.color = SkyColor;
                isRaining = true;
                WaterHeightUP();
                OnRainEffect();
                break;
        }
    }

    public void WaterHeightUP()
    {
        nextHeight = WaterHeight.transform.localPosition;
        nextHeight.y += 0.5f;
        MainArchiveManager.IsLandHeight += 0.5f;
        StartCoroutine("WaterUp");
    }

    IEnumerator WaterUp()
    {
        yield return new WaitForSeconds(0.05f);
        if(WaterHeight.transform.localPosition.y >= nextHeight.y)
        {
            WaterHeight.transform.localPosition = nextHeight;
            StopCoroutine("WaterUp");
            yield break;
        }
        else
        {
            WaterHeight.transform.localPosition = Vector3.Lerp(WaterHeight.transform.localPosition, nextHeight, 0.1f);
            StartCoroutine("WaterUp");
        }

    }
}
