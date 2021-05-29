using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int healthPoints;
    ScoreBoard scoreBoard;
    int scorePerHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        setScorePerHit();
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void setScorePerHit()
    {
        if (gameObject.tag == "Enemy10")
        {
            scorePerHit = 10;
            healthPoints = 5;
        }
        else if (gameObject.tag == "Enemy20")
        {
            scorePerHit = 20;
            healthPoints = 10;
        }

        else if (gameObject.tag == "Enemy30")
        {
            scorePerHit = 30;
            healthPoints = 20;
        }
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)      // delay particle collision detection due to initial high speeds
    {
        if ((int)Time.timeSinceLevelLoad >= 5)
        {
            ProcessScore();

            if (healthPoints <= 0)
            {
                KillEnemy();    // the space ship gets destroyed
            }

        }
    }

    private void ProcessScore()
    {
        scoreBoard.ScoreHit(scorePerHit);
        healthPoints = healthPoints - 1;
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);  // start the explosion fx
        fx.transform.parent = parent;   // make them a part of an empty game object instead of enemy
                                        //gameObject.SetActive(false);  // another method for destroying the game object
        Destroy(gameObject);
    }
}
