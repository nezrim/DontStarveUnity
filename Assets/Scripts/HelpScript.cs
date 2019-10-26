using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{
    public Text helpText;

    public bool isOriginal;

    public string original = "Toggle 'H' for help";
    public string extended = "Move with Arrows\n'Space' to jump\n'F' to fire\n'ESC' to pause\n'C' to place campfire\n'E' to eat mushroom\n'R' to restart game";
    // Start is called before the first frame update
    void Start()
    {
        helpText.text = original;
        isOriginal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchText()
    {
        if (isOriginal)
        {
            helpText.text = extended;
            helpText.fontSize = 80;
            Time.timeScale = 0;
            isOriginal = false;
        }
        else
        {
            helpText.text = original;
            helpText.fontSize = 50;
            Time.timeScale = 1;
            isOriginal = true;
        }
    }
}
