using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Screen_Scene_System
{
    public enum ActionType
    {
        Talk,
        End,
        Change,
        Quest,
        Buy,
        Sell,
        GiveItems,
        GiveKey,
        Join
    }

    public class SceneAction
    {
        public ActionType Action;
        public string Parameter;
    }

    public class SceneOption
    {
        private string _optionText;
        private string _optionScene;
        private SceneAction _optionAction;

        private SceneOption()
        {

        }

        public string OptionText
        {
            get { return _optionText; }
            set { _optionText = value; }
        }

        public string OptionScene
        {
            get { return _optionScene; }
            set { _optionScene = value; }
        }

        public SceneAction OptionAction
        {
            get { return _optionAction; }
            set { _optionAction = value; }
        }

        public SceneOption(string text, string scene, SceneAction action)
        {
            _optionText = text;
            _optionScene = scene;
            _optionAction = action;
        }
    }
}
