using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Screen_Scene_System
{
    public class ConversationScreen : BaseGameState
    {
        private ConversationManager conversations = ConversationManager.Instance;
        private Conversation conversation;
        private SpriteFont font;
        private Player player;
        private NonPlayerCharacter npc;
        private Texture2D background;

        private int num = 0;

        public ConversationScreen(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            font = GameRef.Content.Load<SpriteFont>("scenefont");
            base.Initialize();
        }

        protected override void LoadContent()
        {

            font = GameRef.Content.Load<SpriteFont>("scenefont");
            background = GameRef.Content.Load<Texture2D>(@"/GameScenes/basic_scene");

            //CreateConversation();

            num = conversation.GameScenes.Count;

            SetConversation(null, null, "welcome");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            conversation.Update(gameTime);

            if (Xin.CheckKeyRelease(Keys.Enter) || Xin.CheckKeyRelease(Keys.Space))
            {
                SceneAction action = conversation.CurrentScene.OptionAction;

                switch (action.Action)
                {
                    case ActionType.Talk :
                        conversation.ChangeScene(conversation.CurrentScene.OptionScene);
                        break;
                    case ActionType.Quest :
                        conversation.ChangeScene(conversation.CurrentScene.OptionScene);
                        break;
                    case ActionType.Change :
                        conversation = conversations.GetConversation(conversation.CurrentScene.OptionScene);
                        conversation.StartConversation();
                        break;
                    case ActionType.End :
                        GameRef.stateManager.PopState();
                        break;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameRef.SpriteBatch.Begin();

            //GameRef.SpriteBatch.DrawString(Game1.font, num.ToString() + " game scenes in Conversation", new Vector2(200, 200), Color.Black);
            
            conversation.Draw(gameTime, GameRef.SpriteBatch, background, Game1.font, null);

            GameRef.SpriteBatch.End();
        }

        public void SetConversation(Player player, NonPlayerCharacter npc, string conversation)
        {
            this.player = player;
            this.npc = npc;
            this.conversation = conversations.GetConversation(conversation);
        }

        public void StartConversation()
        {
            conversation.StartConversation();
        }

        public void CreateConversation()
        {
            Conversation conversation = new Conversation("eliza1", "welcome");
            GameScene scene = new GameScene(
            GameRef,
            "basic_scene",
            "The unthinkable has happened. A thief has stolen the eyes of the village guardian." +
            " With out his eyes the dragon will not animated if the village is attacked.",
            new List<SceneOption>());

            SceneAction action = new SceneAction
            {
                Action = ActionType.Talk,
                Parameter = "none"
            };

            SceneOption option = new SceneOption("Continue", "welcome2", action);
            scene.Options.Add(option);
            conversation.AddScene("welcome", scene);

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Will you retrieve the eyes of the dragon for us?",
                new List<SceneOption>());

            action = new SceneAction
            {
                Action = ActionType.Talk,
                Parameter = "none"
            };

            option = new SceneOption("Yes", "eliza2", action);
            scene.Options.Add(option);

            action = new SceneAction
            {
                Action = ActionType.Talk,
                Parameter = "none"
            };

            option = new SceneOption("No", "pleasehelp", action);
            scene.Options.Add(option);

            conversation.AddScene("welcome2", scene);

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Please, you are the only one that can help us. If you change your mind " +
                "come back and see me.",
                new List<SceneOption>());

            action = new SceneAction
            {
                Action = ActionType.End,
                Parameter = "none"
            };

            option = new SceneOption("Bye", "welcome2", action);
            scene.Options.Add(option);

            conversation.AddScene("pleasehelp", scene);

            ConversationManager.Instance.AddConversation("eliza1", conversation);

            conversation = new Conversation("eliza2", "thankyou");

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Thank you for agreeing to help us! Please find Faulke in the inn and ask " +
                "him what he knows about this thief.",
                new List<SceneOption>());

            action = new SceneAction
            {
                Action = ActionType.Quest,
                Parameter = "Faulke"
            };

            option = new SceneOption("Continue", "thankyou2", action);
            scene.Options.Add(option);

            conversation.AddScene("thankyou", scene);

            scene = new GameScene(
                GameRef,
                "basic_scene",
                "Return to me once you've spoken with Faulke.",
                new List<SceneOption>());
            action = new SceneAction
            {
                Action = ActionType.End,
                Parameter = "none"
            };

            option = new SceneOption("Good Bye", "thankyou2", action);
            scene.Options.Add(option);

            conversation.AddScene("thankyou2", scene);

            ConversationManager.Instance.AddConversation("eliza2", conversation);

            SetConversation(null, null, "eliza1");
        }
    }

    public class NonPlayerCharacter
    {
    }

    public class Player
    {
    }
}
