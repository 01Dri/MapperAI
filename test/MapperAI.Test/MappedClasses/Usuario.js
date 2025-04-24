namespace MapperAI.Test.MappedClasses {
  export class Usuario {
    private username: string;
    private email: string;
    private password: string;

    constructor(user: string, mail: string, pass: string) {
      this.username = user;
      this.email = mail;
      this.password = pass;
    }

    public getUsername(): string {
      return this.username;
    }

    public setUsername(user: string): void {
      this.username = user;
    }

    public getEmail(): string {
      return this.email;
    }

    public setEmail(mail: string): void {
      this.email = mail;
    }

    public getPassword(): string {
      return this.password;
    }

    public setPassword(pass: string): void {
      this.password = pass;
    }

    public displayInfo(): void {
      console.log(`Username: ${this.username}, Email: ${this.email}`);
    }
  }
}
