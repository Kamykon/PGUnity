using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Main.Instance.airports.Add(new AirportData(GameObject.Find("lotnisko1"), Random.Range(1, 4)));
        Main.Instance.airports.Add(new AirportData(GameObject.Find("lotnisko2"), Random.Range(1, 4)));
        Main.Instance.airports.Add(new AirportData(GameObject.Find("lotnisko3"), Random.Range(1, 4)));
        Main.Instance.airports.Add(new AirportData(GameObject.Find("lotnisko4"), Random.Range(1, 4)));
        StartCoroutine(startPlanes());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator startPlanes()
    {
        while (true)
        {
            if (Main.Instance.planes.Count != 0)
            {
                Instantiate(GameObject.Find("plane"));
            }
            yield return new WaitForSeconds(5);
        }
    }
}
