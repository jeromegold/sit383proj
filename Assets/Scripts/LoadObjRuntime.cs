using System.Net;
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Dummiesman;

public class LoadObjRuntime : MonoBehaviour
{
    public GameObject prefab;
    private GameObject board;
    private string bucket = "xxxxxxxxxxxx";

    void Start()
    {
        
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            string objName = "wine.obj";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("xxxxxxxxxxxxxxxxxxxxxxxx", bucket, objName));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string readerString = reader.ReadToEnd();
            char[] myChars = readerString.ToCharArray();
      
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(myChars));
            var loadedObj = new OBJLoader().Load(textStream);
            loadedObj.transform.position = new Vector3(-6, 1, 9);
        }
    }
 

    IEnumerator DownloadFile()
    {
        Debug.Log("is this downloaded?");
        string objName = "board.obj";
        string link = String.Format("xxxxxxxxxxxxxxxxxxxxxxxxxxxxx", bucket, objName);
        var uwr = new UnityWebRequest(link, UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath, "prop.obj"); 
        uwr.downloadHandler = new DownloadHandlerFile(path);
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError(uwr.error);
                GameObject loadedObj = new OBJLoader().Load(path);
            }
            else
            {
                Debug.Log(path);
                GameObject loadedObj = new OBJLoader().Load(path);
                loadedObj.transform.position = new Vector3(-6, 1, 9);     
            }
    }

    public void ConvertMeshToUnityScale(GameObject theGameObject, float newSize, string axis = "y")
    {

        Renderer renderer = theGameObject.GetComponent<Renderer>();

        float size = renderer.bounds.size.y;
        if (axis.ToLower() == "x")
        {
            size = renderer.bounds.size.x;
        }
        else if (axis.ToLower() == "z")
        {
            size = renderer.bounds.size.z;
        }

        Vector3 rescale = theGameObject.transform.localScale;

        rescale.x = newSize * rescale.x / size;
        rescale.y = newSize * rescale.y / size;
        rescale.z = newSize * rescale.z / size;

        theGameObject.transform.localScale = rescale;

    }
}
