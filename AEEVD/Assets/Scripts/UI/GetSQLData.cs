using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GetSQLData : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    void Start()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        StartCoroutine(GetRequest("192.168.1.196/sqlretrieve.php"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string rawresponse = webRequest.downloadHandler.text;

                    string[] results = rawresponse.Split('*');

                    float templateHeight = 60f;
                    for(int i = 0; i < results.Length - 1; i++){
                        string[] indexInfo = results[i].Split(',');
                        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
                        entryTransform.gameObject.SetActive(true);
                        TMP_Text textTemp = entryTransform.Find("NameEntryTemplate").GetComponent<TMP_Text>();
                        textTemp.text = indexInfo[0];
                        textTemp = entryTransform.Find("ScoreEntryTemplate").GetComponent<TMP_Text>();
                        textTemp.text =  indexInfo[1];
                        textTemp = entryTransform.Find("WaveEntryTemplate").GetComponent<TMP_Text>();
                        textTemp.text = indexInfo[2];
                    }
                    break;
            }
        }
    }
}
