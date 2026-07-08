using UnityEngine;

public class ListenforCreditsEnd : MonoBehaviour
{
    [SerializeField] private GameObject mainGameCamera;
    [SerializeField] private GameObject cameraBounds;

    private void OnEnable()
    {
        if (mainGameCamera == null) return;
        if (GameManager.Instance == null) return;
        //Adds listener
        GameManager.Instance.OnCloseCredits += HandleCreditsClosed;
    }
    private void OnDisable()
    {
        if (mainGameCamera == null) return;
        if (GameManager.Instance == null) return;
        //Removes listener
        GameManager.Instance.OnCloseCredits -= HandleCreditsClosed;
    }
    private void HandleCreditsClosed()
    {
        mainGameCamera.SetActive(true);
        cameraBounds.SetActive(true);
    }
}
