<?php

namespace MapperAI\Test\MappedClasses;

class Usuario {
    private string $username;
    private string $email;
    private string $password;

    // Construtor
    public function __construct(string $username, string $email, string $password) {
        $this->username = $username;
        $this->email = $email;
        $this->password = $password;
    }

    public function getUsername(): string {
        return $this->username;
    }

    public function setUsername(string $username): void {
        $this->username = $username;
    }

    public function getEmail(): string {
        return $this->email;
    }

    public function setEmail(string $email): void {
        $this->email = $email;
    }

    public function getPassword(): string {
        return $this->password;
    }

    public function setPassword(string $password): void {
        $this->password = $password;
    }

    public function displayInfo(): void {
        echo "Username: " . $this->username . ", Email: " . $this->email . "\n";
    }
}
