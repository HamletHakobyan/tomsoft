using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VBulletinBox.Models;
using System.Web;
using System.Globalization;

namespace VBulletinBox.ViewModels
{
    public class MessageViewModel : ViewModelBase<Message>
    {
        private Message _message;
        public MessageViewModel(Message message)
        {
            _message = message;
        }

        public string Title
        {
            get { return HttpUtility.HtmlDecode(_message.Title); }
        }

        public string FromUser
        {
            get { return _message.FromUser; }
        }

        public string FromUserId
        {
            get { return _message.FromUserId; }
        }

        public string ToUser
        {
            get { return _message.ToUser; }
        }

        public DateTime Date
        {
            get { return DateTime.ParseExact(_message.DateStamp, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture); }
        }

        public string Body
        {
            get { return _message.Body; }
        }

        public override Message Model
        {
            get { return _message; }
        }
    }
}
