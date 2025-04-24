namespace MapperAI.Test.MappedClasses {
  export class ContaBancaria {
    private numero: string;
    private titular: string;
    private saldo: number;

    constructor(numero: string, titular: string, saldo: number = 0) {
      this.numero = numero;
      this.titular = titular;
      this.saldo = saldo;
    }

    public depositar(valor: number): string {
      if (valor > 0) {
        this.saldo += valor;
        return `Depósito de R$${valor} realizado com sucesso. Novo saldo: R$${this.saldo}.`;
      } else {
        return "Valor de depósito inválido.";
      }
    }

    public sacar(valor: number): string {
      if (valor > 0 && valor <= this.saldo) {
        this.saldo -= valor;
        return `Saque de R$${valor} realizado com sucesso. Saldo restante: R$${this.saldo}.`;
      } else {
        return "Saldo insuficiente ou valor de saque inválido.";
      }
    }

    public consultarSaldo(): string {
      return `Saldo atual: R$${this.saldo}.`;
    }
  }
}
