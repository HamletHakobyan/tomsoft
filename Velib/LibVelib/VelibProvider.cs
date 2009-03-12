using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace Velib
{
    public class VelibProvider
    {
        public VelibProvider(Uri baseUri)
        {
            this.BaseUri = baseUri;
            InitUris(baseUri);
        }

        public VelibProvider(string baseUri)
            : this(new Uri(baseUri))
        {
        }

        public VelibProvider(NetworkData network)
        {
            this.BaseUri = network.BaseUri;
            InitUris(network.BaseUri);
            foreach (Station station in network.Stations)
            {
                station.Provider = this;
            }
        }

        private void InitUris(Uri baseUri)
        {
            this.NetworkUri = baseUri.Combine("carto");
            this.StationStatusUri = baseUri.Combine("stationdetails");
        }

        public Uri BaseUri { get; private set; }

        public Uri NetworkUri { get; private set; }

        public Uri StationStatusUri { get; set; }

        private XmlAttributeOverrides _xmlOverrides = null;
        private XmlAttributeOverrides GetXmlOverrides()
        {
            if (_xmlOverrides == null)
            {
                _xmlOverrides = new XmlAttributeOverrides();
                XmlAttributes attr = new XmlAttributes();
                attr.XmlIgnore = true;
                _xmlOverrides.Add(typeof(NetworkData), "Name", attr);
                _xmlOverrides.Add(typeof(NetworkData), "BaseUriString", attr);
            }
            return _xmlOverrides;
        }

        public NetworkData GetNetworkData()
        {
            HttpWebRequest req = WebRequest.Create(NetworkUri) as HttpWebRequest;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    XmlSerializer xs = new XmlSerializer(typeof(NetworkData), GetXmlOverrides());
                    NetworkData network = xs.Deserialize(respStream) as NetworkData;
                    network.BaseUri = this.BaseUri;
                    foreach (Station station in network.Stations)
                    {
                        station.Provider = this;
                    }
                    return network;
                }
            }
            else
            {
                string msg = string.Format("HTTP Error : {0} - {1}", resp.StatusCode, resp.StatusDescription);
                throw new ApplicationException(msg);
            }
        }

        public StationStatus GetStatus(Station station)
        {
            Uri reqUri = StationStatusUri.Combine(station.Number);
            HttpWebRequest req = WebRequest.Create(reqUri) as HttpWebRequest;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    XmlSerializer xs = new XmlSerializer(typeof(StationStatus));
                    StationStatus status = xs.Deserialize(respStream) as StationStatus;
                    return status;
                }
            }
            else
            {
                string msg = string.Format("HTTP Error : {0} - {1}", resp.StatusCode, resp.StatusDescription);
                throw new ApplicationException(msg);
            }
        }
    }
}
