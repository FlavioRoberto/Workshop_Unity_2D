using Assets.Enums;
using Assets.Extensions;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float tempoMovimento = 3f;
    public float velocidade = 3f;
    private float _tempoMovimentando;
    private Rigidbody2D _rigigidBody2D;
    private Animator _animator;

    private void Start()
    {
        _rigigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentar();

    }

    private void Movimentar()
    {
        _tempoMovimentando += Time.deltaTime;

        if(_tempoMovimentando >= tempoMovimento)
        {
            velocidade = -velocidade;
            _tempoMovimentando = 0;
        };

        _rigigidBody2D.velocity = new Vector2(velocidade, 0);

        var direcao = velocidade > 0 ? EDirecaoMovimento.ESQUERDA : EDirecaoMovimento.DIREITA;
        gameObject.transform.DefinirPosicaoMovimento(direcao);
    }
    
    public void Destruir()
    {
        _animator.SetInteger("transicao", 1);
    }
}
