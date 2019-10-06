using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Referenciák
    private Rigidbody2D rBody;
    private Animator anim;
    public GameObject campfire;

    //Mozgás
    public float maxSpeed = 3f;
    public float speed = 3f;
    public float jumpForce = 5f;

    //Segédek
    public bool onGround;
    public bool doubleJump;

    //Statok
    public float currHealth;
    public float maxHealth = 100f;

    public float currSanity;
    public float maxSanity = 100f;

    public float currFed;
    public float maxFed = 100f;

    public int mushroom;
    public int weed;
    public int wood;
    public int stone;

    public bool safeSpot = false;

    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        currHealth = maxHealth;
        currSanity = maxSanity;
        currFed = maxFed;
        mushroom = 1;
        weed = 2;
        wood = 3;
        stone = 4;

    }

    // Update is called once per frame
    void Update()
    {
        //Animatornak értékek passzolása
        anim.SetBool("onGround", onGround);
        anim.SetFloat("speed", Mathf.Abs(rBody.velocity.x));

        //Átfordulás
        if(Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        //Ugrás
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                doubleJump = true;
                rBody.AddForce(Vector2.up * jumpForce * 150);
            }
            else if (doubleJump){
                doubleJump = false;
                rBody.AddForce(Vector2.up * jumpForce * 100);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Stats();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            minusHealth(20);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Destroy(GameObject.FindGameObjectWithTag("Campfire"));
            Instantiate(campfire, new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z), this.transform.rotation);
        }
    }

    void FixedUpdate()
    {
        //Bal-Jobb nyíl
        float horizontalMove = Input.GetAxis("Horizontal");

        rBody.AddForce((Vector2.right * speed )* horizontalMove);

        //Sebesség Limitek
        if (rBody.velocity.x> maxSpeed) {
            rBody.velocity = new Vector2(maxSpeed, rBody.velocity.y);
        }

        if (rBody.velocity.x < -maxSpeed)
        {
            rBody.velocity = new Vector2(-maxSpeed, rBody.velocity.y);
        }

        //Csúszás lassítása
        Vector3 slowVelocity = rBody.velocity;
        slowVelocity.y = rBody.velocity.y;
        slowVelocity.z = 0f;
        slowVelocity.x *= 0.95f;

        if (onGround){
            rBody.velocity = slowVelocity;
        }
    }

    //Stat vezérlők
    //Inventory stats
    void plusMushroom(int amount)
    {
        mushroom += amount;
    }

    void minusMushroom(int amount)
    {
        mushroom += amount;
        if (mushroom < 0)
        {
            mushroom=0;
        }
    }

    void plusWood(int amount)
    {
        wood += amount;
    }

    void minusWood(int amount)
    {
        wood += amount;
        if (wood < 0)
        {
            wood = 0;
        }
    }

    void plusWeed(int amount)
    {
        weed += amount;
    }

    void minusWeed(int amount)
    {
        weed += amount;
        if (weed < 0)
        {
            weed = 0;
        }
    }

    void plusStone(int amount)
    {
        stone += amount;
    }

    void minusStone(int amount)
    {
        stone += amount;
        if (stone < 0)
        {
            stone = 0;
        }
    }


    //Player stats
    void minusHealth(float amount)
    {
        currHealth -= amount;
        if (currHealth < 1)
        {
            OutOfHealth();
        }
    }

    void plusHealth(float amount)
    {
        currHealth += amount;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    void plusSanity(float amount)
    {
        currSanity += amount;
        if (currSanity > maxSanity)
        {
            currSanity = maxSanity;
        }
    }

    void minusFed(float amount)
    {
        currFed -= amount;
        if (currFed < 1)
        {
            OutOfFed();
        }
    }

    void plusFed(float amount)
    {
        currFed += amount;
        if (currFed > maxFed)
        {
            currFed = maxFed;
        }
    }

    //Endgame eventek
    void OutOfHealth()
    {
        Debug.Log("Out of Health");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OutOfSanity()
    {
        Debug.Log("Out of Sanity");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OutOfFed()
    {
        Debug.Log("Out of Fed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Restart()
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Stats()
    {
        Debug.Log("MaxH: " + this.maxHealth);
        Debug.Log("CurrH: " + this.currHealth);
        Debug.Log("MaxS: " + this.maxSanity);
        Debug.Log("CurrS: " + this.currSanity);
        Debug.Log("MaxF: " + this.maxFed);
        Debug.Log("CurrF: " + this.currFed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mushroom")){
            plusMushroom(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Wood"))
        {
            plusWood(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Weed"))
        {
            plusWeed(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Stone"))
        {
            plusStone(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Stone"))
        {
            plusStone(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Campfire"))
        {
            safeSpot = true;
            plusHealth(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
        {
            safeSpot = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
        {
            plusHealth(0.02f);
        }
    }
}