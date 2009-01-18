using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace MediaTek.Utilities
{
    public static class ExpanderHelper
    {

        public static void RegisterExpanderInGrid(Expander expander, GridSplitter splitter, DefinitionBase bandDefinition, GridLength expandedLength)
        {
            expander.SetAttachedValue("_BandDefinition", bandDefinition);
            expander.SetAttachedValue("_ExpandedLength", expandedLength);
            expander.SetAttachedValue("_GridSplitter", splitter);
            splitter.SetAttachedValue("_Expander", expander);
            
            splitter.DragCompleted += new System.Windows.Controls.Primitives.DragCompletedEventHandler(splitter_DragCompleted);
            expander.Expanded += new RoutedEventHandler(expander_Expanded);
            expander.Collapsed += new RoutedEventHandler(expander_Collapsed);
        }

        public static void UnregisterExpanderInGrid(Expander expander)
        {
            expander.ClearAttachedValue("_BandDefinition");
            expander.ClearAttachedValue("_ExpandedLength");
            GridSplitter splitter = expander.GetAttachedValue<GridSplitter>("_GridSplitter");
            splitter.ClearAttachedValue("_Expander");
            
            splitter.DragCompleted -= splitter_DragCompleted;
            expander.Expanded -= expander_Expanded;
            expander.Collapsed -= expander_Collapsed;
        }

        static void expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;
            DefinitionBase bandDef = exp.GetAttachedValue<DefinitionBase>("_BandDefinition");
            if (bandDef is RowDefinition)
            {
                RowDefinition rowDef = bandDef as RowDefinition;
                rowDef.Height = GridLength.Auto;
            }
            else if (bandDef is ColumnDefinition)
            {
                ColumnDefinition colDef = bandDef as ColumnDefinition;
                colDef.Width = GridLength.Auto;
            }            
        }

        static void expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;
            DefinitionBase bandDef = exp.GetAttachedValue<DefinitionBase>("_BandDefinition");
            if (bandDef is RowDefinition)
            {
                RowDefinition rowDef = bandDef as RowDefinition;
                rowDef.Height = exp.GetAttachedValue<GridLength>("_ExpandedLength");
            }
            else if (bandDef is ColumnDefinition)
            {
                ColumnDefinition colDef = bandDef as ColumnDefinition;
                colDef.Width = exp.GetAttachedValue<GridLength>("_ExpandedLength");
            }
        }

        static void  splitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            GridSplitter splitter = sender as GridSplitter;
            Expander exp = splitter.GetAttachedValue<Expander>("_Expander");
            DefinitionBase bandDef = exp.GetAttachedValue<DefinitionBase>("_BandDefinition");
            if (bandDef is RowDefinition)
            {
                RowDefinition rowDef = bandDef as RowDefinition;
                exp.SetAttachedValue("_ExpandedLength", rowDef.Height);
            }
            else if (bandDef is ColumnDefinition)
            {
                ColumnDefinition colDef = bandDef as ColumnDefinition;
                exp.SetAttachedValue("_ExpandedLength", colDef.Width);
            }
        }

    }
}
