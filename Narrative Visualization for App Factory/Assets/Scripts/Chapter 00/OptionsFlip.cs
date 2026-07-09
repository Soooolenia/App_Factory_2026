using UnityEngine;

public class OptionsFlip : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int currentPageIndex = 0;

    [SerializeField] private AudioSource buttonClick;
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
    }
    public void NextPage()
    {
        pages[currentPageIndex].SetActive(false);
        currentPageIndex = (currentPageIndex + 1) % pages.Length;
        pages[currentPageIndex].SetActive(true);

        buttonClick.Play();
    }
    public void PreviousPage()
    {
        pages[currentPageIndex].SetActive(false);
        currentPageIndex = (currentPageIndex - 1 + pages.Length) % pages.Length;
        pages[currentPageIndex].SetActive(true);

        buttonClick.Play();
    }
}
