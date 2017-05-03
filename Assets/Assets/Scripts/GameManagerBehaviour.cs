﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

    public Text goldLabel;
    private int gold;
    public Text waveLabel;
    public GameObject[] nextWaveLabels;
    public Text healthLabel;
    public GameObject[] healthIndicator;

    public bool gameOver = false;


    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
        }
    }
    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }
    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            // creating shake effect when attacked
            if (value < health)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
            }
            // update labels with private variables
            health = value;
            healthLabel.text = "HEALTH: " + health;
            // If health drops to 0 and it's not yet game over, set gameOver to true and trigger the GameOver animation.
            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            }
            // remove one monster from the cookie
            for (int i = 0; i < healthIndicator.Length; i++)
            {
                if (i < Health)
                {
                    healthIndicator[i].SetActive(true);
                }
                else
                {
                    healthIndicator[i].SetActive(false);
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
        Gold = 1000;
        Wave = 0;
        Health = 5;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
