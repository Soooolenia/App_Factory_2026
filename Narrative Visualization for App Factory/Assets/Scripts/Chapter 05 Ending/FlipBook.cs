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

        pages[currentPageIndex].SetActive(false);
        currentPageIndex++;
        pages[currentPageIndex].SetActive(true);

        HandleButtonVisibility();
    }
    public void PreviousPage()
    {
        if (currentPageIndex == 0) return;

        pages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        pages[currentPageIndex].SetActive(true);

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
        GameManager.Instance.LoadSceneAdditive(2);
        mainGameCamera.SetActive(false);
        mapBounds.SetActive(true);
        endingText.SetActive(true);
        gameObject.SetActive(false);
    }
}
