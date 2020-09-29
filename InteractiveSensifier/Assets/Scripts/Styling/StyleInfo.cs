using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace Sensiks.Styling
{
    public class StyleInfo
    {

        #region FIELDS

        private JsonData styles;

        #endregion

        #region CONSTRUCTORS

        public StyleInfo()
        {
            this.styles = new JsonData();
        }

        public StyleInfo(JsonData jsonData)
        {
            this.styles = jsonData;
        }

        #endregion

        #region METHODS

        public bool Contains(string key)
        {
            JsonData jsonData = this.styles;
            foreach (string str in key.Split(' '))
            {
                if (jsonData.Keys.Contains(str))
                {
                    jsonData = jsonData[str];
                }
                else
                {
                    Debug.LogError($"Could not find style key: {key}");
                    return false;
                }
            }
            return true;
        }

        public T GetStyleElement<T>(string key)
        {
            //Get json data at key.
            JsonData jsonData = this.styles;
            string value = "";
            foreach (string str in key.Split(' '))
            {
                if (jsonData.Keys.Contains(str))
                {
                    jsonData = jsonData[str];
                }
                else
                {
                    Debug.LogError($"Could not find style key: {key}");
                    return default(T);
                }
            }
            value = jsonData.ToString();

            Type type = typeof(T);

            //Parse and return correct type.
            if (type == typeof(Color))
            {
                Color val = new Color();
                if (ColorUtility.TryParseHtmlString(value, out val))
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
            }
            else if (type == typeof(float))
            {
                float val = 0.0f;
                if (float.TryParse(value, out val))
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
            }
            else if (type == typeof(int))
            {
                int val = 0;
                if (int.TryParse(value, out val))
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
            }
            else if (type == typeof(bool))
            {
                bool val = false;
                if (bool.TryParse(value, out val))
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
            }
            else if (type == typeof(string))
            {
                return (T)Convert.ChangeType(value.ToString(), typeof(T));
            }

            Debug.LogError($"Could not convert style element to type: {typeof(T)}");
            return default(T);
        }

        #endregion
    }
}