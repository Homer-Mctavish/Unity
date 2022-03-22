using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {

        //UI is disabled when the game starts 
        PauseUI.SetActive(false);

    }

    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {

            //this will enable to tooggle on or off
            paused = !paused;

        }

        if (paused)
        {

            // Shows UI on the screen and freezes the game time

            PauseUI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (!paused)
        {

            PauseUI.SetActive(false);
            Time.timeScale = 1;

        }

    }

    //By making this a public function we are able too see it on our On Click drop down menu

    public void Resume()
    {

        paused = false;
    }

    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {

        SceneManager.LoadScene(0);

    }

    public void Quit()
    {

        Application.Quit();

    }


}