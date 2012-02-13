using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LocalizedResourceExtension
{
    /// <summary>
    /// Interaction logic for AddLocalizedResourceWindow.xaml
    /// </summary>
    public partial class AddLocalizedResourceWindow
    {
        readonly AddLocalizedResourceViewModel _vm;

        public AddLocalizedResourceWindow(string rootName, HashSet<string> usedCultures)
        {
            InitializeComponent();
            _vm = new AddLocalizedResourceViewModel(rootName, usedCultures);
            DataContext = _vm;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public string TargetCultureCode
        {
            get { return _vm.TargetCultureCode; }
        }

        public bool CopyNeutralResources
        {
            get { return _vm.CopyNeutralResources; }
        }

        public bool AddAsSubItem
        {
            get { return _vm.AddAsSubItem; }
        }

        #region AddLocalizedResourceViewModel

        public class AddLocalizedResourceViewModel : INotifyPropertyChanged
        {
            private readonly HashSet<string> _usedCultures;

            public AddLocalizedResourceViewModel(string rootName, HashSet<string> usedCultures)
            {
                _usedCultures = usedCultures;
                Title = string.Format(Properties.Resources.WindowTitleFormat, rootName);
                NeutralCultures = CreateCultureHierarchy(usedCultures).ToList();
            }

            private static IEnumerable<CultureViewModel> CreateCultureHierarchy(HashSet<string> usedCultures)
            {
                var cultures =
                    from sc in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    group sc by sc.Parent into g
                    where !string.IsNullOrEmpty(g.Key.Name)
                    select new
                    {
                        NeutralCulture = g.Key,
                        SpecificCultures = new[] { g.Key }.Concat(g).ToArray()
                    };

                return cultures
                        .Select(c => new NeutralCultureViewModel(c.NeutralCulture, c.SpecificCultures, usedCultures))
                        .OrderBy(c => c.DisplayName);
            }

            private string _title;
            public string Title
            {
                get { return _title; }
                set
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }

            public IList<CultureViewModel> NeutralCultures { get; private set; }

            private NeutralCultureViewModel _selectedNeutralCulture;
            public NeutralCultureViewModel SelectedNeutralCulture
            {
                get { return _selectedNeutralCulture; }
                set
                {
                    _selectedNeutralCulture = value;
                    OnPropertyChanged("SelectedNeutralCulture");
                    if (value == null)
                        SelectedSpecificCulture = null;
                    else
                        SelectedSpecificCulture = value.Children.FirstOrDefault();
                }
            }

            private CultureViewModel _selectedSpecificCulture;
            public CultureViewModel SelectedSpecificCulture
            {
                get { return _selectedSpecificCulture; }
                set
                {
                    _selectedSpecificCulture = value;
                    OnPropertyChanged("SelectedSpecificCulture");
                }
            }

            private bool _isKnownCulture = true;
            public bool IsKnownCulture
            {
                get { return _isKnownCulture; }
                set
                {
                    _isKnownCulture = value;
                    OnPropertyChanged("IsKnownCulture");
                    OnPropertyChanged("IsCustomCulture");
                }
            }

            public bool IsCustomCulture
            {
                get { return !IsKnownCulture; }
                set
                {
                    _isKnownCulture = !value;
                    OnPropertyChanged("IsKnownCulture");
                    OnPropertyChanged("IsCustomCulture");
                }
            }

            private string _customCultureCode;
            public string CustomCultureCode
            {
                get { return _customCultureCode; }
                set
                {
                    _customCultureCode = value;
                    OnPropertyChanged("CustomCultureCode");
                }
            }

            private bool _addAsSubItem = true;
            public bool AddAsSubItem
            {
                get { return _addAsSubItem; }
                set
                {
                    _addAsSubItem = value;
                    OnPropertyChanged("AddAsSubItem");
                }
            }

            private bool _copyNeutralResources = true;
            public bool CopyNeutralResources
            {
                get { return _copyNeutralResources; }
                set
                {
                    _copyNeutralResources = value;
                    OnPropertyChanged("CopyNeutralResources");
                }
            }

            public string TargetCultureCode
            {
                get
                {
                    if (IsKnownCulture)
                        return SelectedSpecificCulture != null ? SelectedSpecificCulture.Code : null;
                    return CustomCultureCode;
                }
            }

            private DelegateCommand _acceptCommand;
            public ICommand AcceptCommand
            {
                get
                {
                    if (_acceptCommand == null)
                    {
                        _acceptCommand = new DelegateCommand(Accept, CanAccept);
                    }
                    return _acceptCommand;
                }
            }

            private bool CanAccept(object arg)
            {
                return !string.IsNullOrEmpty(TargetCultureCode) && !_usedCultures.Contains(TargetCultureCode);
            }

            private static void Accept(object obj)
            {
            }

            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }

        #endregion

        #region CultureViewModel

        public abstract class CultureViewModel
        {
            private readonly CultureInfo _culture;

            protected CultureViewModel(CultureInfo culture)
            {
                _culture = culture;
            }

            public abstract string DisplayName { get; }

            public CultureInfo Culture
            {
                get { return _culture; }
            }

            public string Code
            {
                get { return _culture.IetfLanguageTag; }
            }
        }

        public class NeutralCultureViewModel : CultureViewModel
        {
            public NeutralCultureViewModel(CultureInfo culture, IEnumerable<CultureInfo> children, HashSet<string> usedCultures)
                : base(culture)
            {
                if (children != null)
                    Children = children.Select(c => new SpecificCultureViewModel(c, !usedCultures.Contains(c.IetfLanguageTag)))
                                       .OrderBy(c => c.DisplayName)
                                       .ToList<CultureViewModel>();
                else
                    Children = new List<CultureViewModel>();
            }

            public IList<CultureViewModel> Children { get; private set; }

            public override string DisplayName
            {
                get { return Culture.DisplayName; }
            }
        }

        public class SpecificCultureViewModel : CultureViewModel
        {
            private readonly bool _enabled;

            public SpecificCultureViewModel(CultureInfo culture, bool enabled) : base(culture)
            {
                _enabled = enabled;
            }

            public bool Enabled
            {
                get { return _enabled; }
            }

            public override string DisplayName
            {
                get
                {
                    if (Culture.IsNeutralCulture)
                        return Properties.Resources.Neutral;
                    return Culture.DisplayName;
                }
            }
        }

        #endregion
    }
}
