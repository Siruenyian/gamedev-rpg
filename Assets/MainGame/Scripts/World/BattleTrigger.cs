using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private BattleData battleData;

    public void encounter()
    {
        if (BattleSession.Instance.StartEncounter(battleData))
        {
            SceneLoader.Instance.Load("BattleScene");
            return;
        }
        Debug.Log("Unable to load battle!");
    }
}