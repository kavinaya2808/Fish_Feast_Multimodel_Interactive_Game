using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Sprite blastSprite; // Assign this in Inspector from BombSpawner or prefab

    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy when off-screen
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
    
    public void Explode()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && blastSprite != null)
        {
            sr.sprite = blastSprite;
        }

        // Optional: Disable collider and schedule destroy
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        StartCoroutine(DestroyAfterDelay(0.3f));
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
