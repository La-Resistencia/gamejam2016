using UnityEngine;
using System.Collections;
using System;

public class MultiplayerController : MonoBehaviour {

    private bool isInRequest = false;
    private WWW www = null;

	// Use this for initialization
	void Start () {
        www = new WWW("http://ws.gamejam2016.laresistencia.pe/ping");

        StartCoroutine(handleWWW(www));
	}

    IEnumerator handleWWW(WWW _www)
    {
        yield return _www;

        Debug.Log("returned " + _www.text);
        StartCoroutine(handleWWW(www));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
