using UnityEngine;

public class MobileControlsVisibility : MonoBehaviour
{
    private void Awake()
    {
        bool isAndroid =
        Application.platform == RuntimePlatform.Android ||
        Application.isEditor;

        gameObject.SetActive(isAndroid);
    }
}