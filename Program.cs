




// using Microsoft.EntityFrameworkCore;
// using Pomelo.EntityFrameworkCore.MySql;
// using TodoApi;
// using MySqlConnector;

// var builder = WebApplication.CreateBuilder(args);

// // הגדרת CORS (פתוח לכל - לפיתוח בלבד! *לייצור יש להגדיר דומיינים ספציפיים*)
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAllOrigins",
//         policy =>
//         {
//             policy.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader();
//         });
// });

// // בדיקת מחרוזת החיבור וטיפול בשגיאה
// var connectionString = builder.Configuration.GetConnectionString("ToDoDB");
// if (string.IsNullOrEmpty(connectionString))
// {
//     var errorMessage = "Connection string 'ToDoDB' not found in appsettings.json. Please ensure it is correctly configured.";
//     builder.Logging.AddConsole(); // הוספת לוג קונסול כדי לראות את השגיאה
//     var logger = builder.Build().Services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(errorMessage);
//     throw new InvalidOperationException(errorMessage); // זריקת חריגה עוצרת את האפליקציה
// }

// // הזרקת DbContext
// try
// {
//     builder.Services.AddDbContext<ToDoDbContext>(options =>
//         options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// }
// catch (Exception ex)
// {
//     var errorMessage = $"Error configuring DbContext: {ex.Message}";
//     builder.Logging.AddConsole();
//     var logger = builder.Build().Services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(ex, errorMessage);
//     throw new InvalidOperationException(errorMessage, ex);
// }


// // הגדרת Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // הפעלת CORS
// app.UseCors("AllowAllOrigins");

// // if (app.Environment.IsDevelopment())
// // {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// // }

// app.UseHttpsRedirection();

// // *** הגדרת ה-routes ***

// // פונקציה כללית לטיפול בשגיאות (DRY)
// async Task<IResult> HandleDbOperationAsync(Func<ToDoDbContext, Task<IResult>> operation, ILogger logger, string operationName)
// {
//     try
//     {
//         using (var scope = app.Services.CreateScope())
//         {
//             var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
//             return await operation(db);
//         }
//     }
//     catch (MySqlException mysqlEx)
//     {
//         logger.LogError(mysqlEx, $"שגיאת MySQL בעת {operationName}: {{Message}}", mysqlEx.Message);
//         return Results.Problem($"שגיאת מסד נתונים: {mysqlEx.Message}", statusCode: 500);
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, $"שגיאה כללית בעת {operationName}: {{Message}}", ex.Message);
//         return Results.Problem($"אירעה שגיאה בעת {operationName}.", statusCode: 500);
//     }
// }

// // שליפת כל המשימות (GET)
// app.MapGet("/items", async (ILogger<Program> logger) =>
//     await HandleDbOperationAsync(async db =>
//     {
//         var items = await db.Items.ToListAsync();
//         return Results.Ok(items);
//     }, logger, "שליפת פריטים"));

// // הוספת משימה חדשה (POST)
// app.MapPost("/items", async (Item item, ILogger<Program> logger) =>
//     await HandleDbOperationAsync(async db =>
//     {
//         db.Items.Add(item);
//         await db.SaveChangesAsync();
//         return Results.Created($"/items/{item.Id}", item);
//     }, logger, "הוספת פריט"));

// // עדכון משימה (PUT)
// app.MapPut("/items/{id}", async (int id, Item item, ILogger<Program> logger) =>
//     await HandleDbOperationAsync(async db =>
//     {
//         var existingItem = await db.Items.FindAsync(id);
//         if (existingItem == null)
//         {
//             return Results.NotFound();
//         }

//         existingItem.Name = item.Name;
//         existingItem.IsComplite = item.IsComplite;
//         await db.SaveChangesAsync();
//         return Results.NoContent();
//     }, logger, $"עדכון פריט עם מזהה {id}"));

// // מחיקת משימה (DELETE)
// app.MapDelete("/items/{id}", async (int id, ILogger<Program> logger) =>
//     await HandleDbOperationAsync(async db =>
//     {
//         var item = await db.Items.FindAsync(id);
//         if (item == null)
//         {
//             return Results.NotFound();
//         }

//         db.Items.Remove(item);
//         await db.SaveChangesAsync();
//         return Results.NoContent();
//     }, logger, $"מחיקת פריט עם מזהה {id}"));

// // app.MapGet("/", () => Results.Redirect("/swagger"));
// app.MapGet("/",()=>"Auther Server is running");
// app.Run();




// using Microsoft.EntityFrameworkCore;
// using Pomelo.EntityFrameworkCore.MySql;
// using TodoApi;
// using MySqlConnector;

// var builder = WebApplication.CreateBuilder(args);

// // הגדרת CORS (פתוח לכל - לפיתוח בלבד! *לייצור יש להגדיר דומיינים ספציפיים*)
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAllOrigins",
//         policy =>
//         {
//             policy.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader();
//         });
// });

