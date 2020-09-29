using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace Sensiks.Styling
{
    public class StyleManager
    {
        #region CONSTANT_EXPERESSIONS
        
        //Files.
        public const string StyleDefaultPath = "/Config/Style.json";

        #endregion

        #region FIELDS

        //Singleton.
        private static Lazy<StyleManager> instance = new Lazy<StyleManager>(() => new StyleManager());

        //Style.
        private StyleInfo style = new StyleInfo();

        #endregion

        #region PROPERTIES

        //Singleton.
        public static StyleManager Instance
        {
            get => StyleManager.instance.Value;
        }

        //Files.
        public string AbsoluteStyleDefaultPath
        {
            get => $"{Application.streamingAssetsPath}{StyleManager.StyleDefaultPath}";
        }

        //Style.
        public StyleInfo Style
        {
            get => this.style;
            private set => this.style = value;
        }

        #endregion

        #region CONSTRUCTORS

        private StyleManager()
        {
            this.LoadStyle(this.AbsoluteStyleDefaultPath);
        }

        #endregion

        #region METHODS

        public void LoadStyle(string path)
        {
            try
            {
                this.style = new StyleInfo(JsonMapper.ToObject(File.ReadAllText(path)));
            }
            catch(System.Exception e)
            {
                Debug.LogError($"Could not load style: {e.Message}");
            }
        }

        #endregion

    }
}
