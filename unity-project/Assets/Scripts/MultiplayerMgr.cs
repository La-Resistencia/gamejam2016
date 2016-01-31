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
    public HeroController heroController;
    public HeroController otherController;

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

        heroController = currentCat.GetComponent<HeroController>();
        otherController = otherCat.GetComponent<HeroController>();

        SendPosition();
    }

    private void SendPosition()
    {
        WWWForm form = new WWWForm();
        //form.AddField("x", (currentCat.transform.localPosition.x + ndeltaX).ToString());
        //form.AddField("y", (currentCat.transform.localPosition.y + ndeltaY).ToString());
        //form.AddField("session", timeStamp);
        //Vector2 cur_cat_command = currentCat.GetComponent<HeroController>().sendCommand();

        form.AddField("x", heroController.lastHor.ToString());
        form.AddField("y", heroController.lastVer.ToString());
        form.AddField("z", "1");
        form.AddField("session", timeStamp);
   
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

            var cur_posx = float.Parse(data[0]);
            var cur_posy = float.Parse(data[1]);
            var cur_posz = float.Parse(data[2]);

            var oth_posx = float.Parse(data[3]);
            var oth_posy = float.Parse(data[4]);
            var oth_posz = float.Parse(data[5]);
            
            Debug.Log("RECIEVING: cat1x:" + _www.text);
            //currentCat.GetComponent<HeroController>().receiveCommand(new Vector3(cur_posx, cur_posy, 0.0f));
            //otherCat.GetComponent<HeroController>().receiveCommand(new Vector3(oth_posx, oth_posy, 0.0f));

            heroController.DoCommand(cur_posx, cur_posy);
            otherController.DoCommand(oth_posx, oth_posy);

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
