using UnityEngine;

public class PlayerMenuController : MonoBehaviour
{
    /// Called from the Input System Send Broadcast
    private void OnCancel()
    {
        InGameMenuController.GetInstance().Toggle();
    }
}
