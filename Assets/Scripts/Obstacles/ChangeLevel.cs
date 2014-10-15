using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

    public string iTargetScene;
    private bool mChangeTriggered = false;

    public bool mCheatable = false;

    void OnTriggerStay(Collider other)
    {
        TriggerChangesRequired();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mCheatable && Input.GetKeyDown(KeyCode.C))
        {
            TriggerChangesRequired();
        }
	}

    void TriggerChangesRequired()
    {
        if (iTargetScene != GameDefinitions.SceneNames.Menu)
        {
            if (Application.loadedLevelName == GameDefinitions.SceneNames.S01)
            {
                AchievementManager.UnlockAchievement("Achievement_01");
                if (InterfaceInGame.mLevelTimer < GameDefinitions.RecordTimes.S01)
                {
                    AchievementManager.UnlockAchievement("Achievement_03");
                }
            }
            if (Application.loadedLevelName == GameDefinitions.SceneNames.S02)
            {
                if (InterfaceInGame.mLevelTimer < GameDefinitions.RecordTimes.S02)
                {
                    AchievementManager.UnlockAchievement("Achievement_04");
                }
            }
            if (Application.loadedLevelName == GameDefinitions.SceneNames.S03)
            {
                if (InterfaceInGame.mLevelTimer < GameDefinitions.RecordTimes.S03)
                {
                    AchievementManager.UnlockAchievement("Achievement_05");
                }
            }
            if (Application.loadedLevelName == GameDefinitions.SceneNames.S04)
            {
                if (InterfaceInGame.mLevelTimer < GameDefinitions.RecordTimes.S04)
                {
                    AchievementManager.UnlockAchievement("Achievement_06");
                }
                if (InterfaceInGame.mNumDeaths == 0)
                {
                    AchievementManager.UnlockAchievement("Achievement_07");
                }
            }
            if (Application.loadedLevelName == GameDefinitions.SceneNames.S05)
            {
                if (InterfaceInGame.mLevelTimer < GameDefinitions.RecordTimes.S05)
                {
                    AchievementManager.UnlockAchievement("Achievement_08");
                }
                if (InterfaceInGame.mNumDeaths == 0)
                {
                    AchievementManager.UnlockAchievement("Achievement_09");
                }
            }
            if (Application.loadedLevelName == GameDefinitions.SceneNames.S06)
            {
                if (InterfaceInGame.mLevelTimer < GameDefinitions.RecordTimes.S06)
                {
                    AchievementManager.UnlockAchievement("Achievement_10");
                }
                if (InterfaceInGame.mNumDeaths == 0)
                {
                    AchievementManager.UnlockAchievement("Achievement_11");
                }
            }
        }

        if (iTargetScene != null && !mChangeTriggered)
        {
            GameManager.RequestLevelChange(iTargetScene);
            mChangeTriggered = true;
            InterfaceInGame.mStopTimer = true;
        }
        InterfaceInGame.mNumDeaths = 0;
    }
}
