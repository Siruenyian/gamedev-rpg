using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private Scene previousScene;
    [SerializeField] private GameObject worldRoot;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private string transitionInName = "BattleIn";
    [SerializeField] private string transitionOutName = "BattleOut";

    void Awake()
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
        transitionAnimator.gameObject.SetActive(false);
    }

    public void Load(string sceneName)
    {
        GameManager.Instance.ResetState();

        SceneManager.LoadScene(sceneName);
    }
    public void Load(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }


    public void LoadBattleWrapper(BattleData battleData, Action<BattleResult> onFinished)
    {
        StartCoroutine(LoadBattle(battleData, onFinished));
    }
    public IEnumerator LoadBattle(BattleData battleData, Action<BattleResult> onFinished)
    {
        if (transitionAnimator != null)
            yield return PlayTransition(transitionInName, leaveActive: true);

        BattleSession.Instance.StartEncounter(battleData);

        previousScene = SceneManager.GetActiveScene();

        yield return SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
        worldRoot.SetActive(false);
        transitionAnimator.gameObject.SetActive(false);
        yield return new WaitUntil(() =>
        {
            return BattleSession.Instance.lastResult != BattleResult.None;
        });

        yield return SceneManager.UnloadSceneAsync("BattleScene");
        onFinished?.Invoke(BattleSession.Instance.lastResult);
        if (previousScene.IsValid())
        {
            SceneManager.SetActiveScene(previousScene);
        }

        worldRoot.SetActive(true);
    }

    public void PlayBattleTransition()
    {
        StartCoroutine(PlayTransition(transitionOutName));
    }
    private IEnumerator PlayTransition(string clipname, bool leaveActive = false)
    {
        transitionAnimator.gameObject.SetActive(true);
        transitionAnimator.Play(clipname);
        yield return null;
        yield return new WaitUntil(() =>
            !transitionAnimator.IsInTransition(0) &&
            transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        if (!leaveActive)
            transitionAnimator.gameObject.SetActive(false);
    }

    public IEnumerator UnloadBattle()
    {
        yield return SceneManager.UnloadSceneAsync("BattleScene");
        if (previousScene.IsValid())
        {
            SceneManager.SetActiveScene(previousScene);
        }
        worldRoot.SetActive(true);
    }

    public void Unload(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadMenu()
    {
        Load("MainMenu");
    }

    public void StartNewGame()
    {
        Load("MainGame");
    }

    public void Reload()
    {
        Load(SceneManager.GetActiveScene().name);
    }
}