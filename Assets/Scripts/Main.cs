using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
    
    private GameObject pc;
    private OVRCameraRig cameraRig;
    private Transform rightHand;
 
    public GameObject processNum;
    private GameObject theProcess;
    private TextMeshProUGUI mText;
    private int processInstanceNum;
    private float i;
    private float j;
    private float k;

    void Start()
    {
        i = 6.0f;
        j = 0.0f;
        k = 9.0f;
        processInstanceNum = 1;
    }

    void Update()
    {
        //if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        if (OVRInput.GetDown(OVRInput.Button.One))
        {

            theProcess = Instantiate(processNum, new Vector3(i, j, k), Quaternion.identity) as GameObject;
            // TextMeshPro mText = theProcess.GetComponent<TextMeshPro>();
            TextMeshPro mText = theProcess.GetComponentInChildren<TextMeshPro>();
            Debug.Log(theProcess.ToString());
            Debug.Log(mText.text);
            mText.SetText(processInstanceNum.ToString());
            processInstanceNum = processInstanceNum + 1;
            i = i + 0.4f;
            j = 0.0f;
            k = i + 0.4f;


        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log(theProcess.ToString());


        }
    }
}

