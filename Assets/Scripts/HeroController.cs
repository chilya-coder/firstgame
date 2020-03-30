using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 startPosition;
    private const float MAX_SPEED = 10;
    private bool isGrounded;
    private float ySpeed;
    private float previosYSpeed;
    public GameObject hit;
    private Vector2 hitPos;
    public float hitRate = 0.5f;
    private float canMakeNextHit = 0.5f;
    private bool isFacesRight { get; set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isFacesRight = true;
        previosYSpeed = 0;
        startPosition = rb.position;
    }
    void Death()
    {
        rb.position = startPosition;
        Vector3 localScale = transform.localScale;
        localScale.x = -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
        isFacesRight = true;
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Crawl();
        ySpeed = (rb.position.y - previosYSpeed) * 100;
        previosYSpeed = rb.position.y;
        animator.SetFloat("ySpeed", ySpeed);
        if (Input.GetButtonDown("Fire1") && Time.time > canMakeNextHit)
        {
            canMakeNextHit = Time.time + hitRate;
            Hit();
        } else
        {
            animator.SetBool("isHitting", false);
        }
        if (rb.position.y < -10)
        {
            Death();
        }

    }
    private void FixedUpdate()
    {
    }
    void Run()
    {
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
        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Crawl ()
    {
        if (!Input.GetKey(KeyCode.DownArrow)) {
            animator.SetBool("isCrawling", false);
        } else
        {
            animator.SetBool("isCrawling", true);
        }
    }
    void Hit()
    {
        animator.SetBool("isHitting",true);
        hitPos = transform.position;
        if (isFacesRight)
        {
            hitPos.x += 1;
            Instantiate(hit, hitPos, Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//Передается когда входящий коллайдер контактирует с коллайдером данного объекта
    {
        if (collision.gameObject.tag == "Ground") // проверяем, стоит ли герой на объекте с тегом "Ground"
        {
            isGrounded = true; // если да - значит он приземлился
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false; // нет - значит он еще в полете
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }
}
