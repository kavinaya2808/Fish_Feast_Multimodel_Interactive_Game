using UnityEngine;
using System.Collections;


public class BigFishController : MonoBehaviour
{
    public Sprite closedMouthSprite;  // Set in Inspector
    public Sprite openMouthSprite; 

    public float moveSpeed = 10f;  
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isBiting = false;
    public int fishEatenCount = 0;
    private Coroutine biteResetCoroutine;
    public bool isProtected = false;
    public float protectionDuration = 5f;
    public float protectionTimer = 0f;
    public GameObject shieldObject;

    private float minY = -2f;
    private float maxY = 3.5f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        targetPosition = transform.position;  
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedMouthSprite;
    }
    public void Block()
    {
        isProtected = true;
        protectionTimer = protectionDuration;

        if (shieldObject != null)
            shieldObject.SetActive(true);

        Debug.Log("🛡️ Big Fish is now protected!");
    }

    private void Update()
    {
        
        rb.position = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        if (isProtected)
        {
            protectionTimer -= Time.deltaTime;
            if (protectionTimer <= 0f)
            {
                isProtected = false;
                if (shieldObject != null)
                    shieldObject.SetActive(false);
                Debug.Log("🛡️ Protection expired");
            }
        }
    }

    public void Bite()
    {
         Debug.Log("Big Fish Biting");

        if (biteResetCoroutine != null)
            StopCoroutine(biteResetCoroutine);

        spriteRenderer.sprite = openMouthSprite;
        isBiting = true;

        biteResetCoroutine = StartCoroutine(ResetBitingAfterDelay(1.3f));
    }

    private IEnumerator ResetBitingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBiting = false;
        spriteRenderer.sprite = closedMouthSprite;
        Debug.Log("isBiting set to false after delay");
    }


    public void MoveUp()
    {
        Debug.Log("Big Fish: Move Up");
        float newY = Mathf.Clamp(transform.position.y + 2f, minY, maxY);
        targetPosition = new Vector2(transform.position.x, newY);
    }

    public void MoveDown()
    {
        Debug.Log("Big Fish: Move Down");
        float newY = Mathf.Clamp(transform.position.y - 2f, minY, maxY);
        targetPosition = new Vector2(transform.position.x, newY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            if (isProtected)
            {
                Debug.Log("💣 Bomb hit but Big Fish was protected!");
                
            }
            else
            {
                Debug.Log("💥 Bomb hit! Subtracting 2 fish count.");
                if (fishEatenCount < 0){
                    fishEatenCount = 0;
                } 
                else{
                    fishEatenCount -= 2;
                }

                // Trigger the bomb’s explode logic
                Bomb bomb = other.GetComponent<Bomb>();
                if (bomb != null)
                {
                    bomb.Explode();
                }
            }


        }

        if (isBiting) 
        {
            string smallFishName = other.gameObject.name.ToLower();
            isBiting = false;

            if (smallFishName.Contains("blue_fish"))
            {
                Debug.Log("Big Fish ate a Blue Fish!");
            }
            else if (smallFishName.Contains("pink_fish"))
            {
                Debug.Log("Big Fish ate a Pink Fish!");
            }
            else if (smallFishName.Contains("green_fish"))
            {
                Debug.Log("Big Fish ate a Green Fish!");
            }
            else if (smallFishName.Contains("yellow_fish"))
            {
                Debug.Log("Big Fish ate a Yellow Fish!");
            }
            else if (smallFishName.Contains("red_fish"))
            {
                Debug.Log("Big Fish ate a Red Fish!");
            }
            else
            {
                Debug.Log("Big Fish ate an unknown fish type!");
            }

            if (other.gameObject.CompareTag("Bomb")){
                Debug.Log("💣 Bomb hit but Big Fish was protected!");
            }
            else{ 
                Destroy(other.gameObject);
                fishEatenCount++;
            }
            
            Debug.Log("Fish eaten count: " + fishEatenCount);

            isBiting = false; 
        }
    }
}
