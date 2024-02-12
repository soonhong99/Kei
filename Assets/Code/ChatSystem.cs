using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    // flaskRed 3��
    public Queue<string> strengthSentences;
    // flaskBlue 1��
    public Queue<string> speedSentences;

    public Queue<string> sentences;

    public string currentSentence;
    public TextMeshPro textMeshPro;
    public GameObject quad;
    // public Transform movePoint;
    private Transform updatePoint;

    // �ش� �޼����� ��ġ�� �÷��̾��� ��ġ�� ���Ѵٸ�, ��� update�ϸ鼭 �޼����� �÷��̾ ����ٳ����
    void Update()
    {
        UpdatePoint(updatePoint);
    }

    // chatpoint�� ��ġ�� �������
    public void UpdatePoint(Transform currentPoint)
    {
        updatePoint = currentPoint;
        if (updatePoint != null)
        {
            transform.position = updatePoint.position;
        }
    }
 
    // chat box���� chatpoint�� ���ڰ��� �Ѱܹ޴´�.
    public void Ondialogue(string[] lines, Transform chatPoint)
    {
        // �ش� ���� ������ �������� chatpoint ���������� �ʱ�ȭ.
        transform.position = chatPoint.position;

        sentences = new Queue<string>();
        sentences.Clear();

        // string�� �迭 ���� ���� ť�� ���ʷ� �־��ֱ�.
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }

        StartCoroutine(DialogueFlow(chatPoint));
    }

    // Queue�� ���� string�� �ڷ�ƾ�� �̿��ؼ� ���ʷ� ��ǳ�� ����ֱ�
    IEnumerator DialogueFlow(Transform chatPoint)
    {
        yield return null;
        while (sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();
            textMeshPro.text = currentSentence;
            float x = textMeshPro.preferredWidth;

            // x�� ũ�Ⱑ 3���� ũ�� 3�� ��ȯ�ϰ� 3���� ������ x + 0.3f�� ��ȯ�Ѵ�.
            x = (x > 3) ? 3 : x + 0.3f;
            // ��ǳ���� ũ�⸦ ����Ͽ� �ʱ�ȭ �����ֱ�.
            quad.transform.localScale = new Vector2(x, textMeshPro.preferredHeight + 0.3f);

            // transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + textMeshPro.preferredHeight / 2f);

            // ��簡 ���� ��ǳ���� �־������ ���� �����ٰ� ���� ��縦 ����־�� �Ѵ�.
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
