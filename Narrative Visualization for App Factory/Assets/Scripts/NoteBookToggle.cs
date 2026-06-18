using UnityEngine;

public class NoteBookToggle : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;
    private void Start()
    {
        animator = GetComponent<Animator>();
        downButton.SetActive(false);
        upButton.SetActive(true);
    }
    public void NoteBookUp()
    {
        animator.SetTrigger("Up");
        downButton.SetActive(true);
        upButton.SetActive(false);
    }
    public void NoteBookDown()
    {
        animator.SetTrigger("Down");
        downButton.SetActive(false);
        upButton.SetActive(true);
    }
    public void AddNotes()
    {
        Debug.Log("Add Notes");
    }
}
