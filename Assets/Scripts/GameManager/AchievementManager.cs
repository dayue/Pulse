using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

    public enum AnimationStage
    {
        Hidden = 0,
        SwipeIn,
        Hold,
        SwipeOut
    }

    private static Dictionary<string, Achievement> mAchievements = new Dictionary<string, Achievement>();
    private static List<AchievementAnimation> mUnlockings = new List<AchievementAnimation>();

    public List<Texture> iAchievementTextures = new List<Texture>();
    public GameObject AchievementObject;

    public float iStayDuration = 1f;
    public float iSwipeSpeed = 2f;

    private float iTimer = 0f;
    private static bool mShowAnimation = false;
    private static Achievement mCurrentAchievement;
    private static AnimationStage mAnim = AnimationStage.Hidden;

    public class AchievementAnimation
    {
        public float iTimer = 0f;
        public Achievement mCurrentAchievement;
        public AnimationStage mAnim;
    }    

    public static Achievement GetAchievement(string iName)
    {
        if (mAchievements.ContainsKey(iName))
            return mAchievements[iName];
        else
            return null;
    }

    public static void UnlockAchievement(string iName)
    {
        if (mAchievements.ContainsKey(iName))
        {
            if (mAchievements[iName].mUnlocked)
                return;
            mAchievements[iName].Unlock();
            Debug.Log("Achievement Unlocked: " + iName);
            //mShowAnimation = true;
            //mAnim = AnimationStage.SwipeIn;
            //mCurrentAchievement = mAchievements[iName];

            AchievementAnimation l_temp = new AchievementAnimation();
            l_temp.iTimer = 0f;
            l_temp.mCurrentAchievement = mAchievements[iName];
            l_temp.mAnim = AnimationStage.SwipeIn;
            mUnlockings.Add(l_temp);
        }
        else
            Debug.Log("Achievement not found");
    }

    public void Initialise()
    {
        Achievement l_achievement01 = new Achievement();
        l_achievement01.iName = "Achievement_01";
        l_achievement01.SpriteIndex = 0;
        mAchievements.Add(l_achievement01.iName, l_achievement01);

        Achievement l_achievement02 = new Achievement();
        l_achievement02.iName = "Achievement_02";
        l_achievement02.SpriteIndex = 1;
        mAchievements.Add(l_achievement02.iName, l_achievement02);

        Achievement l_achievement03 = new Achievement();
        l_achievement03.iName = "Achievement_03";
        l_achievement03.SpriteIndex = 2;
        mAchievements.Add(l_achievement03.iName, l_achievement03);

        Achievement l_achievement04 = new Achievement();
        l_achievement04.iName = "Achievement_04";
        l_achievement04.SpriteIndex = 3;
        mAchievements.Add(l_achievement04.iName, l_achievement04);

        Achievement l_achievement05 = new Achievement();
        l_achievement05.iName = "Achievement_05";
        l_achievement05.SpriteIndex = 4;
        mAchievements.Add(l_achievement05.iName, l_achievement05);

        Achievement l_achievement06 = new Achievement();
        l_achievement06.iName = "Achievement_06";
        l_achievement06.SpriteIndex = 5;
        mAchievements.Add(l_achievement06.iName, l_achievement06);

        Achievement l_achievement07 = new Achievement();
        l_achievement07.iName = "Achievement_07";
        l_achievement07.SpriteIndex = 6;
        mAchievements.Add(l_achievement07.iName, l_achievement07);

        Achievement l_achievement08 = new Achievement();
        l_achievement08.iName = "Achievement_08";
        l_achievement08.SpriteIndex = 7;
        mAchievements.Add(l_achievement08.iName, l_achievement08);

        Achievement l_achievement09 = new Achievement();
        l_achievement09.iName = "Achievement_09";
        l_achievement09.SpriteIndex = 8;
        mAchievements.Add(l_achievement09.iName, l_achievement09);

        Achievement l_achievement10 = new Achievement();
        l_achievement10.iName = "Achievement_10";
        l_achievement10.SpriteIndex = 9;
        mAchievements.Add(l_achievement10.iName, l_achievement10);

        Achievement l_achievement11 = new Achievement();
        l_achievement11.iName = "Achievement_11";
        l_achievement11.SpriteIndex = 10;
        mAchievements.Add(l_achievement11.iName, l_achievement11);

        Achievement l_achievement12 = new Achievement();
        l_achievement12.iName = "Achievement_12";
        l_achievement12.SpriteIndex = 11;
        mAchievements.Add(l_achievement12.iName, l_achievement12);

        Achievement l_achievement13 = new Achievement();
        l_achievement13.iName = "Achievement_13";
        l_achievement13.SpriteIndex = 12;
        mAchievements.Add(l_achievement13.iName, l_achievement13);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //ShowAnimation();
        ShowAnimationList();
	}

    void ShowAnimationList()
    {
        if (mUnlockings.Count > 0)
        {
            bool l_destroy = false;
            //Debug.Log("Show animation: " + mAnim.ToString());
            if (mUnlockings[0].mAnim == AnimationStage.SwipeIn)
            {
                AchievementObject.GetComponent<GUITexture>().texture = iAchievementTextures[mUnlockings[0].mCurrentAchievement.SpriteIndex];
                AchievementObject.transform.position = Vector3.Lerp(AchievementObject.transform.position, new Vector3(0.85f, 0.85f, 1f), iSwipeSpeed * Time.deltaTime);
                if ((AchievementObject.transform.position - new Vector3(0.85f, 0.85f, 1f)).magnitude < 0.01f)
                {
                    mUnlockings[0].mAnim = AnimationStage.Hold;
                    //Debug.Log("Go to hold");
                }
            }
            if (mUnlockings[0].mAnim == AnimationStage.Hold)
            {
                mUnlockings[0].iTimer += Time.deltaTime;
                if (mUnlockings[0].iTimer > iStayDuration)
                {
                    mUnlockings[0].iTimer = 0f;
                    mUnlockings[0].mAnim = AnimationStage.SwipeOut;
                    //Debug.Log("Go to Swipeout");
                }
            }
            if (mUnlockings[0].mAnim == AnimationStage.SwipeOut)
            {
                AchievementObject.transform.position = Vector3.Lerp(AchievementObject.transform.position, new Vector3(1.35f, 0.85f, 1f), iSwipeSpeed * Time.deltaTime);
                if ((AchievementObject.transform.position - new Vector3(1.35f, 0.85f, 1f)).magnitude < 0.01f)
                {
                    mUnlockings[0].mAnim = AnimationStage.Hidden;
                    //Debug.Log("Go to done");
                    l_destroy = true;
                }
            }
            if (l_destroy)
                mUnlockings.RemoveAt(0);
        }
    }

    //void ShowAnimation()
    //{
    //    if (mShowAnimation)
    //    {
    //        //Debug.Log("Show animation: " + mAnim.ToString());
    //        if (mAnim == AnimationStage.SwipeIn)
    //        {
    //            AchievementObject.GetComponent<GUITexture>().texture = iAchievementTextures[mCurrentAchievement.SpriteIndex];
    //            AchievementObject.transform.position = Vector3.Lerp(AchievementObject.transform.position, new Vector3(0.85f, 0.85f, 1f), iSwipeSpeed * Time.deltaTime);
    //            if ((AchievementObject.transform.position - new Vector3(0.85f, 0.85f, 1f)).magnitude < 0.01f)
    //            {
    //                mAnim = AnimationStage.Hold;
    //                //Debug.Log("Go to hold");
    //            }
    //        }
    //        if (mAnim == AnimationStage.Hold)
    //        {
    //            iTimer += Time.deltaTime;
    //            if (iTimer > iStayDuration)
    //            {
    //                iTimer = 0f;
    //                mAnim = AnimationStage.SwipeOut;
    //                //Debug.Log("Go to Swipeout");
    //            }
    //        }
    //        if (mAnim == AnimationStage.SwipeOut)
    //        {
    //            AchievementObject.transform.position = Vector3.Lerp(AchievementObject.transform.position, new Vector3(1.35f, 0.85f, 1f), iSwipeSpeed * Time.deltaTime);
    //            if ((AchievementObject.transform.position - new Vector3(1.35f, 0.85f, 1f)).magnitude < 0.01f)
    //            {
    //                mAnim = AnimationStage.Hidden;
    //                //Debug.Log("Go to done");
    //                mShowAnimation = false;
    //            }
    //        }
    //    }
    //}
}
