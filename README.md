# Teste de Nivelamento - Desenvolvedor C# (.NET)

Este repositório contém o desafio técnico para avaliação de desenvolvedores .NET, com foco em construção de APIs RESTful, boas práticas, uso de padrões e domínio da linguagem C#. O teste é dividido em 5 questões com diferentes pesos.

---

## 🎯 Objetivo

Avaliar seus conhecimentos em:
- Git e versionamento
- Estruturação de soluções .NET
- Integração com banco de dados usando Dapper
- Boas práticas com CQRS e Mediator
- Criação de endpoints RESTful com Swagger
- Testes unitários e princípios de idempotência

---

## ✅ Instruções Gerais

1. Clone o repositório e crie sua solução no mesmo.
2. Utilize .NET 6 ou superior.
3. Documente a API com Swagger.
4. Priorize código limpo, boas práticas e organização de camadas (Services, Repositories, DTOs, etc).
5. Caso utilize bibliotecas como Dapper, Mediator, NSubstitute, a avaliação terá pontos adicionais.
6. Entregue o link do repositório versionado (GitHub, GitLab, etc).

---

## 📄 Questões

### 🏦 Questão 1 - Instituição Financeira 
Simulador simples de uma instituição financeira.

---

### 🔄 Questão 2 - Gols - Integração com API Externa 
Implemente um serviço que consulte uma API externa (e.g. football matches) e calcule a quantidade total de gols marcados por um time específico em um determinado ano.

---

### 📁 Questão 3 - Git e Linha de Comando
Siga uma sequência de comandos Git simulando modificações de arquivos com uso do editor `nano`. Ao final, determine corretamente quais arquivos estarão presentes no diretório.

---

### </> Questão 4 - SQL
Escrever um comando select que retorne o assunto, o ano e a quantidade de ocorrências, filtrando apenas assuntos que tenham mais de 3 ocorrências no mesmo ano.

---

### 🏦 Questão 5 - API Bancária com Movimentação e Saldo (Total: 1.000 pontos)
Implemente duas funcionalidades na API de contas correntes:
#### 5.1 - **Movimentação de Conta Corrente** (200 pts)
- POST com dados da movimentação (crédito/débito).
- Validações de negócio (conta ativa, valor positivo, tipo válido).
- Implementar controle de idempotência (100 pts).

#### 5.2 - **Consulta de Saldo da Conta Corrente** (100 pts)
- GET com número da conta.
- Retornar saldo calculado (soma créditos - débitos).

#### Padrões e Qualidade:
- Utilização de Dapper (100 pts)
- Uso de CQRS com Mediator (200 pts)
- Swagger com exemplos (50 pts)
- Testes unitários com mocks (50 pts)
- Estrutura organizada (interfaces, services, enums, mensagens) (100 pts)
- Uso adequado dos recursos da linguagem C# (100 pts)

---

## ⚙️ Tecnologias Sugeridas

- .NET 6+
- Dapper
- CQRS
- MediatR
- Swagger
- Sqlite


