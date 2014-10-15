using UnityEngine;
using System.Collections;

public class GameDefinitions {

    public class SceneNames
    {
        public const string Loading = "LevelLoading";
        public const string Menu = "MenuScene";
        public const string S01 = "Level01";
        public const string S02 = "Level02";
        public const string S03 = "Level03";
        public const string S04 = "Level04";
        public const string S05 = "Level05";
        public const string S06 = "Level06";
        public const string Achievements = "AchievementScreen";
    }

    public class RecordTimes
    {
        public const float S01 = 5.5f;
        public const float S02 = 4.5f;
        public const float S03 = 7.5f;
        public const float S04 = 18f;
        public const float S05 = 8f;
        public const float S06 = 14f;
        public const float SGG = 60f;
        public const float Van = 90f;
    }

    public enum Level
    {
        Load = 0,
        Menu,
        Options,
        L_One
    }
}
