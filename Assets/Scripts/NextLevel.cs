using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameManager gameManager;
    public string namemanchoi;
    public void LoadManChoiMoi()
    {
        if(SceneManager.GetActiveScene().name == "Game4")
        {
            gameManager.GameWin();      // Chỉ hiện game win khi win màn 4
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
