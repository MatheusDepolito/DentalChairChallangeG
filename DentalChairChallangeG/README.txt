

# Sistema de Cadastro de Cadeiras de Dentista

Este projeto é uma API para gerenciar cadeiras de dentista. Ele utiliza .NET para o backend e MySQL como banco de dados. A seguir, estão os passos para configurar e executar o projeto.

## Pré-requisitos

- Docker e Docker Compose
- .NET SDK

## Configuração e Execução

### 1. Configurar o Banco de Dados MySQL com Docker

Primeiro, você precisa iniciar o container do MySQL usando o Docker Compose. Certifique-se de que o Docker e o Docker Compose estão instalados e em execução.

1. Navegue até o diretório onde está localizado o arquivo `docker-compose.yml`.

2. Execute o comando para iniciar o MySQL:

    ```bash
    docker-compose up -d
    ```

   Isso irá iniciar o container do MySQL em segundo plano.

### 2. Rodar o Projeto .NET

Depois que o banco de dados MySQL estiver em execução, você pode iniciar o projeto .NET. O comando `dotnet run` irá automaticamente aplicar as migrations ao banco de dados.

1. Navegue até o diretório do projeto .NET.

2. Execute o comando para iniciar o projeto:

    ```bash
    dotnet run
    ```

   O servidor estará disponível e as migrations serão aplicadas ao banco de dados MySQL.

### 3. Testar a API

Para testar a API, você pode usar o arquivo `.http` que contém todos os endpoints configurados. Certifique-se de que você tem o Visual Studio Code e a extensão "REST Client" instalada para enviar as solicitações HTTP diretamente do arquivo.

1. Abra o arquivo `DentalChairChallangeG.http` no Visual Studio Code.

2. Clique no botão "Send Request" acima de cada bloco de código para enviar as solicitações e ver as respostas da API.

### Arquivo `DentalChairChallangeG.http`

O arquivo `DentalChairChallangeG.http` inclui exemplos de como testar os endpoints da API. Aqui está um exemplo de como você pode usar o arquivo:

```http
# Cria uma nova cadeira
POST {{DentalChairChallangeG_HostAddress}}/chair/create
Accept: application/json
Content-Type: application/json

{
    "Number": "111",
    "Description": "Test5",
    "IsAvailable": true
}

# Obtém todas as cadeiras
GET {{DentalChairChallangeG_HostAddress}}/chair/getAll

# Obtém uma cadeira por ID
GET {{DentalChairChallangeG_HostAddress}}/chair/getChairById/1

# Atualiza uma cadeira existente
PUT {{DentalChairChallangeG_HostAddress}}/chair/1
Accept: application/json
Content-Type: application/json

{
    "Number": "222",
    "Description": "Updated Test",
    "IsAvailable": false
}

# Exclui uma cadeira
DELETE {{DentalChairChallangeG_HostAddress}}/chair/1

