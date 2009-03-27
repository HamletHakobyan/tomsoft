using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Velib.Model
{
    public class Network
    {
        public Network()
        {

        }

        public Network(string name, string baseUri)
        {
            this.Name = name;
            this.BaseUri = baseUri;
        }

        public string Name { get; set; }
        public string BaseUri { get; set; }

        private VelibProvider _provider = null;
        private VelibProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new VelibProvider(BaseUri);
                }
                return _provider;
            }
        }


        private NetworkData _data = null;
        [XmlIgnore]
        public NetworkData Data
        {
            get
            {
                if (_data == null)
                {
                    if (!LoadFromCache())
                    {
                        RefreshData();
                    }
                }
                return _data;
            }
        }

        private bool LoadFromCache()
        {
            string hash = ComputeHash();
            string cacheDir = Path.Combine(App.Current.AppDataPath, "Cache");
            string cacheFileName = Path.Combine(cacheDir, hash + ".xml");
            if (File.Exists(cacheFileName))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(NetworkData));
                    using (XmlReader reader = XmlReader.Create(cacheFileName))
                    {
                        _data = xs.Deserialize(reader) as NetworkData;
                        _data.InitProvider();
                    }
                    return true;
                }
                catch {}
            }
            return false;
        }

        private string ComputeHash()
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(BaseUri);
            byte[] hash = md5.ComputeHash(bytes);
            return ToHexString(hash);
        }

        private string ToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        private void SaveToCache()
        {
            string hash = ComputeHash();
            string cacheDir = Path.Combine(App.Current.AppDataPath, "Cache");
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            string cacheFileName = Path.Combine(cacheDir, hash + ".xml");
            XmlSerializer xs = new XmlSerializer(typeof(NetworkData));
            using (XmlWriter writer = XmlWriter.Create(cacheFileName))
            {
                xs.Serialize(writer, _data);
            }
        }

        public void RefreshData()
        {
            _data = Provider.GetNetworkData();
            SaveToCache();
        }

        public void InvalidateData()
        {
            _data = null;
        }
    }
}
