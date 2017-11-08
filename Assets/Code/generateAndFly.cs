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
            plane.x = Random.Range(1.1f, 1.2f);
            plane.y = Random.Range(0.0f, 1.0f);
            plane.dirx = -plane.x;
            plane.diry = Random.Range(0.0f, 1.0f);
        }
        else
        {
            plane.x = Random.Range(0.0f, 1.0f);
            plane.y = Random.Range(1.1f, 1.2f);
            plane.dirx = Random.Range(0.0f, 1.0f);
            plane.diry = -plane.y;
        }
        plane.plane.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(plane.x, plane.y, 10.0f));
    }

    void move(PlaneData plane)
    {
        
        float step = speed * Time.deltaTime;
        plane.plane.transform.position = Vector3.MoveTowards(plane.plane.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(plane.dirx, plane.diry, 10.0f)), step);
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
