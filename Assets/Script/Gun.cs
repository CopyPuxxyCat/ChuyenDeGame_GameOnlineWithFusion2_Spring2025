using Fusion;
using System.Collections;
using UnityEngine;

public class Gun : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Vị trí đầu nòng súng
    public float bulletSpeed = 20f;
    public Camera playerCamera; // Camera của người chơi
    public GameObject crosshairUI; // Crosshair nằm giữa màn hình
    public Animator animator;

    private bool cursorVisible = false;

    void Start()
    {
        playerCamera = Camera.main;
        if (Object.HasInputAuthority)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (!Object.HasInputAuthority) return;

        // Bật/tắt con trỏ khi nhấn phím L
        if (Input.GetKeyDown(KeyCode.L))
        {
            cursorVisible = !cursorVisible;
            Cursor.visible = cursorVisible;
            Cursor.lockState = cursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Gọi animation bắn local
        animator.SetTrigger("Shoot");
        Delay(1f);
        RPC_PlayShootAnimation(); // Gọi animation cho mọi client

        // Tính ray từ giữa màn hình (tâm của canvas)
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);

        Vector3 shootDirection;

        // Raycast xác định hướng đạn
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            shootDirection = (hit.point - firePoint.position).normalized;
        }
        else
        {
            shootDirection = ray.direction;
        }

        // Gửi RPC spawn đạn
        RPC_SpawnBullet(firePoint.position, shootDirection);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_SpawnBullet(Vector3 spawnPosition, Vector3 direction)
    {
        Runner.Spawn(bulletPrefab, spawnPosition, Quaternion.LookRotation(direction), Object.InputAuthority, (runner, obj) =>
        {
            obj.GetComponent<Bullet>().Initialize(direction);
        });
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_PlayShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Bắn đạn sau khi animation chạy 1 giây
    }
}




