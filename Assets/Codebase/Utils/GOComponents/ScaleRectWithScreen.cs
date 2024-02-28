using UnityEngine;

namespace Assets.Codebase.Utils.GOComponents
{
    public class ScaleRectWithScreen : MonoBehaviour
    {
        private const float ASPECT_RATIO = 16f / 9f; // Соотношение сторон 16:9

        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            UpdateScale();
        }

        private void Update()
        {
            if (Screen.width != rectTransform.rect.width || Screen.height != rectTransform.rect.height)
            {
                UpdateScale();
            }
        }

        private void UpdateScale()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float screenAspectRatio = screenWidth / screenHeight;

            float scale = 1f;

            if (screenAspectRatio > ASPECT_RATIO)
            {
                // Экран шире, чем 16:9, масштабируем по ширине
                scale = ASPECT_RATIO / screenAspectRatio;
            }
            else
            {
                // Экран уже или уже менее 16:9, масштабируем по высоте
                scale = screenAspectRatio / ASPECT_RATIO;
            }

            rectTransform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
