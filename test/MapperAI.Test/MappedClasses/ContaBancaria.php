<?php

namespace MapperAI\Test\MappedClasses;

class ContaBancaria
{
    private string $numero;
    private string $titular;
    private float $saldo;

    public function __construct(string $numero, string $titular, float $saldo = 0.0)
    {
        $this->numero = $numero;
        $this->titular = $titular;
        $this->saldo = $saldo;
    }

    public function depositar(float $valor): string
    {\n        if ($valor > 0) {
            $this->saldo += $valor;
            return "Depósito de R$" . $valor . " realizado com sucesso. Novo saldo: R$" . $this->saldo . ".";
        } else {
            return "Valor de depósito inválido.";
        }
    }

    public function sacar(float $valor): string
    {
        if ($valor > 0 && $valor <= $this->saldo) {
            $this->saldo -= $valor;
            return "Saque de R$" . $valor . " realizado com sucesso. Saldo restante: R$" . $this->saldo . ".";
        } else {
            return "Saldo insuficiente ou valor de saque inválido.";
        }
    }

    public function consultarSaldo(): string
    {
        return "Saldo atual: R$" . $this->saldo . ".";
    }
}
