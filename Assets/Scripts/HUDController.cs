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

    public Player player;

    public bool fullText = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
}
