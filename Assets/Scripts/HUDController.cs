using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text healthText;
    public Text sanityText;
    public Text fedText;

    public Text mushroomText;
    public Text weedText;
    public Text woodText;
    public Text stoneText;
    public Text saveSpotText;
    public Text helpText;

    public Player player;
    public TimeControl time;

    public bool fullText = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        time = GameObject.FindGameObjectWithTag("TimeControl").GetComponent<TimeControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time.isNight)
        {
            whiteText();
        }
        else
        {
            blackText();
        }

        if (player.currHealth < 20)
        {
            healthText.color = Color.red;
        }

        if (player.currSanity < 20)
        {
            sanityText.color = Color.red;
        }

        if (player.currFed < 20)
        {
            fedText.color = Color.red;
        }

        if (fullText)
        {
            //Leírás
            healthText.text = ("Health: " + (int) player.currHealth);
            sanityText.text = ("Sanity: " + (int) player.currSanity);
            fedText.text = ("Food: " + (int) player.currFed);
            mushroomText.text = ("Mushroom: " + (int) player.mushroom);
            weedText.text = ("Weed: " + (int) player.weed);
            woodText.text = ("Wood: " + (int) player.wood);
            stoneText.text = ("Stone: " + (int) player.stone);

            if (player.safeSpot)
            {
                saveSpotText.text = ("Campfire Nearby");
            }
            else
            {
                saveSpotText.text = ("");
            }
        }
        else
        {
            //Csak Statok
            healthText.text = (player.currHealth.ToString());
            sanityText.text = (player.currSanity.ToString());
            fedText.text = (player.currFed.ToString());
            mushroomText.text = (player.mushroom.ToString());
            weedText.text = (player.weed.ToString());
            woodText.text = (player.wood.ToString());
            stoneText.text = (player.stone.ToString());

            if (player.safeSpot)
            {
                saveSpotText.text = ("Campfire Nearby");
            }
            else
            {
                saveSpotText.text = ("");
            }
        }
    }

    void whiteText()
    {
        healthText.color = Color.white;
        sanityText.color = Color.white;
        fedText.color = Color.white;
        mushroomText.color = Color.white;
        weedText.color = Color.white;
        woodText.color = Color.white;
        stoneText.color = Color.white;
        saveSpotText.color = Color.white;
        helpText.color = Color.white;
    }

    void blackText()
    {
        healthText.color = Color.black;
        sanityText.color = Color.black;
        fedText.color = Color.black;
        mushroomText.color = Color.black;
        weedText.color = Color.black;
        woodText.color = Color.black;
        stoneText.color = Color.black;
        saveSpotText.color = Color.black;
        helpText.color = Color.black;
    }
}
