using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour {

    private float startTime;
    private int numPoints = 100;
    int flightOrder = 2;
    public float speed = 1f;

    GameObject firstAirport;
    GameObject thirdAirport;
    GameObject fourthAirport;

    Vector3 firstAirportPosition;
    Vector3 secondAirportPosition;
    Vector3 airportPosition;

    Vector3 firstAirportBoxColliderPosition;
    Vector3 secondAirportBoxColliderPosition;

    Vector3 center;
    Vector3 start;
    Vector3 finish;

    Vector3 touchPosWorld;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
        firstAirport = GameObject.Find("lotnisko1");
        thirdAirport = GameObject.Find("lotnisko3");
        fourthAirport = GameObject.Find("lotnisko4");
        if (flightOrder == 1)
        {
            oneToTwo();
        }
        if (flightOrder == 2)
        {
        //    twoToThree();
        }
        if (flightOrder == 3)
        {
            threeToFour();
        }
    }

    // Update is called once per frame
    TouchPhase touchPhase = TouchPhase.Ended;

    void Update()
    {
        //We check if we have more than one touch happening.
        //We also check if the first touches phase is Ended (that the finger was lifted)
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
            if (Input.GetMouseButtonDown(0))
            {
            //We transform the touch position into word space from screen space and store it.
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                
                switch (touchedObject.transform.name)
                {
                    case "lotnisko1":
                        goToFirstAirport();
                        break;
                    case "lotnisko3":
                        goToThirdAirport();
                        break;
                    case "lotnisko4":
                        goToFourthAirport();
                        break;
                }
            }
        }
    }

    void oneToTwo()
    {
        firstAirportPosition = firstAirport.transform.position;
        Vector3[] positions = getBezier(firstAirportPosition, firstAirportPosition + new Vector3(3f, -11f, 0), secondAirportPosition - new Vector3(11f, 0f, 0f), secondAirportPosition);
        StartCoroutine(movePlane(positions));
        //if (transform.position == secondAirportPosition)
        //{
        //    flightOrder = 2;
        //    startTime = Time.time;
        //}
    }

    void goToFourthAirport()
    {
        airportPosition = fourthAirport.transform.position;
        Vector3[] positions = getBezier(transform.position, transform.position - new Vector3(18f, 0, 0), airportPosition - new Vector3(22f, 0f, 0f), airportPosition);
        StartCoroutine(movePlane(positions));
    }
    void goToThirdAirport()
    {
        airportPosition = thirdAirport.transform.position;
        Vector3[] positions = getBezier(transform.position, transform.position - new Vector3(0, 8f, 0), airportPosition + new Vector3(9f, 0f, 0f), airportPosition);
        StartCoroutine(movePlane(positions));
    }

    void goToFirstAirport()
    {
        airportPosition = firstAirport.transform.position;
        Vector3[] positions = getBezier(transform.position, transform.position - new Vector3(4, 8f, 0), airportPosition + new Vector3(13f, 0f, 0f), airportPosition);
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
        //secondAirportBoxColliderPosition += new Vector3(0, secondAirport.GetComponent<BoxCollider2D>().bounds.size.y / 2, 0);
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
