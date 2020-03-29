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
    float ySpeed;
    float previosYSpeed;
    public static bool isFacesRight { get; set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isFacesRight = true;
        previosYSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        JumpAnimation();
        Crawl();
        ySpeed = (rb.position.y - previosYSpeed) * 100;
        previosYSpeed = rb.position.y;
        animator.SetFloat("ySpeed", ySpeed);
    }
    private void FixedUpdate()
    {
    }
    void Run()
    {
        animator.SetBool("isRunning", true);
        float speed = Input.GetAxis("Horizontal");
        if (speed != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        animator.SetFloat("speed", Mathf.Abs(speed));
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * 12f, ForceMode2D.Impulse);
        }

    }

    void JumpAnimation()
    {
        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        } else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Crawl ()
    {
        if (Input.GetAxis("Vertical") < 0) {
            animator.SetBool("isCrawling", true);
        } else
        {
            animator.SetBool("isCrawling", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//Передается когда входящий коллайдер контактирует с коллайдером данного объекта
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
