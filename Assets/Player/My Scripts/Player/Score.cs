using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
// "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"
    public int score = 0;
    public float CurrentScore;
    public Text scoreText; 
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentScore += Time.deltaTime;
        scoreText.text = "Score: " + (int)CurrentScore;
    }
}
