using System;
using Features.Utilities;
using Item;

namespace Features.Core.Item
{
    [Serializable]
    public class DailyLoginData : SaveableResponse
    {
        public bool isClaimed;
        public int streak;
        public JsonHelper.WrapperArray<BaseItem> rewards = new();
    }

    [Serializable]
    public class ListRewards
    {
        public BaseItem[] items = new BaseItem[28];
    }
}