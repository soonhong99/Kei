using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    // danger sentence �� smell sentence �߰� ����
    public string[] flaskBlueSentences;
    public string[] flaskGreenSentences;
    public string[] flaskRedSentences;
    public string[] flaskYellowSentences;
    public string[] nullSentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    // 10�ʸ��� �޼����� ������ �ϴ� �ڵ�
    //private void Start()
    //{
    //    Invoke("MessagePlayer", 10f);
    //}

    public void MessagePlayer(string flaskColor)
    {
        // �ν��Ͻ�ȭ. �ش� ���ӿ�����Ʈ�� �ټ� ������ų �� ���.
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
        // 10�ʸ��� �޼����� ������ �ϴ� �ڵ�
        // Invoke("MessagePlayer", 10f);
    }

    
    private void OnMouseDown()
    {
        MessagePlayer("null");
    }
}
