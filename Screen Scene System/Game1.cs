using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Screen_Scene_System
{
    public class Game1 : Game
    {
        private StateManager _stateManager;
        public StateManager stateManager { get { return _stateManager; } }
        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

        public ConversationScreen conversationScreen { get; private set; }
        public ScreenMoney screenMoney { get; private set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int money = 0;
        public static SpriteFont font;
        public int _money { get { return money; } set { money = value; } }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _stateManager = new StateManager(this);
            screenMoney = new ScreenMoney(this);
            conversationScreen = new ConversationScreen(this);
            
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            conversationScreen.CreateConversation();



            font = Content.Load<SpriteFont>("font2");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _stateManager.PushState(conversationScreen);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
