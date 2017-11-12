using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
