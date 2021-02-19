using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Screen_Scene_System
{
    public class GameScene
    {
        #region Field Region
        protected Game _game;
        protected string _textureName;
        protected SpriteFont _font;
        protected string _text;
        private List<SceneOption> _options;
        private int _selectedIndex;
        private Color _highLight;
        private Color _normal;
        private Vector2 _textPosition;
        private static Texture2D _selected;
        private bool _isMouseOver;

        private Vector2 _menuPosition = new Vector2(50, 475);

        #endregion

        #region Property Region

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public static Texture2D Selected
        {
            get { return _selected; }
        }

        public List<SceneOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        [ContentSerializerIgnore]
        public SceneAction OptionAction
        {
            get { return _options[_selectedIndex].OptionAction; }
        }

        public string OptionScene
        {
            get { return _options[_selectedIndex].OptionScene; }
        }

        public string OptionText
        {
            get { return _options[_selectedIndex].OptionText; }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
        }

        public bool IsMouseOver
        {
            get { return _isMouseOver; }
        }

        [ContentSerializerIgnore]
        public Color NormalColor
        {
            get { return _normal; }
            set { _normal = value; }
        }

        [ContentSerializerIgnore]
        public Color HighLightColor
        {
            get { return _highLight; }
            set { _highLight = value; }
        }

        public Vector2 MenuPosition
        {
            get { return _menuPosition; }
        }

        #endregion

        #region Constructor Region

  
        private GameScene()
        {
            NormalColor = Color.Blue;
            HighLightColor = Color.Red;
        }

        public GameScene(string text, List<SceneOption> options, string textureName = "basic_scene")
        {
            this._text = text;
            this._options = options;
            this._textureName = textureName;
            _textPosition = Vector2.Zero;
        }

        public GameScene(Game game, string text, string[,] options, SceneAction[] actions, string textureName = "basic_scene")
        {
            this._game = game;
            this._textureName = textureName;

            _textPosition = new Vector2(40, 40);

            LoadContent(textureName);

            this._options = new List<SceneOption>();
            this._highLight = Color.Red;
            this._normal = Color.Black;

            SetOptions(options, actions);
        }

        public GameScene(Game game, string textureName, string text, List<SceneOption> options)
        {
            this._game = game;
            this._textureName = textureName;

            LoadContent(textureName);

            this._options = new List<SceneOption>();
            this._highLight = Color.Red;
            this._normal = Color.Black;

            this._options = options;
        }

        #endregion

        #region Method Region

        public void SetText(string text, SpriteFont font)
        {
            _textPosition = new Vector2(450, 50);

            StringBuilder sb = new StringBuilder();
            float currentLength = 0f;

            if (font == null)
            {
                this._text = text;
                return;
            }

            string[] parts = text.Split(' ');

            foreach (string s in parts)
            {
                Vector2 size = font.MeasureString(s);

                if (currentLength + size.X < 800f)
                {
                    sb.Append(s);
                    sb.Append(" ");
                    currentLength += size.X + font.MeasureString(" ").X;
                }
                else
                {
                    sb.Append("\n\r");
                    sb.Append(s);
                    sb.Append(" ");
                    currentLength = size.X + font.MeasureString(" ").X;
                }
            }

            this._text = sb.ToString();
        }

        public void SetOptions(string[,] options, SceneAction[] action)
        {
            this._options.Clear();

            for (int i = 0; i < options.GetLength(0); i++)
            {
                this._options.Add(new SceneOption(options[i, 0], options[i, 1], action[i]));
            }
        }

        public void Initialize()
        {
        }

        protected void LoadContent(string textureName)
        {
            //_textureManager.AddTexture(textureName, _game.Content.Load<Texture2D>(@"Scenes\" + textureName));
            _selected = _game.Content.Load<Texture2D>("rightarrowUp");
            _font = _game.Content.Load<SpriteFont>("font2");
        }

        public void Update(GameTime gameTime, PlayerIndex index)
        {
            if (Xin.CheckButtonPress(index, Buttons.LeftThumbstickUp) || Xin.CheckKeyPress(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = _options.Count - 1;
            }
            else if (Xin.CheckButtonPress(index, Buttons.LeftThumbstickDown) || Xin.CheckKeyPress(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex > _options.Count - 1)
                    _selectedIndex = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D background, SpriteFont font, Texture2D portrait)
        {
            Vector2 selectedPosition = new Vector2();
            Rectangle portraitRect = new Rectangle(25, 25, 425, 425);
            Color myColor;

            if (_selected == null)
                _selected = _game.Content.Load<Texture2D>("rightarrowUp");

            if (_textPosition == Vector2.Zero)
                SetText(_text, font);

            if (background != null)
                spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);

            if (portrait != null)
                spriteBatch.Draw(portrait, portraitRect, Color.White);
            
            spriteBatch.DrawString(font,
                _text,
                _textPosition,
                Color.White);

            Vector2 position = _menuPosition;

            Rectangle optionRect = new Rectangle(0, (int)position.Y, 1280, font.LineSpacing);
            _isMouseOver = false;

            for (int i = 0; i < _options.Count; i++)
            {
                if (optionRect.Contains(Xin.MouseAsPoint))
                {
                    _selectedIndex = i;
                    _isMouseOver = true;
                }

                if (i == SelectedIndex)
                {
                    myColor = HighLightColor;
                    selectedPosition.X = position.X - 35;
                    selectedPosition.Y = position.Y;

                    spriteBatch.Draw(_selected, selectedPosition, Color.White);
                }
                else
                    myColor = NormalColor;

                spriteBatch.DrawString(font,
                    _options[i].OptionText,
                    position,
                    myColor);

                position.Y += font.LineSpacing + 5;
                optionRect.Y += font.LineSpacing + 5;
            }
        }

        #endregion

        public static void Load(Game GameRef)
        {
            _selected = GameRef.Content.Load<Texture2D>("rightarrowUp");
        }
    }
}
