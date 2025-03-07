using Features.Utilities;
using UnityEngine;
using Utilities;
using Utilities.Pooling;

namespace Features.DamageModule.DamagePopup
{
    public class DamagePopupPool : ObjectPoolingBase<DamagePopupComponent>
    {
        public DamagePopupComponent GetObject(int damage, Vector2 targetPos,
            Vector2 offset = default)
        {
            var damagePopup = base.GetObjectFromPool();
            damagePopup.Init();
            damagePopup.ShowDamage(damage, targetPos, () => ReturnObjectToPool(damagePopup), offset);
            return damagePopup;
        }
    }
}