using Assets.Enums;
using Assets.Extensions;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _velocidade;
    private float _forcaPulo;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _podePular;
    private bool _sofrendoDano;
    private GameObject _gameObjectInimigo;
    private float _tempoEmDano;

    void Start()
    {
        _velocidade = 4f;
        _forcaPulo = 10f;
        _podePular = true;
        _sofrendoDano = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movimentar();
        Pular();
        SofrendoDano();
    }

    void Movimentar()
    {
        var direcao = Input.GetAxis("Horizontal");

        _rigidbody2D.velocity = new Vector2(_velocidade * direcao, _rigidbody2D.velocity.y);

        switch (direcao)
        {
            case 0: if (_podePular && !_sofrendoDano) DefinirAnimacao(EPlayerTransicao.PARADO); break;
            case 1: DefinirMovimentacao(EDirecaoMovimento.DIREITA); break;
            case -1: DefinirMovimentacao(EDirecaoMovimento.ESQUERDA); break;
        }
    }

    void Pular()
    {
        if (!Input.GetButtonDown("Jump") || !_podePular || _sofrendoDano)
            return;

        DefinirAnimacao(EPlayerTransicao.PULANDO);
        _rigidbody2D.AddForce(new Vector2(0f, _forcaPulo), ForceMode2D.Impulse);
        _podePular = false;
    }

    private void DefinirMovimentacao(EDirecaoMovimento direcao)
    {
        transform.DefinirPosicaoMovimento(direcao);

        if (_podePular && !_sofrendoDano)
            DefinirAnimacao(EPlayerTransicao.ANDANDO);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Chao"))
            _podePular = true;
    }

    private void DefinirAnimacao(EPlayerTransicao transicao)
    {
        _animator.SetInteger("transicao", (int)transicao);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            collision.gameObject.transform.parent.BroadcastMessage("Destruir");
            _rigidbody2D.AddForce(new Vector2(0, _forcaPulo * 1.5f), ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("EnemyBody"))
        {
            _sofrendoDano = true;

            DefinirAnimacao(EPlayerTransicao.DANO);

            _gameObjectInimigo = collision.transform.parent.gameObject;

            _gameObjectInimigo.DesabilitarColisoresFilhos();
        }
    }

    private void SofrendoDano()
    {
        if (!_sofrendoDano)
            return;

        _tempoEmDano += Time.deltaTime;

        if (_tempoEmDano > 1)
        {
            _gameObjectInimigo.HabilitarColisoresFilhos();

            _sofrendoDano = false;

            _tempoEmDano = 0;
        }
    }

}
