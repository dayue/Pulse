using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceManager : MonoBehaviour {

    public enum Interfaces
    {
        TitleScreen = 0,
        MainMenu,
        Options,
        LevelOne,
        Blank,
        InGame,
        Achievements
    }
    public static Dictionary<Interfaces, Interface> mInterfaces = new Dictionary<Interfaces,Interface>();

    private static Interface mPrevInterface;
    private static Interface mNextInterface;
    private static Interface mCurrentInterface;

    private static bool mSwitchInterface = false;

    public static bool mSafe = true;

	void Awake ()
    {
        //ChangeInterface(mMainMenuInterface);
	}

    public void Initialise()
    {
        Interface l_blank = new Interface();
        l_blank.Initialise();
        mInterfaces.Add(Interfaces.Blank, l_blank);

        InterfaceMainMenu l_mainMenu = new InterfaceMainMenu();
        l_mainMenu.Initialise();
        mInterfaces.Add(Interfaces.MainMenu, l_mainMenu);

        InterfaceTitle l_titleScreen = new InterfaceTitle();
        l_titleScreen.Initialise();
        mInterfaces.Add(Interfaces.TitleScreen, l_titleScreen);

        InterfaceInGame l_inGame = new InterfaceInGame();
        l_inGame.Initialise();
        mInterfaces.Add(Interfaces.InGame, l_inGame);

        InterfaceAchievements l_Achieve = new InterfaceAchievements();
        l_Achieve.Initialise();
        mInterfaces.Add(Interfaces.Achievements, l_Achieve);

        ChangeInterface(l_mainMenu);
    }
	
	void Update ()
    {
        if (mSwitchInterface)
        {
            mSwitchInterface = false;
            if(mCurrentInterface != null)
                mCurrentInterface.ExitInterface();
            mPrevInterface = mCurrentInterface;
            mCurrentInterface = mNextInterface;
            if (mCurrentInterface != null)
                mCurrentInterface.EnterInterface();
            else
                Debug.Log("mCurrentInterface == null");
        }

        if (mCurrentInterface == null)
        {
            Debug.Log("Current interface null");
            return;
        }

        if (!mSafe)
            return;

        mCurrentInterface.UpdateInterface();
	}

    public static void ChangeInterface(Interface iNewInterface)
    {//interface by variable
        if (iNewInterface != mCurrentInterface && iNewInterface != null)
        {
            mSwitchInterface = true;
            mNextInterface = iNewInterface;
        }
    }

    public static void ChangeInterface(Interfaces iNewInterface)
    {//interface by enum
        Debug.Log("Change interface: " + iNewInterface.ToString());
        if (mInterfaces.ContainsKey(iNewInterface))
        {
            if (mInterfaces[iNewInterface] != mCurrentInterface)
            {
                mSwitchInterface = true;
                mNextInterface = mInterfaces[iNewInterface];
            }
        }
        else
        {
            Debug.Log("Interface not found");
        }
    }
}
