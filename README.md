#Gestão de Filmes

Projeto desenvolvido em C# utilizando arquitetura em camadas.

## Objetivo

Desenvolver uma aplicação de gestão de filmes aplicando:

- Arquitetura em Camadas
- Interfaces
- Regras de negócio
- Persistência de Dados
- Git e GitHub

---

# Estrutura do Projeto

A solução encontra-se dividida em 4 camadas:

## UI (Gestao_Filmes.appConsole)

Responsável pela interação com o utilizador através da consola.

## Business (Gestao_Filmes.Business)

Responsável pelas regras de negócio e validações.

## Data (Gestao_Filmes.Data)

Responsável pela persistência de dados em memória.

## Domain (Gestao_Filmes.Domain)

Responsável pelas entidades e interfaces do sistema.

---

# Funcionalidades Implementadas (Parte 1)

## Gestão de Filmes

- Adicionar filme
- Listar filmes
- Procurar filme por título
- Remover filme

---

# Entidade Filme

Cada filme possui:

- Id
- Título
- Ano
- Língua
- Classificação

---

# Regras de negócio

## Título
- obrigatório
- não permite duplicados

## Classificação
- deve estar entre 0 e 5
- possibilidade e números decimais

---

# Persistência

Nesta primeira fase o sistema utiliza persistência em memória através de:
C#
List<Filme>
