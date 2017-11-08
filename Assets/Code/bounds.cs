using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
