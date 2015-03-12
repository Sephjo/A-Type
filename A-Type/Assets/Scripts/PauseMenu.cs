using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{

    private int buttonWidth = 200;
    private int buttonHeight = 50;
    private int groupWidth = 200;
    private int groupHeight = 170;
    private bool paused = false;

  public bool isPaused()
  {
    return paused;
  }

	// Use this for initialization
	void Start () {
        GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = true;
        Screen.lockCursor = true;
        Time.timeScale = 1;
	}

    void OnGUI()
    {
        if (paused)
        {
            GUI.BeginGroup(new Rect(((Screen.width/2)-(groupWidth/2)), ((Screen.height/2)-(groupHeight/2)),groupWidth, groupHeight ));
            if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),"Menu Principal"))
            {
                Application.LoadLevel(0);
            }
            if (GUI.Button(new Rect(0, 60, buttonWidth, buttonHeight), "Recommencer"))
            {
                Application.LoadLevel(3);
            }
            if (GUI.Button(new Rect(0, 120, buttonWidth, buttonHeight), "Quitter"))
            {
                Application.Quit();
            }
            GUI.EndGroup();
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            paused = TogglePause();
	}
    bool TogglePause()
    {
        if (Time.timeScale == 0)
        {
            GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = true;
            Screen.lockCursor = true;
            Time.timeScale = 1;
            return (false);
        }
        else
        {
            GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = false;
            Screen.lockCursor = false;
            Time.timeScale = 0;
            return (true);
        }
 
    }
}
