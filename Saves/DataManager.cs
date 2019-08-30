using LitJson;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

}

public class DataManager : MonoBehaviour
{
    //public MasterManager masterManager;
    public AchiveManager achiveManager;
    public DataVo dataVo;
    string fileName = "/resources.json";
    bool isExistFile;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("데이터 로드 시작");
        if (!File.Exists(Application.dataPath.Replace("/Assets", "") + fileName))
        {
            dataVo = new DataVo();
            DataLoad(null);
            isExistFile = true;
        }
        else
        {
            StreamReader streamReader = new StreamReader(Application.dataPath.Replace("/Assets", "") + fileName);
            dataVo = JsonMapper.ToObject<DataVo>(streamReader.ReadToEnd());
            streamReader.Close();
            DataLoad(dataVo);
            isExistFile = false;
        }
        achiveManager.AchiveStart();
    }

    public void DataLoad(DataVo data)//초기화
    {
        if (data == null) // 데이터가 없는 경우
        {
            //Debug.Log("데이터 초기화");
            data = new DataVo();
            data.Achive1 = "false";
            data.Achive2 = "false";
            data.Achive3 = "false";
            data.Achive4 = "false";
            data.Achive5 = "false";
            data.Achive6 = "false";
        }

        StringBuilder sb = new System.Text.StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.PrettyPrint = true;

        JsonMapper.ToJson(data, writer);
        JsonData saveData = sb.ToString();

        if (!isExistFile)
        {
            var sr = File.CreateText(Application.dataPath.Replace("/Assets", "") + fileName);
            sr.WriteLine(saveData.ToString());
            sr.Close();
        }
        else
        {
            File.WriteAllText(Application.dataPath.Replace("/Assets", "") + fileName, saveData.ToString());
        }
    }

}
