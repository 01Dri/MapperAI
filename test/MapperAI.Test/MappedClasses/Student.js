namespace MapperAI.Test.MappedClasses {
  export class Student {
    private name: string;
    private age: number;
    private student_id: string;

    constructor(name: string, age: number, student_id: string) {
      this.name = name;
      this.age = age;
      this.student_id = student_id;
    }

    public getName(): string {
      return this.name;
    }

    public getAge(): number {
      return this.age;
    }

    public getStudentId(): string {
      return this.student_id;
    }

    public isAdult(): boolean {
      return this.age >= 18;
    }
  }
}
