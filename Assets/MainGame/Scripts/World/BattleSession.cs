using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSession : MonoBehaviour
{
    public static BattleSession Instance { get; private set; }
    public BattleData battleData { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool StartEncounter(BattleData battleData)
    {
        if (battleData == null)
        {
            return false;
        }
        this.battleData = battleData;
        return true;
    }

    public void EndEncounter()
    {
        this.battleData = null;
    }
}