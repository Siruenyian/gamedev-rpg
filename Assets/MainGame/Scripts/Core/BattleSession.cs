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
        lastResult = BattleResult.None;
    }

    public bool StartEncounter(BattleData battleData)
    {
        if (battleData == null)
        {
            return false;
        }
        this.battleData = battleData;
        lastResult = BattleResult.None;
        return true;
    }

    public BattleResult lastResult { get; private set; }
    public void EndEncounter(BattleResult result)
    {
        lastResult = result;
        this.battleData = null;
    }
}