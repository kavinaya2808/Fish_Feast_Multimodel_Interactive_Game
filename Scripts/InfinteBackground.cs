using UnityEngine;

public class InfiniteBackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public Transform[] backgrounds;
    public float backgroundWidth = 35f;

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // If background moves completely out of view to the left
            if (bg.position.x <= -backgroundWidth)
            {
                // Move it to the right of the other background
                float rightMostX = GetRightMostBackgroundX();
                bg.position = new Vector3(rightMostX + backgroundWidth - 0.1f, bg.position.y, bg.position.z);
            }
        }
    }

    float GetRightMostBackgroundX()
    {
        float maxX = float.MinValue;
        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x > maxX)
                maxX = bg.position.x;
        }
        return maxX;
    }
}
