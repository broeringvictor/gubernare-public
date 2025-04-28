# Sistema de Gestão Processual Jurídica

![.NET 9](https://img.shields.io/badge/.NET-9.0-blue)
![Angular 19](https://img.shields.io/badge/Angular-19-DD0031)
![Python 3.12](https://img.shields.io/badge/Python-3.12-3776AB)
![Redis 7.2](https://img.shields.io/badge/Redis-7.2-DC382D)
[![Licença: MIT](https://img.shields.io/badge/Licença-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Sistema moderno para gestão de processos judiciais confidenciais, desenvolvido em arquitetura de microsserviços com orquestração de containers. Integra **ASP.NET Core 9**, **Angular 19** e **Python 3.12** para oferecer desempenho escalável e seguro.

---

## ✨ Principais Funcionalidades

- **Busca Automatizada de Processos**\
  Microsserviço em Python 3.12 com Selenium para raspagem segura de dados em sistemas judiciais.
- **Orquestração Centralizada**\
  Gerência unificada de containers via **.NET Aspire 1.1 (AppHost)**.
- **Autenticação JWT**\
  Segurança reforçada com hash de senhas (salt + PBKDF2) e criptografia AES-256.
- **Arquitetura Desacoplada**\
  Componentes modulares prontos para evolução para microsserviços independentes.
- **Cache em Tempo Real**\
  Redis garante respostas rápidas em operações de alta frequência.

---

## 🏗️ Visão da Arquitetura



| Componente         | Stack                           | Responsabilidade                               |
| ------------------ | ------------------------------- | ---------------------------------------------- |
| **AppHost**        | .NET Aspire                | Orquestração de containers e dependências      |
| **API**            | ASP.NET Core 9                  | Autenticação JWT e lógica de negócios          |
| **Frontend**       | Angular 19 + TypeScript     | Interface SPA com controle de acesso por roles |
| **Search Service** | Python 3.12 + Flask | Automação para consulta processual, retirado do projeto por possível comercialização.   |
| **Persistência**   | Entity Framework Core        | Repositórios SQL Server         |
| **Cache**          | Redis                           | Cache distribuído para alta performance        |


---

## 🚀 Primeiros Passos

### Pré-requisitos

| Ferramenta         | Versão mínima recomendada | 
| ------------------ | ------------------------- | 
| **.NET SDK**       | 9.0.\*                    | 
| **Node.js**        | 20 LTS                    | 
| **Angular CLI**    | 19.x                      | 
| **Python**         | 3.12.x                    |   
| **Docker Desktop** | 4.30+                     | 
 

### Instalação

```bash
git clone https://github.com/broeringvictor/gubernare-public

```

#### Configurar Secrets

```bash
cd src/Guabernare.Api
dotnet user-secrets set "SendGrid:ApiKey" "<SUA_CHAVE_SENDGRID>"
```

#### Executar via AppHost

```bash
dotnet run --project AppHost/AppHost.csproj
```

O AppHost inicializa API, Angular, Redis e o serviço Python em containers isolados.

---

## 🔧 Decisões Técnicas

- **Por que .NET Aspire?** Orquestra containers localmente e em cloud com observabilidade integrada, reduzindo boilerplate.
- **Python + Selenium em vez de .NET** evita incompatibilidades frequentes do ChromeDriver e acelera ajustes quando os tribunais alteram o HTML.
- **Segurança:** senhas com PBKDF2-HMAC-SHA256 (20 000 iterações) com saltpassword e dados sensíveis cifrados em AES-256-GCM.

---

## 📜 Licença

MIT — consulte o arquivo LICENSE.

> **Aviso:** O código do microsserviço de busca não está incluído neste repositório por razões comerciais. A documentação técnica completa é fornecida sob requisição.

---



