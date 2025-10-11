<?php

namespace MapperAI\Test\MappedClasses;

class Carro
{
    private string $marca;
    private string $modelo;
    private int $ano;

    public function __construct(string $marca, string $modelo, int $ano)
    {
        $this->marca = $marca;
        $this->modelo = $modelo;
        $this->ano = $ano;
    }

    public function getMarca(): string
    {
        return $this->marca;
    }

    public function getModelo(): string
    {
        return $this->modelo;
    }

    public function getAno(): int
    {
        return $this->ano;
    }

    public function exibirInformacoes(): void
    {\n        echo "Marca: " . $this->marca . "\n";
        echo "Modelo: " . $this->modelo . "\n";
        echo "Ano: " . $this->ano . "\n";
    }

    public function isAntigo(): bool
    {
        $anoAtual = (int)date('Y');
        return ($anoAtual - $this->ano) > 20;
    }
}
