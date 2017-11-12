using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour {

    private int numPoints = 100;
    public float speed = 1f;
    private int xAxis = 1;
    private int yAxis = 1;
    private int xAxisAirport = 1;
    private int yAxisAirport = 1;
    private int touchCounter = 0;

    float width;
    float height;

    GameObject firstAirport;
    GameObject secondAirport;
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


    PlaneData plane;

    // Use this for initialization
    void Start () {

        plane = new PlaneData();
        plane.plane = transform.gameObject;
        Main.Instance.planes.Add(plane);
        setStartingPosition(plane);


        firstAirport = GameObject.Find("lotnisko1");
        secondAirport = GameObject.Find("lotnisko2");
        thirdAirport = GameObject.Find("lotnisko3");
        fourthAirport = GameObject.Find("lotnisko4");
        width = Camera.main.orthographicSize * Screen.width / Screen.height;
        height = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update () {
        startMoveAndRotate();
    }

    void OnMouseDown()
    {
        print("clicked");
    }

    void OnMouseUp()
    {
        print("up");
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);
        if (hit)
        {
            string name = hit.transform.gameObject.transform.name;
            print(name);
            if(name.Contains("lotnisko")) {
                plane.startMovement = false;
                switch (name)
                {
                    case "lotnisko1":
                        xAxisAirport = -1;
                        yAxisAirport = 1;
                        flyToTheAirport(firstAirport);
                        break;
                    case "lotnisko2":
                        xAxisAirport = 1;
                        yAxisAirport = 1;
                        flyToTheAirport(secondAirport);
                        break;
                    case "lotnisko3":
                        xAxisAirport = -1;
                        yAxisAirport = -1;
                        flyToTheAirport(thirdAirport);
                        break;
                    case "lotnisko4":
                        xAxisAirport = 1;
                        yAxisAirport = -1;
                        flyToTheAirport(fourthAirport);
                        break;
                }
            }
        }
    }
    void flyToTheAirport(GameObject airport)
    {
        int planeQuarter = getPlainQuarter();
        int airportQuarter = getAirportQuarter();
        print(planeQuarter);
        print(airportQuarter);
        touchCounter++;
        //if (transform.position.Equals(airport.transform.position)) {
        //	touchCounter = 0;
        //}
        if (planeQuarter == airportQuarter)
            landOnTheSameQuarter(airport);
        switch (planeQuarter)
        {
            case 1:
                switch (airportQuarter)
                {
                    case 2:
                        landOnTheQuarterNextToYou(airport);
                        break;
                    case 3:
                        landOnTheQuarterInAnotherCorner(airport);
                        break;
                    case 4:
                        landOnTheQuarterUnderOrAboveYours(airport);
                        break;
                }
                break;
            case 2:
                switch (airportQuarter)
                {
                    case 1:
                        landOnTheQuarterNextToYou(airport);
                        break;
                    case 3:
                        landOnTheQuarterUnderOrAboveYours(airport);
                        break;
                    case 4:
                        landOnTheQuarterInAnotherCorner(airport);
                        break;
                }
                break;
            case 3:
                switch (airportQuarter)
                {
                    case 1:
                        landOnTheQuarterInAnotherCorner(airport);
                        break;
                    case 2:
                        landOnTheQuarterUnderOrAboveYours(airport);
                        break;
                    case 4:
                        landOnTheQuarterNextToYou(airport);
                        break;
                }
                break;
            case 4:
                switch (airportQuarter)
                {
                    case 1:
                        landOnTheQuarterUnderOrAboveYours(airport);
                        break;
                    case 2:
                        landOnTheQuarterInAnotherCorner(airport);
                        break;
                    case 3:
                        landOnTheQuarterNextToYou(airport);
                        break;
                }
                break;
        }
    }
    void oneToTwo()
    {
        firstAirportPosition = firstAirport.transform.position;
        Vector3[] positions = getBezier(firstAirportPosition, firstAirportPosition + new Vector3(3f, -11f, 0), secondAirportPosition - new Vector3(11f, 0f, 0f), secondAirportPosition);
        StartCoroutine(movePlane(positions));
    }
    void landOnTheSameQuarter(GameObject airport)
    {
        airportPosition = airport.transform.position;
        Vector3 firstPoint = new Vector3(xAxis * (-width), yAxis * (-(height + 7)), 0);
        Vector3 secondPoint = new Vector3(xAxis * (-(width + 7)), airportPosition.y, 0);
        Vector3[] positions = getBezier(transform.position, firstPoint, secondPoint, airportPosition);
        StartCoroutine(movePlane(positions));
    }
    void landOnTheQuarterUnderOrAboveYours(GameObject airport)
    {
        airportPosition = airport.transform.position;
        Vector3 firstPoint = new Vector3(xAxis * (-(width + 1)), airportPosition.y, 0);
        Vector3 secondPoint = new Vector3(xAxis * (-(width + 6)), airportPosition.y, 0);
        Vector3[] positions = getBezier(transform.position, firstPoint, secondPoint, airportPosition);
        StartCoroutine(movePlane(positions));
    }
    void landOnTheQuarterInAnotherCorner(GameObject airport)
    {
        airportPosition = airport.transform.position;
        Vector3 firstPoint = new Vector3(transform.position.x, airportPosition.y, 0);
        Vector3 secondPoint = new Vector3(transform.position.x - (xAxis * 3), airportPosition.y, 0);
        Vector3[] positions = getBezier(transform.position, firstPoint, secondPoint, airportPosition);
        StartCoroutine(movePlane(positions));
    }

    void landOnTheQuarterNextToYou(GameObject airport)
    {
        airportPosition = airport.transform.position;
        Vector3 firstPoint = new Vector3(transform.position.x - (xAxis * 6), airportPosition.y, 0);
        Vector3 secondPoint = new Vector3(transform.position.x - (xAxis * 6), yAxis * (-(height + 2)), 0);
        Vector3[] positions = getBezier(transform.position, firstPoint, secondPoint, airportPosition);
        StartCoroutine(movePlane(positions));
    }

    private IEnumerator movePlane(Vector3[] positions)
    {
        for (int i = 0; i < numPoints; i++)
        {
            float step = speed * Time.deltaTime;
            bezierRotate(positions[i]);
            transform.position = positions[i];
            yield return new WaitForSeconds(step);
        }
    }

    private Vector3[] getBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3[] positions = new Vector3[numPoints];
        for (int i = 1; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = calculateBubicBezierPoint(t, p0, p1, p2, p3);
        }
        return positions;
    }

    private Vector3 calculateBubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
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
    private void recognizeTheQuarter(Vector2 touchPosWorld2D)
    {

        if (touchPosWorld2D.x >= 0) //Y-axis right side
        {
            xAxis = 1;
        }
        else
        {
            xAxis = -1;
        }
        if (touchPosWorld2D.y >= 0) //Above X-axis
        {
            yAxis = 1;
        }
        else  //Under X-axis
        {
            yAxis = -1;
        }
    }
    private int getPlainQuarter()
    {
        if (xAxis == 1 && yAxis == 1)
            return 1;
        if (xAxis == -1 && yAxis == 1)
            return 2;
        if (xAxis == -1 && yAxis == -1)
            return 3;
        if (xAxis == 1 && yAxis == -1)
            return 4;
        return 0;
    }
    private int getAirportQuarter()
    {
        if (xAxisAirport == 1 && yAxisAirport == 1)
            return 1;
        if (xAxisAirport == -1 && yAxisAirport == 1)
            return 2;
        if (xAxisAirport == -1 && yAxisAirport == -1)
            return 3;
        if (xAxisAirport == 1 && yAxisAirport == -1)
            return 4;
        return 0;
    }



    void setStartingPosition(PlaneData plane)
    {
        float r;
        r = Random.Range(0.0f, 1.0f);
        if (r < 0.5f)
        {
            plane.startPosition = new Vector2(Random.Range(1.1f, 1.2f), Random.Range(0.0f, 1.0f));
            plane.destinationPosition = new Vector2(-plane.startPosition.x, Random.Range(0.0f, 1.0f));
        }
        else
        {
            plane.startPosition = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(1.1f, 1.2f));
            plane.destinationPosition = new Vector2(Random.Range(0.0f, 1.0f), -plane.startPosition.x);
        }
        plane.plane.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(plane.startPosition.x, plane.startPosition.y, 10.0f));
    }

    void startMoveAndRotate()
    {
        if (plane.startMovement)
        {
            float step = speed * Time.deltaTime;
            plane.plane.transform.position = Vector3.MoveTowards(plane.plane.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(plane.destinationPosition.x, plane.destinationPosition.y, 10.0f)), step);
            Vector3 dir = Camera.main.ViewportToWorldPoint(new Vector3(plane.destinationPosition.x, plane.destinationPosition.y, 0.0f));
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir - transform.position);
        }
    }

    void bezierRotate(Vector3 pos)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, pos - transform.position);
    }
}
