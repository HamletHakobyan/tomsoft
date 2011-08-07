using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace PasteBinSharp.UI
{
    /// <summary>
    /// Interaction logic for SendWindow.xaml
    /// </summary>
    public partial class SendWindow : Window, INotifyPropertyChanged
    {
        private readonly PasteBinEntry _entry;
        private readonly Properties.Settings _settings;

        public SendWindow(PasteBinEntry entry)
        {
            InitializeComponent();
            _entry = entry;
            
            _settings = Properties.Settings.Default;
            PostAnonymously = string.IsNullOrEmpty(_settings.UserName) || string.IsNullOrEmpty(_settings.Password);

            this.DataContext = this;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DoPaste();
        }

        private void DoPaste()
        {
            if (string.IsNullOrEmpty(_entry.Text))
            {
                MessageBox.Show("The Text field can't be empty");
                return;
            }

            if (!EnsureSettings())
                return;

            var client = new PasteBinClient(_settings.ApiDevKey);
            if (!PostAnonymously)
            {
                try
                {
                    client.Login(_settings.UserName, PasswordHelper.UnprotectPassword(_settings.Password));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login error:\r\n" + ex.Message);
                    return;
                }
            }
            try
            {
                client.Paste(_entry);
                if (_entry.Url != null)
                    Process.Start(_entry.Url.AbsoluteUri);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending to PasteBin:\r\n" + ex.Message);
                return;
            }
        }

        private bool EnsureSettings()
        {
            string errorMessage;
            while (!CheckSettings(out errorMessage))
            {
                var r = MessageBox.Show(
                    errorMessage + Environment.NewLine + "Would you like to edit the settings?",
                    "Missing information",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Exclamation);
                if (r == MessageBoxResult.Yes)
                {
                    SettingsWindow settingsWindow = new SettingsWindow(_settings);

                    if (settingsWindow.ShowDialog() == true)
                    {
                        continue;
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool CheckSettings(out string errorMessage)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(_settings.ApiDevKey))
                errors.Add("The API Key is mandatory");
            if (!PostAnonymously)
            {
                if (string.IsNullOrEmpty(_settings.UserName) || string.IsNullOrEmpty(_settings.Password))
                {
                    errors.Add("The user name and password are mandatory");
                }
            }
            errorMessage = string.Join(Environment.NewLine, errors);
            return !errors.Any();
        }

        public PasteBinEntry Entry
        {
            get { return _entry; }
        }

        public IEnumerable<NamedValue<string>> FormatValues
        {
            get
            {
                yield return new NamedValue<string> { Value = "4cs", Name = "4CS" };
                yield return new NamedValue<string> { Value = "6502acme", Name = "6502 ACME Cross Assembler" };
                yield return new NamedValue<string> { Value = "6502kickass", Name = "6502 Kick Assembler" };
                yield return new NamedValue<string> { Value = "6502tasm", Name = "6502 TASM/64TASS" };
                yield return new NamedValue<string> { Value = "abap", Name = "ABAP" };
                yield return new NamedValue<string> { Value = "actionscript", Name = "ActionScript" };
                yield return new NamedValue<string> { Value = "actionscript3", Name = "ActionScript 3" };
                yield return new NamedValue<string> { Value = "ada", Name = "Ada" };
                yield return new NamedValue<string> { Value = "algol68", Name = "ALGOL 68" };
                yield return new NamedValue<string> { Value = "apache", Name = "Apache Log" };
                yield return new NamedValue<string> { Value = "applescript", Name = "AppleScript" };
                yield return new NamedValue<string> { Value = "apt_sources", Name = "APT Sources" };
                yield return new NamedValue<string> { Value = "asm", Name = "ASM (NASM)" };
                yield return new NamedValue<string> { Value = "asp", Name = "ASP" };
                yield return new NamedValue<string> { Value = "autoconf", Name = "autoconf" };
                yield return new NamedValue<string> { Value = "autohotkey", Name = "Autohotkey" };
                yield return new NamedValue<string> { Value = "autoit", Name = "AutoIt" };
                yield return new NamedValue<string> { Value = "avisynth", Name = "Avisynth" };
                yield return new NamedValue<string> { Value = "awk", Name = "Awk" };
                yield return new NamedValue<string> { Value = "bascomavr", Name = "BASCOM AVR" };
                yield return new NamedValue<string> { Value = "bash", Name = "Bash" };
                yield return new NamedValue<string> { Value = "basic4gl", Name = "Basic4GL" };
                yield return new NamedValue<string> { Value = "bibtex", Name = "BibTeX" };
                yield return new NamedValue<string> { Value = "blitzbasic", Name = "Blitz Basic" };
                yield return new NamedValue<string> { Value = "bnf", Name = "BNF" };
                yield return new NamedValue<string> { Value = "boo", Name = "BOO" };
                yield return new NamedValue<string> { Value = "bf", Name = "BrainFuck" };
                yield return new NamedValue<string> { Value = "c", Name = "C" };
                yield return new NamedValue<string> { Value = "c_mac", Name = "C for Macs" };
                yield return new NamedValue<string> { Value = "cil", Name = "C Intermediate Language" };
                yield return new NamedValue<string> { Value = "csharp", Name = "C#" };
                yield return new NamedValue<string> { Value = "cpp", Name = "C++" };
                yield return new NamedValue<string> { Value = "cpp-qt", Name = "C++ (with QT extensions)" };
                yield return new NamedValue<string> { Value = "c_loadrunner", Name = "C: Loadrunner" };
                yield return new NamedValue<string> { Value = "caddcl", Name = "CAD DCL" };
                yield return new NamedValue<string> { Value = "cadlisp", Name = "CAD Lisp" };
                yield return new NamedValue<string> { Value = "cfdg", Name = "CFDG" };
                yield return new NamedValue<string> { Value = "chaiscript", Name = "ChaiScript" };
                yield return new NamedValue<string> { Value = "clojure", Name = "Clojure" };
                yield return new NamedValue<string> { Value = "klonec", Name = "Clone C" };
                yield return new NamedValue<string> { Value = "klonecpp", Name = "Clone C++" };
                yield return new NamedValue<string> { Value = "cmake", Name = "CMake" };
                yield return new NamedValue<string> { Value = "cobol", Name = "COBOL" };
                yield return new NamedValue<string> { Value = "coffeescript", Name = "CoffeeScript" };
                yield return new NamedValue<string> { Value = "cfm", Name = "ColdFusion" };
                yield return new NamedValue<string> { Value = "css", Name = "CSS" };
                yield return new NamedValue<string> { Value = "cuesheet", Name = "Cuesheet" };
                yield return new NamedValue<string> { Value = "d", Name = "D" };
                yield return new NamedValue<string> { Value = "dcs", Name = "DCS" };
                yield return new NamedValue<string> { Value = "delphi", Name = "Delphi" };
                yield return new NamedValue<string> { Value = "oxygene", Name = "Delphi Prism (Oxygene)" };
                yield return new NamedValue<string> { Value = "diff", Name = "Diff" };
                yield return new NamedValue<string> { Value = "div", Name = "DIV" };
                yield return new NamedValue<string> { Value = "dos", Name = "DOS" };
                yield return new NamedValue<string> { Value = "dot", Name = "DOT" };
                yield return new NamedValue<string> { Value = "e", Name = "E" };
                yield return new NamedValue<string> { Value = "ecmascript", Name = "ECMAScript" };
                yield return new NamedValue<string> { Value = "eiffel", Name = "Eiffel" };
                yield return new NamedValue<string> { Value = "email", Name = "Email" };
                yield return new NamedValue<string> { Value = "epc", Name = "EPC" };
                yield return new NamedValue<string> { Value = "erlang", Name = "Erlang" };
                yield return new NamedValue<string> { Value = "fsharp", Name = "F#" };
                yield return new NamedValue<string> { Value = "falcon", Name = "Falcon" };
                yield return new NamedValue<string> { Value = "fo", Name = "FO Language" };
                yield return new NamedValue<string> { Value = "f1", Name = "Formula One" };
                yield return new NamedValue<string> { Value = "fortran", Name = "Fortran" };
                yield return new NamedValue<string> { Value = "freebasic", Name = "FreeBasic" };
                yield return new NamedValue<string> { Value = "gambas", Name = "GAMBAS" };
                yield return new NamedValue<string> { Value = "gml", Name = "Game Maker" };
                yield return new NamedValue<string> { Value = "gdb", Name = "GDB" };
                yield return new NamedValue<string> { Value = "genero", Name = "Genero" };
                yield return new NamedValue<string> { Value = "genie", Name = "Genie" };
                yield return new NamedValue<string> { Value = "gettext", Name = "GetText" };
                yield return new NamedValue<string> { Value = "go", Name = "Go" };
                yield return new NamedValue<string> { Value = "groovy", Name = "Groovy" };
                yield return new NamedValue<string> { Value = "gwbasic", Name = "GwBasic" };
                yield return new NamedValue<string> { Value = "haskell", Name = "Haskell" };
                yield return new NamedValue<string> { Value = "hicest", Name = "HicEst" };
                yield return new NamedValue<string> { Value = "hq9plus", Name = "HQ9 Plus" };
                yield return new NamedValue<string> { Value = "html4strict", Name = "HTML" };
                yield return new NamedValue<string> { Value = "html5", Name = "HTML 5" };
                yield return new NamedValue<string> { Value = "icon", Name = "Icon" };
                yield return new NamedValue<string> { Value = "idl", Name = "IDL" };
                yield return new NamedValue<string> { Value = "ini", Name = "INI file" };
                yield return new NamedValue<string> { Value = "inno", Name = "Inno Script" };
                yield return new NamedValue<string> { Value = "intercal", Name = "INTERCAL" };
                yield return new NamedValue<string> { Value = "io", Name = "IO" };
                yield return new NamedValue<string> { Value = "j", Name = "J" };
                yield return new NamedValue<string> { Value = "java", Name = "Java" };
                yield return new NamedValue<string> { Value = "java5", Name = "Java 5" };
                yield return new NamedValue<string> { Value = "javascript", Name = "JavaScript" };
                yield return new NamedValue<string> { Value = "jquery", Name = "jQuery" };
                yield return new NamedValue<string> { Value = "kixtart", Name = "KiXtart" };
                yield return new NamedValue<string> { Value = "latex", Name = "Latex" };
                yield return new NamedValue<string> { Value = "lb", Name = "Liberty BASIC" };
                yield return new NamedValue<string> { Value = "lsl2", Name = "Linden Scripting" };
                yield return new NamedValue<string> { Value = "lisp", Name = "Lisp" };
                yield return new NamedValue<string> { Value = "llvm", Name = "LLVM" };
                yield return new NamedValue<string> { Value = "locobasic", Name = "Loco Basic" };
                yield return new NamedValue<string> { Value = "logtalk", Name = "Logtalk" };
                yield return new NamedValue<string> { Value = "lolcode", Name = "LOL Code" };
                yield return new NamedValue<string> { Value = "lotusformulas", Name = "Lotus Formulas" };
                yield return new NamedValue<string> { Value = "lotusscript", Name = "Lotus Script" };
                yield return new NamedValue<string> { Value = "lscript", Name = "LScript" };
                yield return new NamedValue<string> { Value = "lua", Name = "Lua" };
                yield return new NamedValue<string> { Value = "m68k", Name = "M68000 Assembler" };
                yield return new NamedValue<string> { Value = "magiksf", Name = "MagikSF" };
                yield return new NamedValue<string> { Value = "make", Name = "Make" };
                yield return new NamedValue<string> { Value = "mapbasic", Name = "MapBasic" };
                yield return new NamedValue<string> { Value = "matlab", Name = "MatLab" };
                yield return new NamedValue<string> { Value = "mirc", Name = "mIRC" };
                yield return new NamedValue<string> { Value = "mmix", Name = "MIX Assembler" };
                yield return new NamedValue<string> { Value = "modula2", Name = "Modula 2" };
                yield return new NamedValue<string> { Value = "modula3", Name = "Modula 3" };
                yield return new NamedValue<string> { Value = "68000devpac", Name = "Motorola 68000 HiSoft Dev" };
                yield return new NamedValue<string> { Value = "mpasm", Name = "MPASM" };
                yield return new NamedValue<string> { Value = "mxml", Name = "MXML" };
                yield return new NamedValue<string> { Value = "mysql", Name = "MySQL" };
                yield return new NamedValue<string> { Value = "newlisp", Name = "newLISP" };
                yield return new NamedValue<string> { Value = "text", Name = "None" };
                yield return new NamedValue<string> { Value = "nsis", Name = "NullSoft Installer" };
                yield return new NamedValue<string> { Value = "oberon2", Name = "Oberon 2" };
                yield return new NamedValue<string> { Value = "objeck", Name = "Objeck Programming Langua" };
                yield return new NamedValue<string> { Value = "objc", Name = "Objective C" };
                yield return new NamedValue<string> { Value = "ocaml-brief", Name = "OCalm Brief" };
                yield return new NamedValue<string> { Value = "ocaml", Name = "OCaml" };
                yield return new NamedValue<string> { Value = "pf", Name = "OpenBSD PACKET FILTER" };
                yield return new NamedValue<string> { Value = "glsl", Name = "OpenGL Shading" };
                yield return new NamedValue<string> { Value = "oobas", Name = "Openoffice BASIC" };
                yield return new NamedValue<string> { Value = "oracle11", Name = "Oracle 11" };
                yield return new NamedValue<string> { Value = "oracle8", Name = "Oracle 8" };
                yield return new NamedValue<string> { Value = "oz", Name = "Oz" };
                yield return new NamedValue<string> { Value = "pascal", Name = "Pascal" };
                yield return new NamedValue<string> { Value = "pawn", Name = "PAWN" };
                yield return new NamedValue<string> { Value = "pcre", Name = "PCRE" };
                yield return new NamedValue<string> { Value = "per", Name = "Per" };
                yield return new NamedValue<string> { Value = "perl", Name = "Perl" };
                yield return new NamedValue<string> { Value = "perl6", Name = "Perl 6" };
                yield return new NamedValue<string> { Value = "php", Name = "PHP" };
                yield return new NamedValue<string> { Value = "php-brief", Name = "PHP Brief" };
                yield return new NamedValue<string> { Value = "pic16", Name = "Pic 16" };
                yield return new NamedValue<string> { Value = "pike", Name = "Pike" };
                yield return new NamedValue<string> { Value = "pixelbender", Name = "Pixel Bender" };
                yield return new NamedValue<string> { Value = "plsql", Name = "PL/SQL" };
                yield return new NamedValue<string> { Value = "postgresql", Name = "PostgreSQL" };
                yield return new NamedValue<string> { Value = "povray", Name = "POV-Ray" };
                yield return new NamedValue<string> { Value = "powershell", Name = "Power Shell" };
                yield return new NamedValue<string> { Value = "powerbuilder", Name = "PowerBuilder" };
                yield return new NamedValue<string> { Value = "proftpd", Name = "ProFTPd" };
                yield return new NamedValue<string> { Value = "progress", Name = "Progress" };
                yield return new NamedValue<string> { Value = "prolog", Name = "Prolog" };
                yield return new NamedValue<string> { Value = "properties", Name = "Properties" };
                yield return new NamedValue<string> { Value = "providex", Name = "ProvideX" };
                yield return new NamedValue<string> { Value = "purebasic", Name = "PureBasic" };
                yield return new NamedValue<string> { Value = "pycon", Name = "PyCon" };
                yield return new NamedValue<string> { Value = "python", Name = "Python" };
                yield return new NamedValue<string> { Value = "q", Name = "q/kdb+" };
                yield return new NamedValue<string> { Value = "qbasic", Name = "QBasic" };
                yield return new NamedValue<string> { Value = "rsplus", Name = "R" };
                yield return new NamedValue<string> { Value = "rails", Name = "Rails" };
                yield return new NamedValue<string> { Value = "rebol", Name = "REBOL" };
                yield return new NamedValue<string> { Value = "reg", Name = "REG" };
                yield return new NamedValue<string> { Value = "robots", Name = "Robots" };
                yield return new NamedValue<string> { Value = "rpmspec", Name = "RPM Spec" };
                yield return new NamedValue<string> { Value = "ruby", Name = "Ruby" };
                yield return new NamedValue<string> { Value = "gnuplot", Name = "Ruby Gnuplot" };
                yield return new NamedValue<string> { Value = "sas", Name = "SAS" };
                yield return new NamedValue<string> { Value = "scala", Name = "Scala" };
                yield return new NamedValue<string> { Value = "scheme", Name = "Scheme" };
                yield return new NamedValue<string> { Value = "scilab", Name = "Scilab" };
                yield return new NamedValue<string> { Value = "sdlbasic", Name = "SdlBasic" };
                yield return new NamedValue<string> { Value = "smalltalk", Name = "Smalltalk" };
                yield return new NamedValue<string> { Value = "smarty", Name = "Smarty" };
                yield return new NamedValue<string> { Value = "sql", Name = "SQL" };
                yield return new NamedValue<string> { Value = "systemverilog", Name = "SystemVerilog" };
                yield return new NamedValue<string> { Value = "tsql", Name = "T-SQL" };
                yield return new NamedValue<string> { Value = "tcl", Name = "TCL" };
                yield return new NamedValue<string> { Value = "teraterm", Name = "Tera Term" };
                yield return new NamedValue<string> { Value = "thinbasic", Name = "thinBasic" };
                yield return new NamedValue<string> { Value = "typoscript", Name = "TypoScript" };
                yield return new NamedValue<string> { Value = "unicon", Name = "Unicon" };
                yield return new NamedValue<string> { Value = "uscript", Name = "UnrealScript" };
                yield return new NamedValue<string> { Value = "vala", Name = "Vala" };
                yield return new NamedValue<string> { Value = "vbnet", Name = "VB.NET" };
                yield return new NamedValue<string> { Value = "verilog", Name = "VeriLog" };
                yield return new NamedValue<string> { Value = "vhdl", Name = "VHDL" };
                yield return new NamedValue<string> { Value = "vim", Name = "VIM" };
                yield return new NamedValue<string> { Value = "visualprolog", Name = "Visual Pro Log" };
                yield return new NamedValue<string> { Value = "vb", Name = "VisualBasic" };
                yield return new NamedValue<string> { Value = "visualfoxpro", Name = "VisualFoxPro" };
                yield return new NamedValue<string> { Value = "whitespace", Name = "WhiteSpace" };
                yield return new NamedValue<string> { Value = "whois", Name = "WHOIS" };
                yield return new NamedValue<string> { Value = "winbatch", Name = "Winbatch" };
                yield return new NamedValue<string> { Value = "xbasic", Name = "XBasic" };
                yield return new NamedValue<string> { Value = "xml", Name = "XML" };
                yield return new NamedValue<string> { Value = "xorg_conf", Name = "Xorg Config" };
                yield return new NamedValue<string> { Value = "xpp", Name = "XPP" };
                yield return new NamedValue<string> { Value = "yaml", Name = "YAML" };
                yield return new NamedValue<string> { Value = "z80", Name = "Z80 Assembler" };
                yield return new NamedValue<string> { Value = "zxbasic", Name = "ZXBasic" };
            }
        }

        public IEnumerable<NamedValue<PasteBinExpiration>> ExpirationValues
        {
            get
            {
                yield return new NamedValue<PasteBinExpiration> { Value = PasteBinExpiration.Never, Name = "Never" };
                yield return new NamedValue<PasteBinExpiration> { Value = PasteBinExpiration.TenMinutes, Name = "10 minutes" };
                yield return new NamedValue<PasteBinExpiration> { Value = PasteBinExpiration.OneHour, Name = "1 hour" };
                yield return new NamedValue<PasteBinExpiration> { Value = PasteBinExpiration.OneDay, Name = "1 day" };
                yield return new NamedValue<PasteBinExpiration> { Value = PasteBinExpiration.OneMonth, Name = "1 month" };
            }
        }

        private bool _postAnonymously;
        public bool PostAnonymously
        {
            get { return _postAnonymously; }
            set
            {
                _postAnonymously = value;
                OnPropertyChanged("PostAnonymously");
                OnPropertyChanged("PostAsUser");
            }
        }

        public bool PostAsUser
        {
            get { return !PostAnonymously; }
        }

        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_settings.UserName))
                    return "(not set)";
                return _settings.UserName;
            }
        }
        

        public struct NamedValue<TValue>
        {
            public TValue Value { get; set; }
            public string Name { get; set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow(_settings);
            if (window.ShowDialog() == true)
            {
                OnPropertyChanged("UserName");
            }
        }
    }
}
