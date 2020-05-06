using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LineRenderer lr;
    public float maxLaserLength;
    RaycastHit hit;

    public LineRenderer cl;

    private GameObject pc;
    private GameObject customRightHand;
    private OVRCameraRig cameraRig;
    private Transform rightHand;
    private Transform forward;

    private Transform process1Location;
    private Transform process2Location;

    void Start()
    {
        pc = GameObject.Find("OVRPlayerController");
        OVRCameraRig cameraRig = pc.GetComponentInChildren<OVRCameraRig>();
        rightHand = cameraRig.rightHandAnchor;
    }

 
    void Update()
    {
      if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
      {
            Vector3 fwd = rightHand.transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(rightHand.transform.position, fwd, out hit, 100.0f))
            {
                lr.SetPosition(0, rightHand.transform.position);
                lr.SetPosition(1, hit.point);
                Debug.Log(hit.collider.gameObject.ToString());
            }
      }
    }
}

