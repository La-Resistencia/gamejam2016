﻿using UnityEngine;
using System.Collections;
using System;

public class MultiplayerController : MonoBehaviour {
    public const string BASE_URL ="http://10.10.10.13:9117";

    public const float deltaX = 0.5f;
    public const float deltaY = 0.5f;

    public CatController cat1;
    public CatController cat2;

    private CatController currentCat;
    private CatController otherCat;

    string timeStamp;
    private WWW www = null;

    private float ndeltaX = 0f;
    private float ndeltaY = 0f;
    private bool chase = false;
    // Use this for initialization
    void Start ()
    {
        timeStamp = GetTimestamp(DateTime.Now);
        WWWForm form = new WWWForm();
        form.AddField("session", timeStamp);
        //WWW _www = new WWW("http://ws.gamejam2016.laresistencia.pe/configuresession", form);
        WWW _www = new WWW(BASE_URL + "/configuresession", form);
        StartCoroutine(handleConfigureSession(_www));
	}

    IEnumerator handleConfigureSession(WWW _www)
    {
        yield return _www;

        if (_www.text == "cat1")
        {
            currentCat = cat1;
            otherCat = cat2;
        }
        else
        {
            currentCat = cat2;
            otherCat = cat1;
        }

        SendPosition();
    }

    IEnumerator handleWWW(WWW _www)
    {
        yield return _www;

        if(_www.text != "NIL")
        {
            string[] data = _www.text.Split(';');

            
            Vector3 position = currentCat.gameObject.transform.localPosition;
            position.x = (float.Parse(data[0]))*transform.localScale.x;
            position.y = float.Parse(data[1])*transform.localScale.y;
            currentCat.gameObject.transform.localPosition = position;

            position = otherCat.gameObject.transform.localPosition;
            position.x = float.Parse(data[2]) * transform.localScale.x;
            position.y = float.Parse(data[3]) * transform.localScale.y;
            otherCat.gameObject.transform.localPosition = position;
        }

        //Debug.Log("returned " + _www.text);
        SendPosition();
    }

    private void SendPosition()
    {
        WWWForm form = new WWWForm();
        form.AddField("x", (currentCat.transform.localPosition.x/transform.localScale.x + ndeltaX).ToString());
        form.AddField("y", (currentCat.transform.localPosition.y/transform.localScale.y + ndeltaY).ToString());
        form.AddField("session", timeStamp);
        
        if (chase)
        {
            www = new WWW(BASE_URL + "/catch", form);
            chase = false;
        }
        else
        {
            www = new WWW(BASE_URL + "/updateposition", form);
        }

        ndeltaX = 0f;
        ndeltaY = 0f;

        StartCoroutine(handleWWW(www));
    }

    private string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmssffff");
    }

    
	void Update () {
        if (currentCat == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            chase = true;
        }
	    else if(Input.GetKeyDown(KeyCode.UpArrow)){
            ndeltaY += deltaY;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ndeltaY -= deltaY;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ndeltaX -= deltaX;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ndeltaX += deltaX;
        }
	}
}
