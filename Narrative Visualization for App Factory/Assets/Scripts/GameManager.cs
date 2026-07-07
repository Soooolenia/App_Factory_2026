using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentSceneIndex = 1;

    public static GameManager Instance;

    public delegate void OnCloseCreditsHandler();
    public event OnCloseCreditsHandler OnCloseCredits;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //Load Main Menu
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void LoadSceneAtIndex(int index)
    {
        SceneManager.UnloadSceneAsync(currentSceneIndex);

        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        currentSceneIndex = index;
    }

    public void LoadSceneAdditive(int index)
    {
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
    }
    public void CloseCredits()
    {
        SceneManager.UnloadSceneAsync(2);
        OnCloseCredits?.Invoke();
    }
}
