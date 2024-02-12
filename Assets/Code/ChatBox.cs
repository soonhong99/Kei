using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    // 10�ʸ��� �޼����� ������ �ϴ� �ڵ�
    //private void Start()
    //{
    //    Invoke("MessagePlayer", 10f);
    //}

    public void MessagePlayer()
    {
        // �ν��Ͻ�ȭ. �ش� ���ӿ�����Ʈ�� �ټ� ������ų �� ���.
        GameObject gameObject = Instantiate(chatBoxPrefab);

        gameObject.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
        gameObject.GetComponent<ChatSystem>().UpdatePoint(chatTr);
        // 10�ʸ��� �޼����� ������ �ϴ� �ڵ�
        // Invoke("MessagePlayer", 10f);
    }

    
    private void OnMouseDown()
    {
        MessagePlayer();
    }
}
