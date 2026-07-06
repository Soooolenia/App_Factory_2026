using UnityEngine;

public class CreditEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CreditEnd Triggered");
        GameManager.Instance.LoadSceneAtIndex(1);
    }
}
