using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponeManager : MonoBehaviour
{
    public MasterManager masterManager;

    public Transform Sponer1Pos;

    public GameObject SponerObject1;

    IEnumerator Spone1()
    {
        yield return new WaitForSecondsRealtime(40f);
       // Debug.Log("늑대가 스폰됨");
        Instantiate(SponerObject1,Sponer1Pos);
        StartCoroutine("Spone1");
    }
}
