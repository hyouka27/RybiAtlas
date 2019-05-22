using System;
using System.Collections.Generic;
using Android.Widget;

namespace START
{
    internal class ActivityListItem_Adapter : IListAdapter
    {
        private ActivityListItem activityListItem;
        private List<Tuple<string, int>> items;

        public ActivityListItem_Adapter(ActivityListItem activityListItem, List<Tuple<string, int>> items)
        {
            this.activityListItem = activityListItem;
            this.items = items;
        }
    }
}