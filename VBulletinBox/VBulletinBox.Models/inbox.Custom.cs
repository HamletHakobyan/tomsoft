using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Web;
using System;
using System.Linq;
using Developpez.Dotnet;

namespace VBulletinBox.Models
{
    public partial class MessageRepository
    {
        public MessageRepository()
        {
            this.Folders = new System.Collections.Generic.List<MessageFolder>();
        }

        public MessageRepository(VBulletinAccount account)
            : this()
        {
            this.Account = account;
        }

        public static MessageRepository FromFile(string filename, VBulletinAccount account)
        {
            XmlSerializer xs = new XmlSerializer(typeof(MessageRepository));
            using (XmlReader rd = XmlReader.Create(filename))
            {
                MessageRepository mr = xs.Deserialize(rd) as MessageRepository;
                mr.Account = account;
                return mr;
            }
        }

        public void Save(string filename)
        {
            string dir = Path.GetDirectoryName(filename);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            XmlSerializer xs = new XmlSerializer(typeof(MessageRepository));
            using (StreamWriter wr = new StreamWriter(filename))
            {
                xs.Serialize(wr, this);
            }
        }

        public string Name
        {
            get { return (Account != null) ? Account.DisplayName : ""; }
        }
        
        [XmlIgnore]
        public VBulletinAccount Account { get; private set; }

        public int Merge(MessageRepository other)
        {
            int count = 0;
            foreach (MessageFolder otherFolder in other.Folders)
            {
                MessageFolder folder = this.Folders.FirstOrDefault(f => f.Name == otherFolder.Name);
                if (folder == null)
                {
                    this.Folders.Add(otherFolder);
                    count += otherFolder.Messages.Count;
                }
                else
                {
                    count += folder.Merge(otherFolder);
                }
            }
            return count;
        }
    }

    public partial class MessageFolder
    {
        public MessageFolder()
        {
            this.Messages = new System.Collections.Generic.List<Message>();
        }

        public int Merge(MessageFolder otherFolder)
        {
            var otherMessages = otherFolder.Messages.Select(m => new { Digest = m.GetDigest(), Message = m });
            var myMessages = this.Messages.Select(m => new { Digest = m.GetDigest(), Message = m });

            var newMessages = (from o in otherMessages
                               where !myMessages.Any(m => m.Digest == o.Digest)
                               select o.Message).ToList();

            this.Messages.AddRange(newMessages);
            return newMessages.Count;
        }
    }

    public partial class Message
    {
        public string GetDigest()
        {
            string concat =
                string.Concat(
                    this.FromUser,
                    this.FromUserId,
                    this.ToUser,
                    this.DateStamp,
                    this.Title,
                    this.Body);
            return concat.GetMD5Digest();
        }
    }
}
