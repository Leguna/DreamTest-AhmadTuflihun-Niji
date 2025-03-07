using Features.Utilities;
using UnityEngine;
using Utilities;
using Utilities.Pooling;

namespace Features.DamageModule.HealthBar
{
    public class HealthBarPool : ObjectPoolingBase<HealthBarComponent>
    {
        public void GetObject(IHealthBar healthBarData, Transform target, Vector2 offset = default)
        {
            var healthBar = base.GetObjectFromPool();
            healthBar.Init(healthBarData, target, offset);
        }
    }
}