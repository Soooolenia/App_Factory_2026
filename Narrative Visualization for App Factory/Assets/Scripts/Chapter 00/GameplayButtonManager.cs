using UnityEngine;

public class GameplayButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;

    [SerializeField] private AudioSource backButtonClick;
    [SerializeField] private AudioSource quitButtonClick;
    [SerializeField] private AudioSource optionsButtonClick;
    public void OptionsOn()
    {
        optionsMenu.SetActive(true);
        optionsButtonClick.Play();
    }
    public void OptionsOff()
    {
        backButtonClick.Play();
        optionsMenu.SetActive(false);
    }
    public void CreditsOn()
    {
        creditsMenu.SetActive(true);
    }   
    public void CreditsOff()
    {
        backButtonClick.Play();
        creditsMenu.SetActive(false);
    }
    public void QuitGame()
    {
        quitButtonClick.Play();
        Debug.Log("Quit");
        Application.Quit();
    }
}
