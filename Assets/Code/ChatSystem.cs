using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    // flaskRed 3번
    public Queue<string> strengthSentences;
    // flaskBlue 1번
    public Queue<string> speedSentences;

    public Queue<string> sentences;

    public string currentSentence;
    public TextMeshPro textMeshPro;
    public GameObject quad;
    // public Transform movePoint;
    private Transform updatePoint;

    // 해당 메세지의 위치가 플레이어의 위치가 변한다면, 계속 update하면서 메세지가 플레이어를 따라다녀야함
    void Update()
    {
        UpdatePoint(updatePoint);
    }

    // chatpoint의 위치만 갖고오기
    public void UpdatePoint(Transform currentPoint)
    {
        updatePoint = currentPoint;
        if (updatePoint != null)
        {
            transform.position = updatePoint.position;
        }
    }
 
    // chat box에서 chatpoint의 인자값을 넘겨받는다.
    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        // 해당 게임 옵젝의 포지션을 chatpoint 포지션으로 초기화.
        transform.position = chatPoint.position;

        sentences = new Queue<string>();
        sentences.Clear();

        // string의 배열 값을 전부 큐에 차례로 넣어주기.
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }

        StartCoroutine(DialogueFlow(chatPoint));
    }

    // Queue에 담은 string을 코루틴을 이용해서 차례로 말풍선 띄워주기
    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;
        while (sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();
            textMeshPro.text = currentSentence;
            float x = textMeshPro.preferredWidth;

            // x의 크기가 3보다 크면 3을 반환하고 3보다 작으면 x + 0.3f를 반환한다.
            x = (x > 3) ? 3 : x + 0.3f;
            // 말풍선의 크기를 계산하여 초기화 시켜주기.
            quad.transform.localScale = new Vector2(x, textMeshPro.preferredHeight + 0.3f);

            // transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + textMeshPro.preferredHeight / 2f);

            // 대사가 한줄 말풍선에 넣어놨으니 조금 쉬었다가 다음 대사를 집어넣어야 한다.
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
