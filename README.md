# Royalties Payments
Aplicação desenvolvida em **C#/.NET** para processamento e cálculo de pagamentos de royalties a partir de dados de assinantes, streams, músicas, compositores e performers.
O projeto recebe os dados através de arquivos JSON, processa as informações, calcula os royalties de cada participante e gera um arquivo CSV com os pagamentos.

## 🚀 Funcionalidades
* Leitura de dados de tracks através de arquivo JSON;
* Leitura de dados de subscribers através de arquivo JSON;
* Associação entre:
  * Tracks;
  * Compositions;
  * Composers;
  * Performers;
  * Subscribers;
  * Streams
* Cálculo dos royalties de compositores;
* Cálculo dos royalties de performers;
* Distribuição do valor das assinaturas entre compositores e performers;
* Geração de arquivo CSV com os pagamentos;
* Testes automatizados das principais funcionalidades.

## 🏗️ Estrutura do projeto

```text
Royalties-payments/
│
├── Data/
│   ├── tracks-metadata.json
│   └── subscribers.json
│
├── Models/
│   ├── Track.cs
│   ├── Composition.cs
│   ├── Composer.cs
│   ├── Performer.cs
│   ├── Subscriber.cs
│   └── Streaming.cs
│
├── Reader/
│   ├── TrackReader.cs
│   └── SubscriberReader.cs
│
├── Service/
│   ├── TrackService.cs
│   └── RoyaltyCalculator.cs
│
├── Writer/
│   └── CreateFileCSV.cs
│
├── Program.cs
│
└── Royalties-payments-Testes/
    └── Testes automatizados
```

## 🔄 Fluxo da aplicação

```text
Arquivos JSON
     │
     ▼
  Readers
     │
     ▼
 TrackService
     │
     ▼
TrackInformation
     │
     ▼
RoyaltyCalculator
     │
     ▼
RoyaltyPayment
     │
     ▼
CreateFileCSV
     │
     ▼
Arquivo CSV
```

## 💰 Cálculo dos royalties
O valor pago pelo assinante é dividido entre:
* **50% para compositores**
* **50% para performers**

Os valores são calculados considerando os streams realizados pelo assinante e as pessoas relacionadas a cada track.
Quando uma track possui mais de um compositor, o valor destinado aos compositores é distribuído entre eles.
Da mesma forma, quando aplicável, o valor destinado aos performers é distribuído entre os performers relacionados à track.

## 📄 Arquivo de saída
Ao finalizar o processamento, a aplicação gera um arquivo CSV contendo:

```text
IdRecebedor;TipoRoyalty;TotalRoyalties
```

Exemplo:
```text
8f4...;Composer;12,50
3a2...;Performer;12,50
```
O `IdRecebedor` identifica o compositor ou performer que receberá o pagamento.

## 🧪 Testes

O projeto possui testes automatizados utilizando **xUnit**.
Os testes abrangem principalmente:

* `TrackService`
  * Busca de tracks
  * Associação com compositions
  * Associação com composers
  * Associação com performers
  * Tratamento de dados não encontrados

* `RoyaltyCalculator`
  * Cálculo dos royalties
  * Processamento de múltiplos streams
  * Distribuição dos valores entre os participantes

* `TrackReader`
  * Leitura do JSON
  * JSON inválido
  * Arquivo vazio
  * Tratamento de exceções

* `SubscriverReader`
  * Leitura do JSON
  * JSON inválido
  * Arquivo vazio
  * Tratamento de exceções

* `CreateFileCSV`
  * Criação do arquivo CSV
  * Escrita dos dados
  * Validação da geração do arquivo

Para executar os testes:

```bash
dotnet test
```

## ▶️ Como executar

### Pré-requisitos
* .NET SDK
* Windows, Linux ou macOS para execução pelo código-fonte

Clone o repositório:
```bash
git clone <URL_DO_REPOSITORIO>
```
Entre na pasta do projeto:
```bash
cd Royalties-payments
```
Execute a aplicação:
```bash
dotnet run
```

## 📦 Executável
Também está disponível uma versão compilada para Windows na seção **Releases** do GitHub.
Para executar a versão publicada, baixe os arquivos da Release e mantenha a estrutura de arquivos necessária, incluindo os arquivos JSON utilizados pela aplicação.

## 🛠️ Tecnologias utilizadas
* **C#**
* **.NET**
* **xUnit**
* **System.Text.Json**
* **Git**
* **GitHub**

## 📚 Objetivo do projeto
Este projeto foi desenvolvido como parte do meu processo de aprendizado e prática em desenvolvimento **C#/.NET**, com foco em:
* Orientação a objetos;
* Separação de responsabilidades;
* Leitura e processamento de JSON;
* Manipulação de coleções;
* Regras de negócio;
* Testes automatizados;
* Tratamento de exceções;
* Geração de arquivos CSV;
* Organização de uma aplicação .NET

## 👩‍💻 Autora
**Bianca Barrancos Teixeira**
Desenvolvedora .NET Júnior | C# | VB.NET | SQL Server