using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaHit : MonoBehaviour
{
    [SerializeField] private float threshold = 0.5f;
    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
