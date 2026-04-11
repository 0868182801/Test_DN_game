using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameManager gameManager;
    public string namemanchoi;     
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();   
    }
    public void LoadManChoiMoi()
    {
        // LƯU MÀN CHƠI HIỆN TẠI
        string nameScene = SceneManager.GetActiveScene().name;   
        PlayerPrefs.SetString("ContinueScene", namemanchoi);        // Màn chơi tiếp theo 
        PlayerPrefs.Save();

        if(nameScene == "Game4")     // Chỉ hiện game win khi hoàn thành màn chơi 4
        {
            gameManager.GameWin();
        }
        else 
        {
            SceneManager.LoadScene(namemanchoi);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            LoadManChoiMoi();
        }
    }
}
