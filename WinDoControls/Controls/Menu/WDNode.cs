using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDoControls.Controls.Menu
{
    public class WDNode
    {
        public string ParentKey { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public object Data { get; set; }

        public static WDMenuItemList GetTree(List<WDNode> list, string parent, EventHandler eventHandler)
        {
            var ml = new WDMenuItemList();
            foreach (var item in list.Where(x => x.ParentKey == parent))
            {
                var i = new WDMenuItem
                {
                    Key = item.Key,
                    Text = item.Text,
                    Data = item.Data,
                    MenuItems = GetTree(list, item.Key, eventHandler),
                };

                i.Click += eventHandler;
                ml.Add(i);
            }
            return ml;
        }
    }
}
