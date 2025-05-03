using System;
using System.Collections.Generic;

namespace JMT
{
    public static class CategorySystem
    {
        public static List<TItem> FilteringCategory<TItem, TCategoryOwner>(List<TItem> list, Enum category, Func<TItem, TCategoryOwner> selector)
            where TCategoryOwner : ICategorizable
        {
            // selector : 리턴값이 있는 액션, 함수를 매개변수로 담은거임.
            // Func<TItem, TCategoryOwner> : 여기서 TItem을 주면 TCategoryOwner형식으로 리턴 값이 돌아옴.
            var result = new List<TItem>();
            foreach (var item in list)
            {
                var owner = selector(item);
                if (owner.Category.Equals(category))
                    result.Add(item);
            }
            return result;
        }
    }
}
