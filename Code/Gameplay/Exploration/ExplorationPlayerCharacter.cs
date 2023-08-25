using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationPlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private InteractionManager _interaction;

    private bool _canMove;
    private Vector2 _moveAxis;

    public float CurrentSpeed => _rigidbody.velocity.magnitude;

    public InteractionManager Interaction => _interaction;

    public void Initialize()
    {
        _canMove = true;
    }

    public void SetMovement(Vector2 newMovement)
    {
        _moveAxis = newMovement;
    }

    private void Update()
    {
        if (!_canMove) return;

        _rigidbody.velocity = new Vector2(_moveAxis.x, _moveAxis.y) * speed;
    }
}
