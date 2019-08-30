using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.Text;
using System.IO;

public class AchiveManager : MonoBehaviour
{
    public static bool Achivements1;

    public static bool Achivements2;

    public static bool Achivements3;

    public static bool Achivements4;

    public static bool Achivements5;

    public static bool Achivements6;
    //----------------------------------------
    public BoxCollider AchiveSet1;
    public GameObject AchiveCover1;

    public BoxCollider AchiveSet2;
    public GameObject AchiveCover2;

    public BoxCollider AchiveSet3;
    public GameObject AchiveCover3;

    public BoxCollider AchiveSet4;
    public GameObject AchiveCover4;

    public BoxCollider AchiveSet5;
    public GameObject AchiveCover5;

    public BoxCollider AchiveSet6;
    public GameObject AchiveCover6;

    //----------------------------------------
    static string fileName = "/resources.json";

    public static DataVo dataVo;

    //public static DataVo dataVoInput;
    //--------------------------------

    public static bool Achive1Comp;
    public static bool Achive2Comp;
    public static bool Achive3Comp;
    public static bool Achive4Comp;
    public static bool Achive5Comp;
    public static bool Achive6Comp;

    //--------------------------------
    //public static bool Achive1;
    public void AchiveStart()
    {
       // SetChellenge();
        //Debug.Log(File.Exists(Application.dataPath.Replace("/Assets", "") + fileName));
        if (File.Exists(Application.dataPath.Replace("/Assets", "") + fileName))
        {
            StreamReader streamReader = new StreamReader(Application.dataPath.Replace("/Assets", "") + fileName);
            dataVo = JsonMapper.ToObject<DataVo>(streamReader.ReadToEnd());
           // dataVoInput = dataVo;

            Achivements1 = bool.Parse(dataVo.Achive1);

            Achivements2 = bool.Parse(dataVo.Achive2);

            Achivements3 = bool.Parse(dataVo.Achive3);

            Achivements4 = bool.Parse(dataVo.Achive4);

            Achivements5 = bool.Parse(dataVo.Achive5);

            Achivements6 = bool.Parse(dataVo.Achive6);

            streamReader.Close();
        }
        SetChellenge();
       // Debug.Log(Achivements1);
    }

    public static void UpdateAchive(bool boolean, int key)
    {
        switch (key)
        {
            case 1:
                Achivements1 = boolean;
                dataVo.Achive1 = Achivements1.ToString();
                break;
            case 2:
                Achivements2 = boolean;
                dataVo.Achive2 = Achivements2.ToString();
                break;
            case 3:
                Achivements3 = boolean;
                dataVo.Achive3 = Achivements3.ToString();
                break;

            case 4:
                Achivements4 = boolean;
                dataVo.Achive4 = Achivements4.ToString();
                break;

            case 5:
                Achivements5 = boolean;
                dataVo.Achive5 = Achivements5.ToString();
                break;

            case 6:
                Achivements6 = boolean;
                dataVo.Achive6 = Achivements6.ToString();
                break;
        }


        StringBuilder sb = new System.Text.StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.PrettyPrint = true;

        JsonMapper.ToJson(dataVo, writer);
        JsonData saveData = sb.ToString();

        File.WriteAllText(Application.dataPath.Replace("/Assets", "") + fileName, saveData.ToString());
    }

    public void DataAccess(DataVo dataVo)
    {
        Achivements1 = bool.Parse(dataVo.Achive1);


        Achivements2 = bool.Parse(dataVo.Achive2);


        Achivements3 = bool.Parse(dataVo.Achive3);


        Achivements4 = bool.Parse(dataVo.Achive4);


        Achivements5 = bool.Parse(dataVo.Achive5);


        Achivements6 = bool.Parse(dataVo.Achive6);


        SetChellenge();
    }

    public void SetChellenge()
    {
        if (Achivements1)
        {
            AchiveSet1.enabled = true;
            AchiveCover1.SetActive(false);
           // Achive1Comp = true;
        }

        if (Achivements2)
        {
            AchiveSet2.enabled = true;
            AchiveCover2.SetActive(false);
          //  Achive2Comp = true;
        }

        if (Achivements3)
        {
            AchiveSet3.enabled = true;
            AchiveCover3.SetActive(false);
            //Achive3Comp = true;
        }

        if (Achivements4)
        {
            AchiveSet4.enabled = true;
            AchiveCover4.SetActive(false);
            //Achive4Comp = true;
        }

        if (Achivements5)
        {
            AchiveSet5.enabled = true;
            AchiveCover5.SetActive(false);
            //Achive5Comp = true;
        }

        if (Achivements6)
        {
            AchiveSet6.enabled = true;
            AchiveCover6.SetActive(false);
           // Achive6Comp = true;
        }
    }

   /* IEnumerator CheckAchivements()
    {
        SetChellenge();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("CheckAchivements");
    }*/
}
