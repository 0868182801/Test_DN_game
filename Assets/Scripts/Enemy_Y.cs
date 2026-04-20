using UnityEngine;

public class Enemy_Y : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;   // khoảng cách di chuyển so với vị trí khời đầu (giới hạn khoảng cách di chuyển)
    private Vector3 startPos;   // Vị trí khởi đầu của Enemy
    private bool movingUp = true;    // đang di chuyển lên trên
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;  // gán = ví trị hiện tại của Enemy
    }

    // Update is called once per frame
    void Update()
    {
        float lowerBound = startPos.y - distance;   // Di chuyển xuống dưới
        float upperBound = startPos.y + distance;  // Di chuyển lên trên
        if (movingUp)
        {
            transform.Translate(Vector2.up * speed *Time.deltaTime);
            if(transform.position.y >= upperBound)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if(transform.position.y <= lowerBound)   // Nếu vượt quá giới hạn di chuyển qua trái 
            {
                movingUp = true;                     // thì di chuyển lại qua phải
            }
        }
    }
}
