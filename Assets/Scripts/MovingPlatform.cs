using System.Diagnostics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;      // Điểm mà bệ đứng di chuyển tới và di chuyển quay lại
    [SerializeField] private Transform pointB;      // Điểm mà bệ đứng di chuyển tới và di chuyển quay lại
    [SerializeField] private float speed = 2f;      // Tốc độ di chuyển tới 2 điểm  A và B
    private Vector3 target;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {   // // Move với tốc độ speed tới điểm target đc gán = tọa độ điểm A
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)     // Khoảg cách đến target < 0.1f (K dùng = 0.0f vì nó k phù hợp vs unity)
        {
            if (target == pointA.position)
            {
                target = pointB.position;
            }
            else target = pointA.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)      // Phương thức có sẵn gọi Khi 2 collider va chạm vào nhau
    {
        if (collision.gameObject.CompareTag("Player"))          // Khi va chạm với đối tượng có tag Player
        {
            collision.transform.SetParent(transform);     // Đặt Player làm con của Platform
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))     // Khi 2 đối tượng rời khỏi nhau không còn quan hệ cha con
        {
            collision.transform.SetParent(null);         
        }     
    }
}

