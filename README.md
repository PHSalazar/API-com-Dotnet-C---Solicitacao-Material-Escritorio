# API .NET com Dapper - Solicitação de Materiais de Escritório

## :computer: Descrição

Esta é uma API desenvolvida em .NET e C# para simular 'Solicitação de materiais de escritório'. Ela permite listar materiais cadastrados, consultar materiais por ID e fazer solicitações de materiais com controle de estoque.

## :gear: Funcionalidades

A API oferece três endpoints principais:

- **getAll**: Obtém uma lista de todos os materiais cadastrados no banco de dados.
  
- **getMaterialByID**: Retorna um material específico baseado no ID fornecido. Em caso de erro (ex.: ID inválido), retorna uma mensagem de erro em um modelo de resposta (`ResponseModel`).

- **SolicitarMaterial**: Este endpoint é dividido em duas etapas:
  - **Validação do Material**: Verifica se o material existe no banco de dados (usando o ID) e se a quantidade solicitada está disponível em estoque. Se houver algum problema, retorna um `ResponseModel` com uma mensagem de erro específica.
  - **Atualização do Estoque**: Caso a validação seja bem-sucedida, o estoque é atualizado com a quantidade atualizada (quantidadeBD - quantidadeSolicitada). Em caso de sucesso, retorna um `ResponseModel` com o objeto atualizado, a nova quantidade em estoque e uma mensagem de sucesso.

## :toolbox: Tecnologias e Recursos Utilizados

Neste projeto, trabalhei com tecnologias e ferramentas essenciais para o desenvolvimento de APIs com .NET e otimização de banco de dados:

- **.NET C#**.
- **Dapper**.
- **SQL Server**.
- **Swagger**.

## :memo: Notas

Este projeto foi desenvolvido para fins de estudo e aprimoramento. Pode conter erros e possui características a serem melhoradas.

## :rocket: Tecnologias

- [Swagger](https://swagger.io/docs/)
- [Dapper](https://www.nuget.org/packages/dapper)
- [.NET](https://dotnet.microsoft.com/pt-br/)
