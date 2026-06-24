using UnityEngine;
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