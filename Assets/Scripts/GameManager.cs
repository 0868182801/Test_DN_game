using UnityEngine;
using TMPro;    // Thư viện để use canvas points
using UnityEngine.SceneManagement;  // màn chơi
public class GameManager : MonoBehaviour
{
    private int highScore = 0;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject scoreTextObject;    // GameObject chứa TextMeshProUGUI để hiển thị điểm số
    [SerializeField] private GameObject highScoreTextObject;
    [SerializeField] private GameObject gameOverUi;     // Panel game over
    [SerializeField] private GameObject gameWinUi;      
    [SerializeField] private GameObject gameStart;      //Hướng dẫn bắt đầu chơi 
    [SerializeField] private GameObject threeTym;  // Hình ảnh 3 trái tim đại diện cho mạng sống của player
    [SerializeField] private GameObject menubutton;    // Nút menu  
    private bool isGameOver = false;   // Kiểm tra game thua chưa
    private bool isGameWin = false;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame(); 
        highScore = GetScoreData();
        highScoreText.text = "Score:" + Mathf.FloorToInt(highScore);
    }
    
    void Update()
    {
        if(isGameOver || isGameWin) return; 
        HandleStartGameInput();
        UpdateScore();  // Khi game bắt đầu cập nhật lại điểm
    }

    public void AddScore(int points)
    {
        if(!isGameOver && !isGameWin)  // Kiểm tra nếu game chưa thua hoặc chưa win game tức đang chơi 
        {
            score += points;
            UpdateScore();
        }
    }

    private void StartGame()
    {
        Time.timeScale = 0;        
        gameOverUi.SetActive(false); // Ẩn panel gameover
        gameWinUi.SetActive(false);  // Ẩn panel win
        gameStart.SetActive(true);   // Hiển thị hướng dẫn bắt đầu
        scoreTextObject.SetActive(false); // Tắt hiển thị điểm số
        highScoreTextObject.SetActive(false); // Tắt hiển thị điểm cao
        threeTym.SetActive(false); // Ẩn hình ảnh 3 trái tim
        menubutton.SetActive(false); // Ẩn nút menu chưa bắt đầu trò chơi
    }

    private void HandleStartGameInput()     //Kiểm tra nếu người chơi đã nhấn phím thì bắt đầu trò chơi
    {
        if(Input.GetKeyDown(KeyCode.Space)) // Nhấn phím Space để bắt đầu chơi
        {
            Time.timeScale = 1; 
            gameStart.SetActive(false); 
            threeTym.SetActive(true);
            scoreTextObject.SetActive(true); 
            highScoreTextObject.SetActive(true); 
            menubutton.SetActive(true); 
        }
    }

    private void UpdateScore()  
    {
        //scoreText.text = "Score:" + Mathf.FloorToInt(score);
        scoreText.text = score.ToString();  // score là kiểu nguyên mà scoreText kiểu chuỗi lên phải ép kiểu để gán
    }

    private void SaveScoreData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string highScoreKey = "HighScore_" + sceneName; // Khóa duy nhất cho mỗi màn
        if (score > highScore)
        {
            highScore = score;
            //highScoreText.text = highScore.ToString();
            highScoreText.text = "Score:" + Mathf.FloorToInt(highScore);
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
            Debug.Log("SAVE TOP SCORE");
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
        //gameWinUi.SetActive(false);

        // Lưu màn chơi hiện tại khi thua để có thể tiếp tục sau này
        string nameScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("ContinueScene", nameScene);
        PlayerPrefs.Save();
        SaveScoreData();
    }

    public void GameWin()
    {
        isGameWin = true;
        string currentScene = SceneManager.GetActiveScene().name;   // Lấy tên của cảnh hiện tại để kiểm tra
        if(currentScene == "Game5") // Chỉ màn cuối mới hiện Win
        {
            gameWinUi.SetActive(true);
            Time.timeScale = 0; // Dừng game khi thắng
        }
        else
        {
            // Nếu chưa phải màn cuối thì chuyển sang màn tiếp theo
            Time.timeScale = 1;
        }
        SaveScoreData();
    }

    public void RestartGame()
    {
        isGameOver = false;
        isGameWin = false; // Đặt lại trạng thái thắng khi khởi động lại
        score = 0;
        UpdateScore();
        Time.timeScale = 1;   //Nhấn phím bthg
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