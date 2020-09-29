using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sensiks.Styling
{
    public class MultiStyleElement : MonoBehaviour
    {
#region FIELDS
        [SerializeField]
        private string[] styles;

        private Color[] colors;
#endregion

#region METHODS
        public void Start()
        {
            this.colors = new Color[this.styles.Length];

            for (int i = 0; i < styles.Length; i++)
            {
                this.colors[i] = StyleManager.Instance.Style.GetStyleElement<Color>(this.styles[i]);
            }

            this.SetColor(0);
        }

        public void SetColor(int colorIndex)
        {
            if (colorIndex >= 0 && colorIndex < this.colors.Length)
            {
                if (this.TryGetComponent<Graphic>(out Graphic graphic))
                {
                    graphic.color = this.colors[colorIndex];
                }
                else if (this.TryGetComponent<Material>(out Material material))
                {
                    material.color = this.colors[colorIndex];
                }
            }
            else
            {
                UnityEngine.Debug.Log($"[Styling] - Color index out of bounds!");
            }
        }
#endregion
    }
}
