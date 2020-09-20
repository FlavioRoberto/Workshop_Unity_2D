using Assets.Enums;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _velocidade;
    private float _forcaPulo;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _podePular;

    void Start()
    {
        _velocidade = 3f;
        _forcaPulo = 10f;
        _podePular = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movimentar();
        Pular();
    }

    void Movimentar()
    {
        if (!_podePular)
            return;

        var direcao = Input.GetAxis("Horizontal");

        _rigidbody2D.velocity = new Vector2(_velocidade * direcao, _rigidbody2D.velocity.y);

        switch (direcao)
        {
            case 0: DefinirAnimacao(EPlayerTransicao.PARADO); break;
            case 1: DefinirMovimentacao(direcao, 0); break;
            case -1: DefinirMovimentacao(direcao, 180); break;
        }
    }

    void Pular()
    {
        if (!Input.GetButtonDown("Jump") || !_podePular)
            return;

        DefinirAnimacao(EPlayerTransicao.PULANDO);
        _rigidbody2D.AddForce(new Vector2(0f, _forcaPulo), ForceMode2D.Impulse);
        _podePular = false;
    }

    private void DefinirMovimentacao(float direcao, float angulo)
    {
        transform.eulerAngles = new Vector2(0f, angulo);
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
}
