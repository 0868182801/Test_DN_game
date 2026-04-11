using UnityEngine;
using System.Collections; 

public class PlayerCollision : MonoBehaviour    // Xử lý va chạm
{
    private GameManager gameManager;
    private AudioManager audioManager;
    private PlayerController playerController;
    private int trapHitCount = 0;                // Đếm số lần va chạm với bẫy
    private int killHitCount = 0;           // Đếm số lần va chạm với item bình thuốc
    [SerializeField] private GameObject[] hearts;  // Mảng chứa các hình ảnh trái tim đại diện cho mạng sống của player

    private void Awake() 
    {
        gameManager = FindAnyObjectByType<GameManager>();       // Sử dụng để gọi các hàm trong GameManager 
        audioManager = FindAnyObjectByType<AudioManager>(); 
        playerController = GetComponent<PlayerController>();    // Tham chiếu đến PlayerController để kích hoạt animation khi va chạm
    }
    private void OnTriggerEnter2D(Collider2D collision)     // Ktra va chạm khi player chạm vào collider coin (có tích isTrigger)
    {
        /* if(collision.CompareTag("Coin")){   
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            gameManager.AddScore(1);        // Số lượng điểm (points) tăng lên khi chạm 1 đồng coin
            Debug.Log("Hit Coin");
        } */
        if(collision.CompareTag("Coin") || collision.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            if(collision.CompareTag("Coin")==true) gameManager.AddScore(1);   // Số lượng điểm (points) tăng lên khi chạm 1 đồng coin
            else gameManager.AddScore(10);
            Debug.Log("Hit Coin");      
        }
        else if (collision.CompareTag("Trap")||collision.CompareTag("Enemy"))
        {
            trapHitCount++;      // Tăng số lần va chạm
            Debug.Log("Hit Trap or Enemy " + trapHitCount);
            hearts[hearts.Length - trapHitCount].SetActive(false);  // Ẩn hình ảnh tim tương ứng với số lần va chạm (giảm mạng sống) 
            
            // Kích hoạt animation va chạm
            playerController.TriggerHitAnimation();

            if (trapHitCount >= 3)           // Lần thứ 3 -> Game Over
            {
                audioManager.PlayGameOverSound();
                audioManager.backgroundAudioSource.Stop();
                gameManager.GameOver();
                Debug.Log("Die");
            }
            else
            {
                // Lần đầu: phát âm thanh va chạm nhẹ
                audioManager.PlayKillSound(); 
            }
        }
        else if (collision.CompareTag("Killer"))
        {
            killHitCount++;                  // Tăng số lần va chạm
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);  // Ẩn đối tượng sau khi va chạm 
            Debug.Log("Hit Killer: " + killHitCount);
            playerController.TriggerHitAnimation();

            if (killHitCount >= 2)           // Lần thứ 2 -> Game Over
            {
                audioManager.PlayGameOverSound();
                audioManager.backgroundAudioSource.Stop();
                gameManager.GameOver();
                Debug.Log("Die");
            }
            else
            {
                // Lần đầu: phát âm thanh va chạm nhẹ
                audioManager.PlayKillSound(); // Bạn cần thêm clip này trong AudioManager
            }
        }
        else if (collision.CompareTag("Max"))
        {
            hearts[2].SetActive(false);     // Ẩn hình ảnh tim sau khi rơi
            hearts[1].SetActive(false);
            hearts[0].SetActive(false);
            audioManager.PlayGameOverSound();
            audioManager.backgroundAudioSource.Stop();
            gameManager.GameOver();
            Debug.Log("Die");
        }
        else if(collision.CompareTag("Key"))   
        {
            audioManager.backgroundAudioSource.Stop();
            audioManager.PlayKeySound();
            Destroy(collision.gameObject);
            gameManager.GameWin();
            Debug.Log("You Win");
        }       
    }
}
