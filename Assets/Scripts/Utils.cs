using UnityEngine;
using UnityEngine.UI;

static public class Utils
{
    /// <summary>
    /// ���������� ����� � ���� UI ��������.
    /// </summary>
    /// <param name="gameObject">UI ������ � ��������� Text.</param>
    /// <param name="text">����� � ���� string.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, string text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text;
        }
    }

    /// <summary>
    /// ���������� ����� � ���� UI ��������.
    /// </summary>
    /// <param name="gameObject">UI ������ � ��������� Text.</param>
    /// <param name="text">����� � ���� int.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, int text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text.ToString();
        }
    }

    /// <summary>
    /// ���������� ����� � ���� UI ��������.
    /// </summary>
    /// <param name="gameObject">UI ������ � ��������� Text.</param>
    /// <param name="text">����� � ���� float.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, float text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text.ToString();
        }
    }

    /// <summary>
    /// ���������� ����� � ���� UI ��������.
    /// </summary>
    /// <param name="gameObject">UI ������ � ��������� Text.</param>
    /// <param name="text">����� � ���� double.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, double text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text.ToString();
        }
    }

    /// <summary>
    /// �������� �� ������������ ������� ������.
    /// </summary>
    /// <param name="key">��� ������.</param>
    /// <returns>��������, ���������� ��������� �������.</returns>
    static internal bool CheckKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ���������� �������� isTrigger � Colider`� �������.
    /// </summary>
    /// <param name="gameObject">������� ������.</param>
    /// <param name="boolean">�������� ��������.</param>
    static internal void SetIsTrigger(GameObject gameObject, bool boolean)
    {
        if(gameObject.GetComponent<Collider>() != null)
        {
            gameObject.GetComponent<Collider>().isTrigger = boolean;
        }
        else if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = boolean;
        }
    }

    /// <summary>
    /// ���������� �������� ��������.
    /// </summary>
    /// <param name="child">������� �������.</param>
    /// <param name="targetParent">������� ��������.</param>
    /// <returns></returns>
    static internal bool ChangeParent(Transform child, Transform targetParent)
    {
        if(targetParent.gameObject == null || child.gameObject == null)
        {
            return false;
        }

        child.parent = targetParent;
        return true;
    }

    /// <summary>
    /// ������������ �������� � ��������.
    /// </summary>
    /// <param name="elems">������ �������� ���������.</param>
    static internal void ActivateElemsInHierarchy(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            elem.SetActive(true);
        }
    }

    /// <summary>
    /// �������������� �������� � ��������.
    /// </summary>
    /// <param name="elems">������ �������� ���������.</param>
    static internal void DeactivateElemsInHierarchy(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            elem.SetActive(false);
        }
    }

    /// <summary>
    /// ������� ���������� �������� � �������� �� ���������������.
    /// </summary>
    /// <param name="elems">������ �������� ���������.</param>
    static internal void ReverseElemsCondition(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            if (elem.activeInHierarchy)
            {
                elem.SetActive(false);
            }
            else
            {
                elem.SetActive(true);
            }
        }
    }
}