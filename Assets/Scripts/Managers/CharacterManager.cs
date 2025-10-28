using UnityEngine;

public class CharacterManager : BaseManager<CharacterManager>
{
    public GameObject player;
    public float moveSpeed = 5f;          // 移动速度
    public Rigidbody2D rb;
    
    private Vector2 moveInput;            // 输入方向
    private bool _isPlayerVisible;
    
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    private void Start()
    {
        player.SetActive(false);
        _isPlayerVisible = false;
    }
    
    void Update()
    {
        // 获取WASD输入（水平 + 垂直）
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D 或 ← →
        float moveY = Input.GetAxisRaw("Vertical");   // W/S 或 ↑ ↓

        moveInput = new Vector2(moveX, moveY).normalized; // 归一化避免斜向移动过快
    }

    void FixedUpdate()
    {
        // 使用 Rigidbody2D 移动
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void ShowPlayer()
    {
        player.SetActive(true);
        _isPlayerVisible = true;
    }

    public void HidePlayer()
    {
        player.SetActive(false);
        _isPlayerVisible = false;
    }
}