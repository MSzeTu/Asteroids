using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Manager : MonoBehaviour
{
    int shipLives;
    public static int score;
    public int ShipLives
    {
        get { return shipLives; }
        set { shipLives = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        shipLives = 3;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Draws GUI
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 25;
        GUI.Box(new Rect(10, 10, 250, 80), "Lives left: " + shipLives + "\nCurrent Score: " + score);
    }

    //Lowers amount of lives on collision
    public void LowerLife()
    {
        shipLives--;
    }

    //Raises score if shot asteroid or fragment
    public void RaiseScore(bool isFragment)
    {
        if (!isFragment)
        {
            score += 20;
        }
        else
        {
            score += 50;
        }
        
    }
}
