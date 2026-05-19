using UnityEngine;

public class EnvLoad : MonoBehaviour
{
    void Awake()
    {
        HomeAssistantSettings.Load();
    }
}

