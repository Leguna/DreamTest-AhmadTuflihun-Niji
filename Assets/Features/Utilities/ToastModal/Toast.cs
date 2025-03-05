using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Utilities.ToastModal
{
    public class Toast : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text text;

        private Sequence _tween;

        public void Show(string message, float showDuration = .5f, float fadeDuration = 0.2f)
        {
            _tween.Kill();
            text.text = message;
            gameObject.SetActive(true);
            text.alpha = 0f;
            text.rectTransform.anchoredPosition = new Vector2(0f, -300f);
            background.color = new Color(0f, 0f, 0f, 0f);
            var alphaFade = text.DOFade(1f, fadeDuration);
            var bgFade = background.DOFade(0.5f, fadeDuration);
            var moveUp = text.rectTransform.DOAnchorPosY(-250f, fadeDuration);
            _tween = DOTween.Sequence().Append(alphaFade).Join(bgFade).Join(moveUp)
                .AppendInterval(showDuration)
                .OnComplete(() => Hide());
        }

        private void Hide(float duration = 0.2f)
        {
            _tween.Kill();
            var alphaFade = text.DOFade(0f, duration);
            var bgFade = background.DOFade(0f, duration);
            var moveDown = text.rectTransform.DOAnchorPosY(-200f, duration);
            _tween = DOTween.Sequence().Append(alphaFade).Join(bgFade).Join(moveDown).OnComplete(() =>
            {
                gameObject.SetActive(false);
                text.text = "";
            });
        }
    }
}