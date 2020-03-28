using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    Rigidbody2D rg;
    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        startPosition = new Vector2(rg.position.x, rg.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (rg.position.y < -5)
        {
            rg.position = startPosition;
        }
    }
}
