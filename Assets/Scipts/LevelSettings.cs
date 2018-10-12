using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

    #region Singleton
    public static LevelSettings Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

    }
    #endregion

    public enum LEVEL {COMMON, RAFT, BEDROOM}
    public enum GAMEMODE {ONE, TWO}
    public enum DEADMODE {HEIGHT, OUT}


    public LEVEL level;
    public GAMEMODE gameMode;
    public DEADMODE deadMode;

    public bool deathPenalty;
    public bool permanentDeath;

}
