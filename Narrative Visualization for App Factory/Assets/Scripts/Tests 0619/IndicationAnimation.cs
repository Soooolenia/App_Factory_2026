using UnityEngine;
using UnityEngine.UI;

public class IndicationAnimation : MonoBehaviour
{
    private Image arrow;

    private float fillAmount = 0f;
    [SerializeField] private float fillSpeed;

    private bool isFillOriginLeft = true;

    private void Awake()
    {
        arrow = GetComponent<Image>();
        arrow.type = Image.Type.Filled;
        arrow.fillMethod = Image.FillMethod.Horizontal;
    }

    private void Update()
    {
        if (isFillOriginLeft)
        {
            fillAmount += fillSpeed;
            arrow.fillAmount = Mathf.Clamp01(fillAmount);

            if (fillAmount >= 1f)
            {
                isFillOriginLeft = !isFillOriginLeft;

                if (isFillOriginLeft)
                {
                    SetOriginLeft();
                    fillAmount = 0f;
                }

                else if (!isFillOriginLeft)
                {
                    SetOriginRight();
                    fillAmount = 1f;
                }
            }
        }

        else
        {
            fillAmount -= fillSpeed;
            arrow.fillAmount = Mathf.Clamp01(fillAmount);

            if (fillAmount <= 0)
            {
                isFillOriginLeft = !isFillOriginLeft;

                if (isFillOriginLeft)
                {
                    SetOriginLeft();
                    fillAmount = 0f;
                }

                else if (!isFillOriginLeft)
                {
                    SetOriginRight();
                    fillAmount = 0f;
                }
            }
        }
        
    }
    private void SetOriginLeft()
    {
        arrow.fillOrigin = (int)Image.OriginHorizontal.Left;
    }
    private void SetOriginRight()
    {
        arrow.fillOrigin = (int)Image.OriginHorizontal.Right;
    }
}
