namespace MapperAI.Test.MappedClasses {
  export class Carro {
    private marca: string;
    private modelo: string;
    private ano: number;

    constructor(marca: string, modelo: string, ano: number) {
      this.marca = marca;
      this.modelo = modelo;
      this.ano = ano;
    }

    public getMarca(): string {
      return this.marca;
    }

    public getModelo(): string {
      return this.modelo;
    }

    public getAno(): number {
      return this.ano;
    }

    public exibirInformacoes(): void {
      console.log(`Marca: ${this.marca}`);
      console.log(`Modelo: ${this.modelo}`);
      console.log(`Ano: ${this.ano}`);
    }

    public isAntigo(): boolean {
      const anoAtual = new Date().getFullYear();
      return (anoAtual - this.ano) > 20;
    }
  }
}
