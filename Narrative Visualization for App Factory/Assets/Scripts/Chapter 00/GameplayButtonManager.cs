using UnityEngine;

public class GameplayButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    public void OptionsOn()
    {
        optionsMenu.SetActive(true);
    }
    public void OptionsOff()
    {
        optionsMenu.SetActive(false);
    }
    public void CreditsOn()
    {
        creditsMenu.SetActive(true);
    }   
    public void CreditsOff()
    {
        creditsMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
