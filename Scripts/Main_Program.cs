using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Program : MonoBehaviour
{
    public GameObject ship;
    Input_Manager input;
    GUI_Manager gui;
    Bullet_Manager bManager;
    Ship shipScript;
    Asteroid_Manager asteroidM;
    Scene_Changer sceneChange;
    ShakeBehavior screenShake;
    int incomingInput;
    public bool fired;
    bool isCoroutineExecuting;
    private AudioSource source;
    private AudioClip laser;
    private AudioClip crash;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        ship = Instantiate((GameObject)Resources.Load("Prefabs/ship"), new Vector3(0, 0, 0), Quaternion.identity);
        shipScript = ship.GetComponent<Ship>();
        input = this.GetComponent<Input_Manager>();
        gui = this.GetComponent<GUI_Manager>();
        gui.ShipLives = 3;
        asteroidM = this.GetComponent<Asteroid_Manager>();
        bManager = this.GetComponent<Bullet_Manager>();
        sceneChange = gameObject.GetComponent<Scene_Changer>();
        screenShake = Camera.main.GetComponent<ShakeBehavior>();
        isCoroutineExecuting = false;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        laser = audioSources[0].clip;
        crash = audioSources[1].clip;
    }

    // Update is called once per frame
    void Update()
    {
        //Handles a LOT of input results
        incomingInput = input.returnInput();
        shipScript.SetTransform();
        shipScript.WrapVehicle();
        switch (incomingInput)
        {
            case 1:
                shipScript.Drive();
                break;
            case 2:
                shipScript.RotateRight();
                shipScript.Slow();
                break;
            case 3:
                shipScript.RotateLeft();
                shipScript.Slow();
                break;
            case 4:
                shipScript.RotateRight();
                shipScript.Drive();
                break;
            case 5:
                shipScript.RotateLeft();
                shipScript.Drive();
                break;
            case 0:
                shipScript.Slow();
                if (fired == false)
                {
                    source.PlayOneShot(laser);
                    bManager.Fire(ship);
                    fired = true;
                    Invoke("UnFlag", 1);
                }
                break;
            case 10:
                shipScript.Drive();
                if (fired == false)
                {
                    source.PlayOneShot(laser);
                    bManager.Fire(ship);
                    fired = true;
                    Invoke("UnFlag", 1);
                }
                break;
            case 12:
                shipScript.RotateRight();
                if (fired == false)
                {
                    source.PlayOneShot(laser);
                    bManager.Fire(ship);
                    fired = true;
                    Invoke("UnFlag", 1);
                }
                shipScript.Slow();
                break;
            case 13:
                shipScript.RotateLeft();
                if (fired == false)
                {
                    source.PlayOneShot(laser);
                    bManager.Fire(ship);
                    fired = true;
                    Invoke("UnFlag", 1);
                }
                shipScript.Slow();
                break;
            case 14:
                shipScript.Drive();
                shipScript.RotateRight();
                if (fired == false)
                {
                    source.PlayOneShot(laser);
                    bManager.Fire(ship);
                    fired = true;
                    Invoke("UnFlag", 1);
                }
                break;
            case 15:
                shipScript.Drive();
                shipScript.RotateLeft();
                if (fired == false)
                {
                    source.PlayOneShot(laser);
                    fired = true;
                    Invoke("UnFlag", 1);
                }
                break;
            default:
                shipScript.Slow();
                break;

        }

        for (int i = 0; i < asteroidM.asteroids.Count; i++) //Runs ship and Asteroid collision
        {
            if (shipScript.CollisionDetection(asteroidM.asteroids[i], asteroidM.asteroids))
            {
                source.PlayOneShot(crash);
                screenShake.TriggerShake();
                shipScript.HaltShip();
                gui.LowerLife();
            }
        }

        for (int f = 0; f < asteroidM.fragments.Count; f++) //Runs ship and Fragment Collision
        {
            if (shipScript.CollisionDetection(asteroidM.fragments[f], asteroidM.fragments))
            {
                source.PlayOneShot(crash);
                screenShake.TriggerShake();
                shipScript.HaltShip();
                gui.LowerLife();
            }
        }

        for (int b = 0; b < bManager.bulletList.Count; b++) //Runs bullet and asteroid collision
        {
            int colliderResult = asteroidM.CollisionDetection(bManager.bulletList[b], bManager.bulletList);
            if (colliderResult == 1) //Collided with asteroid
            {
               source.PlayOneShot(crash);
               gui.RaiseScore(false);
               bManager.DeleteBullet(bManager.bulletList[b]);
            }
            else if (colliderResult == 2) //Collided with fragment
            {
                source.PlayOneShot(crash);
                gui.RaiseScore(true);
                bManager.DeleteBullet(bManager.bulletList[b]);
            }
        }
        //Ends the game if Ship lives reach 0
        if (gui.ShipLives == 0)
        {
            sceneChange.GameOver();
        }
    }

    //Unflags the fired boolean
    void UnFlag()
    {
        fired = false;
    }
}
