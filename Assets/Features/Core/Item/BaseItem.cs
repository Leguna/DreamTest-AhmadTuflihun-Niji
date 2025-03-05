using System;
using Constant;
using Features.Utilities.SaveLoad;
using UnityEngine;

namespace Item
{
    [Serializable]
    public class BaseItem : Updateable<BaseItem>, ISaveable
    {
        public string id = "";
        public string itemName = "";
        public string description = "";
        private int _quantity;
        public Sprite icon;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                onUpdateAction?.Invoke(this);
            }
        }

        public string IconPath() => GameConst.ItemIconPath + id;
        public string ResourcePath() => GameConst.ItemSoPath + id;

        public string GetUniqueIdentifier() => GetType().Name;
        public object CaptureState() => JsonUtility.ToJson(this);
        public BaseItem RestoreState<T>(object state)
        {
            throw new NotImplementedException();
        }

        public void RestoreState(object state)
        {
            JsonUtility.FromJsonOverwrite((string)state, this);
        }
    }

    public abstract class Updateable<T>
    {
        public Action<T> onUpdateAction;
    }
}