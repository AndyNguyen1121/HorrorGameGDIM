using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class ShakeUIText : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Tween tween;
        [SerializeField] private float strength = 10f;
        [SerializeField] private int vibrato = 50;

        private void OnEnable()
        {
            if (tween != null)
                tween.Restart();
            else
            {
                text = GetComponent<TextMeshProUGUI>();
                tween = text.rectTransform.DOShakeAnchorPos(0.1f, strength, vibrato).SetLoops(-1);
            }
        }
    }
}