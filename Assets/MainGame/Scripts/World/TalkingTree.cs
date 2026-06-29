using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkingTree : MonoBehaviour
{
    [SerializeField] private DialogueData keygivedialogueData;
    private int talkedCount;
    private bool hasTalked;
    [SerializeField] private DialogueActivator treedialogueActivator;
    void Awake()
    {
        hasTalked = false;
        talkedCount = 0;
    }
    public void UpdateTalkedCount()
    {
        if (hasTalked)
        {
            treedialogueActivator.ResetDialogue();
            return;
        }
        talkedCount++;
        if (talkedCount > 2)
        {
            treedialogueActivator.SetNewDialogue(keygivedialogueData);
            hasTalked = true;
            talkedCount = 0;
        }
    }

}