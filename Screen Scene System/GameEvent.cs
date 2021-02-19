using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Screen_Scene_System
{
    public enum GameEventType { Conversation, Quest, Combat, Search, Party }
    public enum GameEventTimeType { Pre, Post, During }

    public class SyntaxException : Exception
    {
    }

    public class GameEvent
    {
        private GameEventTimeType _timeType;
        private string _script;
        private int _xCoord;
        private int _yCoord;
        private bool _success;
        private bool _enabled;

        public GameEventTimeType EventTimeType
        {
            get { return _timeType; }
            private set { _timeType = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        public string Script
        {
            get { return _script; }
            set { _script = value; }
        }

        public int XCoord
        {
            get { return _xCoord; }
            set { _xCoord = value; }
        }

        public int YCoord
        {
            get { return _yCoord; }
            set { _yCoord = value; }
        }

        private GameEvent()
        {
        }

        public GameEvent(GameEventTimeType timeType, string script)
        {
            EventTimeType = timeType;
            Script = script;
        }

        public object RunScript(object world, params string[] parameters)
        {
            ConversationManager conversations = ConversationManager.Instance;

            object returnValue = null;

            string[] tokens = _script.Split(new char[] { ' ', '\n', '\r', '\t' } );
            bool running = true;
            int lookAhead = 0;
            string currentToken = tokens[0];
            string nextToken;
            bool evaluation = false;

            for (int i = 0; running && i < tokens.Length; i += lookAhead)
            {
                string text;
                string symbol;
                string compValue;

                lookAhead = 0;

                switch (currentToken.ToLower().Trim())
                {
                    case "end" :
                        running = false;
                        break;
                    case "endif" :
                        lookAhead++;
                        if (i + lookAhead < tokens.Length)
                            currentToken = tokens[i + lookAhead].ToLower().Trim();
                        break;
                    case "elseif" :
                        if (evaluation == true)
                        {
                            running = false;
                            continue;
                        }
                        break;
                    case "if" :
                        lookAhead++;
                        nextToken = tokens[i + lookAhead];

                        lookAhead++;
                        symbol = tokens[i + lookAhead];

                        lookAhead++;
                        compValue = tokens[i + lookAhead];

                        switch (nextToken)
                        {
                            case "conversation" :
                                if (symbol == "==")
                                {
                                    evaluation = compValue == parameters[0];
                                }
                                else if (symbol == "!=")
                                {
                                    evaluation = compValue != parameters[0];
                                }
                                else
                                    throw new SyntaxException();

                                if (evaluation)
                                {
                                    lookAhead++;
                                    currentToken = tokens[i + lookAhead].ToLower().Trim();
                                    continue;
                                }
                                else
                                {
                                    if (tokens[i + lookAhead + 1].ToLower().Trim() != "endif" && tokens[i + lookAhead + 1] != "elseif")
                                    {
                                        throw new SyntaxException();
                                    }
                                    else
                                    {
                                        lookAhead++;
                                        currentToken = tokens[i + lookAhead].ToLower().Trim();
                                    }
                                }
                                break;
                        }
                        break;
                    case "enable" :
                        lookAhead++;
                        nextToken = tokens[i + lookAhead];

                        switch (nextToken.ToLower().Trim())
                        {
                            case "portal" :
                                lookAhead++;

                                text = tokens[i + lookAhead];

                                lookAhead++;

                                if (lookAhead + i < tokens.Length)
                                    currentToken = tokens[lookAhead + i];
                                else
                                    running = false;
                                break;
                            case "conversation" :
                                break;
                            case "quest" :
                                break;
                            case "event" :
                                break;
                        }
                        break;
                    case "disable":
                        lookAhead++;
                        nextToken = tokens[i + lookAhead];

                        switch (nextToken.ToLower().Trim())
                        {
                            case "portal":
                                lookAhead++;

                                text = tokens[i + lookAhead];

                                lookAhead++;

                                if (lookAhead + i < tokens.Length)
                                    currentToken = tokens[lookAhead + i];
                                else
                                    running = false;
                                break;
                            case "conversation":
                                break;
                            case "quest":
                                break;
                            case "event":
                                break;
                        }
                        break;
                    case "join":
                        lookAhead++;
                        nextToken = tokens[i + lookAhead];
                        break;
                }
            }

            return returnValue;
        }
    }
}
