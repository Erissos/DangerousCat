using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementDirection;
    public float speed;
    public float jumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack1 = 0;
    float nextAttack2 = 0;

    private bool isFacingRight = true;
    private bool isGrounded;

    Rigidbody2D rb;
    Animator anim;

    public GameObject groundCheck;

    public LayerMask groundLayer;

    public Transform attackPoint;
    public float attackDistance;
    public LayerMask enemyLayers;
    public float damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        Jump();
        CheckSurface();
        CheckAnimations();
        Attack1Control();
        Attack2Control();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
        anim.SetFloat("runSpeed", Mathf.Abs(movementDirection * speed));
    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void CheckRotation()
    {
        if (isFacingRight && movementDirection < 0)
        {
            Flip();

        }
        else if (!isFacingRight && movementDirection > 0)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckSurface()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
    }

    public void Attack1()
    {
        anim.SetTrigger("Attack1");
        EnemyAttackSystem();
    }

    public void Attack2()
    {
        anim.SetTrigger("Attack2");
        EnemyAttackSystem();
    }

    public void Attack1Control()
    {
        if (Time.time > nextAttack1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack1();
                nextAttack1 = Time.time + 1f / attackRate;
                if (Mathf.Abs(rb.velocity.x) > 0)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }
    }



    public void Attack2Control()
    {
        if (Time.time > nextAttack2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack2();
                nextAttack2 = Time.time + 10f / attackRate;
                if (Mathf.Abs(rb.velocity.x) > 0)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }
    }

    public void EnemyAttackSystem()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamege(damage);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackDistance);
    }
}


//DragonCubeGames
