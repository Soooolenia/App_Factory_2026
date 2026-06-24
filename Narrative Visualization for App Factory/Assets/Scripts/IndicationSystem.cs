using UnityEngine;

public class IndicationSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject arrow;
    private void Start()
    {
        arrow.SetActive(false);
    }
    public void IndicationOn()
    {
        arrow.SetActive(true);
        animator.SetTrigger("On");
    }
}
