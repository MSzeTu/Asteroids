using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMesh scoreText = gameObject.GetComponent<TextMesh>();
        scoreText.text = "Final score: " + GUI_Manager.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
