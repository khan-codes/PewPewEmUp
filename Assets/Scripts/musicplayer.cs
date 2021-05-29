using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class musicplayer : MonoBehaviour
{
    void Awake()
    {
        int numOfPlayers = FindObjectsOfType<musicplayer>().Length;

        if (numOfPlayers > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
