using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storey : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _firstNum;
    [SerializeField] private SpriteRenderer _secondNum;
    [SerializeField] private SpriteRenderer _sign;
    [SerializeField] private int _storeyNum;

    public int StoreyNum { get { return _storeyNum; } private set { _storeyNum = value; } }

    public void ChangeStoreyNum(int newNum, Sprite firstNum, Sprite secondNum)
    {
        StoreyNum = newNum;
        _firstNum.sprite = firstNum;
        _secondNum.sprite = secondNum;
        _sign?.gameObject.SetActive(newNum < 0);
    }
}
