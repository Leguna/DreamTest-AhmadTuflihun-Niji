using Features.Utilities;
using UnityEngine;

namespace Features.DamageModule.DamagePopup
{
    public class DamagePopupPool : ObjectPoolingBase<DamagePopupComponent>
    {
        public DamagePopupComponent GetObject(int damage, Vector2 targetPos,
            Vector2 offset = default)
        {
            var damagePopup = base.GetObject();
            damagePopup.Init();
            damagePopup.ShowDamage(damage, targetPos, () => ReturnObject(damagePopup), offset);
            return damagePopup;
        }
    }
}