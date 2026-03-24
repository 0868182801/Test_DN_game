using UnityEngine;
using TMPro;    // Thư viện để use canvas points
using UnityEngine.SceneManagement;  // màn chơi
public class GameManager : MonoBehaviour
{
    private int highScore = 0;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject gameOverUi;     // Panel game over
    [SerializeField] private GameObject gameWinUi;      
    private bool isGameOver = false;   // Kiểm tra game thua chưa
    private bool isGameWin = false;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();  // Khi game bắt đầu cập nhật lại điểm
        gameOverUi.SetActive(false); // Ẩn panel gameover
        gameWinUi.SetActive(true);     
        highScore = GetScoreData();
        highScoreText.text = highScore.ToString(); // Hiển thị điểm cao khi bắt đầu
    }
    
    public void AddScore(int points)
    {
        if(!isGameOver && !isGameWin)  // Kiểm tra nếu game chưa thua hoặc chưa win game tức đang chơi 
        {
            score += points;
            UpdateScore();
        }
    }

    private void UpdateScore()  
    {
        scoreText.text = score.ToString();  // score là kiểu nguyên mà scoreText kiểu chuỗi lên phải ép kiểu để gán
    }

    private void SaveScoreData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string highScoreKey = "HighScore_" + sceneName; // Khóa duy nhất cho mỗi màn
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
            Debug.Log("Save TOP SCORE");
        }
    }

    public int GetScoreData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string highScoreKey = "HighScore_" + sceneName; // Khóa duy nhất cho mỗi màn
        return PlayerPrefs.GetInt(highScoreKey, 0); // Mặc định là 0 nếu chưa có điểm cao
    }

    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;             // K thể ấn phím
        gameOverUi.SetActive(true);     // Hiển thị panel gameover 
        gameWinUi.SetActive(false);
        SaveScoreData();
    }

    public void GameWin()
    {
        isGameWin = true;
        //gameWinUi.SetActive(true); // Hiển thị giao diện thắng khi người chơi thắng
        Time.timeScale = 1; // Tạm dừng trò chơi khi thắng
        SaveScoreData();
    }

    public void RestartGame()
    {
        isGameOver = false;
        isGameWin = false; // Đặt lại trạng thái thắng khi khởi động lại
        score = 0;
        UpdateScore();
        Time.timeScale = 1;             //Nhấn phím bthg
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại cảnh hiện tại    
    }

    public void GotoMenu()
    {
        Time.timeScale = 1; // Khôi phục thời gian thực trước khi tải cảnh, tránh lỗi khi cảnh mới chạy
        SceneManager.LoadScene("Menu"); // Tải cảnh menu
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    
     public bool IsGameWin()
    {
        return isGameWin;
    }

    public void timeOver()
    {
        
    }
}
