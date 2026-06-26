using UnityEngine;
using System.Threading.Tasks;

public class EraseUI : MonoBehaviour
{
    [SerializeField] private GameObject x;
    [SerializeField] private Animator ring;
    [SerializeField] private Animator line;
    [SerializeField] private GameObject text;
    private void Awake()
    {
        x.SetActive(false);
        text.SetActive(false);
    }
    public async void StartAnimation()
    {
        x.SetActive(true);
        ring.SetTrigger("On");
        await Task.Delay(833);
        line.SetTrigger("On");
        await Task.Delay(1600);
        text.SetActive(true);
    }
}
