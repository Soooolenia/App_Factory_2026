using Unity.VisualScripting;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    [SerializeField] private GameObject nextBounds;
    public void UnlockNextBounds()
    {
        nextBounds.SetActive(true);
    }
}
