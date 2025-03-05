using Features.DamageModule.DamagePopup;
using Features.DamageModule.HealthBar;
using UnityEngine;

namespace Features.DamageModule
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] private DamagePopupComponent damagePopupPrefab;
        [SerializeField] private DamagePopupPool damagePopupPoolPrefab;

        [SerializeField] private HealthBarComponent healthBarPrefab;
        private HealthBarPool _healthBarPoolComponent;

        public void Init()
        {
            damagePopupPoolPrefab = Instantiate(damagePopupPoolPrefab, transform);
            damagePopupPoolPrefab.Init(damagePopupPrefab);
            _healthBarPoolComponent = transform.gameObject.AddComponent<HealthBarPool>();
            _healthBarPoolComponent.Init(healthBarPrefab);
        }

        public void ShowHealthBar(IHealthBar healthBarData, Transform position, Vector2 offset = default)
        {
            _healthBarPoolComponent.GetObject(healthBarData, position, offset);
        }

        public void ShowDamagePopup(int damage, Vector3 position, Vector2 offset)
        {
            damagePopupPoolPrefab.GetObject(damage, position, offset);
        }

        public void HideHealthBar(HealthBarComponent healthBarComponent)
        {
            _healthBarPoolComponent.ReturnObject(healthBarComponent);
        }
    }
}