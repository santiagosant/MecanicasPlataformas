using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 2f;
    public float maxSpeed = 5f;
    private Rigidbody2D rb2d;
    public float jumpPower = 6.5f;
    public bool jump;
    public bool grounded;
    public bool doubleJump;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (grounded) {
                jump = true;
                doubleJump = true;
            }else if (doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movimiento horizontal y control de velocidad
        float h = Input.GetAxis("Horizontal");

        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed); 
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        

        //Salto

        if (jump)
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
       
    }


    //controla la variable ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }
}
