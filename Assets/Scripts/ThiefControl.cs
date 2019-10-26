using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefControl : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Animator anim;
    private new Renderer renderer;

    private Color originalColor;

    private Vector2 velocity;
    public Transform playerTransform;
    public Player player;
    public float maxSpeed = 0.01f;
    public float speed = 5f;

    public float reactionSpeed = 0.1f;

    public float life = 20f;

    public AudioSource santaSound;

    public bool followSound = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        santaSound = GameObject.FindGameObjectWithTag("SantaSound").GetComponent<AudioSource>();

        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        originalColor = renderer.material.color;
    }


    private void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rBody.velocity.x));

        if (rBody.velocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (rBody.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Debug.Log(reachedPlayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - playerTransform.transform.position.x) < 10)
        {
            if (!followSound)
            {
                santaSound.Play();
                followSound = true;
            }
            rBody.AddForce((Vector2.right * 100f) * getDirection());
        }

        if (rBody.velocity.x > maxSpeed)
        {
            rBody.velocity = new Vector2(maxSpeed, rBody.velocity.y);
        }

        if (rBody.velocity.x < -maxSpeed)
        {
            rBody.velocity = new Vector2(-maxSpeed, rBody.velocity.y);
        }

        renderer.material.color = originalColor;
    }

    int getDirection()
    {
        //Debug.Log(transform.position.x - player.transform.position.x);
        if (transform.position.x - playerTransform.transform.position.x > 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            renderer.material.color = Color.red;
            minusLife();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            renderer.material.color = originalColor;
        }
    }

    void minusLife()
    {
        life -= 1;
        if (life < 1)
        {
            if (santaSound.isPlaying)
            {
                santaSound.Stop();
            }
            this.gameObject.SetActive(false);
        }
    }
}
