using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kirito_Solutions.AdvanceSingleTon;

namespace Kirito_Solutions.tooltip
{

    public class TooltipSystem : SingleTon<TooltipSystem>
    {
        private CanvasGroup canvasGroup;
        public TextMeshProUGUI headerField, contentField;
        public LayoutElement layoutElement;
        public int characterWrapLimit = 80;
        public int headerLength = 12, contentLength;
        public RectTransform tooptipTransform;
        private void Awake()
        {
            this.InitialiseInstance();
            gameObject.SetActive(true);
            canvasGroup = this.tooptipTransform.gameObject.GetComponent<CanvasGroup>();
        }
        float _fadeInTime, _fadeOutTime;
        public void Show(string content, string header = "", float fadeInTime = 0.2f, float fadeOutTime = 0.2f)
        {
            gameObject.SetActive(true);
            Time.timeScale = 1;
            _fadeInTime = fadeInTime;
            _fadeOutTime = fadeOutTime;
            StartCoroutine(Fade(true));
            this.SetText(content, header);

        }

        IEnumerator Fade(bool isFadeIn)
        {
            if (isFadeIn)
            {
                canvasGroup.alpha = 0.3f;
                this.tooptipTransform.gameObject.SetActive(true);
                float _time = _fadeInTime;
                while (_time > 0f)
                {
                    _time -= Time.deltaTime;
                    canvasGroup.alpha = Mathf.InverseLerp(1f, 0.3f, _time);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
            else
            {
                this.tooptipTransform.gameObject.SetActive(true);
                canvasGroup.alpha = 1f;
                float _time = _fadeOutTime;
                while (_time > 0f)
                {
                    _time -= Time.deltaTime;
                    canvasGroup.alpha = Mathf.InverseLerp(0.3f, 1f, _time);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                this.tooptipTransform.gameObject.SetActive(false);
            }

        }
        public void Hide()
        {
            StartCoroutine(Fade(false));
        }
        /////////////////////


        private void Start()
        {
            gameObject.SetActive(false);
        }
        public void SetText(string content, string header = "")
        {
            if (string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.text = header;
                headerField.gameObject.SetActive(true);
            }

            contentField.text = content;

            headerLength = headerField.text.Length;
            contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                headerLength = headerField.text.Length;
                contentLength = contentField.text.Length;

                layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
            }

            Vector2 mousePosition = Input.mousePosition;
            //float pivotX = mousePosition.x / Screen.width;
            //float pivotY = (mousePosition.y) / (Screen.height);

            //rectTransform.pivot = new Vector2(pivotX, pivotY);
            this.transform.position = mousePosition;
        }
    }
}
