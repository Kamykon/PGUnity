using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour {

    private float startTime;
    private int numPoints = 500;
    int flightOrder = 2;
    public float speed = 1f;

    GameObject firstAirport;
    GameObject secondAirport;
    GameObject thirdAirport;
    GameObject fourthAirport;
    GameObject planeg;

    Vector3 firstAirportPosition;
    Vector3 secondAirportPosition;

    Vector3 firstAirportBoxColliderPosition;
    Vector3 secondAirportBoxColliderPosition;

    Vector3 center;
    Vector3 start;
    Vector3 finish;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
        firstAirport = GameObject.Find("lotnisko1");
        secondAirport = GameObject.Find("lotnisko2");
        thirdAirport = GameObject.Find("lotnisko3");
        fourthAirport = GameObject.Find("lotnisko4");
        planeg = GameObject.Find("New Sprite");
        if (flightOrder == 1)
        {
           oneToTwo();
        }
        if (flightOrder == 2)
        {
           twoToThree();
        }
        if (flightOrder == 3)
        {
            threeToFour();
        }
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    void oneToTwo()
    {
        firstAirportPosition = firstAirport.transform.position;
        secondAirportPosition = secondAirport.transform.position;
        Vector3[] positions = getBezier(firstAirportPosition, firstAirportPosition + new Vector3(3f, -11f, 0), secondAirportPosition - new Vector3(11f, 0f, 0f), secondAirportPosition);
        StartCoroutine(movePlane(positions));
        if (transform.position == secondAirportPosition)
        {
            flightOrder = 2;
            startTime = Time.time;
        }
    }

    void twoToThree()
    {
        firstAirportPosition = secondAirport.transform.position;
        secondAirportPosition = fourthAirport.transform.position;
        Vector3[] positions = getBezier(firstAirportPosition, firstAirportPosition - new Vector3(18f, 0, 0), secondAirportPosition - new Vector3(22f, 0f, 0f), secondAirportPosition);
        StartCoroutine(movePlane(positions));
    }

    private IEnumerator movePlane(Vector3[] positions)
    {
        for (int i = 0; i < numPoints; i++)
        {
            float step = speed * Time.deltaTime;
            transform.position = positions[i];
            yield return new WaitForSeconds(step);
        }
    }

    void threeToFour()
    {
        firstAirportPosition = thirdAirport.transform.position;
        secondAirportPosition = fourthAirport.transform.position;
        firstAirportBoxColliderPosition = thirdAirport.GetComponent<BoxCollider2D>().bounds.max;
        secondAirportBoxColliderPosition = fourthAirport.GetComponent<BoxCollider2D>().bounds.min;
        secondAirportBoxColliderPosition += new Vector3(0, secondAirport.GetComponent<BoxCollider2D>().bounds.size.y / 2, 0);
        Vector3 center = (firstAirportPosition + secondAirportPosition);
        float fracComplete = (Time.time - startTime) / 2;
        Vector3 start = firstAirportPosition - center;
        Vector3 finish = secondAirportBoxColliderPosition - center;

        transform.position = Vector3.Slerp(start, finish, fracComplete);
        transform.position += center;
        transform.position = Vector3.MoveTowards(transform.position, secondAirportPosition, fracComplete);
    }

    private Vector3[] getBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3[] positions = new Vector3[numPoints];
        for(int i = 1; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = calculateBubicBezierPoint(t, p0, p1, p2, p3);
        }
        return positions;
    }

    private Vector3 calculateBubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3 )
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;
    }
}
