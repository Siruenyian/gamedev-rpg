using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class BattleLog : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private ScrollRect scrollRect;
    void Awake()
    {
        logText.text = "";
    }
    public void Show(string message)
    {
        if (string.IsNullOrEmpty(logText.text))
        {

            logText.text = message;
        }
        else
        {
            logText.text += "\n" + message;
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}