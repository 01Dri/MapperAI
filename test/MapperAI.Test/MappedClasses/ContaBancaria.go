package MapperAI.Test.MappedClasses

import "fmt"

type ContaBancaria struct {
	Numero  string
	Titular string
	Saldo   float64
}

func NewContaBancaria(numero string, titular string, saldo float64) *ContaBancaria {
	return &ContaBancaria{Numero: numero, Titular: titular, Saldo: saldo}
}

func (c *ContaBancaria) Depositar(valor float64) string {
	if valor > 0 {
		c.Saldo += valor
		return fmt.Sprintf("Dep\u00f3sito de R$%.2f realizado com sucesso. Novo saldo: R$%.2f.", valor, c.Saldo)
	} else {
		return "Valor de dep\u00f3sito inv\u00e1lido."
	}
}

func (c *ContaBancaria) Sacar(valor float64) string {
	if valor > 0 && valor <= c.Saldo {
		c.Saldo -= valor
		return fmt.Sprintf("Saque de R$%.2f realizado com sucesso. Saldo restante: R$%.2f.", valor, c.Saldo)
	} else {
		return "Saldo insuficiente ou valor de saque inv\u00e1lido."
	}
}

func (c *ContaBancaria) ConsultarSaldo() string {
	return fmt.Sprintf("Saldo atual: R$%.2f.", c.Saldo)
}
