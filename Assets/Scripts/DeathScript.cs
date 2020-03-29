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
        startPosition = rg.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rg.position.y < -10)
        {
            rg.position = startPosition;
            Vector3 localScale = transform.localScale;
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
            HeroMovementScript.isFacesRight = true;
        }
    }
}
