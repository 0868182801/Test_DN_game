using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
  public Button continueButton;     // Ẩn nút Continue nếu chưa có màn chơi đã lưu

  void Start()
  {
    if(!PlayerPrefs.HasKey("ContinueScene"))
    {
      continueButton.interactable = false;
    }
  }
  public void PlayGame()
  {
    SceneManager.LoadScene("Game1");     
  }

  public void QuitGame()
  {
    Application.Quit();     
  }
  
  public void ContinueGame()
  {
    if(PlayerPrefs.HasKey("ContinueScene"))
    {
        string scene = PlayerPrefs.GetString("ContinueScene");
        SceneManager.LoadScene(scene);
    }
    else
    {
        Debug.Log("No save data!");
    }
  }
}