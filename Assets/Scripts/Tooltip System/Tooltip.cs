using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        //Disable header if is null
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;


        //Check if layout element needed
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        //Ternary structure     //If header or character pass over the limit, layoutElement will be enabled
        layoutElement.enabled = headerLength > characterWrapLimit || contentLength > characterWrapLimit;
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            //Ternary structure     //If header or character pass over the limit, layoutElement will be enabled
            layoutElement.enabled = headerLength > characterWrapLimit || contentLength > characterWrapLimit;
        }

        FollowMousePos();
    }

    private void FollowMousePos()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
