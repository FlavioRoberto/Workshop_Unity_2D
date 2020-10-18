using Assets.Enums;
using Assets.Extensions;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _velocidade;
    private float _forcaPulo;
    private bool _podePular;
    private bool _sofrendoDano;
    private float _tempoEmDano;
    private float _vida;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private GameObject _gameObjectInimigo;

    void Start()
    {
        _vida = 1;
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
            DiminuirVida();

            _sofrendoDano = true;

            DefinirAnimacao(EPlayerTransicao.DANO);

            _gameObjectInimigo = collision.transform.parent.gameObject;

            _gameObjectInimigo.DesabilitarColisoresFilhos();
        }
    }

    private void DiminuirVida()
    {
        if (_sofrendoDano)
            return;

        _vida -= _vida * 0.25f;

        var barraVida = FindObjectOfType<BarraVida>();

        barraVida.DefinirVida((EStatusVida)_vida);

        var barraVida2 = FindObjectOfType<BarraVida2>();

        barraVida2.Vida = _vida;
    }

    private void SofrendoDano()
    {
        if (!_sofrendoDano)
            return;

        _gameObjectInimigo.HabilitarColisoresFilhos();

        _tempoEmDano += Time.deltaTime;
                
        if (_tempoEmDano > 2)
        {
            _sofrendoDano = false;

            _tempoEmDano = 0;
        }
    }

}
