using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVievChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer _head;
    [SerializeField] SpriteRenderer _body;
    [SerializeField] SpriteRenderer _katana;
    [Header("Sprites")]
    [SerializeField] Sprite[] _headVariables;
    [SerializeField] Sprite[] _bodyVariables;
    [SerializeField] private bool _moveKatana = true;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var _playerDirection = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;

        if (rigidbody.velocity != Vector2.zero)
        {
            if (_playerDirection < 180 && _playerDirection > 0)
            {
                _head.sprite = _headVariables[1];
                _body.sprite = _bodyVariables[1];
                
                if (_moveKatana == true)
                    _katana.sortingOrder = -1;
            }
            else if (_playerDirection > -180 || _playerDirection < 0)
            {
                _head.sprite = _headVariables[0];
                _body.sprite = _bodyVariables[0];
                
                if (_moveKatana == true)
                    _katana.sortingOrder = +1;
            }
        }
    }
}
