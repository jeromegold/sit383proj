using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using System.Net.Security;
using System.Net;
using System.IO;
using System;
using System.Security.Cryptography.X509Certificates;


public class MapTile : MonoBehaviour
{  
    public Material mapMaterial;
    public GameObject marker;
    public GameObject mapPlane;

    private float longitude = 0.0f;
    private float latitude = 0.0f;
    private int zoom = 0;

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
         return true;
    }

    void Start()
    {
        ServicePointManager.
        ServerCertificateValidationCallback = TrustCertificate;
        Debug.Log("MapTile Manager started");
        updateMapView();
    }

    private void getTileCoordinates(float longitude,
    float latitude, int zoom, out int x, out int y)
    {
        x = (int)(Mathf.Floor((longitude + 180.0f) /
        360.0f * Mathf.Pow(2.0f, zoom)));
        y = (int)(Mathf.Floor((1.0f - Mathf.Log(Mathf.
        Tan(latitude * Mathf.PI / 180.0f) + 1.0f /
        Mathf.Cos(latitude * Mathf.PI / 180.0f)) /
        Mathf.PI) / 2.0f * Mathf.Pow(2.0f, zoom)));

        Debug.Log("GetTileCoords started");
    }

    private void getGeoCoordinates(int x, int y, int zoom, out float longitude, out float latitude)
    {
        float n = Mathf.PI - 2.0f * Mathf.PI * y / Mathf.
        Pow(2.0f, zoom);

        longitude = x / Mathf.Pow(2.0f, zoom) * 360.0f -
        180.0f;
        latitude = 180.0f / Mathf.PI * Mathf.Atan(0.5f *
        (Mathf.Exp(n) - Mathf.Exp(-n)));

        Debug.Log("GetGeoCoords started");
    }

    private void updateTexture(int x, int y, int z)
    {
        string url = "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=46.414382,10.013988&heading=151.78&pitch=-0.76&key=AIzaSyABdg-YgFm4sCbjdjj8kR4aSksXROYDERE&signature=lBsZZ0YvK0mwhmeLmRrnFACAuzU=";
        Debug.Log("Retrieving: " + url);
        WebRequest www = WebRequest.Create(url);
        ((HttpWebRequest)www).UserAgent = "Map"; 

        var response = www.GetResponse();
        Debug.Log(response);

        Texture2D tex = new Texture2D(2, 2);
        ImageConversion.LoadImage(tex, new BinaryReader(response.GetResponseStream()).ReadBytes(1000000));
        mapMaterial.mainTexture = tex;
    }


    private void updateMapView()
    {
        int x;
        int y;
        getTileCoordinates(longitude, latitude, zoom, out x, out y);
        updateTexture(x, y, zoom);

        // Place a marker at the current position on the tile.
        float cornerLatA;
        float cornerLongA;
        float cornerLatB;
        float cornerLongB;
        getGeoCoordinates(x, y, zoom, out cornerLongA,
        out cornerLatA);
        getGeoCoordinates(x + 1, y + 1, zoom, out cornerLongB, out cornerLatB);

        float r = 10.0f * ((-(longitude - cornerLongA) / (
        cornerLongB - cornerLongA))) + 5.0f;
        float d = 10.0f * ((-(latitude - cornerLatA) / (
        cornerLatB - cornerLatA))) + 5.0f;
        marker.transform.position = mapPlane.transform.
        position - mapPlane.transform.forward * d +
        mapPlane.transform.right * r;
    }


    public void onButtonEvent(float dx, float dy, int dz)
    {
        zoom += dz;
        float step = 0.3f * 1.0f / Mathf.Pow(2.0f, zoom);
        longitude += 360.0f * dx * step;
        latitude += 180.0f * dy * step;

        updateMapView();
    }
    
    public void leftButton()
    {
        onButtonEvent(-1.0f, 0.0f, 0);
    }
    public void rightButton()
    {
        onButtonEvent(1.0f, 0.0f, 0);
    }
    public void upButton()
    {
        onButtonEvent(0.0f, 1.0f, 0);
    }
    public void downButton()
    {
        onButtonEvent(0.0f, -1.0f, 0);
    }
    public void inButton()
    {
        onButtonEvent(0.0f, 0.0f, 1);
    }
    public void outButton()
    {
        onButtonEvent(0.0f, 0.0f, -1);
    }
}