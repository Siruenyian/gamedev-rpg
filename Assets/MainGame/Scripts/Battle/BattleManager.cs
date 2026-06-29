using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public BattleState state;

    private BattleData battleData;

    [SerializeField] private BattleUnit player;
    [SerializeField] private BattleUnit enemy;

    [SerializeField] private BattleData defaultBattleData;
    [SerializeField] private BattleUI battleUI;
    [SerializeField] private BattleLog battleLogger;

    private void Start()
    {
        if (BattleSession.Instance.battleData == null)
        {
            battleData = defaultBattleData;
        }
        else
        {
            battleData = BattleSession.Instance.battleData;
        }

        Debug.Log(battleData);

        battleUI.Initialize(this);

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        state = BattleState.START;

        player.Initialize(battleData.player);

        enemy.Initialize(battleData.enemy);


        battleLogger.Show("Battle Start!");

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;

        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");


        battleLogger.Show("Player Turn");

        battleUI.EnableButtons(true);
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        battleUI.EnableButtons(true);
        StartCoroutine(PlayerAttack());
    }
    public void OnDefenseButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        battleUI.EnableButtons(true);
        StartCoroutine(PlayerDefense());
    }
    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        battleUI.EnableButtons(true);
        bool canRun = player.Run();
        if (canRun)
        {
            EndBattle();
            battleLogger.Show($"player Runs away!");
            return;
        }
        battleLogger.Show($"player's angle is broken, can't run!");
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerDefense()
    {

        int defenseValue = player.Defend();
        battleLogger.Show($"player doubles defense to {defenseValue}!");
        yield return new WaitForSeconds(1f);
        if (enemy.IsDead())
        {
            state = BattleState.WIN;

            EndBattle();

            yield break;
        }
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerAttack()
    {
        int trueDamage = enemy.TakeDamage(player.data.attack);
        battleLogger.Show($"player attacks for {trueDamage} damage!");

        yield return new WaitForSeconds(1f);

        if (enemy.IsDead())
        {
            state = BattleState.WIN;

            EndBattle();

            yield break;
        }

        state = BattleState.ENEMYTURN;

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        battleLogger.Show("Enemy Turn");

        yield return new WaitForSeconds(1f);
        int trueDamage = player.TakeDamage(enemy.data.attack);
        battleLogger.Show($"Enemy attacks for {trueDamage} damage!");


        if (player.IsDead())
        {
            state = BattleState.LOSE;

            EndBattle();

            yield break;
        }

        state = BattleState.PLAYERTURN;

        PlayerTurn();
    }

    void EndBattle()
    {
        battleUI.EnableButtons(false);

        if (state == BattleState.WIN)
        {
            battleLogger.Show("VICTORY");

            Debug.Log("Victory");
            SceneLoader.Instance.Load("GameScene");
        }

        if (state == BattleState.LOSE)
        {
            battleUI.SetTurnText("Defeat");
            battleLogger.Show("DEFEAT- skill issue");

            Debug.Log("Defeat");
            SceneLoader.Instance.Load("GameScene");

        }
    }
}