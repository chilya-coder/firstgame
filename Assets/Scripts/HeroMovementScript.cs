using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator animator;
    const float MAX_SPEED = 10;
    bool isFacesLeft = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
    private void FixedUpdate()
    {
    }
    void Run()
    {
        float speed = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));
        rb.velocity = new Vector2(speed * MAX_SPEED, rb.velocity.y);
        if (speed > 0 && isFacesLeft || speed < 0 && !isFacesLeft)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacesLeft = !isFacesLeft;
        Vector3 localSkale = transform.localScale;
        localSkale.x = -localSkale.x;
        transform.localScale = localSkale;
    }
}
