using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sensiks.Styling
{
    public class TextElement : MonoBehaviour
    {

        #region FIELDS

        [SerializeField]
        private string style;

        #endregion

        #region METHODS

        public void Start()
        {
            string text = StyleManager.Instance.Style.GetStyleElement<string>(this.style);
            if (this.TryGetComponent<Text>(out Text textElement))
            {
                textElement.text = text;
            }
        }

        #endregion

    }
}
