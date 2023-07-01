using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public static class ImageExtensions
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            Color newColor = image.color;
            newColor.SetAlpha(alpha);

            image.color = newColor;
        }
    }
}