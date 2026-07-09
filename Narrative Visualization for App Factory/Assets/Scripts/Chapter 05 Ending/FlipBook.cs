using Unity.VisualScripting;
using UnityEngine;

public class FlipBook : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int currentPageIndex = 0;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject endingButton;

    [SerializeField] private GameObject mapBounds;

    [SerializeField] private GameObject mainGameCamera;

    [SerializeField] private GameObject endingText;

    [SerializeField] private float currentVolumeLevel = 0;
    [SerializeField] private float actualVolumeLevel = 0;

    [SerializeField] private VolumeControl volumeControl;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource finishReadingButton;

    [SerializeField] private VolumeControl endCreditMusic;

    private void Start()
    {
        pages[currentPageIndex].SetActive(true);

        foreach (var page in pages)
        {
            if (page != pages[currentPageIndex])
            {
                page.SetActive(false);
            }
        }

        HandleButtonVisibility();
    }
    public void NextPage()
    {
        if (currentPageIndex >= pages.Length - 1) return;

        VolumeIncrease();

        pages[currentPageIndex].SetActive(false);
        currentPageIndex++;
        pages[currentPageIndex].SetActive(true);

        buttonClick.Play();

        HandleButtonVisibility();
    }
    public void PreviousPage()
    {
        if (currentPageIndex == 0) return;

        pages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        pages[currentPageIndex].SetActive(true);

        buttonClick.Play();

        HandleButtonVisibility();
    }
    private void HandleButtonVisibility()
    {
        if (currentPageIndex == pages.Length - 1)
        {
            nextButton.SetActive(false);
            endingButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            endingButton.SetActive(false);
        }

        if (currentPageIndex == 0)
        {
            previousButton.SetActive(false);
        }
        else
        {
            previousButton.SetActive(true);
        }
    }
    public void EndReading()
    {
        Debug.Log("Loading Credits");

        finishReadingButton.Play();
        endCreditMusic.FadeToVolume(1f, 2f);
        volumeControl.FadeToVolume(0f, 2f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadSceneAdditive(2);
        }
        
        mainGameCamera.SetActive(false);
        mapBounds.SetActive(true);
        endingText.SetActive(true);
        gameObject.SetActive(false);
    }
    private void VolumeIncrease()
    {
        if (currentVolumeLevel < currentPageIndex) return;

        currentVolumeLevel += 1;

        actualVolumeLevel = currentVolumeLevel + 0.3f;

        VolumeUpdate();
    }

    private void VolumeUpdate()
    {
        volumeControl.FadeToVolume(actualVolumeLevel * 0.25f, 1f);
    }
}
