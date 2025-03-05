using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Features.DamageModule.DamagePopup
{
    public class DamagePopupComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private Color damageColor = Color.red;
        [SerializeField] private int damageFontSize = 20;
        [SerializeField] private float damageDuration = 0.5f;

        private Canvas canvas;

        public void Init()
        {
            canvas = damageText.canvas;
            damageText.enabled = true;
        }

        public void ShowDamage(int damage, Vector2 targetPos, Action onFinish, Vector2 offset = default)
        {
            if (Camera.main != null) targetPos = Camera.main.WorldToScreenPoint(targetPos + offset);

            targetPos.x -= (float)Screen.width / 2;
            targetPos.y -= (float)Screen.height / 2;
            targetPos /= canvas.scaleFactor;

            damageText.text = damage.ToString();
            damageText.color = damageColor;
            damageText.fontSize = damageFontSize;

            // Reset Position and Alpha
            damageText.rectTransform.anchoredPosition = targetPos;
            damageText.alpha = 1f;

            // Move and Fade
            damageText.rectTransform.DOLocalMoveY(damageText.rectTransform.localPosition.y + 10, damageDuration)
                .SetEase(Ease.OutQuad);
            damageText.DOFade(0f, damageDuration).SetEase(Ease.OutQuad)
                .OnComplete(() => onFinish?.Invoke());
        }
    }
}