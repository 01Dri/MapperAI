package MapperAI.Test.MappedClasses

import "time"

type Carro struct {
	Marca  string
	Modelo string
	Ano    int
}

func NewCarro(marca string, modelo string, ano int) *Carro {
	return &Carro{Marca: marca, Modelo: modelo, Ano: ano}
}

func (c *Carro) GetMarca() string {
	return c.Marca
}

func (c *Carro) GetModelo() string {
	return c.Modelo
}

func (c *Carro) GetAno() int {
	return c.Ano
}

func (c *Carro) ExibirInformacoes() {
	println("Marca:", c.Marca)
	println("Modelo:", c.Modelo)
	println("Ano:", c.Ano)
}

func (c *Carro) IsAntigo() bool {
	anoAtual := time.Now().Year()
	return (anoAtual - c.Ano) > 20
}