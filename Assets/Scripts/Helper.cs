using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    //Tags are identified here to prevent any handwriting errors
    public static class Tags
    {
        public static readonly string player = "Player";
        public static readonly string obstacle = "Obstacles";
        public static readonly string diamond = "Diamond";
        public static readonly string finishLine = "FinishLine";
        public static readonly string sceneManagement = "SceneManagement";
    }

    public static class AnimationParameters
    {
        public static readonly string run1 = "Run1";
        public static readonly string run2 = "Run2";
        public static readonly string dance = "Dance";
    }
    //Playerpref tags are declared here to prevent handwriting errors
    public static class PlayerPrefs
    {
        public static readonly string currencyAmount = "CurrencyAmount";
        public static readonly string diamondAmount = "DiamondAmount";
        public static readonly string currentLevel = "CurrentLevel";
        public static readonly string currencyNeedsToUpgradeStack = "CurrencyNeedsToUpgradeStack";
        public static readonly string startStackAmount = "startStackAmount";
    }
    

   
    
} // Helper
