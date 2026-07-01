using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private BattleData battleData;
    [SerializeField] private UnityEvent onVictory;
    [SerializeField] private UnityEvent onDefeat;

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
        onVictory?.Invoke();
        // Destroy(this.gameObject);
    }

    private void HandleDefeat()
    {
        onDefeat?.Invoke();
    }
}