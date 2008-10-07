using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using System.Diagnostics;

namespace MediaTek.Utilities
{
    public static class ValidationService
    {
        public static ValidationResult Validate(DependencyObject obj)
        {
            object value = null;
            ValidationRule rule = GetRule(obj);
            DependencyProperty dp = GetProperty(obj);
            if (dp != null)
                value = obj.GetValue(dp);
            if (rule != null)
            {
                ValidationResult result = rule.Validate(value, System.Globalization.CultureInfo.CurrentCulture);
                if (result.IsValid)
                {
                    SetErrorText(obj, null);
                    SetHasError(obj, false);
                }
                else
                {
                    string errorText = null;
                    if (result.ErrorContent != null)
                        errorText = result.ErrorContent.ToString();
                    SetErrorText(obj, errorText);
                    SetHasError(obj, true);
                }
                return result;
            }
            else
            {
                SetErrorText(obj, null);
                SetHasError(obj, false);
                return new ValidationResult(true, "No validation rule");
            }
        }

        #region Wrappers for private members from Validation class

        private static MethodInfo _showValidationAdorner = null;
        private static void ShowValidationAdorner(DependencyObject targetElement, bool show)

        {
            if (_showValidationAdorner == null)
            {
                _showValidationAdorner = typeof(Validation).GetMethod("ShowValidationAdorner", BindingFlags.Static | BindingFlags.NonPublic);
            }
            _showValidationAdorner.Invoke(null, new object[] { targetElement, show });
        }

        #endregion

        #region Rule attached property

        public static ValidationRule GetRule(DependencyObject obj)
        {
            return (ValidationRule)obj.GetValue(RuleProperty);
        }

        public static void SetRule(DependencyObject obj, ValidationRule value)
        {
            obj.SetValue(RuleProperty, value);
        }

        // Using a DependencyProperty as the backing store for Rule.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RuleProperty =
            DependencyProperty.RegisterAttached(
                "Rule",
                typeof(ValidationRule),
                typeof(ValidationService),
                new UIPropertyMetadata(
                    null,
                    (sender, e) =>
                    {
                        Validate(sender as DependencyObject);
                    }));

        #endregion

        #region Property attached property
		
        public static DependencyProperty GetProperty(DependencyObject obj)
        {
            return (DependencyProperty)obj.GetValue(PropertyProperty);
        }

        public static void SetProperty(DependencyObject obj, DependencyProperty value)
        {
            obj.SetValue(PropertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for Property.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.RegisterAttached(
                "Property",
                typeof(DependencyProperty),
                typeof(ValidationService),
                new UIPropertyMetadata(
                    null,
                    (sender, e) =>
                    {
                        Validate(sender as DependencyObject);
                    }));

	    #endregion

        #region HasError attached property

        public static bool GetHasError(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasErrorProperty);
        }

        public static void SetHasError(DependencyObject obj, bool value)
        {
            obj.SetValue(HasErrorProperty, value);
            ShowValidationAdorner(obj, value);
        }

        // Using a DependencyProperty as the backing store for HasError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.RegisterAttached(
                "HasError",
                typeof(bool),
                typeof(ValidationService),
                new UIPropertyMetadata(false));

        #endregion

        #region ErrorText attached property

        public static string GetErrorText(DependencyObject obj)
        {
            return (string)obj.GetValue(ErrorTextProperty);
        }

        public static void SetErrorText(DependencyObject obj, string value)
        {
            obj.SetValue(ErrorTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for ErrorText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorTextProperty =
            DependencyProperty.RegisterAttached("ErrorText", typeof(string), typeof(ValidationService), new UIPropertyMetadata(null));

        #endregion

        #region GroupName attached property

        public static string GetGroupName(DependencyObject obj)
        {
            return (string)obj.GetValue(GroupNameProperty);
        }

        public static void SetGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(GroupNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for GroupName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached(
                "GroupName",
                typeof(string),
                typeof(ValidationService),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits,
                    (sender, e) =>
                    {
                        if (e.OldValue != null)
                            RemoveFromGroup(sender as DependencyObject, e.OldValue as string);
                        if (e.NewValue != null)
                            AddToGroup(sender as DependencyObject, e.NewValue as string);
                    }));

        #endregion

        #region Group validation

        private static Dictionary<string, HashSet<WeakReference>> _groups = new Dictionary<string, HashSet<WeakReference>>();

        private static void AddToGroup(DependencyObject obj, string groupName)
        {
            if (!_groups.ContainsKey(groupName))
                _groups.Add(groupName, new HashSet<WeakReference>());
            _groups[groupName].Add(new WeakReference(obj));
        }

        private static void RemoveFromGroup(DependencyObject obj, string groupName)
        {
            if (!_groups.ContainsKey(groupName))
                return;
            var references = from wr in _groups[groupName]
                             where !wr.IsAlive || wr.Target.Equals(obj)
                             select wr;
            foreach (var wr in references.ToArray())
            {
                _groups[groupName].Remove(wr);
            }
        }

        public static bool ValidateGroup(string groupName)
        {
            bool isValid = true;
            if (_groups.ContainsKey(groupName))
            {
                foreach (WeakReference wr in _groups[groupName].ToArray())
                {
                    if (wr.IsAlive)
                    {
                        DependencyObject obj = wr.Target as DependencyObject;
                        ValidationResult vr = Validate(obj);
                        if (!vr.IsValid)
                            isValid = false;
                    }
                    else
                    {
                        _groups[groupName].Remove(wr);
                    }
                }
            }
            return isValid;
        }

        public static void ClearGroup(string groupName)
        {
            _groups.Remove(groupName);
        }

        #endregion
    }
}
