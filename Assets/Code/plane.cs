using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour {

    private float startTime;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        var pos1 = GameObject.Find("lotnisko1").transform.position;
        var pos2 = GameObject.Find("lotnisko2").transform.position;
        Vector3 center = (pos1 + pos2);
        center += new Vector3(0, 8, 0);
        float fracComplete = (Time.time - startTime) / 2;
        Vector3 riseRelCenter = pos1 - center;
        Vector3 setRelCenter = pos2 - center;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
}
