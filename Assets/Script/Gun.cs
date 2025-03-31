using Fusion;
using UnityEngine;

public class Gun : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Vị trí đầu nòng súng
    public float bulletSpeed = 20f;

    private Camera mainCamera;
    public Animator animator;

    void Start()
    {
        mainCamera = Camera.main; // Lấy camera chính
    }

    void Update()
    {
        if (!Object.HasInputAuthority) return; // Chỉ chạy cho player sở hữu

        if (Input.GetMouseButtonDown(0)) // Nhấn chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Kích hoạt animation bắn
        animator.SetTrigger("Shoot");

        // Gửi RPC để tất cả player thấy animation bắn
        RPC_PlayShootAnimation();

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 shootDirection = (hit.point - firePoint.position).normalized;

            // Gửi RPC để tất cả người chơi đều thấy viên đạn bắn ra
            RPC_SpawnBullet(firePoint.position, shootDirection);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_SpawnBullet(Vector3 spawnPosition, Vector3 direction)
    {
        // Tạo đạn trên mạng
        Runner.Spawn(bulletPrefab, spawnPosition, Quaternion.LookRotation(direction), Object.InputAuthority, (runner, obj) =>
        {
            obj.GetComponent<Bullet>().Initialize(direction);
        });
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_PlayShootAnimation()
    {
        animator.SetTrigger("Shoot"); // Gọi animation trên tất cả client
    }
}



