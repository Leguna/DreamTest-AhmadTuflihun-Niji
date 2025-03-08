using DG.Tweening;
using Features.DamageModule.HealthBar;
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

        private IHealthBar healthBarData;

        public void Init(IHealthBar newHealthBarData, Transform newTargetTransform, Vector3 offset = default)
        {
            transform.SetParent(newTargetTransform);
            GetComponent<Canvas>().worldCamera = Camera.main;
            transform.localPosition = offset;

            healthBarData = newHealthBarData;
            healthBar.color = healthBarData.Color;

            SetListener(healthBarData);
            UpdateBar(healthBarData.CurrentHealth);
        }

        private void SetListener(IHealthBar newHealthBarData)
        {
            newHealthBarData.OnDamageTaken += DecreaseHealth;
            newHealthBarData.OnHealTaken += IncreaseHealth;
        }

        private void UpdateBar(int amount)
        {
            var fillAmountFloat = (float)amount / healthBarData.MaxHealth;
            healthBar.DOFillAmount(fillAmountFloat, changeSpeed);
            healthText.text = $"{amount}/{healthBarData.MaxHealth}";
        }

        private void DecreaseHealth(int amount)
        {
            healthBarData.CurrentHealth -= amount;
            UpdateBar(healthBarData.CurrentHealth);
        }

        private void IncreaseHealth(int amount)
        {
            healthBarData.CurrentHealth += amount;
            UpdateBar(healthBarData.CurrentHealth);
        }
    }
}