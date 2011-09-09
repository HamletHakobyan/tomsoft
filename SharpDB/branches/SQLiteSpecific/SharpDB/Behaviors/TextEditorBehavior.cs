using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ICSharpCode.AvalonEdit;
using SharpDB.Util;
using ICSharpCode.AvalonEdit.Editing;
using Developpez.Dotnet.Windows;
using System.Reflection;

namespace SharpDB.Behaviors
{
    public static class TextEditorBehavior
    {
        static TextEditorBehavior()
        {
            _caretTextAreaFieldInfo = typeof(Caret).GetField("textArea", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        #region Attached properties

        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached(
              "Enabled",
              typeof(bool),
              typeof(TextEditorBehavior),
              new UIPropertyMetadata(
                false,
                EnabledChanged));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached(
              "Text",
              typeof(string),
              typeof(TextEditorBehavior),
              new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                TextChanged));

        public static readonly DependencyProperty SelectionProperty =
            DependencyProperty.RegisterAttached(
              "Selection",
              typeof(TextEditorSelection),
              typeof(TextEditorBehavior),
              new FrameworkPropertyMetadata(
                default(TextEditorSelection),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                SelectionChanged));

        public static readonly DependencyProperty SelectedTextProperty =
            DependencyProperty.RegisterAttached(
              "SelectedText",
              typeof(string),
              typeof(TextEditorBehavior),
              new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                SelectedTextChanged));

        public static readonly DependencyProperty CaretPositionProperty =
            DependencyProperty.RegisterAttached(
              "CaretPosition",
              typeof(CaretPosition),
              typeof(TextEditorBehavior),
              new FrameworkPropertyMetadata(
                default(CaretPosition),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                CaretPositionChanged));

        #endregion

        #region Property accessors

        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static TextEditorSelection GetSelection(DependencyObject obj)
        {
            return (TextEditorSelection)obj.GetValue(SelectionProperty);
        }

        public static void SetSelection(DependencyObject obj, TextEditorSelection value)
        {
            obj.SetValue(SelectionProperty, value);
        }

        public static string GetSelectedText(DependencyObject obj)
        {
            return (string)obj.GetValue(SelectedTextProperty);
        }

        public static void SetSelectedText(DependencyObject obj, string value)
        {
            obj.SetValue(SelectedTextProperty, value);
        }

        public static CaretPosition GetCaretPosition(DependencyObject obj)
        {
            return (CaretPosition)obj.GetValue(CaretPositionProperty);
        }

        public static void SetCaretPosition(DependencyObject obj, CaretPosition value)
        {
            obj.SetValue(CaretPositionProperty, value);
        }

        #endregion

        #region Property change handlers

        private static void EnabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = o as TextEditor;
            if (textEditor == null)
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (oldValue && !newValue)
            {
                DisableBehaviors(textEditor);
            }
            if (newValue && !oldValue)
            {
                EnableBehaviors(textEditor);
            }
        }

        private static void TextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = o as TextEditor;
            if (textEditor == null)
                return;

            var oldValue = (string)e.OldValue;
            var newValue = (string)e.NewValue;

            if (textEditor.Text != newValue)
                textEditor.Text = newValue;

        }

        private static void SelectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = o as TextEditor;
            if (textEditor == null)
                return;

            var oldValue = (TextEditorSelection)e.OldValue;
            var newValue = (TextEditorSelection)e.NewValue;

            var actualSelection = GetTextEditorSelection(textEditor);
            if (!actualSelection.Equals(newValue))
                textEditor.Select(newValue.Start, newValue.Length);
        }

        private static void SelectedTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = o as TextEditor;
            if (textEditor == null)
                return;

            var oldValue = (string)e.OldValue;
            var newValue = (string)e.NewValue;

            if (textEditor.SelectedText != newValue)
                textEditor.SelectedText = newValue;
        }

        private static void CaretPositionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = o as TextEditor;
            if (textEditor == null)
                return;

            var oldValue = (CaretPosition)e.OldValue;
            var newValue = (CaretPosition)e.NewValue;

            if (textEditor.CaretOffset != newValue.Offset)
                textEditor.CaretOffset = newValue.Offset;
        }

        #endregion

        #region Private methods

        private static void EnableBehaviors(TextEditor textEditor)
        {
            textEditor.TextChanged += TextEditor_TextChanged;
            textEditor.TextArea.SelectionChanged += TextArea_SelectionChanged;
            textEditor.TextArea.Caret.PositionChanged += TextArea_CaretPositionChanged;
        }

        private static void DisableBehaviors(TextEditor textEditor)
        {
            textEditor.TextChanged -= TextEditor_TextChanged;
            textEditor.TextArea.SelectionChanged -= TextArea_SelectionChanged;
            textEditor.TextArea.Caret.PositionChanged -= TextArea_CaretPositionChanged;
        }

        private static TextEditorSelection GetTextEditorSelection(TextEditor textEditor)
        {
            int start;
            int length;

            var segment = textEditor.TextArea.Selection.Segments.FirstOrDefault();
            if (segment != null)
            {
                start = segment.Offset;
                length = segment.Length;
            }
            else
            {
                start = textEditor.TextArea.Caret.Offset;
                length = 0;
            }
            return new TextEditorSelection(start, length);
        }

        #endregion
        
        #region Event handlers

        static void TextEditor_TextChanged(object sender, EventArgs e)
        {
            var textEditor = sender as TextEditor;
            if (textEditor == null)
                return;
            if (GetText(textEditor) != textEditor.Text)
                textEditor.SetCurrentValue(TextProperty, textEditor.Text);
        }

        static void TextArea_SelectionChanged(object sender, EventArgs e)
        {
            var textArea = sender as TextArea;
            if (textArea == null)
                return;

            var textEditor = textArea.FindAncestor<TextEditor>();
            if (textEditor == null)
                return;

            var actualSelection = GetTextEditorSelection(textEditor);
            if (!actualSelection.Equals(GetSelection(textEditor)))
                textEditor.SetCurrentValue(SelectionProperty, actualSelection);

            if (textEditor.SelectedText != GetSelectedText(textEditor))
                textEditor.SetCurrentValue(SelectedTextProperty, textEditor.SelectedText);
        }

        static FieldInfo _caretTextAreaFieldInfo;
        static void TextArea_CaretPositionChanged(object sender, EventArgs e)
        {
            var caret = sender as Caret;
            if (caret == null)
                return;

            var textArea = _caretTextAreaFieldInfo.GetValue(caret) as TextArea;
            if (textArea == null)
                return;

            var textEditor = textArea.FindAncestor<TextEditor>();
            if (textEditor == null)
                return;

            var actualPosition = new CaretPosition(caret.Offset, caret.Line - 1);
            if (!actualPosition.Equals(GetCaretPosition(textEditor)))
                textEditor.SetCurrentValue(CaretPositionProperty, actualPosition);
        }

        #endregion
    }
}
