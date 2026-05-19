using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class FishEatenUIImage : MonoBehaviour
{
    public BigFishController bigFish;
    public Sprite[] numberSprites; // Array of 0-9 number sprites
    public List<Image> digitImages = new List<Image>(); // Assign UI Image components

    void Update()
    {
        UpdateFishEatenDisplay(bigFish.fishEatenCount);
    }

    void UpdateFishEatenDisplay(int count)
    {
        if (count < 0) count = 0;
        string countStr = count.ToString();

        // Ensure enough UI image components
        while (digitImages.Count < countStr.Length)
        {
            GameObject newDigit = new GameObject("Digit", typeof(Image));
            newDigit.transform.SetParent(transform);
            Image img = newDigit.GetComponent<Image>();
            digitImages.Add(img);
        }

        for (int i = 0; i < digitImages.Count; i++)
        {
            if (i < countStr.Length)
            {
                int digit = int.Parse(countStr[i].ToString());
                digitImages[i].sprite = numberSprites[digit];
                digitImages[i].enabled = true;
            }
            else
            {
                digitImages[i].enabled = false; // Hide unused digits
            }
        }
    }
}
