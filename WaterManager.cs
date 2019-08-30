using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour {

    public bool isWater = false;

    public float waterGrav;
    private float originGrav;

    public Color LightColor;

    private Color originLightColor;
    private Color originColor;
    private float originFogDesity;

    public Light CameraLight;

    public MasterManager masterManager;

    // Use this for initialization
    void Start () {
        LightColorSetting();
        originGrav = 0;
	}

    public void LightColorSetting()
    {
        originLightColor = masterManager.Weathercheck.SkyColor;
        originColor = RenderSettings.fogColor;
        originFogDesity = RenderSettings.fogDensity;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CameraCol")
        {
            CamGetWater();
        }

        if (collision.gameObject.tag == "Player")
        {
            GetWater(collision);
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "CameraCol")
        {
            CamGetOutWater();
        }

        if (collision.gameObject.tag == "Player")
        {
            GetOutWater(collision);
        }
    }

    public void CamGetWater()
    { 
        CameraLight.color = LightColor;
        masterManager.FogCheck.WaterinFog();

        if (masterManager.Weathercheck.isRaining) masterManager.Weathercheck.OffRainEffect();
    }

    public void CamGetOutWater()
    {
        CameraLight.color = masterManager.Weathercheck.SkyColor;
        masterManager.FogCheck.WaterOutFog();

        if (masterManager.Weathercheck.isRaining) masterManager.Weathercheck.OnRainEffect();
    }

    public void GetWater(Collider playerCol)
    {
        isWater = true;
    }

    public void GetOutWater(Collider playerCol)
    {
        if (isWater)
        {
            isWater = false;
        }
    }
}
