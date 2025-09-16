using Essence;
using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiNetcodeTest : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    NetworkVariable<int> ScoreValue = new NetworkVariable<int>(10,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    public Action<int> OnScoreCall; // �Լ��� ������ �� �ִ� ����.  
    //public Func<int, int> MyFunc;   // ù��° Ÿ�Ժ��� �Ű������̰� , ������ �Ű������� ���ϰ��̴�. 
    //public Func<int, string> MyFunc2;
    //public void Func() { }
    //public void Func2(int Val) { }
    //public int Func3(int Val2) { return 3; }
    //public int Func4(int Val2, int Val3) { return 3; }
    //public string Func5(int Val) { return ""; }

    // ��Ʈ��ũ �ڵ�� �������� �����ϴ���, Ŭ���̾�Ʈ���� �����ؾ� �ϴ��� ������ �ؾ��Ѵ�.
    // ���ǹ��� ����ؼ� ��������, ������ �ƴ��� �����ϰ� �� ���¿� �´� �Լ��� �����ϴ� ������ �ڵ带 �����Ѵ�.

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            OnScoreCall += HandleAddPoint;
            //MyFunc2 + ScoreValue.Valueue += 10; // Ư���� ���������� �����ϰ� �ʹ�.
        }

        ScoreValue.OnValueChanged += OnScoreValueChanged;       
    }

    private void HandleAddPoint(int value)
    {
        ScoreValue.Value += value;
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer) 
        {
            OnScoreCall -= HandleAddPoint; 
        }

        ScoreValue.OnValueChanged -= OnScoreValueChanged;
    }

    private void OnScoreValueChanged(int previousValue, int newValue)
    {
        // ��� Client�� �����ؾ��ϴ� �ڵ�.

        scoreText.SetText($"score : {newValue}");
    }



}
