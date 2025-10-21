using UnityEngine;

public class MainCameraManager : BaseManager<MainCameraManager>
{
    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }
}