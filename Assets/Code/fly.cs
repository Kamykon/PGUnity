using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour {
    float speed = 10.0f;
    int flightOrder = 1;
    private Vector3 startPosition;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        startPosition.y += 4;
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update () {
        var pos1 = GameObject.Find("lotnisko1").transform.position;
        var pos2 = GameObject.Find("lotnisko2").transform.position;
        var pos3 = GameObject.Find("lotnisko3").transform.position;
        var pos4 = GameObject.Find("lotnisko4").transform.position;
        if (flightOrder == 1)
        {
            twoToOne();
        }
        if (flightOrder == 2)
        {
            oneToThree();
        }
        if (flightOrder == 3)
        {
            threeToFour();
        }
        if (flightOrder == 4)
        {
            fourToTwo();
        }
    }

    void twoToOne()
    {
        var pos1 = GameObject.Find("lotnisko1").transform.position;
        var pos2 = GameObject.Find("lotnisko2").transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2));
        transform.position = startPosition + new Vector3(distance / 2 * Mathf.Sin(Time.time), 2 * Mathf.Cos(Time.time), 0);
        var spriteRender = GameObject.Find("ss").GetComponent<SpriteRenderer>();
        if (spriteRender.bounds.Intersects(GameObject.Find("l1s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = false;
            startPosition.y = 0;
            startPosition.x = -8;
            transform.position = startPosition;
            flightOrder = 2;
        }
        if (spriteRender.bounds.Intersects(GameObject.Find("l2s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = true;
        }
    }
    
    void oneToThree()
    {
        var pos1 = GameObject.Find("lotnisko1").transform.position;
        var pos3 = GameObject.Find("lotnisko3").transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(pos3.x - pos1.x, 2) + Mathf.Pow(pos3.y - pos1.y, 2));
        transform.position = startPosition + new Vector3(Mathf.Cos(Time.time), -distance / 2 * Mathf.Sin(Time.time), 0);
        var spriteRender = GameObject.Find("ss").GetComponent<SpriteRenderer>();
        if (spriteRender.bounds.Intersects(GameObject.Find("l3s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = false;
            startPosition.y = -4;
            startPosition.x = 0;
            transform.position = startPosition;
            flightOrder = 3;
        }
        if (spriteRender.bounds.Intersects(GameObject.Find("l1s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = true;
        }
    }

    void threeToFour()
    {
        var pos3 = GameObject.Find("lotnisko3").transform.position;
        var pos4 = GameObject.Find("lotnisko4").transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(pos4.x - pos3.x, 2) + Mathf.Pow(pos4.y - pos3.y, 2));
        transform.position = startPosition + new Vector3(-distance / 2 * Mathf.Cos(Time.time), 2 * Mathf.Sin(Time.time), 0);
        var spriteRender = GameObject.Find("ss").GetComponent<SpriteRenderer>();
        if (spriteRender.bounds.Intersects(GameObject.Find("l4s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = false;
            startPosition.y = 0;
            startPosition.x = 8;
            transform.position = startPosition;
            flightOrder = 4;
        }
        if (spriteRender.bounds.Intersects(GameObject.Find("l3s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = true;
        }
    }

    void fourToTwo()
    {
        var pos2 = GameObject.Find("lotnisko2").transform.position;
        var pos4 = GameObject.Find("lotnisko4").transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(pos4.x - pos2.x, 2) + Mathf.Pow(pos4.y - pos2.y, 2));
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time), distance / 2 * Mathf.Cos(Time.time), 0);
        var spriteRender = GameObject.Find("ss").GetComponent<SpriteRenderer>();
        if (spriteRender.bounds.Intersects(GameObject.Find("l2s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = false;
            startPosition.y = 4;
            startPosition.x = 0;
            transform.position = startPosition;
            flightOrder = 1;
        }
        if (spriteRender.bounds.Intersects(GameObject.Find("l4s").GetComponent<SpriteRenderer>().bounds))
        {
            spriteRender.enabled = true;
        }
    }
}
