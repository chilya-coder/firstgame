using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator animator;
    const float MAX_SPEED = 10;
    bool isGrounded;
    public static bool isFacesRight { get; set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isFacesRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        RunAnimation();
        Jump();
        JumpAnimation();
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
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        transform.localScale = localScale;
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * 12f, ForceMode2D.Impulse);
        }

    }

    void JumpAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") // проверяем, стоит ли герой на объекте с тегом "Ground"
        {
            isGrounded = true; // если да - значит он приземлился
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false; // нет - значит он еще в полете
        }
    }
}
