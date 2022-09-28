using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���������ʂ̋@�\
/// </summary>
public abstract class ItemBase : MonoBehaviour
{
    Vector2 _CursorPosition;
    private string playername;
    //    public abstract void Activate1();
    //    public abstract void Activate2();
    [SerializeField] protected bool _isFollowing;
    [SerializeField] Color _color1;
    [SerializeField] Color _color2;
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected GameManager.Turn _nowTurn = GameManager.Turn.SelectItem;
    [SerializeField] protected GameObject _selectImage;
    protected void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
        _gameManager.TurnChangeActionMethod += TurnChecker;
        _nowTurn = GameManager.Turn.SelectItem;
        Debug.Log("��������");
    }
    protected void Update()
    {
        if (_nowTurn == GameManager.Turn.SetItem && gameObject.tag != "isChoice")
        {
            _gameManager.TurnChangeActionMethod -= TurnChecker;
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("isChoice"))
        {
            ChangeColor(true, _color2);
        }
        ShowImage();
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            ColliderOnOff(true);
            ChangeColor(true, _color1);
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cursor"))
        {
            ChangeColor(true, _color1);
        }
    }
    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Cursor"))
        {
            ChangeColor(false, _color2);
        }
    }

    /// <summary>
    /// �����A�C�e�����ǂ̂悤�ɓ������v���C���[�����ɋ�����摜����������摜��OnOff�؂�ւ����Ăق���
    /// </summary>
    protected void ShowImage()
    {
        if (_selectImage != null)
        {
            if (_gameManager.NowTurn == GameManager.Turn.SelectItem)
            {
                _selectImage.gameObject.SetActive(true);
            }
            else
            {
                _selectImage.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// �A�C�e���I�����A�A�C�e���ɃJ�[�\�����킳��ƐF���ς��B
    /// </summary>
    /// <param name="cursorcheck"></param>
    protected virtual void ChangeColor(bool cursorcheck, Color color)
    {

        if (cursorcheck)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;

        }
        else if (!cursorcheck)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }

    }
    /// <summary>
    /// �I�����ꂽ��R���C�_�[���S���Ȃ��Ȃ��ĂĂق����A�܂��u���Ƃ��̓R���C�_�[�܂��߂��Ă��炤�B
    /// </summary>
    /// <param name="colliderSwitch"></param>
    void ColliderOnOff(bool colliderSwitch)
    {
        if (TryGetComponent(out BoxCollider2D BC2D))
        {
            BC2D.enabled = colliderSwitch;
        }
        if (TryGetComponent(out CircleCollider2D CC2D))
        {
            CC2D.enabled = colliderSwitch;
        }
        if (TryGetComponent(out PolygonCollider2D PC2D))
        {
            PC2D.enabled = colliderSwitch;
        }
    }
    protected void TurnChecker()
    {
        _nowTurn = _gameManager.NowTurn;
    }
}
