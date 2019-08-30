using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public MasterManager masterManager;

    private Color originFogColor;
    private float originFogDesity;

    public Color WaterFogColor;
    public float WaterFogDesity;
    public Color WaterNightFogCol;
    public float WaterNightFogDes;

    public Color NowFogColor;
    public float NowFogDesity;

    // Start is called before the first frame update
    void Start()
    {
        //초기화
        originFogColor = RenderSettings.fogColor;
        originFogDesity = RenderSettings.fogDensity;
        NowFogColor = originFogColor;
        NowFogDesity = originFogDesity;

    }

    public void WaterinFog()
    {
        if (masterManager.TimeCheck.isNight)
        {
            RenderSettings.fogColor = WaterNightFogCol;
            RenderSettings.fogDensity = WaterNightFogDes;
        }
        else
        {
            RenderSettings.fogColor = WaterFogColor;
            RenderSettings.fogDensity = WaterFogDesity;
        }
    }

    public void WaterOutFog()
    {
        RenderSettings.fogColor = NowFogColor;
        RenderSettings.fogDensity = NowFogDesity;
    }

    public void ChangeNightFog()
    {
       StartCoroutine("NightFog");
    }

    IEnumerator NightFog()
    {

        yield return new WaitForSeconds(0.1f);
        NowFogColor.r -= 0.03f;
        NowFogColor.g -= 0.03f;
        NowFogColor.b -= 0.03f;
        NowFogDesity += 0.0015f;
        RenderSettings.fogDensity = NowFogDesity;

        RenderSettings.fogColor = NowFogColor;

        if (RenderSettings.fogColor.r <= 0f)
        {
           StopCoroutine("NightFog");
        }
        else
        {
           StartCoroutine("NightFog");
        }
    }

    public void ChangeNoonFog()
    {
       StartCoroutine("NoonFog");
    }

    IEnumerator NoonFog()
    {
        yield return new WaitForSeconds(0.1f);

        NowFogColor.r += 0.03f;
        NowFogColor.g += 0.03f;
        NowFogColor.b += 0.03f;
        NowFogDesity -= 0.0015f;

        RenderSettings.fogColor = NowFogColor;
        RenderSettings.fogDensity = NowFogDesity;

        if (RenderSettings.fogColor.r >= 1f)
        {
            StopCoroutine("NoonFog");
        }
        else
        {
            StartCoroutine("NoonFog");
        }
    }
}
