using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject hero;
    Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
    void Run()
    {
        Vector2 heroPos = hero.transform.position;
        
        Vector2 velocity = rigidbody.velocity;
        if (heroPos.x - transform.position.x > 0)
        {
            velocity.x = 4f;
        }
        else if (heroPos.x - transform.position.x < 0)
        {
            velocity.x = -4f;
        }
        rigidbody.velocity = velocity;
    }
}
