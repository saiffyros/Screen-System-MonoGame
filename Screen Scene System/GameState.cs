using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Screen_Scene_System
{
    public abstract partial class GameState : DrawableGameComponent
    {
        protected GameState _tag;
        protected readonly IStateManager _manager;
        protected ContentManager _content;
        protected readonly List<GameComponent> _childComponents;

        public List<GameComponent> Components
        {
            get { return _childComponents; }
        }

        public GameState Tag
        {
            get { return _tag; }
        }


        public GameState(Game game) : base(game)
        {
            _tag = this;

            _childComponents = new List<GameComponent>();
            _content = Game.Content;

            _manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in _childComponents)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (GameComponent component in _childComponents)
            {
                if (component is DrawableGameComponent component1 && component1.Visible)
                {
                    component1.Draw(gameTime);
                }
            }
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (_manager.CurrentState == _tag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent component1)
                {
                    component1.Visible = true;
                }
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent component1)
                {
                    component1.Visible = false;
                }
            }
        }
    }
}