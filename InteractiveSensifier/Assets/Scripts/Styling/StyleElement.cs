using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sensiks.Styling
{
    public class StyleElement : MonoBehaviour
    {

        #region FIELDS

        [SerializeField]
        private string style;

        #endregion

        #region METHODS

        public void Start()
        {
            Color color = StyleManager.Instance.Style.GetStyleElement<Color>(this.style);
            if (this.TryGetComponent<Graphic>(out Graphic graphic))
            {
                graphic.color = color;
            }
            else if (this.TryGetComponent<Material>(out Material material))
            {
                material.color = color;
            }
        }

        #endregion

    }
}
