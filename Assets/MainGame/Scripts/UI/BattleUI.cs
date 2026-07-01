using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("Buttons")]

    public Button attackButton;
    public Button defenseButton;
    public Button runButton;
    public Button victorycontinueButton;
    public Button defeatcontinueButton;

    [SerializeField] BattleLog BattleLogger;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject defeatCanvas;

    private void Start()
    {
        HideEndScreens();
    }

    public void ShowVictory()
    {
        defeatCanvas.SetActive(false);
        victoryCanvas.SetActive(true);
    }

    public void ShowDefeat()
    {
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(true);
    }

    public void HideEndScreens()
    {
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(false);
    }

    public void SetTurnText(string text)
    {
        BattleLogger.Show(text);
    }

    public void EnableAttackButton(bool enabled)
    {
        attackButton.interactable = enabled;
    }
    public void EnableDefenseButton(bool enabled)
    {
        defenseButton.interactable = enabled;
    }
    public void EnableRunButton(bool enabled)
    {
        runButton.interactable = enabled;
    }
    public void EnableButtons(bool enabled)
    {
        attackButton.interactable = enabled;
        defenseButton.interactable = enabled;
        runButton.interactable = enabled;
    }
}