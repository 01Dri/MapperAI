<?php

namespace MapperAI\Test\MappedClasses;

class Student
{
    private string $name;
    private int $age;
    private string $student_id;

    public function __construct(string $name, int $age, string $student_id)
    {
        $this->name = $name;
        $this->age = $age;
        $this->student_id = $student_id;
    }

    public function getName(): string
    {
        return $this->name;
    }

    public function getAge(): int
    {
        return $this->age;
    }

    public function getStudentId(): string
    {
        return $this->student_id;
    }

    public function isAdult(): bool
    {
        return $this->age >= 18;
    }
}
