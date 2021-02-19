using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Screen_Scene_System
{
    public class Conversations
    {
        private Dictionary<string, Conversation> _conversationList = new Dictionary<string, Conversation>();

        [ContentSerializer]
        public Dictionary<string, Conversation> ConversationList
        {
            get { return _conversationList; }
            set { _conversationList = value; }
        }

        public Conversations()
        {

        }
    }
}
