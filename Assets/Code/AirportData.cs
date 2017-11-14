using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportData {

    public GameObject airport;
    public Vector2 position;
    public int capacity;
    public int landedPLanesCounter = 0;

    public AirportData(GameObject airport, int capacity)
    {
        this.airport = airport;
        this.capacity = capacity;
        this.position = airport.transform.position;

    }
    public bool canLand()
    {
        return landedPLanesCounter < capacity;
    }
}
