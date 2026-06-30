using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public DefaultInputActions Input { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Input = new DefaultInputActions();
    }

    private void OnEnable()
    {
        Input.Enable();
        Input.Gameplay.Enable();
        Input.UI.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
        Input.Gameplay.Disable();
        Input.UI.Disable();
    }
}