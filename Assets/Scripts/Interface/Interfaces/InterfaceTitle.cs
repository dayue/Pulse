using UnityEngine;
using System.Collections;

public class InterfaceTitle : Interface {

    // Use this for initialisation
    public override void Initialise()
    {
        base.Initialise();
    }

    public override void ButtonCreation()
    {
        float l_buttonWidth = Screen.width / 2f - 50f;
        float l_buttonHeight = Screen.height / 4f - 50f;
        createButton("TS_Start", "Start Game", new Rect(Screen.width/2f - l_buttonWidth/2f, Screen.height/2f + (0.2f*Screen.height), l_buttonWidth, l_buttonHeight));
    }

    // This is the main update
    public override void UpdateInterface()
    {
        base.UpdateInterface();
        if (GetButtonPress("TS_Start"))
        {
            InterfaceManager.ChangeInterface(InterfaceManager.Interfaces.MainMenu);
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
