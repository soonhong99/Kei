using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    // danger sentence 나 smell sentence 추가 예정
    public string[] flaskBlueSentences;
    public string[] flaskGreenSentences;
    public string[] flaskRedSentences;
    public string[] flaskYellowSentences;
    public string[] nullSentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    // 10초마다 메세지가 나오게 하는 코드
    //private void Start()
    //{
    //    Invoke("MessagePlayer", 10f);
    //}

    public void MessagePlayer(string flaskColor)
    {
        // 인스턴스화. 해당 게임오브젝트를 다수 생성시킬 때 사용.
        GameObject gameObject = Instantiate(chatBoxPrefab);

        if (flaskColor == "flaskBlue")
        {
            gameObject.GetComponent<ChatSystem>().Ondialogue(flaskBlueSentences, chatTr);

        }

        else if (flaskColor == "flaskGreen")
        {
            gameObject.GetComponent<ChatSystem>().Ondialogue(flaskGreenSentences, chatTr);

        }

        else if (flaskColor == "flaskRed")
        {
            gameObject.GetComponent<ChatSystem>().Ondialogue(flaskRedSentences, chatTr);

        }

        else if (flaskColor == "flaskYellow")
        {
            gameObject.GetComponent<ChatSystem>().Ondialogue(flaskYellowSentences, chatTr);

        }

        else if (flaskColor == "null")
        {
            gameObject.GetComponent<ChatSystem>().Ondialogue(nullSentences, chatTr);
        }

        gameObject.GetComponent<ChatSystem>().UpdatePoint(chatTr);
        // 10초마다 메세지가 나오게 하는 코드
        // Invoke("MessagePlayer", 10f);
    }

    
    private void OnMouseDown()
    {
        MessagePlayer("null");
    }
}
