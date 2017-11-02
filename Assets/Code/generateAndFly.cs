using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateAndFly : MonoBehaviour {

    float x;
    float y;
    float dirx;
    float diry;
    // Use this for initialization
    float speed = 1.0f;
	void Start () {
        
        float r;
        r = Random.Range(0.0f, 1.0f);
        if(r < 0.5f)
        {
            x = Random.Range(1.1f, 1.2f);
            y = Random.Range(0.0f, 1.0f);
            dirx = -x;
            diry = Random.Range(0.0f, 1.0f);
        }
        else
        {
            x = Random.Range(0.0f, 1.0f);
            y = Random.Range(1.1f, 1.2f);
            dirx = Random.Range(0.0f, 1.0f);
            diry = -y;
        }
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 10.0f));
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.ViewportToWorldPoint(new Vector3(dirx, diry, 10.0f)), step);
    }
}
