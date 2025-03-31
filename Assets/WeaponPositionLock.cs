using UnityEngine;
using System.Collections.Generic;

public class WeaponPositionLock : MonoBehaviour
{
    [SerializeField] List<Transform> aimTargets; // Danh sách các Transform cần di chuyển
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;

    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            foreach (Transform aimTarget in aimTargets)
            {
                aimTarget.position = Vector3.Lerp(aimTarget.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            }
        }
    }
}
