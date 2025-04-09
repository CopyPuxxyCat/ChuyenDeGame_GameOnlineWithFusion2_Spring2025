using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    private bool isCursorLocked = true;

    void Start()
    {
        LockCursor(); // Bắt đầu game sẽ khóa con trỏ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCursorLocked)
                UnlockCursor();
            else
                LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
}
