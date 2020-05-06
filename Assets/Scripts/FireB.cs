using System.Net;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

[Serializable]
public class FireTask
{
    public string stringValue { get; set; }
}

[Serializable]
public class Fields
{
    public FireTask task { get; set; }
}

[Serializable]
public class Document
{
    public string name { get; set; }
    public Fields fields { get; set; }
    public DateTime createTime { get; set; }
    public DateTime updateTime { get; set; }
}

public class TaskObj
{
    public IList<Document> documents { get; set; }
}


public class FireB : MonoBehaviour
{
    private const string FB_API_KEY = "xxxxxxxxxxxxxxxx";
    private string projectID = "xxxxxxxx";
    private int i, j, k = 0;

    public GameObject prefab;
    private GameObject currentFlag;
    public static Text taskText;

    
    void Start()
    {

        GetTask();

    }

    public void GetTask()
    {

        string projectID = "xxxxxxxx";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", projectID));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        var obj = JsonConvert.DeserializeObject(jsonResponse) as dynamic;
        var jsonArray = obj.documents;
        string documentsArray = JsonConvert.SerializeObject(jsonArray);
        List<Document> documentsList = JsonConvert.DeserializeObject<List<Document>>(documentsArray);

        i = 7;
        j = 0;
        k = 9;

        for (var m = 0; m < documentsList.Count; m++)
        {

            currentFlag = Instantiate(prefab, new Vector3(i, j, k), Quaternion.identity) as GameObject;
            i = i + 1;
            j = j + 1;
            k = k + 1;
            Debug.Log("the position is, " + i + j + k);

            TextMeshPro mText = currentFlag.GetComponentInChildren<TextMeshPro>();
            mText.SetText(jsonArray[m].fields.name.stringValue.ToString());

        }
    }
}
  