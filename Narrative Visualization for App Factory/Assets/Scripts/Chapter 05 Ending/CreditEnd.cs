using UnityEngine;

public class CreditEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.CloseCredits();
    }
}
