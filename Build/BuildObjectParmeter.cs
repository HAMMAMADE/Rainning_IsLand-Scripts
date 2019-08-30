using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObjectParmeter : MonoBehaviour
{
    public Material CanMat;

    public Material CantMat;

    public bool CanBuild;

    public bool WaterTarget;

    private void Start()
    {
        if (!WaterTarget)
        {
            CanBuild = true;
            SetMat();
        }
        else
        {
            CanBuild = false;
            SetMat();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject)
        {
            gameObject.GetComponent<MeshRenderer>().material = CantMat;
            CanBuild = false;
        }

        if(WaterTarget && collision.gameObject.tag == "OnWater")
        {
            CanBuild = true;
            SetMat();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (!WaterTarget && collision.gameObject)
        {
            CanBuild = true;
            SetMat();
        }

        if (WaterTarget && collision.gameObject.tag == "Water")
        {
            CanBuild = false;
            SetMat();
        }
    }

    public void SetMat()
    {
        if (CanBuild)
        {
            gameObject.GetComponent<MeshRenderer>().material = CanMat;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = CantMat;
        }
    }
}
