# Outsera Back

Este é o backend do projeto Outsera, desenvolvido em ASP.NET Core.

## Requisitos

- .NET 8.0 SDK
- Visual Studio Code ou Visual Studio 2022

## Configuração do Projeto

1. Clone o repositório:

    ```sh
    git clone https://github.com/seu-usuario/outsera-back.git
    cd outsera-back
    ```

2. Restaure as dependências do projeto:

    ```sh
    dotnet restore
    ```

## Executando o Projeto

1. Compile o projeto:

    ```sh
    dotnet build
    ```

2. Execute o projeto:

    ```sh
    dotnet run
    ```

3. A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

## Executando os Testes de Integração

1. Navegue até o diretório do projeto:

    ```sh
    cd outsera-back
    ```

2. Execute os testes:

    ```sh
    dotnet test
    ```

## Estrutura do Projeto

- [Controllers](http://_vscodecontentref_/0): Contém os controladores da API.
- [Services](http://_vscodecontentref_/1): Contém os serviços utilizados pelos controladores.
- [IntegrationTests](http://_vscodecontentref_/2): Contém os testes de integração.

## Exemplo de Uso

### Endpoint: `GET /api/movies/awards-interval`

Retorna o produtor com maior e menor intervalo entre prêmios consecutivos.

#### Respostas

- `200 OK`: Retorna os produtores com os intervalos entre prêmios.
- `400 Bad Request`: Se houver erro na requisição.

#### Exemplo de Resposta

```json
{
    "Min": {
        "Producer": "Producer A",
        "Interval": 1,
        "PreviousWin": 2000,
        "FollowingWin": 2001
    },
    "Max": {
        "Producer": "Producer B",
        "Interval": 10,
        "PreviousWin": 1990,
        "FollowingWin": 2000
    }
}