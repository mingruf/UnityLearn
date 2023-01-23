using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0,75,0);
    public float timeToFade = 1f;
    RectTransform rectTransform;
    TextMeshProUGUI textMeshProUGUI;
    Color startColor;
    private float timeElapsed;
    private void Awake()
    {
        rectTransform= GetComponent<RectTransform>();
        textMeshProUGUI= GetComponent<TextMeshProUGUI>();
        startColor= textMeshProUGUI.color;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed< timeToFade) 
        {
            float newAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshProUGUI.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
