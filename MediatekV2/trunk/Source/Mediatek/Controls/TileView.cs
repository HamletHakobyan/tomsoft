using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

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
