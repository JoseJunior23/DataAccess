using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=school-db;User ID=sa;Password=110620#Db;Trusted_Connection=False; TrustServerCertificate=True;";


using (var connection = new SqlConnection(connectionString))
{
  // CreateManyCategory(connection);
  // DeleteCategory(connection);
  // UpdateCategory(connection);
  // ListCategories(connection);
  // CreateCategory(connection);
  // ExecuteProcedure(connection);
  ExecuteReadProcedure(connection);
}

static void ListCategories(SqlConnection connection)
{
  var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
  foreach (var item in categories)
  {
    Console.WriteLine($"{item.Id} - {item.Title}");
  }

}

static void CreateCategory(SqlConnection connection)
{
  var category = new Category();
  category.Id = Guid.NewGuid();
  category.Title = "Amazon AWS";
  category.Url = "amazon";
  category.Description = "Categoria destinada a serviços Aws Cloud";
  category.Order = 8;
  category.Summary = "AWS Cloud";
  category.Featured = false;

  var insertSql = "INSERT INTO [Category] VALUES(@id, @title, @url, @summary, @order, @description, @featured)";

  var rows = connection.Execute(insertSql, new
  {
    category.Id,
    category.Title,
    category.Url,
    category.Summary,
    category.Order,
    category.Description,
    category.Featured
  });
  Console.WriteLine($"{rows} linhas inseridas");
}

static void UpdateCategory(SqlConnection connection)
{
  var updateQuery = "UPDATE [Category]  SET [Title]=@title WHERE [Id]=@id ";
  var rows = connection.Execute(updateQuery, new
  {
    id = "af3407aa-11ae-4621-a2ef-2028b85507c4",
    title = "Frontend 2024"
  });
  Console.WriteLine($"{rows} linhas atualizadas");
}

static void DeleteCategory(SqlConnection connection)
{
  var deleteQuery = "DELETE [Category] WHERE [Id]=@id ";
  var rows = connection.Execute(deleteQuery, new
  {
    id = "9ace7460-0c19-45b6-9480-86b1097c946d",
  });
  Console.WriteLine($"{rows} linha deletada");
}

static void CreateManyCategory(SqlConnection connection)
{
  var category = new Category();
  category.Id = Guid.NewGuid();
  category.Title = "Amazon AWS";
  category.Url = "amazon";
  category.Description = "Categoria destinada a serviços Aws Cloud";
  category.Order = 8;
  category.Summary = "AWS Cloud";
  category.Featured = false;

  var category2 = new Category();
  category2.Id = Guid.NewGuid();
  category2.Title = "Nova Categoria";
  category2.Url = "categoria";
  category2.Description = "Categoria nova";
  category2.Order = 9;
  category2.Summary = "Categoria";
  category2.Featured = false;

  var insertSql = "INSERT INTO [Category] VALUES(@id, @title, @url, @summary, @order, @description, @featured)";

  var rows = connection.Execute(insertSql, new[]{
    new
    {
      category.Id,
      category.Title,
      category.Url,
      category.Summary,
      category.Order,
      category.Description,
      category.Featured
    },
    new
    {
      category2.Id,
      category2.Title,
      category2.Url,
      category2.Summary,
      category2.Order,
      category2.Description,
      category2.Featured
    }
  });
  Console.WriteLine($"{rows} linhas inseridas");
}

static void ExecuteProcedure(SqlConnection connection)
{
  var procedure = "spDeleteStudent";
  var pars = new { StudentId = "" };
  var affectedRow = connection.Execute(procedure, pars, commandType: System.Data.CommandType.StoredProcedure);

  Console.WriteLine($"{affectedRow} linhas afetadas");
}

static void ExecuteReadProcedure(SqlConnection connection)
{
  var procedure = "spGetCoursesByCategory";
  var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
  var courses = connection.Query(procedure, pars, commandType: System.Data.CommandType.StoredProcedure);

  foreach (var item in courses)
  {
    Console.WriteLine(item.Title);
  }
}



