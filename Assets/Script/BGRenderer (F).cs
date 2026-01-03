using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitBackgroundToCamera : MonoBehaviour
{
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var sprite = spriteRenderer.sprite;
        if (sprite == null) return;

        var spriteWidth = sprite.bounds.size.x;
        var spriteHeight = sprite.bounds.size.y;

        var cam = Camera.main;
        float worldScreenHeight = cam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * cam.aspect;

        transform.localScale = new Vector3(
            worldScreenWidth / spriteWidth,
            worldScreenHeight / spriteHeight,
            1f
        );
    }
}
