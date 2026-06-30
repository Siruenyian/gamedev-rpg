using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private BattleData battleData;

    // public void encounter()
    // {
    //     if (BattleSession.Instance.StartEncounter(battleData))
    //     {
    //         // SceneLoader.Instance.Load("BattleScene");
    //         SceneLoader.Instance.StartCoroutine(SceneLoader.Instance.LoadBattle());
    //         return;
    //     }
    //     Debug.Log("Unable to load battle!");
    // }
    public void encounter()
    {
        SceneLoader.Instance.LoadBattlef(battleData, OnBattleFinished);
    }


    private void OnBattleFinished(BattleResult result)
    {
        if (result == BattleResult.Win)
            HandleVictory();
        else
            HandleDefeat();
    }
    private void HandleVictory()
    {
        // Destroy enemy
        // Unlock door
        Destroy(this.gameObject);
    }

    private void HandleDefeat()
    {
        // Respawn player
    }
}