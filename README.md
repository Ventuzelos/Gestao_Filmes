# Gestão de Filmes

Projeto desenvolvido em C# utilizando arquitetura em camadas.

## Objetivo

Criar uma aplicação para gestão de filmes utilizando:

Arquitetura em Camadas
Interfaces
Regras de Negócio
Persistência de Dados
Git e GitHub
---

# Estrutura do Projeto
O projeto está dividido em 4 camadas:

### UI (Gestao_Filmes.AppConsole)
Responsável pela interação com o utilizador.

### Business (Gestao_Filmes.Business)
Responsável pelas validações e regras de negócio.

### Data (Gestao_Filmes.Data)
Responsável pela persistência dos dados.

### Domain (Gestao_Filmes.Domain)
Responsável pelas entidades e interfaces.
---

# Entidades
## Filme
Id
Título
Ano
Língua
Classificação
Categoria
Realizador

## Categoria
Id
Nome

## Realizador
Id
Nome
País
---

# Funcionalidades
### Filmes
Adicionar filme
Listar filmes
Procurar filme
Atualizar classificação
Remover filme

### Categorias
Adicionar categoria
Listar categorias
Remover categoria

### Realizadores
Adicionar realizador
Listar realizadores
Remover realizador
---

# Regras de Negócio
### Filme
O título é obrigatório
Não permite filmes com o mesmo título
A classificação tem de estar entre 0 e 5

### Categoria
O nome é obrigatório
Não permite categorias repetidas

### Realizador
O nome é obrigatório
O país é obrigatório
Não permite realizadores repetidos
---

# Persistência de Dados
Durante o desenvolvimento foi utilizada persistência em memória através de listas.
Na parte final do projeto foi implementada persistência em SQLite, permitindo guardar os dados mesmo após fechar a aplicação.
---

# Funcionalidades Extra
* Mostrar total de filmes
* Listar filmes por categoria
* Listar filmes por realizador
* Mostrar o filme com melhor classificação

---

# Tecnologias Utilizadas

* C#
* SQLite
* Git
* GitHub
