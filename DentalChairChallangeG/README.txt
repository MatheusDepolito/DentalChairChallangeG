

# Sistema de Cadastro de Cadeiras de Dentista

Este projeto � uma API para gerenciar cadeiras de dentista. Ele utiliza .NET para o backend e MySQL como banco de dados. A seguir, est�o os passos para configurar e executar o projeto.

## Pr�-requisitos

- Docker e Docker Compose
- .NET SDK

## Configura��o e Execu��o

### 1. Configurar o Banco de Dados MySQL com Docker

Primeiro, voc� precisa iniciar o container do MySQL usando o Docker Compose. Certifique-se de que o Docker e o Docker Compose est�o instalados e em execu��o.

1. Navegue at� o diret�rio onde est� localizado o arquivo `docker-compose.yml`.

2. Execute o comando para iniciar o MySQL:

    ```bash
    docker-compose up -d
    ```

   Isso ir� iniciar o container do MySQL em segundo plano.

### 2. Rodar o Projeto .NET

Depois que o banco de dados MySQL estiver em execu��o, voc� pode iniciar o projeto .NET. O comando `dotnet run` ir� automaticamente aplicar as migrations ao banco de dados.

1. Navegue at� o diret�rio do projeto .NET.

2. Execute o comando para iniciar o projeto:

    ```bash
    dotnet run
    ```

   O servidor estar� dispon�vel e as migrations ser�o aplicadas ao banco de dados MySQL.

### 3. Testar a API

Para testar a API, voc� pode usar o arquivo `.http` que cont�m todos os endpoints configurados. Certifique-se de que voc� tem o Visual Studio Code e a extens�o "REST Client" instalada para enviar as solicita��es HTTP diretamente do arquivo.

1. Abra o arquivo `DentalChairChallangeG.http` no Visual Studio Code.

2. Clique no bot�o "Send Request" acima de cada bloco de c�digo para enviar as solicita��es e ver as respostas da API.

### Arquivo `DentalChairChallangeG.http`

O arquivo `DentalChairChallangeG.http` inclui exemplos de como testar os endpoints da API. Aqui est� um exemplo de como voc� pode usar o arquivo:

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

# Obt�m todas as cadeiras
GET {{DentalChairChallangeG_HostAddress}}/chair/getAll

# Obt�m uma cadeira por ID
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

