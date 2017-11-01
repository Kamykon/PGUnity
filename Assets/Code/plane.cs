using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour {

    private float startTime;
    int flightOrder = 1;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if(flightOrder == 1)
        {
            oneToTwo();
        }
        if(flightOrder == 2)
        {
            twoToThree();
        }
        if (flightOrder == 3)
        {
            threeToFour();
        }
    }

    void oneToTwo()
    {
        var pos1 = GameObject.Find("lotnisko1").transform.position;
        var pos2 = GameObject.Find("lotnisko2").transform.position;
        Vector3 center = (pos1 + pos2);
        center += new Vector3(0, 8, 0);
        float fracComplete = (Time.time - startTime) / 2;
        Vector3 start = pos1 - center;
        Vector3 finish = pos2 - center;

        transform.position = Vector3.Slerp(start, finish, fracComplete);
        transform.position += center;
        if(transform.position == pos2)
        {
            flightOrder = 2;
            startTime = Time.time;
        }
    }

    void twoToThree()
    {
        var pos1 = GameObject.Find("lotnisko2").transform.position;
        var pos2 = GameObject.Find("lotnisko3").transform.position;
        Vector3 center = (pos1 + pos2);
        center += new Vector3(0, 12, 0);
        float fracComplete = (Time.time - startTime) / 2;
        Vector3 start = pos1 - center;
        Vector3 finish = pos2 - center;

        transform.position = Vector3.Slerp(start, finish, fracComplete);
        transform.position += center;

        if (transform.position == pos2)
        {
            flightOrder = 3;
            startTime = Time.time;
        }
    }

    void threeToFour()
    {
        var pos1 = GameObject.Find("lotnisko3").transform.position;
        var pos2 = GameObject.Find("lotnisko4").transform.position;
        Vector3 center = (pos1 + pos2);
        center += new Vector3(12, 0, 0);
        float fracComplete = (Time.time - startTime) / 2;
        Vector3 start = pos1 - center;
        Vector3 finish = pos2 - center;

        transform.position = Vector3.Slerp(start, finish, fracComplete);
        transform.position += center;
    }
}
