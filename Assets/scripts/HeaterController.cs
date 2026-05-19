using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HeaterController : MonoBehaviour
{
    public string plugID = "";


    private void OnDestroy()
    {
        SendTurnOffImmediate();
    }

    private void SendTurnOffImmediate()
    {
        string url =
            $"{HomeAssistantSettings.Address}/api/services/switch/turn_off";

        string json =
            $"{{\"entity_id\":\"{plugID}\"}}";

        var body = System.Text.Encoding.UTF8.GetBytes(json);

        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {HomeAssistantSettings.Token}");

        request.SendWebRequest();
    }


    private void OnTriggerEnter(Collider other)
    {
        TurnOnHeater();
    }
    
    private void OnTriggerExit(Collider other)
    {
        TurnOffHeater();
    }

    private void TurnOnHeater()
    {
        StartCoroutine(SendTurnRequest(true));
    }

    private void TurnOffHeater()
    {
        StartCoroutine(SendTurnRequest(false));
    }

    IEnumerator SendTurnRequest(bool isOn)
    {
        string turnUrl =
            $"{HomeAssistantSettings.Address}/api/services/switch/{(isOn ? "turn_on" : "turn_off")}";

        string json =
            $"{{\"entity_id\":\"{plugID}\"}}";

        byte[] body =
            Encoding.UTF8.GetBytes(json);

        UnityWebRequest request =
            new UnityWebRequest(turnUrl, "POST");

        request.uploadHandler =
            new UploadHandlerRaw(body);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        request.SetRequestHeader(
            "Authorization",
            $"Bearer {HomeAssistantSettings.Token}"
        );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Switch turned {(isOn ? "on" : "off")}");
        }
        else
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
    }
}