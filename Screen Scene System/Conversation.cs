using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Screen_Scene_System
{
    public class Conversation
    {
        private string _name;
        private bool _enabled = true;
        private string _firstScene;
        private string _currentScene;
        private Dictionary<string, GameScene> _scenes;
        private Dictionary<GameEventType, GameEvent> _events = new Dictionary<GameEventType, GameEvent>();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = true; }
        }

        public string FirstScene
        {
            get { return _firstScene; }
            set { _firstScene = value; }
        }

        public GameScene CurrentScene
        {
            get { return _scenes[_currentScene]; }
        }

        public Dictionary<string, GameScene> GameScenes
        {
            get { return _scenes; }
            set { _scenes = value; }
        }

        [ContentSerializer(Optional=true)]
        public Dictionary<GameEventType, GameEvent> Events
        {
            get { return _events; }
            private set { _events = value; }
        }

        private Conversation()
        {
        }

        public Conversation(string name, string firstScene)
        {
            this._scenes = new Dictionary<string, GameScene>();
            this._name = name;
            this._firstScene = this._currentScene = firstScene;
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime, PlayerIndex.One);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D background, SpriteFont font, Texture2D portrait)
        {
            CurrentScene.Draw(gameTime, spriteBatch, background, font, portrait);
        }

        public void AddScene(string sceneName, GameScene scene)
        {
            if (!_scenes.ContainsKey(sceneName))
                _scenes.Add(sceneName, scene);
        }

        public GameScene GetScene(string sceneName)
        {
            if (_scenes.ContainsKey(sceneName))
                return _scenes[sceneName];

            return null;
        }

        public void StartConversation()
        {
            _currentScene = _firstScene;
        }

        public void ChangeScene(string sceneName)
        {
            _currentScene = sceneName;
        }
    }
}
