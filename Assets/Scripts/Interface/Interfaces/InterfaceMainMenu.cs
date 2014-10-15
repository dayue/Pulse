using UnityEngine;
using System.Collections;

public class InterfaceMainMenu : Interface {

    private string m_DifficultyString = "Normal";

    // Use this for initialisation
    public override void Initialise()
    {
        base.Initialise();
    }

    public override void ButtonCreation()
    {
        float l_buttonWidth = Screen.width / 2f - 50f;
        float l_buttonHeight = Screen.height / 5f - 50f;
        float l_buttonWidth2 = Screen.width / 2f /2f - 25f;
        createButton("MM_Start", "Begin (Start)", new Rect(50f, 100f + l_buttonHeight + 5f, l_buttonWidth, l_buttonHeight));
        createButton("MM_Normal", "Difficulty (Triangle): " + m_DifficultyString, new Rect(50f, 100f + (l_buttonHeight + 5f) * 2f, l_buttonWidth, l_buttonHeight));
        //createButton("MM_Hard", "Hard (Triangle)", new Rect(50f + l_buttonWidth2, 100f + (l_buttonHeight + 5f) * 2f, l_buttonWidth2, l_buttonHeight));
        createButton("MM_Achievements", "Achievements (Square)", new Rect(50f, 100f + (l_buttonHeight + 5f) * 3f, l_buttonWidth, l_buttonHeight));
        createButton("MM_Quit", "Quit (L + R)", new Rect(50f, 100f + (l_buttonHeight + 5f) *4f, l_buttonWidth, l_buttonHeight));
        createButton("MM_Credits", "Created by David Yue", new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.1f,
                                                                    Mathf.Clamp(Screen.width * 0.4f, 100f, 150f), Mathf.Clamp(Screen.height * 0.2f, 30f, 40f)));
        //createText("MM_Title", "Kagami", new Rect(Screen.width / 2f - 50f, 50f, 100f, 30f));
    }

    // This is the main update
    public override void UpdateInterface()
    {
        base.UpdateInterface();
        if (GetButtonPress("MM_Start") || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.InGame);
            //Application.LoadLevel(GameDefinitions.SceneNames.One);
            GameManager.RequestLevelChange(GameDefinitions.SceneNames.S01);
        }

        if (GetButtonPress("MM_Normal") || (Input.GetKeyDown(KeyCode.JoystickButton3)))
        {
            GameManager.mHardMode = !GameManager.mHardMode;
            if (GameManager.mHardMode)
                m_DifficultyString = "Hard";
            else
                m_DifficultyString = "Normal";
        }

        //if (GetButtonPress("MM_Hard") || (Input.GetKeyDown(KeyCode.JoystickButton3) && !GameManager.mHardMode))
        //{
        //    GameManager.mHardMode = true;
        //}
        if (GetButtonPress("MM_Achievements") || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.Achievements);
            GameManager.RequestLevelChange(GameDefinitions.SceneNames.Achievements);
        }
        if (GetButtonPress("MM_Quit") || (Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5)))
        {
            Application.Quit();
        }
    }

    //Unchaged
    public virtual void EnterInterface()
    {//This should handle enabling all the buttons of the interface
        base.EnterInterface();
    }

    public virtual void ExitInterface()
    {//this should handle disabling all the buttons of the interface
        base.ExitInterface();
    }
}
