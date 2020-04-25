using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject uiPause;  

    void Update()
    {
        //toggle pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    /// <summary>
    /// Toggleling Pause menu
    /// </summary>
    public void PauseMenu()
    {
        // checks after pressing "escape" button if pause Menu is active or not

        // if pause Menu is not active
        if (uiPause.activeInHierarchy == false)
        {
            // pause Menu is setting active
            uiPause.gameObject.SetActive(true);
       
            // disable enviroment physics
            Time.timeScale = 0;

            // disable Player controls 
            GameObject.Find("Player").GetComponent<Player>().enabled = false;     
        }

        // if pause menu is active after pressing "escape" button
        else
        {
            // pause menu is setting inactive
            uiPause.gameObject.SetActive(false);

            // enable enviroment physics
            Time.timeScale = 1;

            // enable Player and controls
            GameObject.Find("Player").GetComponent<Player>().enabled = true;
        }
    }

    /// <summary>
    /// return to Pausemenu button
    /// </summary>
    public void Back()
    {
        uiPause.gameObject.SetActive(true);
    }

    /// <summary>
    /// Return to Main menu
    /// </summary>
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
