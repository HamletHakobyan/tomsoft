using System.Windows;
using System.Windows.Controls;

namespace Mediatek.Controls
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(BulletListItem))]
    public class BulletList : ItemsControl
    {
        static BulletList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BulletList), new FrameworkPropertyMetadata(typeof(BulletList)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BulletListItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is BulletListItem;
        }
    }
}
