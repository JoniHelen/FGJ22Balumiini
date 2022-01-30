using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class ScrollRightAndFadeInOut : MonoBehaviour
{
    //using local spaces
    Vector3 startPos;
    Vector3 endPos;
    RectTransform myRect;
    TextMeshProUGUI text;
    private void Start()
    {
        myRect = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
        textColor = text.color;
        startPos = myRect.localPosition + Vector3.left* Screen.width;
        endPos = myRect.localPosition + Vector3.right * Screen.width;
        myRect.localPosition = startPos;
    }

    float duration = 0;

    private void OnEnable()
    {
        ResetRect();
    }

    private void Update()
    {
        Animate();
    }
    float distance = 0;
    public float amount = 3.75f;
    public float speed = 2;
    Color textColor;
    void Animate()
    {
        //lerp position left to right
        duration += Time.deltaTime * (speed);
        distance = Mathf.Pow(Mathf.Sin(duration) * (myRect.rect.size.x * scaler.x*amount), 2);
        myRect.localPosition += Vector3.right * Mathf.Abs(distance);
        //lerp color alpha 0->100->0
        //textColor.a = 1-Mathf.Sin(duration);
        //text.color = textColor;
        if (myRect.localPosition.x >= endPos.x)
        {
            enabled = false;
        }
    }

    public void ResetRect()
    {
        duration = 0;
        if (myRect != null && startPos != null)
            myRect.localPosition = startPos;
    }
    public CanvasScaler canvasScaler;
    Vector2 scaler
    {
        get => new Vector2(canvasScaler.referenceResolution.x / Screen.width, 
            canvasScaler.referenceResolution.y / Screen.height);
    }
}
