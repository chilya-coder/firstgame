using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator animator;
    const float MAX_SPEED = 10;
    bool isFacesRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        RunAnimation();
    }
    private void FixedUpdate()
    {
    }
    void Run()
    {
        animator.SetBool("isRunning", true);
        float speed = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        animator.SetFloat("Speed", Mathf.Abs(speed));
        rb.velocity = new Vector2(speed * MAX_SPEED, rb.velocity.y);
        if (speed < 0 && isFacesRight || speed > 0 && !isFacesRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacesRight = !isFacesRight;
        Vector3 localSkale = transform.localScale;
        localSkale.x = -localSkale.x;
        transform.localScale = localSkale;
    }

    void RunAnimation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
