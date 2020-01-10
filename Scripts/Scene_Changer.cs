using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Changer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeScene();
    }

    //Changes scene when player loses or when enter is pressed, based on screen
    public void ChangeScene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Home_Screen"))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePlay");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver"))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Home_Screen");
            }
        }
    }

    //Ends the game when called
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
    
