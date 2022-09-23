using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// �v���C���[�l���I���t�F�C�Y
/// 1�A�v���C���[�l���I��
/// 2�A�v���C���[�F�I��
/// �Q�[���t�F�C�Y
/// 1�A�Q�[���v���C�̃^�[��
/// �S�������񂾂�|�C���g�W�v�̃^�[����
/// 2�A�|�C���g�W�v�̃^�[��
/// �����������N�����ڕW�_���B��������A�܂��͈��^�[�����o������I���t�F�C�Y��
/// 3�A�A�C�e���I���^�[��
/// �������S�����A�C�e���I��������A�܂��͈�莞�Ԍo������A�C�e���ݒu�^�[����
/// 4�A�A�C�e���ݒu�^�[��
/// �������S�����A�C�e���ݒu������A�܂��͈�莞�Ԍo������Q�[���v���C�^�[����
/// �N�������Ԑ؂�܂Őݒu���Ă��Ȃ�������A���̏ꏊ�ɐݒu�����
/// �I���t�F�C�Y
/// 1�A���������ЂƂ��h�A�b�v�����
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("���̐��̑��Ă��x�z����ŋ�Enum����")]
    public Turn NowTurn;

    [Header("�C���X�^���X���������̂���")]
    [SerializeField] List<GameObject> _cursolList = new();
    [SerializeField] List<GameObject> _playerList = new();
    [SerializeField] List<Canvas> _playerCanvas = new();
    [SerializeField] GameObject _startingPoint;
    [SerializeField] GameObject _resetCursorPoint;
    [SerializeField] GameObject _summonItem;
    [SerializeField] GameObject _goal;
    [SerializeField] CinemachineGroup _cinemachineGroup;
    [SerializeField] Canvas _result;
    [SerializeField] Text _text;
    [SerializeField] GameObject _itemTurnCamera;

    

    [Header("�ϐ�����")]
    [SerializeField] int _clearLine = 100;
    [SerializeField] float _TurnChangeTime = 5;
    [SerializeField] float _CountChangeTime;

    [Header("�ق��̂Ƃ���ɓn������")]
    public List<GameObject> _isChoiceCursol;
    public List<GameObject> _isPutCursol;
    public List<GameObject> _choiceList = new();




    private void Update()
    {
        _text.text = NowTurn.ToString();
        _CountChangeTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TurnChange();
        }
        if (_isChoiceCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            _isChoiceCursol.Clear();
        }
        if (_isPutCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            _isPutCursol.Clear();
        }
        if (NowTurn == Turn.Result)
        {
            if (_CountChangeTime >= _TurnChangeTime) TurnChange();
        }
    }
    public void TurnChange()
    {
        switch (NowTurn)
        {
            case Turn.GamePlay://GamePlay�I���̎�

                _result.gameObject.SetActive(true);
                _CountChangeTime = 0;

                NowTurn = GameManager.Turn.Result;
                break;
            case Turn.Result://Result�I���̎�
                NowTurn = GameManager.Turn.SelectItem;
                
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                _result.gameObject.SetActive(false);
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].GetComponent<PlayerCursor>()._isFollowing = false; 
                }
                _itemTurnCamera.SetActive(true);
                _resetCursorPoint.GetComponent<CursorStart>().SelectSceneStart = true;
                _summonItem.GetComponent<SummonItem>()._isChoiceItem = true;
                for (int i = 0; i < _playerList.Count; i++)
                {
                    if (_playerList[0].GetComponent<PlayerMove>()._scorePoint >= _clearLine)
                    {
                        NowTurn = GameManager.Turn.GameEnd;
                        Debug.Log("GameEnd");
                        _playerCanvas[i].gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            case Turn.SelectItem://Select�I���̎�
                _isChoiceCursol.Clear();//��ɂ��������Ə����Ă��邯�ǁA�f�o�b�O�p
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                for (int i = 0; i < _choiceList.Count; i++)
                {
                    _choiceList[i].SetActive(true);
                }
                NowTurn = GameManager.Turn.SetItem;
                break;
            case Turn.SetItem://Set�I���̎�
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(false);
                }
                _startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                _cinemachineGroup._playerCameraReset = true;
                _isPutCursol.Clear();//�f�o�b�O�p
                _itemTurnCamera.SetActive(false);
                NowTurn = GameManager.Turn.GamePlay;
                break;
            case Turn.GameEnd:


                break;

            default:
                break;
        }
    }
    public enum Turn
    {
        GamePlay,
        Result,
        SelectItem,
        SetItem,
        GameEnd,
    }
}
