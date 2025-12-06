using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AspectLockRectTransform : MonoBehaviour {
    public Image aspectLockedImage;
    private RectTransform rectTransform;

    void OnEnable() {
        Canvas.willRenderCanvases += UpdateRect;
        rectTransform = (RectTransform)transform;
    }

    void OnDisable() {
        Canvas.willRenderCanvases -= UpdateRect;
    }

    void UpdateRect() {
        if (aspectLockedImage == null)
            return;

        float originalWidth = aspectLockedImage.sprite.rect.width;
        float originalHeight = aspectLockedImage.sprite.rect.height;

        double aspect = originalWidth / originalHeight;

        float maxWidth = aspectLockedImage.rectTransform.rect.width;
        float maxHeight = aspectLockedImage.rectTransform.rect.height;

        double widthBasedOnHeight = maxHeight * aspect;

        // only scale based on height if target width is smaller than max width
        if (widthBasedOnHeight <= maxWidth) {
            // resize rect based on image aspect ratio
            ResizeRect((float)widthBasedOnHeight, maxHeight);
        }
        // otherwise, scale based on width
        else {
            double heightBasedOnWidth = maxWidth / aspect;

            // resize rect based on image aspect ratio
            ResizeRect(maxWidth, (float)heightBasedOnWidth);
        }
    }

    void ResizeRect(float width, float height) {
        rectTransform.sizeDelta = new Vector2(width, height);
    }
}