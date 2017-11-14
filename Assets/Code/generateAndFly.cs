using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateAndFly : MonoBehaviour {

    // Use this for initialization
    float speed = 2.0f;

    void Start () {
        StartCoroutine(startPlanes());
    }
	
	// Update is called once per frame
	void Update () {

        for(int i = 0; i < Main.Instance.planes.Count; i++)
        {
            move(Main.Instance.planes[i]);
        }
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

    void move(PlaneData plane)
    {
        if (plane.startMovement)
        {
            float step = speed * Time.deltaTime;
            plane.plane.transform.position = Vector3.MoveTowards(plane.plane.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(plane.destinationPosition.x, plane.destinationPosition.y, 10.0f)), step);
        }

    }

    PlaneData generatePlanes()
    {
        PlaneData plane = new PlaneData();
        plane.plane = Instantiate(GameObject.Find("plane"));
        return plane;
    }

    IEnumerator startPlanes()
    {
        while (true)
        {
            if(Main.Instance.planes.Count == 0)
            {
                PlaneData plane = new PlaneData();
                plane.plane = GameObject.Find("plane");
                Main.Instance.planes.Add(plane);
                setStartingPosition(Main.Instance.planes[0]);
            }
            else
            {
                Main.Instance.planes.Add(generatePlanes());
                setStartingPosition(Main.Instance.planes[Main.Instance.planes.Count - 1]);
            }
            yield return new WaitForSeconds(3);
        }
    }
}
