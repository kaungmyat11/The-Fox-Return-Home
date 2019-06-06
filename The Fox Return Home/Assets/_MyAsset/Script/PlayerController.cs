using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public GameObject MissionText;
    public GameObject restartButton;
    public GameObject player;
    public Animator animator;
    public bool facingRight = true;
    Rigidbody2D rb;
    public float speed = 10f;
    Vector3 movement;
    int score = 0;
    //float horizontal = 0f;
    public Text scoreText;
    private float jumpSpeed = 6f;
    private bool isJumping = false;
    Vector3 restartPoint;

    bool ropeJoint = false;
    public RopeAttached[] RopeAttached;
    public GameObject gameOvertext;

    bool die = false;
    bool win = false;

    public GameObject winText;

    //animator.SetBool("isDie", false);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        restartPoint = transform.position;
        MissionText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Horizontal");

        //Vector2 movement = new Vector2(h, 0);
        //rb.AddForce(movement * speed, ForceMode2D.Force);

        //float jumpInput = Input.GetAxis("Jump");

        if (rb.transform.position.y < -5)
        {
            Die();
        }
        //animator.SetBool("isDie", false);

        for(int i = 0; i < RopeAttached.Length; i++)
        {
            if (RopeAttached[i].ropeActive == true)
            ropeJoint = RopeAttached[i].ropeActive;
        }

        Invoke("DeleteMission", 2f);

        if(!die && !ropeJoint)
        {
            move();
        }

        if(die && Input.GetMouseButton(0))
        {
            Restart();
        }

        for (int i = 0; i < RopeAttached.Length; i++)
        {
            if (RopeAttached[i].ropeActive == true)
            {
                if (Input.GetMouseButton(1))
                {
                    Destroy(RopeAttached[i].curHook);
                    RopeAttached[i].ropeActive = false;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            score = score + 1;
            UpdateScore(score);
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.CompareTag("Enemy") && !die)
        {
            //Invoke("Die", 2f);
            Die();
        }
        if (other.gameObject.CompareTag("House") && score > 14)
        {
            Invoke("Win", 0.5f);
            win = true;
        }
    }

    void UpdateScore(int s)
    {
        scoreText.text = "Score : " + s;
    }
    
    void DeleteMission()
    {
        MissionText.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        ropeJoint = false;
        OnLanding();
    }

    void Win()
    {
        winText.SetActive(true);
        restartButton.SetActive(true);
        Time.timeScale = 0;
    }

    bool finished = false;

    void Die()
    {
        die = true;
        rb.Sleep();
        if(die && !finished)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            finished = true;
        }
        
        animator.SetBool("isDie", true);
        gameOvertext.SetActive(true);
        Invoke("DelayTime", 2f);
        //Time.timeScale = 0;
        //transform.position = restartPoint;
        //rb.WakeUp();
        //for(int i = 1; i < 20; i++ )
        //{
        //    if(i == 19)
        //    {
        //        Restart();
        //    }
        //}
    }

    private void move()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");  //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        movement.Set(h, 0, 0);
        movement = movement * speed * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(h));
        transform.position += movement;

        //jump
        if(CrossPlatformInputManager.GetButtonDown("Jump") && !isJumping)  //Input.GetKeyDown(KeyCode.Space)  Input.GetButtonDown("Jump")
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        //if(isJumping == false)
        //{
        //    animator.SetBool("isJumping", false);
        //}
        //else
        //{
        //    animator.SetBool("isJumping", true);
        //}

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if(h < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DelayTime()
    {
        Time.timeScale = 0;
    }

    void DeleteRope()
    {
        for (int i = 0; i < RopeAttached.Length; i++)   
        {
            if (RopeAttached[i].ropeActive == true)
            {
                if (CrossPlatformInputManager.GetButtonDown("1"))
                {
                    Destroy(RopeAttached[i].curHook);
                    RopeAttached[i].ropeActive = false;
                }
            }
        }
    }
}
