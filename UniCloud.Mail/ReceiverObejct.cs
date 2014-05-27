using LumiSoft.Net.POP3.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniCloud.Mail
{
    public class ReceiverObject
    {
        private POP3_ClientMessage _Message;
        private bool _Saved;
        private List<object> _Objects;

        public POP3_ClientMessage Message { get { return _Message; } set { _Message = value; } }
        public List<object> Objects { get { return _Objects; } }
        public bool Saved { get { return _Saved; } set { _Saved = value; } }

        public ReceiverObject()
        {
            _Saved = false;
            _Objects = new List<object>();
        }
    }
}
