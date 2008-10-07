using System;
using System.Data.Common;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MediaTek.Controls;
using MediaTek.Utilities;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace MediaTek.DataModel
{
    public partial class DvdEntities
    {
        private bool _modified = false;
        public bool Modified
        {
            get { return _modified; }
            set
            {
                if (value != _modified)
                {
                    _modified = value;
                    OnModifiedChanged();
                }
            }
        }

        public event EventHandler ModifiedChanged;

        protected void OnModifiedChanged()
        {
            if (ModifiedChanged != null)
                ModifiedChanged(this, EventArgs.Empty);
        }

        public DbTransaction Transaction { get; set; }
        public void BeginTransaction()
        {
            Transaction = this.Connection.BeginTransaction();
        }
        public void Commit()
        {
            Transaction.Commit();
        }
        public void Rollback()
        {
            Transaction.Rollback();
        }
    }

    [EditorControl(typeof(MovieEditor))]
    public partial class Movie : IFilterable, IHasContextMenu
    {

        #region Partial method declarations

        partial void OnCoverChanging(BitmapSource value);
        partial void OnCoverChanged();

        #endregion

        #region Partial method implementations

        partial void OnCoverRawChanging(byte[] value)
        {
            if (!isSettingCover)
            {
                BitmapSource img = null;
                if (value != null)
                    img = ImageHelper.ImageFromBytes(value);
                this.OnCoverChanging(img);
                OnPropertyChanging("Cover");
            }
        }

        partial void OnCoverRawChanged()
        {
            if (!isSettingCover)
            {
                this.OnPropertyChanged("Cover");
                this.OnCoverChanged();
            }
        }

        #endregion

        private static BitmapCache bitmapCache = new BitmapCache();

        bool isSettingCover = false;
        public BitmapSource Cover
        {
            get
            {
                return bitmapCache.GetBitmapFromCache(this.EntityKey, this.CoverRaw);
            }
            set
            {
                try
                {
                    isSettingCover = true;
                    this.OnCoverChanging(value);
                    if (value != null)
                    {
                        bitmapCache.SetBitmap(this.EntityKey, value);
                        CoverRaw = ImageHelper.BytesFromImage(value);
                    }
                    else
                    {
                        bitmapCache.ClearBitmap(this.EntityKey);
                        CoverRaw = null;
                    }
                    this.OnPropertyChanged("Cover");
                    this.OnCoverChanged();
                }
                finally
                {
                    isSettingCover = false;
                }
            }
        }

        public bool Lent
        {
            get
            {
                return CurrentLend != null;
            }
        }
        
        public Lend CurrentLend
        {
            get
            {
                return (from lnd in this.Lends
                        where !lnd.Returned && lnd.EntityState == EntityState.Unchanged
                        orderby lnd.LentDate descending
                        select lnd).FirstOrDefault();
            }
        }

        public void NotifyLend()
        {
            OnPropertyChanged("Lent");
        }

        public override string ToString()
        {
            return this.Title;
        }

        #region IFilterable Members

        public bool IsMatch(string pattern)
        {
            string p = pattern.ToLower();
            if (this.Title.ToLower().Contains(p))
                return true;
            if (!String.IsNullOrEmpty(this.OriginalTitle) && this.OriginalTitle.ToLower().Contains(p))
                return true;
            if (this.Director != null && this.Director.Name.ToLower().Contains(p))
                return true;
            if (this.Year == p)
                return true;
            return false;
        }

        #endregion


        #region IHasContextMenu Members

        public string ContextMenuKey
        {
            get { return "mnuMovie"; }
        }

        #endregion
    }

    [EditorControl(typeof(DirectorEditor))]
    public partial class Director : IFilterable, IHasContextMenu
    {
        #region Partial method declarations

        partial void OnPictureChanged();

        partial void OnPictureChanging(BitmapSource value);

        #endregion

        #region Partial method implementations

        partial void OnPictureRawChanging(byte[] value)
        {
            if (!isSettingPicture)
            {
                BitmapSource img = null;
                if (value != null)
                    img = ImageHelper.ImageFromBytes(value);
                this.OnPictureChanging(img);
                OnPropertyChanging("Picture");
            }
        }

        partial void OnPictureRawChanged()
        {
            if (!isSettingPicture)
            {
                this.OnPropertyChanged("Picture");
                this.OnPictureChanged();
            }
        }

        #endregion

        private static BitmapCache bitmapCache = new BitmapCache();

        bool isSettingPicture = false;
        public BitmapSource Picture
        {
            get
            {
                return bitmapCache.GetBitmapFromCache(this.EntityKey, this.PictureRaw);
            }
            set
            {
                try
                {
                    isSettingPicture = true;
                    this.OnPictureChanging(value);
                    if (value != null)
                    {
                        bitmapCache.SetBitmap(this.EntityKey, value);
                        PictureRaw = ImageHelper.BytesFromImage(value);
                    }
                    else
                    {
                        bitmapCache.ClearBitmap(this.EntityKey);
                        PictureRaw = null;
                    }
                    this.OnPropertyChanged("Picture");
                    this.OnPictureChanged();
                }
                finally
                {
                    isSettingPicture = false;
                }
            }
        }
	
        public override string ToString()
        {
            return this.Name;
        }

        #region IFilterable Members

        public bool IsMatch(string pattern)
        {
            string p = pattern.ToLower();
            if (this.Name.ToLower().Contains(p))
                return true;
            if (this.Country != null && this.Country.Name.ToLower().Contains(p))
                return true;
            return false;
        }

        #endregion

        #region IHasContextMenu Members

        public string ContextMenuKey
        {
            get { return "mnuDirector"; }
        }

        #endregion
    }

    [EditorControl(typeof(LanguageEditor))]
    public partial class Language : IFilterable, IHasContextMenu
    {
        #region Partial method declarations

        partial void OnSymbolChanging(BitmapSource value);
        partial void OnSymbolChanged();

        #endregion

        #region Partial method implementations

        partial void OnSymbolRawChanging(byte[] value)
        {
            if (!isSettingSymbol)
            {
                BitmapSource img = null;
                if (value != null)
                    img = ImageHelper.ImageFromBytes(value);
                this.OnSymbolChanging(img);
                OnPropertyChanging("Symbol");
            }
        }

        partial void OnSymbolRawChanged()
        {
            if (!isSettingSymbol)
            {
                this.OnPropertyChanged("Symbol");
                this.OnSymbolChanged();
            }
        }

        #endregion

        private static BitmapCache bitmapCache = new BitmapCache();

        bool isSettingSymbol = false;
        public BitmapSource Symbol
        {
            get
            {
                return bitmapCache.GetBitmapFromCache(this.EntityKey, this.SymbolRaw);
            }
            set
            {
                try
                {
                    isSettingSymbol = true;
                    this.OnSymbolChanging(value);
                    if (value != null)
                    {
                        bitmapCache.SetBitmap(this.EntityKey, value);
                        SymbolRaw = ImageHelper.BytesFromImage(value);
                    }
                    else
                    {
                        bitmapCache.ClearBitmap(this.EntityKey); 
                        SymbolRaw = null;
                    }
                    this.OnPropertyChanged("Symbol");
                    this.OnSymbolChanged();
                }
                finally
                {
                    isSettingSymbol = false;
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region IFilterable Members

        public bool IsMatch(string pattern)
        {
            string p = pattern.ToLower();
            if (this.Name.ToLower().Contains(p))
                return true;
            if (this.Code.ToLower().Contains(p))
                return true;
            return false;
        }

        #endregion

        #region IHasContextMenu Members

        public string ContextMenuKey
        {
            get { return "mnuLanguage"; }
        }

        #endregion
    }

    [EditorControl(typeof(CountryEditor))]
    public partial class Country : IFilterable, IHasContextMenu
    {
        #region Partial method declarations

        partial void OnFlagChanging(BitmapSource value);
        partial void OnFlagChanged();

        #endregion

        #region Partial method implementations

        partial void OnFlagRawChanging(byte[] value)
        {
            if (!isSettingFlag)
            {
                BitmapSource img = null;
                if (value != null)
                    img = ImageHelper.ImageFromBytes(value);
                this.OnFlagChanging(img);
                OnPropertyChanging("Flag");
            }
        }

        partial void OnFlagRawChanged()
        {
            if (!isSettingFlag)
            {
                this.OnPropertyChanged("Flag");
                this.OnFlagChanged();
            }
        }

        #endregion

        private static BitmapCache bitmapCache = new BitmapCache();

        bool isSettingFlag = false;
        public BitmapSource Flag
        {
            get
            {
                return bitmapCache.GetBitmapFromCache(this.EntityKey, this.FlagRaw);
            }
            set
            {
                try
                {
                    isSettingFlag = true;
                    this.OnFlagChanging(value);
                    if (value != null)
                    {
                        bitmapCache.SetBitmap(this.EntityKey, value);
                        FlagRaw = ImageHelper.BytesFromImage(value);
                    }
                    else
                    {
                        bitmapCache.ClearBitmap(this.EntityKey);
                        FlagRaw = null;
                    }
                    this.OnPropertyChanged("Flag");
                    this.OnFlagChanged();
                }
                finally
                {
                    isSettingFlag = false;
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region IFilterable Members

        public bool IsMatch(string pattern)
        {
            string p = pattern.ToLower();
            if (this.Name.ToLower().Contains(p))
                return true;
            if (this.Language != null && this.Language.Name.ToLower().Contains(p))
                return true;
            return false;
        }

        #endregion

        #region IHasContextMenu Members

        public string ContextMenuKey
        {
            get { return "mnuCountry"; }
        }

        #endregion
    }

    [EditorControl(typeof(MediaTypeEditor))]
    public partial class MediaType : IFilterable, IHasContextMenu
    {
        #region Partial method declarations

        partial void OnSymbolChanging(BitmapSource value);
        partial void OnSymbolChanged();

        #endregion

        #region Partial method implementations

        partial void OnSymbolRawChanging(byte[] value)
        {
            if (!isSettingSymbol)
            {
                BitmapSource img = null;
                if (value != null)
                    img = ImageHelper.ImageFromBytes(value);
                this.OnSymbolChanging(img);
                OnPropertyChanging("Symbol");
            }
        }

        partial void OnSymbolRawChanged()
        {
            if (!isSettingSymbol)
            {
                this.OnPropertyChanged("Symbol");
                this.OnSymbolChanged();
            }
        }

        #endregion

        private static BitmapCache bitmapCache = new BitmapCache();

        bool isSettingSymbol = false;
        public BitmapSource Symbol
        {
            get
            {
                return bitmapCache.GetBitmapFromCache(this.EntityKey, this.SymbolRaw);
            }
            set
            {
                try
                {
                    isSettingSymbol = true;
                    this.OnSymbolChanging(value);
                    if (value != null)
                    {
                        bitmapCache.SetBitmap(this.EntityKey, value);
                        SymbolRaw = ImageHelper.BytesFromImage(value);
                    }
                    else
                    {
                        bitmapCache.ClearBitmap(this.EntityKey);
                        SymbolRaw = null;
                    }
                    this.OnPropertyChanged("Symbol");
                    this.OnSymbolChanged();
                }
                finally
                {
                    isSettingSymbol = false;
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region IFilterable Members

        public bool IsMatch(string pattern)
        {
            string p = pattern.ToLower();
            if (this.Name.ToLower().Contains(p))
                return true;
            return false;
        }

        #endregion

        #region IHasContextMenu Members

        public string ContextMenuKey
        {
            get { return "mnuMediaType"; }
        }

        #endregion
    }

    [EditorControl(typeof(LendEditor))]
    public partial class Lend : IFilterable, IHasContextMenu
    {
        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
            if (this.Movie != null)
                this.Movie.NotifyLend();
        }

        public override string ToString()
        {
            return this.Movie.Title + " lent to " + this.LentTo;
        }

        public bool Returned
        {
            get { return ReturnDate.HasValue; }
        }

        #region IFilterable Members

        public bool IsMatch(string pattern)
        {
            string p = pattern.ToLower();
            if (this.Movie.Title.ToLower().Contains(p))
                return true;
            if (this.LentTo.ToLower().Contains(p))
                return true;
            return false;
        }

        #endregion

        #region IHasContextMenu Members

        public string ContextMenuKey
        {
            get { return "mnuLend"; }
        }

        #endregion
    }
}
