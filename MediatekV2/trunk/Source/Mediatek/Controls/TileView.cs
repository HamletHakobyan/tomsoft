using System.Windows;
using System.Windows.Controls;

namespace Mediatek.Controls
{
    public class TileView : ViewBase
    {
        protected override object DefaultStyleKey
        {
            get
            {
                return new ComponentResourceKey(this.GetType(), "tileViewDSK");
            }
        }

        protected override object ItemContainerDefaultStyleKey
        {
            get
            {
                return new ComponentResourceKey(this.GetType(), "tileViewItemContainerDSK");
            }
        }
    }
}
