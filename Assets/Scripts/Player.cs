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
    public TimeControl time;
    public GameObject bullet;
    private new Renderer renderer;
    public GameObject deathObject;
    public HelpScript hs;
    public Pause pause;

    private Color originalColor;

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
    public bool isDead = false;

    public bool safeSpot = false;

    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource shootSound;
    public AudioSource deathSound;
    public AudioSource safeSpotSound;
    public AudioSource pickUpSound;
    public AudioSource stealSound;


    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pause>();

        hs = GameObject.FindGameObjectWithTag("Help").GetComponent<HelpScript>();
        time = GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>();

        walkSound = GameObject.FindGameObjectWithTag("WalkSound").GetComponent<AudioSource>();
        jumpSound = GameObject.FindGameObjectWithTag("PlayerJumpSound").GetComponent<AudioSource>();
        shootSound = GameObject.FindGameObjectWithTag("ShootSound").GetComponent<AudioSource>();
        deathSound = GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>();
        safeSpotSound = GameObject.FindGameObjectWithTag("FireSound").GetComponent<AudioSource>();
        pickUpSound = GameObject.FindGameObjectWithTag("PickUpSound").GetComponent<AudioSource>();
        stealSound = GameObject.FindGameObjectWithTag("StealSound").GetComponent<AudioSource>();


        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        originalColor = renderer.material.color;
        currHealth = maxHealth;
        currSanity = maxSanity;
        currFed = maxFed;
        mushroom = 1;
        weed = 1;
        wood = 1;
        stone = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.paused && !isDead)
        {
            if (time.isNight)
            {
                if (safeSpot)
                {
                    plusSanity(0.05f);
                    minusFed(0.05f);
                }
                else
                {
                    minusSanity(0.1f);
                    minusFed(0.1f);
                }
            }
            else
            {
                minusFed(0.01f);
            }


            //Animatornak értékek passzolása
            anim.SetBool("shoot", false);
            anim.SetBool("onGround", onGround);
            anim.SetFloat("speed", Mathf.Abs(rBody.velocity.x));

            if (rBody.velocity.x > 0.1f)
            {
                if (!walkSound.isPlaying)
                {
                    walkSound.Play();
                }
            }
            else
            {
                if (walkSound.isPlaying)
                {
                    walkSound.Pause();
                }       
            }

            //Átfordulás
            if (Input.GetAxis("Horizontal") < -0.1f)
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
                    jumpSound.Play();
                    doubleJump = true;
                    rBody.AddForce(Vector2.up * jumpForce * 150);
                }
                else if (doubleJump)
                {
                    jumpSound.Play();
                    doubleJump = false;
                    rBody.AddForce(Vector2.up * jumpForce * 100);
                }

            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                shootSound.Play();
                anim.SetBool("shoot", true);
                Instantiate(bullet, GameObject.FindGameObjectWithTag("ShootPoint").GetComponent<Transform>().position, this.transform.rotation);
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
                minusHealth(10);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                plusHealth(10);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                hs.switchText();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (mushroom > 0)
                {
                    if (currFed <= 80)
                    {
                        plusFed(20);
                        minusMushroom(1);
                    }
                    else
                    {
                        Debug.Log("Dont waste your mushromm, u moron");
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.C) && onGround)
            {
                if (weed>0 && wood > 0 && stone > 0)
                {
                    weed -= 1;
                    wood -= 1;
                    stone -= 1;
                    Destroy(GameObject.FindGameObjectWithTag("Campfire"));
                    Instantiate(campfire, new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z), this.transform.rotation);

                }
                else
                {
                    Debug.Log("Not enough resource for campfire");
                }
            }
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("EnemyMale"));
            Destroy(GameObject.FindGameObjectWithTag("EnemyThief"));
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Application.Quit();
            }
        }

        
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //Bal-Jobb nyíl
            float horizontalMove = Input.GetAxis("Horizontal");

            rBody.AddForce((Vector2.right * speed) * horizontalMove);
        }

        //Sebesség Limitek
        if (rBody.velocity.x > maxSpeed)
        {
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

        if (onGround)
        {
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
        mushroom -= amount;
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
        wood -= amount;
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
        weed -= amount;
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
        stone -= amount;
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

    void minusSanity(float amount)
    {
        currSanity -= amount;
        if (currSanity < 1)
        {
            OutOfSanity();
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
        Die();
    }

    void OutOfSanity()
    {
        Debug.Log("Out of Sanity");
        Die();
    }

    void OutOfFed()
    {
        Debug.Log("Out of Fed");
        Die();
    }

    void Restart()
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Die()
    {
        deathSound.Play();
        isDead = true;
        anim.Play("PlayerDead", -1, 0.0f);
        Debug.Log("You Died");
        float timeAmount = time.elapsedTime % 1;
        float days = time.daysCount - 1;
        Debug.Log("You have survived for " + days + " days, " + Mathf.Floor(timeAmount % 1f * 24).ToString("00") + " hours, " + Mathf.Floor(((timeAmount % 1f * 24) % 1f) * 60).ToString("00") + " minutes.");
        var obj = Instantiate(deathObject, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.position, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform.rotation);
        obj.transform.parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform;

        AudioSource[] sounds;
        sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource sound in sounds)
        {
            if(sound.CompareTag("DayMusic") || sound.CompareTag("NightMusic") || sound.CompareTag("DeathSound"))
            {
                //Debug.Log("Skipped: " + sound.tag);
                continue;
            }
            //Debug.Log("Stopped: " + sound.tag);
            sound.Stop();
        }
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
            pickUpSound.Play();
            plusMushroom(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Wood"))
        {
            pickUpSound.Play();
            plusWood(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Weed"))
        {
            pickUpSound.Play();
            plusWeed(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Stone"))
        {
            pickUpSound.Play();
            plusStone(1);
            Destroy(collision.gameObject);
        }


        if (collision.CompareTag("Campfire"))
        {
            safeSpot = true;
            if (!safeSpotSound.isPlaying)
            {
                safeSpotSound.Play();
            }
            plusHealth(0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
        {
            safeSpotSound.Pause();
            safeSpot = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
        {
            plusHealth(0.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyMale")
        {
            minusHealth(0.5f);
            collision.gameObject.GetComponent<EnemyControl>().reachedPlayer = true;
            renderer.material.color = Color.red;
        }

        if (collision.gameObject.tag == "EnemyThief")
        {
            santaStealedMyStuffs();
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyMale")
        {
            minusHealth(0.5f);
            collision.gameObject.GetComponent<EnemyControl>().reachedPlayer = true;  
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyMale")
        {
            collision.gameObject.GetComponent<EnemyControl>().reachedPlayer = false;
            renderer.material.color = originalColor;
        }
    }

    private void santaStealedMyStuffs()
    {
        stealSound.Play();
        mushroom = 0;
        weed = 0;
        wood = 0;
        stone = 0;
}
}