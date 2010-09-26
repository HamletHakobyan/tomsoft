using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Mediatek.Web.Helpers
{
    public static class ImageHelper
    {
        public static string GetPath(this Mediatek.Entities.Image image, bool thumbnail)
        {
            if (image != null)
            {
                string path = thumbnail
                    ? string.Format("/Content/DbImages/Thumbnails/{0}.png", image.Id)
                    : string.Format("/Content/DbImages/{0}.png", image.Id);

                string physicalPath = HttpContext.Current.Server.MapPath(path);
                if (File.Exists(physicalPath))
                {
                    return path;
                }
                else
                {
                    var imageData = image.Data;
                    if (imageData != null && imageData.Bytes != null)
                    {
                        if (thumbnail)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData.Bytes))
                            using (var img = System.Drawing.Image.FromStream(ms))
                            using (var thumb = img.GetThumbnailImage(100, 150, null, IntPtr.Zero))
                            {
                                thumb.Save(physicalPath);
                            }
                        }
                        else
                        {
                            File.WriteAllBytes(physicalPath, imageData.Bytes);
                        }
                        return path;
                    }
                }
            }
            return "/Content/MissingImage.png";
        }
    }
}