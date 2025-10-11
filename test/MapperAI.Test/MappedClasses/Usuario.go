package MapperAI.Test.MappedClasses

import "fmt"

type Usuario struct {
	Username string
	Email    string
	Password string
}

func NewUsuario(username string, email string, password string) *Usuario {
	return &Usuario{Username: username, Email: email, Password: password}
}

func (u *Usuario) GetUsername() string {
	return u.Username
}

func (u *Usuario) SetUsername(username string) {
	u.Username = username
}

func (u *Usuario) GetEmail() string {
	return u.Email
}

func (u *Usuario) SetEmail(email string) {
	u.Email = email
}

func (u *Usuario) GetPassword() string {
	return u.Password
}

func (u *Usuario) SetPassword(password string) {
	u.Password = password
}

func (u *Usuario) DisplayInfo() {
	fmt.Printf("Username: %s, Email: %s\n", u.Username, u.Email)
}