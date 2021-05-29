using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] float loadDelay = 2f;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        if ((int)Time.timeSinceLevelLoad >= 5)
        {
            deathFX.SetActive(true);
            SendMessage("DiableControlls");
            Invoke("LoadLevel", loadDelay);
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
