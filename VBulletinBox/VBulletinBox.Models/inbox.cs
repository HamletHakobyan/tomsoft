namespace VBulletinBox.Models
{
    using System.Xml.Serialization;
    using System.Collections.Generic;
    
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "privatemessages", Namespace = "", IsNullable = false)]
    public partial class MessageRepository {
        
        private List<MessageFolder> itemsField;
        
        [System.Xml.Serialization.XmlElementAttribute("folder", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<MessageFolder> Folders
        {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class MessageFolder {
        
        private List<Message> privatemessageField;
        
        private string nameField;
        
        [System.Xml.Serialization.XmlElementAttribute("privatemessage", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<Message> Messages
        {
            get {
                return this.privatemessageField;
            }
            set {
                this.privatemessageField = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "name")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class Message {
        
        private string datestampField;
        
        private string titleField;
        
        private string fromuserField;
        
        private string fromuseridField;
        
        private string touserField;
        
        private string messageField;
        
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "datestamp", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DateStamp {
            get {
                return this.datestampField;
            }
            set {
                this.datestampField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "title", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "fromuser", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FromUser {
            get {
                return this.fromuserField;
            }
            set {
                this.fromuserField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "fromuserid", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FromUserId {
            get {
                return this.fromuseridField;
            }
            set {
                this.fromuseridField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "touser", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ToUser {
            get {
                return this.touserField;
            }
            set {
                this.touserField = value;
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "message", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Body {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }

    }
}
