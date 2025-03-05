using Features.Utilities.SaveLoad;
using UnityEngine;

namespace Features.Core.Item
{
    public class SaveableResponse : ISaveable
    {
        public string GetUniqueIdentifier()
        {
            return GetType().Name;
        }

        public object CaptureState()
        {
            return JsonUtility.ToJson(this);
        }

        public void RestoreState(object state)
        {
            JsonUtility.FromJsonOverwrite((string)state, this);
        }
    }
}