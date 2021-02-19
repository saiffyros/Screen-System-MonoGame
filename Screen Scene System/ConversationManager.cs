using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Screen_Scene_System
{
    public sealed class ConversationManager
    {
        private static ConversationManager _instance = new ConversationManager();
        private Dictionary<string, Conversation> _conversationList = new Dictionary<string, Conversation>();

        public static ConversationManager Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        public Dictionary<string, Conversation> ConversationList
        {
            get { return _conversationList; }
            set { _conversationList = value; }
        }

        private ConversationManager()
        {
        }

        public void AddConversation(string name, Conversation conversation)
        {
            if (!_conversationList.ContainsKey(name))
                _conversationList.Add(name, conversation);
        }

        public Conversation GetConversation(string name)
        {
            if (_conversationList.ContainsKey(name))
                return _conversationList[name];

            return null;
        }

        public bool ContainsConversation(string name)
        {
            return _conversationList.ContainsKey(name);
        }

        public void ClearConversations()
        {
            _conversationList.Clear();
        }
    }
}
