using UnityEngine;
using UnityEngine.UI;

public class RadialUIAnimation : MonoBehaviour
{
    private Image outline;

    private float fillAmount = 0f;

    [SerializeField] private float fillSpeed;

    private bool isClockwise = true;

    private void Awake()
    {
        outline = GetComponent<Image>();
        outline.type = Image.Type.Filled;
        outline.fillMethod = Image.FillMethod.Radial360;
        outline.fillClockwise = true;
    }
    private void Update()
    {
        if (isClockwise)
        {
            fillAmount += fillSpeed;
            outline.fillAmount = Mathf.Clamp01(fillAmount);

            if (fillAmount >= 1f)
            {
                isClockwise = !isClockwise;
                outline.fillClockwise = !outline.fillClockwise;
            }

            //if (fillAmount >= 1f)
            //{
            //    isClockwise = !isClockwise;
            //    outline.fillClockwise = !isClockwise;

            //    if (isClockwise)
            //    {
            //        fillAmount = 0f;
            //    }

            //    else if (!isClockwise)
            //    {
            //        fillAmount = 1f;
            //    }
            //}
        }

        else
        {
            fillAmount -= fillSpeed;
            outline.fillAmount = Mathf.Clamp01(fillAmount);

            if (fillAmount <= 0f)
            {
                isClockwise = !isClockwise;
                outline.fillClockwise = !outline.fillClockwise;
            }

            //if (fillAmount <= 0)
            //{
            //    isClockwise = !isClockwise;
            //    outline.fillClockwise = !isClockwise;

            //    if (isClockwise)
            //    {
            //        fillAmount = 0f;
            //    }

            //    else if (!isClockwise)
            //    {
            //        fillAmount = 1f;
            //    }
            //}
        }

    }
}
