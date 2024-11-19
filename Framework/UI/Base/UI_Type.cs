using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    ///  TOOD: 구현 필요
    /// </summary>
    public class UI_Type
    {
        public string Path { get; private set; }
        public string Name { get; private set; }

        public UI_Type(string path)
        {
            Path = path;
            Name = path.Substring(path.LastIndexOf('/') + 1);
        }
        
        public override string ToString()
        {
            return string.Format("path : {0} name : {1}", Path, Name);
        }
        
        public static readonly UI_Type MainMenu = new UI_Type("View/MainMenuView");
        public static readonly UI_Type OptionMenu = new UI_Type("View/OptionMenuView");
        public static readonly UI_Type NextMenu = new UI_Type("View/NextMenuView");
        public static readonly UI_Type HighScore = new UI_Type("View/HighScoreView");
    }
}
