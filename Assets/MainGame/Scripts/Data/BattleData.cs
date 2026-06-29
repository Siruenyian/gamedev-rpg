using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Battle")]

public class BattleData : ScriptableObject
{
    public string battleName;
    public CharacterData player;
    public CharacterData enemy;
    public AudioClip battleMusic;
}