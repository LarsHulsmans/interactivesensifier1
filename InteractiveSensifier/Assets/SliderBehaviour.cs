using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    public Text number;
    public Slider slider;

    public GameObject TextBackground;
    public RectTransform BackgorundTransform;

    private Coroutine openingText;
    private Coroutine closingText;

    public bool animate;

    public void UpdateSliderText() 
    {
        if (number.gameObject.activeInHierarchy) 
        {
            number.text = Mathf.Round(slider.value).ToString();
        }
    }

    public void SetTextActive() 
    {
        if (animate) 
        {
            if (closingText != null)
            {
                StopCoroutine(closingText);
                closingText = null;
            }
            TextBackground.SetActive(true);
            number.text = Mathf.Round(slider.value).ToString();
            if (openingText == null)
            {
                BackgorundTransform.sizeDelta = new Vector2(50, 0);
                openingText = StartCoroutine(OpenText());
            }
        }
        else 
        {
            TextBackground.SetActive(true);
        }
    }

    public void SetTextInactive() 
    {
        if (animate) 
        {
            if (openingText != null)
            {
                StopCoroutine(openingText);
                openingText = null;
            }
           
            if (closingText == null)
            {
                closingText = StartCoroutine(CloseText());
            }
        }
        else 
        {
            TextBackground.SetActive(false);
        }
    }

    public IEnumerator OpenText() 
    {
        float height = BackgorundTransform.sizeDelta.y;
        float speed = 5;
        
        while(height < 50) 
        {
            height += speed;
            BackgorundTransform.sizeDelta = new Vector2(50, height);
            yield return new WaitForSeconds(0.01f);
        }
        BackgorundTransform.sizeDelta = new Vector2(50, 50);
        openingText = null;
    }

    public IEnumerator CloseText() 
    {
        float height = BackgorundTransform.sizeDelta.y;
        float speed = 5;

        while (height > 0)
        {
            height -= speed;
            BackgorundTransform.sizeDelta = new Vector2(50, height);
            yield return new WaitForSeconds(0.01f);
        }
        BackgorundTransform.sizeDelta = new Vector2(50, 0);
        TextBackground.SetActive(false);
        closingText = null;
    }
}
