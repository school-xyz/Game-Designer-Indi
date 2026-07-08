using System;
using UnityEngine;

public class TransactionLog : MonoBehaviour
{
    //Вызов: Записывает транзакцию в Console
    public void Log(string resourceName, int delta, string reason)
    {
        Debug.Log($"[{DateTime.Now:HH:mm:ss}] {resourceName} {(delta >= 0 ? "+" : "")}{delta} ({reason})");
    }
}
