using UnityEngine;

public class ListenforCreditsEnd : MonoBehaviour
{
    [SerializeField] private GameObject mainGameCamera;
    [SerializeField] private GameObject cameraBounds;

    private void OnEnable()
    {
        //Adds listener
        GameManager.Instance.OnCloseCredits += HandleCreditsClosed;
    }
    private void OnDisable()
    {
        //Removes listener
        GameManager.Instance.OnCloseCredits -= HandleCreditsClosed;
    }
    private void HandleCreditsClosed()
    {
        mainGameCamera.SetActive(true);
        cameraBounds.SetActive(true);
    }
}
