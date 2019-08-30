using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSponManager : MonoBehaviour
{
    public MasterManager masterManager;

    public Transform[] Sponers;

    public GameObject[] TargetObjects;

    int SponerNum;

    int ObjectNum;

    private void Start()
    {
        StartCoroutine("ObjectSponeCount");
    }

    public void ObjectSpone()
    {
        SponerNum = Random.Range(0, Sponers.Length);

        if(Sponers[SponerNum].childCount <= 0)
        {
            ObjectNum = Random.Range(0, TargetObjects.Length);
            Instantiate(TargetObjects[ObjectNum], Sponers[SponerNum]);
        }
    }

    IEnumerator ObjectSponeCount()
    {
        ObjectSpone();
        yield return new WaitForSecondsRealtime(30f);
        StartCoroutine("ObjectSponeCount");
    }
}
