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
    public Turn NowTurn;
    
    [SerializeField] GameObject p1Cursol;

    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject summonItem;

    [SerializeField] GameObject pointManager;

    [SerializeField] Player1Move player1;
    [SerializeField] Player2Move player2;
    [SerializeField] Player3Move player3;
    [SerializeField] Player4Move player4;
    [SerializeField] Canvas Result;
    [SerializeField] int clearLine = 100;
    [SerializeField] float TurnChangeTime=5;
    [SerializeField] float CountChangeTime;
    [SerializeField] public List<GameObject> _isChoiceCursol;
    [SerializeField] public List<GameObject> _isPutCursol;
    [SerializeField] CinemachineGroup cinemachineGroup;

    [SerializeField] Text text;
    private void Update()
    { 
        text.text =NowTurn.ToString();
        CountChangeTime += Time.deltaTime;
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

        if (NowTurn == Turn.GamePlay)
        {
            if ((player1.Score.HasFlag(PlayerState.GetScore.isGoal) || player1.Score.HasFlag(PlayerState.GetScore.Death))
            && (player2.Score.HasFlag(PlayerState.GetScore.isGoal) || player2.Score.HasFlag(PlayerState.GetScore.Death))
            && (player3.Score.HasFlag(PlayerState.GetScore.isGoal) || player3.Score.HasFlag(PlayerState.GetScore.Death))
            && (player4.Score.HasFlag(PlayerState.GetScore.isGoal) || player4.Score.HasFlag(PlayerState.GetScore.Death)))
            {
                TurnChange();
            }
        }
        if (NowTurn == Turn.Result)
        {
            if (CountChangeTime >= TurnChangeTime) TurnChange();
        }
    }
    public void TurnChange()
    {
        switch (NowTurn)
        {
            case Turn.GamePlay:
                p1Cursol.SetActive(false);
                Result.gameObject.SetActive(true);
                pointManager.GetComponent<PointManager>()._isCheck = true;
                CountChangeTime = 0;
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
                if (player2.GetComponent<Player2Move>().P2Score >= clearLine)
                {
                    NowTurn = GameManager.Turn.GameEnd;
                    Debug.Log("GameEnd");
                    break;
                }
                if (player3.GetComponent<Player3Move>().P3Score >= clearLine)
                {
                    NowTurn = GameManager.Turn.GameEnd;
                    Debug.Log("GameEnd");
                    break;
                }
                if (player4.GetComponent<Player4Move>().P4Score >= clearLine)
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
                startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                cinemachineGroup.cameraReset = true;
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
