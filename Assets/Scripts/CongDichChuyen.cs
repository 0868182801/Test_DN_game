using UnityEngine;

public class CongDichChuyen : MonoBehaviour
{
    [SerializeField] private Transform diemDichChuyenDen;   // Điểm mà player sẽ được dịch chuyển đến khi chạm vào cổng dịch chuyển

    public Transform GetDiemDichChuyenDen()
    {
        return diemDichChuyenDen;   // Trả về điểm dịch chuyển đến
    }
}
