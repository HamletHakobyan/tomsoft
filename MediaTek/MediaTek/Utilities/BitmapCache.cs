using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Data;

namespace MediaTek.Utilities
{
    public class BitmapCache
    {
        private Dictionary<EntityKey, BitmapSource> _cache = new Dictionary<EntityKey, BitmapSource>();


        public BitmapSource GetBitmapFromCache(EntityKey key, byte[] rawData)
        {
            if (key != null && _cache.ContainsKey(key))
            {
                return _cache[key];
            }
            else if (rawData != null)
            {
                BitmapSource bitmap = ImageHelper.ImageFromBytes(rawData);
                if (key != null)
                    _cache[key] = bitmap;
                return bitmap;

            }
            else
            {
                return null;
            }
        }

        public BitmapSource GetBitmapFromCache(EntityKey key)
        {
            return GetBitmapFromCache(key, null);
        }

        public void SetBitmap(EntityKey key, BitmapSource value)
        {
            if (key != null)
                _cache[key] = value;
        }

        public void ClearBitmap(EntityKey key)
        {
            if (key != null)
            _cache.Remove(key);
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
