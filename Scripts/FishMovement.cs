using UnityEngine;
public class FishMovement : MonoBehaviour
{
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
}
