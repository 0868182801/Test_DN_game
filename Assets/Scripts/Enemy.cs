using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;   // khoảng cách di chuyển so với vị trí khời đầu (giới hạn khoảng cách di chuyển)
    private Vector3 startPos;   // Vị trí khởi đầu của Enemy
    private bool movingRight = true;    // đang di chuyển sang bên phải
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;  // gán = ví trị hiện tại của Enemy
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance;   // Di chuyển qua trái
        float rightBound = startPos.x + distance;  // Di chuyển qua phải
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed *Time.deltaTime);
            if(transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if(transform.position.x <= leftBound)   // Nếu vượt quá giới hạn di chuyển qua trái 
            {
                movingRight = true;                 // thì di chuyển lại qua phải
                Flip();
            }
        }
    }

    void Flip()     // Lập ảnh nhân vật khi di chuyển qua trái và phải 
    {
        Vector3 scaler = transform.localScale;  // Biến lấy giá trị tọa độ hiện tại của Enemy
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
