using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public GameObject nivelCompletado;
    public GameObject menu;
    public GameObject proxNivel;
    public float speed = 2f;
    public float maxSpeed = 5f;
    private Rigidbody2D rb2d;
    public float jumpPower = 6.5f;
    private bool jump;
    private bool grounded;
    private bool doubleJump;
    private bool stop;
    public GameObject sonidoDeSalto;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Para saltar
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

        //Tecla para frenar eje X
        if (Input.GetKey(KeyCode.K))
        {
            stop = true;
        }

        //Menu de "pausa" (mentira porque no pause el juego)
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            menu.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            menu.SetActive(false);
        }

        //Recargar el nivel 
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

            //Sonido de salto
            GameObject sonido = Instantiate(sonidoDeSalto);
            Destroy(sonido, .5f);
        }

        //Stop-Frenado de golpe        
        if (stop)
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            stop = false;
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

    //Finalizar nivel
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nivelCompletado.SetActive(true);
        proxNivel.SetActive(true);
    }
}
