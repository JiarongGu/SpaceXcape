using UnityEngine;

public class Blink : MonoBehaviour
{
    public float interval = 1f;
    public float startDelay = 0f;

    public bool defaultState = false;
    
    private SpriteRenderer spriteRenderer;
    private float alpha = 1f;
    private float invert = 0.1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (defaultState)
            StartBlinking();
    }

    public void StartBlinking(float duration) {
        StartBlinking();
        Invoke(nameof(StopBlinking), duration);
    }

    public void StartBlinking() {
        InvokeRepeating(nameof(Blinking), startDelay, interval / 20);
    }

    public void StopBlinking() {
        spriteRenderer.color = Color.white;
        CancelInvoke(nameof(Blinking));
    }
    
    public void Blinking()
    {
        if (alpha < 0.2 || alpha > 0.9)
            invert = invert * -1;

        alpha = alpha + invert;

        spriteRenderer.color = new Color(1, 1, 1, alpha);
    }
}
