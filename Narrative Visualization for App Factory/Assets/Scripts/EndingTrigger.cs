using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject lastBound;
    [SerializeField] private GameObject endingBound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        lastBound.SetActive(false);
        endingBound.SetActive(true);
        //Debug.Log("Ending Sequence Started!");
    }
}