// // בדיקת מחרוזת החיבור וטיפול בשגיאה
// var connectionString = builder.Configuration.GetConnectionString("ToDoDB");
// if (string.IsNullOrEmpty(connectionString))
// {
//     var errorMessage = "Connection string 'ToDoDB' not found in appsettings.json. Please ensure it is correctly configured.";
//     builder.Logging.AddConsole(); // הוספת לוג קונסול כדי לראות את השגיאה
//     var logger = builder.Build().Services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(errorMessage);
//     throw new InvalidOperationException(errorMessage); // זריקת חריגה עוצרת את האפליקציה
// }

// // הזרקת DbContext
// try
// {
//     builder.Services.AddDbContext<ToDoDbContext>(options =>
//         options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// }
// catch (Exception ex)
// {
//     var errorMessage = $"Error configuring DbContext: {ex.Message}";
//     builder.Logging.AddConsole();
//     var logger = builder.Build().Services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(ex, errorMessage);
//     throw new InvalidOperationException(errorMessage, ex);
// }

// // הגדרת Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // הפעלת CORS
// app.UseCors("AllowAllOrigins");

// // if (app.Environment.IsDevelopment())
// // {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// // }

// app.UseHttpsRedirection();

// // *** הגדרת ה-routes ***

// // פונקציה כללית לטיפול בשגיאות (DRY)
// async Task<IResult> HandleDbOperationAsync(Func<ToDoDbContext, Task<IResult>> operation, ILogger logger, string operationName)
// {
//     try
//     {
//         using (var scope = app.Services.CreateScope())
//         {
//             var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
//             return await operation(db);
//         }
//     }
//     catch (MySqlException mysqlEx)
//     {
//         logger.LogError(mysqlEx, $"שגיאת MySQL בעת {operationName}: {{Message}}", mysqlEx.Message);
//         return Results.Problem($"שגיאת מסד נתונים: {mysqlEx.Message}", statusCode: 500);
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, $"שגיאה כללית בעת {operationName}: {{Message}}", ex.Message);
//         return Results.Problem($"אירעה שגיאה בעת {operationName}.", statusCode: 500);
//     }
// }

// // שליפת כל המשימות (GET)
// app.MapGet("/items", async (ILogger<Program> logger) =>
// {
//     logger.LogInformation("Request: GET /items"); // לוג בקשה
//     return await HandleDbOperationAsync(async db =>
//     {
//         var items = await db.Items.ToListAsync();
//         logger.LogInformation($"Response: GET /items - {items.Count} items found"); // לוג תגובה
//         return Results.Ok(items);
//     }, logger, "שליפת פריטים");
// });

// // הוספת משימה חדשה (POST)
// app.MapPost("/items", async (Item item, ILogger<Program> logger) =>
// {
//     logger.LogInformation($"Request: POST /items - Name: {item.Name}, IsComplite: {item.IsComplite}"); // לוג בקשה
//     return await HandleDbOperationAsync(async db =>
//     {
//         db.Items.Add(item);
//         await db.SaveChangesAsync();
//         logger.LogInformation($"Response: POST /items - Item added with ID: {item.Id}"); // לוג תגובה
//         return Results.Created($"/items/{item.Id}", item);
//     }, logger, "הוספת פריט");
// });

// // עדכון משימה (PUT)
// app.MapPut("/items/{id}", async (int id, Item item, ILogger<Program> logger) =>
// {
//     logger.LogInformation($"Request: PUT /items/{id} - Name: {item.Name}, IsComplite: {item.IsComplite}"); // לוג בקשה
//     return await HandleDbOperationAsync(async db =>
//     {
//         var existingItem = await db.Items.FindAsync(id);
//         if (existingItem == null)
//         {
//             logger.LogInformation($"Response: PUT /items/{id} - Item not found"); // לוג תגובה
//             return Results.NotFound();
//         }

//         existingItem.Name = item.Name;
//         existingItem.IsComplite = item.IsComplite;
//         await db.SaveChangesAsync();
//         logger.LogInformation($"Response: PUT /items/{id} - Item updated"); // לוג תגובה
//         return Results.NoContent();
//     }, logger, $"עדכון פריט עם מזהה {id}");
// });

// // מחיקת משימה (DELETE)
// app.MapDelete("/items/{id}", async (int id, ILogger<Program> logger) =>
// {
//     logger.LogInformation($"Request: DELETE /items/{id}"); // לוג בקשה
//     return await HandleDbOperationAsync(async db =>
//     {
//         var item = await db.Items.FindAsync(id);
//         if (item == null)
//         {
//             logger.LogInformation($"Response: DELETE /items/{id} - Item not found"); // לוג תגובה
//             return Results.NotFound();
//         }

//         db.Items.Remove(item);
//         await db.SaveChangesAsync();
//         logger.LogInformation($"Response: DELETE /items/{id} - Item deleted"); // לוג תגובה
//         return Results.NoContent();
//     }, logger, $"מחיקת פריט עם מזהה {id}");
// });

// // app.MapGet("/", () => Results.Redirect("/swagger"));
// app.MapGet("/", () => "Auther Server is running");
// app.Run();


