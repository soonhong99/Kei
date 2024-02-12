using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    // 10초마다 메세지가 나오게 하는 코드
    //private void Start()
    //{
    //    Invoke("MessagePlayer", 10f);
    //}

    public void MessagePlayer()
    {
        // 인스턴스화. 해당 게임오브젝트를 다수 생성시킬 때 사용.
        GameObject gameObject = Instantiate(chatBoxPrefab);

        gameObject.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
        gameObject.GetComponent<ChatSystem>().UpdatePoint(chatTr);
        // 10초마다 메세지가 나오게 하는 코드
        // Invoke("MessagePlayer", 10f);
    }

    
    private void OnMouseDown()
    {
        MessagePlayer();
    }
}
