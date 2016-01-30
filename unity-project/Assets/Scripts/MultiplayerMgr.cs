using UnityEngine;
using System.Collections;
using System;

public class MultiplayerMgr : MonoBehaviour {
    public const string BASE_URL = "http://10.10.10.13:9117";

    string timeStamp;
    private WWW www = null;

    public GameObject player1;
    public GameObject player2;

    private GameObject currentCat;
    private GameObject otherCat;
	// Use this for initialization
	void Start () {
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
            currentCat = player1;
            otherCat = player2;
        }
        else
        {
            currentCat = player2;
            otherCat = player1;
        }

        SendPosition();
    }

    private void SendPosition()
    {
        WWWForm form = new WWWForm();
        //form.AddField("x", (currentCat.transform.localPosition.x + ndeltaX).ToString());
        //form.AddField("y", (currentCat.transform.localPosition.y + ndeltaY).ToString());
        //form.AddField("session", timeStamp);
        form.AddField("x", (currentCat.GetComponent<HeroController>().Velocity.x).ToString());
        form.AddField("y", (currentCat.GetComponent<HeroController>().Velocity.y).ToString());
        form.AddField("session", timeStamp);
        //www = new WWW("http://ws.gamejam2016.laresistencia.pe/updateposition", form);
        www = new WWW(BASE_URL + "/updateposition", form);

        //ndeltaX = 0f;
        //ndeltaY = 0f;

        StartCoroutine(handleWWW(www));
    }

    IEnumerator handleWWW(WWW _www)
    {
        yield return _www;

        if (_www.text != "NIL")
        {
            string[] data = _www.text.Split(';');

            var posx = float.Parse(data[0]);
            var posy = float.Parse(data[1]);
            
            Debug.Log("RECIEVING: cat1x:" + _www.text);
            currentCat.GetComponent<HeroController>().receiveVelocity(new Vector3(posx, 0.0f, posy));
            //Vector3 position = currentCat.gameObject.transform.localPosition;
            //position.x = float.Parse(data[0]);
            //position.y = float.Parse(data[1]);
            //currentCat.gameObject.transform.localPosition = position;

            //position = otherCat.gameObject.transform.localPosition;
            //position.x = float.Parse(data[2]);
            //position.y = float.Parse(data[3]);
            //otherCat.gameObject.transform.localPosition = position;
        }

        //Debug.Log("returned " + _www.text);
        SendPosition();
    }

    private string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmssffff");
    }

	// Update is called once per frame
	void Update () {
        SendPosition();
	}
}