using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using TodoApi;
using MySqlConnector;
using Microsoft.Extensions.Logging; // הוספת שימוש ב-ILogger

var builder = WebApplication.CreateBuilder(args);

// הגדרת CORS (פתוח לכל - לפיתוח בלבד! *לייצור יש להגדיר דומיינים ספציפיים*)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// בדיקת מחרוזת החיבור וטיפול בשגיאה
var connectionString = Environment.GetEnvironmentVariable("ToDoDB"); // קריאה מתוך משתנה סביבה

if (string.IsNullOrEmpty(connectionString))
{
    var errorMessage = "Connection string 'ToDoDB' not found in environment variables. Please ensure it is correctly configured.";
    builder.Logging.AddConsole(); // הוספת לוג קונסול כדי לראות את השגיאה
    var logger = builder.Build().Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(errorMessage);
    throw new InvalidOperationException(errorMessage); // זריקת חריגה עוצרת את האפליקציה
}

// הזרקת DbContext
try
{
    builder.Services.AddDbContext<ToDoDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}
catch (Exception ex)
{
    var errorMessage = $"Error configuring DbContext: {ex.Message}";
    builder.Logging.AddConsole();
    var logger = builder.Build().Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, errorMessage);
    throw new InvalidOperationException(errorMessage, ex);
}

// הגדרת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// הפעלת CORS
app.UseCors("AllowAllOrigins");

// הסרת התנאי לסביבת פיתוח
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// *** הגדרת ה-routes ***

// פונקציה כללית לטיפול בשגיאות (DRY)
async Task<IResult> HandleDbOperationAsync(Func<ToDoDbContext, Task<IResult>> operation, ILogger logger, string operationName)
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
            return await operation(db);
        }
    }
    catch (MySqlException mysqlEx)
    {
        logger.LogError(mysqlEx, $"שגיאת MySQL בעת {operationName}: {{Message}}", mysqlEx.Message);
        return Results.Problem($"שגיאת מסד נתונים: {mysqlEx.Message}", statusCode: 500);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"שגיאה כללית בעת {operationName}: {{Message}}", ex.Message);
        return Results.Problem($"אירעה שגיאה בעת {operationName}.", statusCode: 500);
    }
}

// שליפת כל המשימות (GET)
app.MapGet("/items", async (ILogger<Program> logger) =>
{
    logger.LogInformation("Request: GET /items"); // לוג בקשה
    return await HandleDbOperationAsync(async db =>
    {
        var items = await db.Items.ToListAsync();
        logger.LogInformation($"Response: GET /items - {items.Count} items found"); // לוג תגובה
        return Results.Ok(items);
    }, logger, "שליפת פריטים");
});

// הוספת משימה חדשה (POST)
app.MapPost("/items", async (Item item, ILogger<Program> logger) =>
{
    logger.LogInformation($"Request: POST /items - Name: {item.Name}, IsComplite: {item.IsComplite}"); // לוג בקשה
    return await HandleDbOperationAsync(async db =>
    {
        db.Items.Add(item);
        await db.SaveChangesAsync();
        logger.LogInformation($"Response: POST /items - Item added with ID: {item.Id}"); // לוג תגובה
        return Results.Created($"/items/{item.Id}", item);
    }, logger, "הוספת פריט");
});

// עדכון משימה (PUT)
app.MapPut("/items/{id}", async (int id, Item item, ILogger<Program> logger) =>
{
    logger.LogInformation($"Request: PUT /items/{id} - Name: {item.Name}, IsComplite: {item.IsComplite}"); // לוג בקשה
    return await HandleDbOperationAsync(async db =>
    {
        var existingItem = await db.Items.FindAsync(id);
        if (existingItem == null)
        {
            logger.LogInformation($"Response: PUT /items/{id} - Item not found"); // לוג תגובה
            return Results.NotFound();
        }

        existingItem.Name = item.Name;
        existingItem.IsComplite = item.IsComplite;
        await db.SaveChangesAsync();
        logger.LogInformation($"Response: PUT /items/{id} - Item updated"); // לוג תגובה
        return Results.NoContent();
    }, logger, $"עדכון פריט עם מזהה {id}");
});

// מחיקת משימה (DELETE)
app.MapDelete("/items/{id}", async (int id, ILogger<Program> logger) =>
{
    logger.LogInformation($"Request: DELETE /items/{id}"); // לוג בקשה
    return await HandleDbOperationAsync(async db =>
    {
        var item = await db.Items.FindAsync(id);
        if (item == null)
        {
            logger.LogInformation($"Response: DELETE /items/{id} - Item not found"); // לוג תגובה
            return Results.NotFound();
        }

        db.Items.Remove(item);
        await db.SaveChangesAsync();
        logger.LogInformation($"Response: DELETE /items/{id} - Item deleted"); // לוג תגובה
        return Results.NoContent();
    }, logger, $"מחיקת פריט עם מזהה {id}");
});

// app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("/", () => "Auther Server is running");
app.Run();