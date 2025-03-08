using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DamageModule.HealthBar
{
    public class HealthBarComponent : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private float changeSpeed = 0.5f;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private Vector3 offset = new Vector3(0, 5, 0);

        private IHealthBar _healthBarData;

        public void Init(IHealthBar newHealthBarData, Transform newTargetTransform, Vector3 offset = default)
        {
            if (offset == default) offset = this.offset;

            transform.SetParent(newTargetTransform);
            GetComponent<Canvas>().worldCamera = Camera.main;
            transform.localPosition = offset;

            _healthBarData = newHealthBarData;
            healthBar.color = _healthBarData.Color;

            SetListener(_healthBarData);
            UpdateBar(_healthBarData.CurrentHealth);
        }

        private void SetListener(IHealthBar newHealthBarData)
        {
        }

        public void UpdateBar(int amount)
        {
            var fillAmountFloat = (float)amount / _healthBarData.MaxHealth;
            healthBar.DOFillAmount(fillAmountFloat, changeSpeed);
            healthText.text = $"{amount}/{_healthBarData.MaxHealth}";
        }

        private void DecreaseHealth(int amount)
        {
            _healthBarData.CurrentHealth -= amount;
            UpdateBar(_healthBarData.CurrentHealth);
        }

        private void IncreaseHealth(int amount)
        {
            _healthBarData.CurrentHealth += amount;
            UpdateBar(_healthBarData.CurrentHealth);
        }
    }
}