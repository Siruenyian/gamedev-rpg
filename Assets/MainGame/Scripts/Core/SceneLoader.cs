using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

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

    private Scene previousScene;
    [SerializeField] private GameObject worldRoot;

    public void LoadBattlef(BattleData battleData, Action<BattleResult> onFinished)
    {
        StartCoroutine(LoadBattle(battleData, onFinished));
    }
    public IEnumerator LoadBattle(BattleData battleData, Action<BattleResult> onFinished)
    {
        // previousScene = SceneManager.GetActiveScene();
        // worldRoot.SetActive(false);
        // yield return SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);

        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
        BattleSession.Instance.StartEncounter(battleData);

        previousScene = SceneManager.GetActiveScene();
        worldRoot.SetActive(false);

        yield return SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));

        yield return new WaitUntil(() =>
        {
            Debug.Log($"Waiting... {BattleSession.Instance.lastResult}");
            return BattleSession.Instance.lastResult != BattleResult.None;
        });

        Debug.Log("Battle finished!");
        yield return SceneManager.UnloadSceneAsync("BattleScene");
        onFinished?.Invoke(BattleSession.Instance.lastResult);
        if (previousScene.IsValid())
        {
            SceneManager.SetActiveScene(previousScene);
        }

        worldRoot.SetActive(true);

    }

    public IEnumerator UnloadBattle()
    {
        yield return SceneManager.UnloadSceneAsync("BattleScene");
        if (previousScene.IsValid())
        {
            SceneManager.SetActiveScene(previousScene);
        }
        worldRoot.SetActive(true);
        // Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        // foreach (var camera in cameras)
        // {
        //     if (camera.CompareTag("MainCamera"))
        //     {
        //         camera.gameObject.SetActive(false);
        //     }
        // }
    }

    private void CleanupScene()
    {
        Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        foreach (var camera in cameras)
        {
            if (!camera.CompareTag("BattleCamera"))
            {
                camera.gameObject.SetActive(false);
            }
        }

        EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

        bool foundOne = false;

        foreach (var es in eventSystems)
        {
            if (!foundOne)
            {
                foundOne = true;
                es.gameObject.SetActive(true);
            }
            else
            {
                Destroy(es.gameObject);
            }
        }

        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);

        bool foundListener = false;

        foreach (var listener in listeners)
        {
            if (!foundListener)
            {
                foundListener = true;
                listener.enabled = true;
            }
            else
            {
                listener.enabled = false;
            }
        }
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