using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour {
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        print("clicked");

    }

    void OnMouseUp()
    {
        print("up");
        for(int i = 0; i < Main.Instance.planes.Count; i++)
        {
            if(transform == Main.Instance.planes[i].plane.transform)
            {
                Main.Instance.planes[i].dirx = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
                Main.Instance.planes[i].diry = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
            }
        }
    }
}
