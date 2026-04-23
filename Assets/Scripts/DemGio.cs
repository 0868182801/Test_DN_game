using UnityEngine;
using TMPro;

public class DemGio : MonoBehaviour
{
    public TextMeshProUGUI timeText;    // Lưu trữ text cho thời gian
    private float startTime;    // Thời gian bắt đầu bộ đếm giờ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;  // Lưu thời gian bắt đầu bộ đếm giờ (Time.time: biến lấy thời gian khi game bắt đầu chạy)
    }

    // Update is called once per frame
    void Update()
    {
        float thoiGianDaTroiQua;    
        thoiGianDaTroiQua = Time.time - startTime;  // Tính thời gian đã trôi qua kể từ khi bắt đầu bộ đếm giờ

        int phut = (int)(thoiGianDaTroiQua / 60);   
        int giay = (int)(thoiGianDaTroiQua % 60);   

        timeText.text = string.Format("{0:00}:{1:00}", phut, giay); // Cập nhật text hiển thị thời gian theo định dạng MM:SS
    }
}
