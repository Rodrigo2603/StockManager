# EstoqueAPI

API REST desenvolvida em ASP.NET Core para gerenciamento de produtos e categorias de um sistema de estoque.

## 🛠 Tecnologias

- ASP.NET Core 8.0  
- Entity Framework Core  
- PostgreSQL  
- Swagger (Swashbuckle)  

## 📦 Funcionalidades

- **CRUD de Produtos**  
  - Nome, Quantidade, Preço e Categoria associada  
- **CRUD de Categorias**  
  - Nome único e vinculação com produtos  
- **Validações**  
  - Nome único para produtos e categorias  
  - Impede exclusão de categorias com produtos associados  

## ⚙️ Configuração

Crie um arquivo `appsettings.json` com sua string de conexão (um template já foi adicionado ao repositório):

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=<database_name>;Username=<your_username>;Password=<your_password>"
  }
}
```

## 🚀 Rodando o Projeto

```bash
# 1. Restaure os pacotes
dotnet restore

# 2. Atualize o banco de dados (cria o schema com base nas migrations)
dotnet ef database update

# 3. Execute a aplicação
dotnet run
```

Depois, abra no navegador:  
👉 [http://localhost:5290/swagger](http://localhost:5290/swagger)
