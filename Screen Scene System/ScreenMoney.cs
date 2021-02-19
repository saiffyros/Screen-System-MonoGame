using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Screen_Scene_System
{
    public class ScreenMoney : BaseGameState
    {
        public ScreenMoney(Game game) : base(game)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (GameRef._money < 100)
            {
                GameRef.screenMoney.Show();
            }
            else
            {
                GameRef.screenMoney.Hide();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            //GameRef.SpriteBatch.DrawString(GameRef.font, GameRef._money.ToString(), new Vector2(200, 200), Color.Black);
            GameRef.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}