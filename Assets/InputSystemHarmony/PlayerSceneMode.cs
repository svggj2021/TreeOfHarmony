using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneMode : MonoBehaviour
{
   public enum PlayerSceneModeType
    {
        Exploring,Fighting
    }

    public static PlayerSceneModeType mode=PlayerSceneModeType.Exploring;
}
