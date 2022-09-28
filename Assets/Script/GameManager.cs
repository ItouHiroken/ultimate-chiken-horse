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
    [Header("���̐��̑��Ă��i��ŋ�Enum����")]
    public Turn NowTurn;
    [Tooltip("�^�[���`�F���W����Ƃ��̃A�N�V�����f���Q�[�g")]
    public delegate void TurnChangeAction();
    public TurnChangeAction TurnChangeActionMethod = default;
    [Header("�C���X�^���X���������̂���")]
    [SerializeField, Tooltip("�J�[�\������")] List<GameObject> _cursolList = new();
    [SerializeField, Tooltip("�v���C���[����")] List<GameObject> _playerList = new();
    [SerializeField, Tooltip("bool��n������")] StartingPoint _startingPoint;
    [SerializeField, Tooltip("bool��n������")] CursorStart _resetCursorPoint;
    [SerializeField, Tooltip("bool��n������")] SummonItem _summonItem;
    [SerializeField, Tooltip("bool��n������")] Goal _goal;
    [SerializeField, Tooltip("bool��n������")] CinemachineGroup _cinemachineGroup;
    [SerializeField, Tooltip("���U���g�̃v���C���[�̏����L�����o�X")] List<Canvas> _playerCanvas = new();
    [SerializeField, Tooltip("���U���g�^�[���̎��̃L�����o�X")] Canvas _result;
    [SerializeField, Tooltip("�f�o�b�O�p�A�^�[���������Ă����")] Text _text;
    [SerializeField, Tooltip("�A�C�e����I���A�ݒu���鎞�Ɏg���J����")] GameObject _itemTurnCamera;

    [Header("�ϐ�����")]
    [SerializeField, Tooltip("��������X�R�A�̃��C��")] int _clearLine = 100;
    [SerializeField, Tooltip("���U���g�^�[���̎���")] float _resultTime = 5;

    [Header("�ق��̂Ƃ���ɓn������")]
    [Tooltip("�J�[�\�����A�C�e���I�������炱���ɒǉ������")] public List<GameObject> IsChoiceCursol;
    [Tooltip("�J�[�\�����I�������A�C�e�����ǉ������")] public List<GameObject> ChoiceList = new();
    [Tooltip("�J�[�\�����A�C�e���ݒu�����炱���ɒǉ������")] public List<GameObject> IsPutCursol;

    private void Update()
    {
        //�f�o�b�O�p�A���̃^�[���������Ă����
        _text.text = NowTurn.ToString();
        //�f�o�b�O�p�A�^�[����؂�ւ��Ă����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TurnChange();
        }
        //�����J�[�\�����S���A�C�e����I�񂾂�^�[�����؂�ւ��
        if (IsChoiceCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            IsChoiceCursol.Clear();
        }
        //�����J�[�\�����S���A�C�e����ݒu������^�[�����؂�ւ��
        if (IsPutCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            IsPutCursol.Clear();
        }
    }
    public void TurnChange()
    {
        Debug.Log("��������"+NowTurn);
        switch (NowTurn)
        {
            ///Play��Result
            case Turn.GamePlay://GamePlay�I���̎�
                //���U���g�̃L�����o�X��true
                _result.gameObject.SetActive(true);
                //���U���g�^�[���̎��Ԃ𐧌䂷��
                //���\��p�ŏ����Ă遫������������������������������������������
                //Invoke(nameof(TurnChange), _resultTime);
                //���̃^�[�������U���g�^�[���ɐ؂�ւ���
                NowTurn = GameManager.Turn.Result;
                TurnChangeActionMethod();
                break;

            ///Result��Select
            ///      ��End
            case Turn.Result://Result�I���̎�

                ///���̃^�[�����A�C�e���I���^�[���ɐ؂�ւ���
                NowTurn = GameManager.Turn.SelectItem;
                //�J�[�\�����ĂыN����
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                //���U���g�L�����o�X������
                _result.gameObject.SetActive(false);
                //�J�[�\���̋@�\�́u���Ă��āv��bool��false�ɂ���
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].GetComponent<PlayerCursor>()._isFollowing = false;
                }
                //�A�C�e���I���A�ݒu�^�[���p�̃J������true
                _itemTurnCamera.SetActive(true);
                //�J�[�\�����ʒu�ɏ�������bool
                _resetCursorPoint.GetComponent<CursorStart>().SelectSceneStart = true;
                //�A�C�e����������bool
                _summonItem.GetComponent<SummonItem>()._isChoiceItem = true;
                ////�����N�����ڕW�_�������Ă�����
                //1.���̃^�[�����Q�[���G���h�ɐ؂�ւ���
                //2.�v���C���[�̏������L�����o�X��true
                for (int i = 0; i < _playerList.Count; i++)
                {
                    if (_playerList[0].GetComponent<PlayerMove>()._scorePoint >= _clearLine)
                    {
                        NowTurn = GameManager.Turn.GameEnd;
                        Debug.Log("GameEnd");
                        _playerCanvas[i].gameObject.SetActive(true);
                        TurnChangeActionMethod();
                        break;
                    }
                }
                TurnChangeActionMethod();
                break;
            ///Select��Set
            case Turn.SelectItem://Select�I���̎�
                IsChoiceCursol.Clear();//�J�[�\���̖l�I�т܂����惊�X�g�̒��g�����Z�b�g����(�Ȃ���)
                //�J�[�\���̓A�C�e����I�񂾂�J�[�\�����g��flase�ɂ��邯�ǁA�^�[�����؂�ւ���Ă܂�true�ɂ���B
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                //�I�񂾃A�C�e��������false
                for (int i = 0; i < ChoiceList.Count; i++)
                {
                    if (!ChoiceList[i]) { break; }
                    ChoiceList[i].SetActive(true);
                }
                //���̃^�[�����A�C�e���ݒu�^�[���ɐ؂�ւ���
                NowTurn = GameManager.Turn.SetItem;
                TurnChangeActionMethod();
                break;

            ///Set��Play
            case Turn.SetItem:
                //�J�[�\���͂������Ȃ��Ȃ��Ăق���
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(false);
                }
                //�v���C���[�����̃|�W�V�����ɖ߂�bool
                _startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                //�V�l�}�V�[���J�����̃^�[�Q�b�g�O���[�v�Ƀv���C���[����������bool
                _cinemachineGroup._playerCameraReset = true;
                //�f�o�b�O�p
                IsPutCursol.Clear();
                //�A�C�e���p�J������������
                _itemTurnCamera.SetActive(false);
                //���̃^�[�����v���C�^�[���ɐ؂�ւ���
                NowTurn = GameManager.Turn.GamePlay;
                TurnChangeActionMethod();
                break;
            case Turn.GameEnd:
                TurnChangeActionMethod();
                break;

            default:
                break;
        }
        Debug.Log("����"+NowTurn);
    }
    /// <summary>
    /// ���̃^�[���ł�
    /// </summary>
    public enum Turn
    {
        GamePlay,
        Result,
        SelectItem,
        SetItem,
        GameEnd,
    }
}
