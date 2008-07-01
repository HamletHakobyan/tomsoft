using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaTek
{

    partial class Movie : IFilterable
    {
        public ImageSource Cover
        {
            get
            {
                if (CoverRaw != null)
                {
                    MemoryStream stream = new MemoryStream(CoverRaw);
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.StreamSource = stream;
                    //img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();
                    return img;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnCoverChanging(value);
                this.SendPropertyChanging();
                BitmapImage img = value as BitmapImage;
                if (img != null)
                {
                    MemoryStream stream = new MemoryStream();
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(img));
                    encoder.Save(stream);
                    CoverRaw = stream.GetBuffer();
                }
                else
                {
                    CoverRaw = null;
                }
                this.SendPropertyChanged("Cover");
                this.OnCoverChanged();
            }
        }

        partial void OnCoverChanging(ImageSource value);
        partial void OnCoverChanged();

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
    }

    public partial class Director : IFilterable
    {
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
    }

    public partial class Language : IFilterable
    {
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
    }

    public partial class Country : IFilterable
    {
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
    }

    public partial class MediaType : IFilterable
    {
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
    }

    public partial class Lend : IFilterable
    {
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
    }
}
