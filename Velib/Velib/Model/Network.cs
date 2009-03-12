using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    _data = Provider.GetNetworkData();
                }
                return _data;
            }
        }

        public void RefreshData()
        {
            _data = Provider.GetNetworkData();
        }

        public void InvalidateData()
        {
            _data = null;
        }
    }
}
