using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public enum ClueType
{
    Main,
    Secondary,
    None
}

public class TakeNotes : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private NoteBookToggle noteBookToggle;
    [SerializeField] private string clueDescription;
    [SerializeField] private NoteContentManager noteContentManager;
    [SerializeField] private ClueType clueType;
    [SerializeField] private ClueManager clueManager;
    [SerializeField] private NoteTakeToggle noteTakeToggle;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            CheckTap(Touchscreen.current.primaryTouch.position.ReadValue());
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            CheckTap(Mouse.current.position.ReadValue());
    }

    void CheckTap(Vector2 screenPos)
    {
        Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            WriteDownNotes();
            gameObject.SetActive(false);
        }
    }

    private async void WriteDownNotes()
    {
        noteBookToggle.NoteBookUp();
        await Task.Delay(1000);
        noteContentManager.takeNotes(clueDescription, onNoteWritten);
    }

    private void onNoteWritten()
    {
        noteTakeToggle.NoteTaken();

        switch (clueType)
        {
            case ClueType.Main:
                clueManager.MainClueNote();
                break;
            case ClueType.Secondary:
                clueManager.SecondaryClueUpdate(1);
                break;
            case ClueType.None:
                break;
        }
    }
}
