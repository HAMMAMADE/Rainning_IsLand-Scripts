using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObjectParm : MonoBehaviour
{
    [SerializeField] private MasterManager masterManager;

    [SerializeField] private bool IthaveFire;
    [SerializeField] private bool ItisBait;
    [SerializeField] private bool ItisBoat;
    [SerializeField] private bool ItisHaze;

    public Rigidbody rigid;
    public GameObject thisObject;

    public int Time;
    bool interactPlayer;

    Vector3 nextPos;

    public int PlaypowerAdd;

    public int PlayFishingAdd;

    public float PlayHealthAdd;

    private void Start()
    {  
        masterManager = GameObject.Find("MasterManager").GetComponent<MasterManager>();
        Debug.Log(masterManager.Weathercheck.WaterHeight.transform.position.y);
        if (IthaveFire)
        {
           // masterManager = GameObject.Find("MasterManager").GetComponent<MasterManager>();
            StartCoroutine("CountingTime");
        }
        if (ItisBait)
        {
            StartCoroutine("CountingTimeBait");
        }

        masterManager.PlayerCheck.AddPower += PlaypowerAdd;
        masterManager.PlayerCheck.FishingTime += PlayFishingAdd;
        masterManager.PlayerCheck.AddHealth += PlayHealthAdd;

    }

    IEnumerator CountingTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time -= 1;

        if (interactPlayer) {
            masterManager.UiCheck.GetTime = Time;
            masterManager.UiCheck.FireTimeCheck();
        }

        if(Time <= 0)
        {
            Destroy(thisObject);
            Time = 0;

            masterManager.UiCheck.WaitInteract();
            masterManager.UiCheck.EndFireUI();
            interactPlayer = false;
            masterManager.PlayerCheck.interActFire = false;
            masterManager.PlayerCheck.isWithFire = false;

            StopCoroutine("CountingTime");
        }
        StartCoroutine("CountingTime");
    }

    IEnumerator CountingTimeBait()
    {
        yield return new WaitForSecondsRealtime(240f);
        Destroy(thisObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && IthaveFire &&!interactPlayer)
        {
            masterManager.fireCheck.fireParm = thisObject.GetComponent<BuildObjectParm>();
            masterManager.UiCheck.FireBaseReady();
            masterManager.PlayerCheck.interActFire = true;
            interactPlayer = true;
        }

        if (collision.gameObject.tag == "Player" && ItisHaze)
        { 
            masterManager.UiCheck.HazeReady();
            masterManager.PlayerCheck.interActHaze = true;
            masterManager.HazeCheck.Hazeparm = gameObject.GetComponentInChildren<HazeParameter>();
            //masterManager.PlayerCheck.interActFire = true;
        }

        if (collision.gameObject.tag == "OnWater"&& IthaveFire)
        {
            Time = 0;
            Destroy(thisObject);
        }

        if(collision.gameObject.tag == "OnWater" && ItisBoat)
        {
            nextPos = thisObject.transform.position;
            rigid.drag = 5500;
            rigid.useGravity = false;
            StartCoroutine("OnBoatWater");
        }

    }

    IEnumerator OnBoatWater()
    {
        // yield return new WaitForSeconds(0.05f);
        yield return null;
        if(thisObject.transform.position.y < masterManager.Weathercheck.WaterHeight.transform.position.y+0.2)//masterManager.Weathercheck.WaterHeight.transform.localPosition.y + 9.75f)
        {
            nextPos.y += 0.01f;
            thisObject.transform.position = nextPos;
            //StartCoroutine("OnBoatWater");
        }
        else
        {
            nextPos.y = masterManager.Weathercheck.WaterHeight.transform.position.y + 0.2f;
            thisObject.transform.position = nextPos;
           // StopCoroutine("OnBoatWater");
        }
        StartCoroutine("OnBoatWater");
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "OnWater" && ItisBoat)
        {
            rigid.drag = 0;
            rigid.useGravity = true;
            //StopCoroutine("OnBoatWater");
        }

        if (collision.gameObject.tag == "Player" && IthaveFire && interactPlayer)
        {
            masterManager.UiCheck.WaitInteract();
            masterManager.UiCheck.EndFireUI();
            interactPlayer = false;
            masterManager.PlayerCheck.interActFire = false;
            masterManager.PlayerCheck.isWithFire = false;
        }

        if (collision.gameObject.tag == "Player" && ItisHaze)
        {
            //masterManager.HazeCheck.Hazeparm = null;
            masterManager.UiCheck.WaitInteract();
            masterManager.PlayerCheck.interActHaze = false;
            masterManager.PlayerCheck.isWithHaze = false;
        }
    }

}
