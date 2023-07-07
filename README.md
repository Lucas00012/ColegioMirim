
# Colégio Mirim

Projeto de desafio utilizando .NET C#




## Rodando o projeto

Para rodar esse projeto, insira a string de conexão do seu banco de dados SQL Server em `ConnectionStrings:Default` no arquivo `appsettings.Development.json` e também `appsettings.Testing.json` caso deseje rodar os testes de integração. As tabelas serão criadas automaticamente em ambos os casos

Há um login padrão de administrador com as credenciais `admin@colegiomirim.com` e `@Aa123456`
## Funcionalidades

- Adicionar/editar alunos
- Adicionar/editar turmas
- Adicionar/editar vínculos de alunos e turmas
- Alterar senha
- Editar perfil
- Autenticação JWT e autorização baseada em roles
- Paginação e pesquisa de alunos, turmas e vínculos de aluno/turma
- "Utilização" das inativações:

-- Ao desativar o aluno, o mesmo perde acesso ao sistema;

-- Ao desativar turma ou vínculo de aluno/turma, o aluno não consegue mais visualizar a turma em questão.


## Demonstração

![colegio-mirim](https://github.com/Lucas00012/Facebook-Video-Downloader-2022/assets/51132386/105ae7d1-5018-476b-a5f9-5d60cf7d5417)


## Autores

- [@Lucas00012](https://www.github.com/octokatherine)

