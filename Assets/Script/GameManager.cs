using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] Player1Move p1;
    //[SerializeField] Player2Move p2;
    //[SerializeField] Player3Move p3;
    //[SerializeField] Player4Move p4;
    public Turn NowTurn;

    [SerializeField] GameObject p1Cursol;

    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject summonItem;

    [SerializeField] Player1Move player1;
    [SerializeField] Canvas Result;
    [SerializeField] int clearLine = 10;

    [SerializeField] public List<GameObject> _isChoiceCursol;
    [SerializeField] public List<GameObject> _isPutCursol;
    [SerializeField] CinemachineGroup cinemachineGroup;
    private void Update()
    {
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

    }
    public void TurnChange()
    {
        switch (NowTurn)
        {
            case Turn.GamePlay:
                p1Cursol.SetActive(false);
                Result.gameObject.SetActive(true);
                NowTurn = GameManager.Turn.Result;
                break;
            case Turn.Result:
                NowTurn = GameManager.Turn.SelectItem;
                p1Cursol.SetActive(true);
                Result.gameObject.SetActive(false);
                if (player1.GetComponent<Player1Move>().P1Score >= clearLine)
                {
                    NowTurn = GameManager.Turn.GameEnd;
                    Debug.Log("GameEnd");
                    break;
                }
                summonItem.GetComponent<SummonItem>()._isChoiceItem = true;
                break;
            case Turn.SelectItem:

                NowTurn = GameManager.Turn.SetItem;
                break;
            case Turn.SetItem:
                NowTurn = GameManager.Turn.GamePlay;
                startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                cinemachineGroup.cameraReset= true;
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
