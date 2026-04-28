using UnityEngine;
using TMPro;    // Thư viện để use canvas points
using UnityEngine.SceneManagement;  // màn chơi
public class GameManager : MonoBehaviour
{
    private int highScore = 0;
    private float highTime = 0;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI highTimeText;  
    [SerializeField] private GameObject scoreTextObject;    // GameObject chứa TextMeshProUGUI để hiển thị điểm số
    [SerializeField] private GameObject highScoreTextObject;
    [SerializeField] private GameObject timeTextObject;   // GameObject chứa TextMeshProUGUI để hiển thị thời gian
    [SerializeField] private GameObject highTimeTextObject; // GameObject chứa TextMesh
    [SerializeField] private GameObject gameOverUi;     // Panel game over
    [SerializeField] private GameObject gameWinUi;      
    [SerializeField] private GameObject gameStart;      //Hướng dẫn bắt đầu chơi 
    [SerializeField] private GameObject threeTym;  // Hình ảnh 3 trái tim đại diện cho mạng sống của player
    [SerializeField] private GameObject menubutton;    // Nút menu  
    private float startTime;    // Thời gian bắt đầu bộ đếm giờ
    private bool isGameOver = false;   // Kiểm tra game thua chưa
    private bool isGameWin = false;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame(); 
        highScore = GetScoreData();
        highScoreText.text = "Score:" + Mathf.FloorToInt(highScore);
        highTime = GetTimeData();
        highTimeText.text = string.Format("{0:00}:{1:00}", (int)(highTime / 60), (int)(highTime % 60));
        startTime = Time.time;  // Lưu thời gian bắt đầu bộ đếm giờ (Time.time: biến lấy thời gian khi game bắt đầu chạy)
    }
    
    void Update()
    {
        if(isGameOver || isGameWin) return; 
        HandleStartGameInput();
        UpdateScore();  // Khi game bắt đầu cập nhật lại điểm
        
        float thoiGianDaTroiQua;    
        thoiGianDaTroiQua = Time.time - startTime;  // Tính thời gian đã trôi qua kể từ khi bắt đầu bộ đếm giờ

        int phut = (int)(thoiGianDaTroiQua / 60);   
        int giay = (int)(thoiGianDaTroiQua % 60);   

        timeText.text = string.Format("{0:00}:{1:00}", phut, giay); // Cập nhật text hiển thị thời gian theo định dạng MM:SS
    }

    public void AddScore(int points) // Hàm được gọi khi va chạm với tag coin bên Script PlayerCollision
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
        timeTextObject.SetActive(false); // Ẩn text hiển thị thời gian
        highTimeTextObject.SetActive(false); // Ẩn text hiển thị thời gian cao nhất
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
            timeTextObject.SetActive(true); // Hiển thị text hiển thị thời gian
            highTimeTextObject.SetActive(true); // Hiển thị text hiển thị thời gian cao nhất
        }
    }

    private void UpdateScore()  
    {
        //scoreText.text = "Score:" + Mathf.FloorToInt(score);
        scoreText.text = score.ToString();  // score là kiểu nguyên mà scoreText kiểu chuỗi lên phải ép kiểu để gán
    }

    /* private void SaveScoreData()    // Lưu điểm cao vào PlayerPrefs khi game kết thúc
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
        string highTimeKey = "HighTime_" + sceneName; // Khóa duy nhất cho mỗi
        if ((Time.time - startTime) < highTime)
        {
            highTime = Time.time - startTime;
            highTimeText.text = string.Format("{0:00}:{1:00}", (int)(highTime / 60), (int)(highTime % 60));
            PlayerPrefs.SetFloat(highTimeKey, highTime);
            PlayerPrefs.Save();
            Debug.Log("SAVE TOP HIGH TIME");
        }
    } */
    private void SaveScoreData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string highScoreKey = "HighScore_" + sceneName;
        string highTimeKey = "HighTime_" + sceneName;

        float currentTime = Time.time - startTime;  
        int currentScore = score;

        // Lấy dữ liệu điểm và thời gian qua màn đã lưu trước đó
        int oldHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        float oldHighTime = PlayerPrefs.GetFloat(highTimeKey, -1f);

        bool shouldSave = false;    // Kiểm tra thành tích qua màn phá kỉ lục không

        // Trường hợp chưa có 1 trong 2 dữ liệu thời gian và điểm 
        if (oldHighTime < 0)
        {
            shouldSave = true;
        }
        // Ưu tiên điểm cao rồi mới đến thời gian sớm nhất
        else if (currentScore > oldHighScore)
        {
            shouldSave = true;
        }
        // Nếu điểm bằng nhau ưu tiên thời gian sớm nhất
        else if (currentScore == oldHighScore)
        {
            if (currentTime < oldHighTime)
            {
                shouldSave = true;
            }
        }

        if (shouldSave)
        {
            highScore = currentScore;
            highTime = currentTime;

            // Cập nhật UI
            highScoreText.text = "Score:" + Mathf.FloorToInt(highScore);
            highTimeText.text = string.Format("{0:00}:{1:00}", (int)(highTime / 60), (int)(highTime % 60));

            // Lưu vào PlayerPrefs
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.SetFloat(highTimeKey, highTime);
            PlayerPrefs.Save();

            Debug.Log($"SAVE HIGH SCORE & TIME: Score={highScore}, Time={highTime:F2}s");
        }
        else
        {
            Debug.Log("CHƯA PHÁ KỈ LỤC");
        }
    }

    public int GetScoreData()   
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string highScoreKey = "HighScore_" + sceneName; // Khóa duy nhất cho mỗi màn
        return PlayerPrefs.GetInt(highScoreKey, 0); // Mặc định là 0 nếu chưa có điểm cao
    }

    public float GetTimeData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string highTimeKey = "HighTime_" + sceneName; 
        return PlayerPrefs.GetFloat(highTimeKey, 0); 
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