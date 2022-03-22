using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public int LevelToLoad;

    void OnTriggerEnter2D(Collider2D col)
    {

        SceneManager.LoadScene(LevelToLoad);

    }
}
