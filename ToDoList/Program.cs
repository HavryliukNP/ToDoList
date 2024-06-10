using GraphiQl;
using GraphQL;
using GraphQL.Types;
using ToDoList.Data;
using ToDoList.Mutations;
using ToDoList.Query;
using ToDoList.Repositories;
using ToDoList.Schema;
using ToDoList.Type;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ToDoContext>();
builder.Services.AddSingleton<XmlStorageContext>();

builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<XmlTaskRepository>();
builder.Services.AddScoped<XmlCategoryRepository>();

builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<TaskMutation>();
builder.Services.AddTransient<RootMutation>();

builder.Services.AddTransient<TaskType>();
builder.Services.AddTransient<CategoryType>();
builder.Services.AddTransient<TaskInputType>();
builder.Services.AddTransient<CategoryInputType>();

builder.Services.AddTransient<TaskQuery>();
builder.Services.AddTransient<CategoryQuery>();
builder.Services.AddTransient<RootQuery>();

builder.Services.AddTransient<ISchema, RootSchema>();

builder.Services.AddGraphQL(b => b.AddAutoSchema<ISchema>().AddSystemTextJson());

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseGraphiQl("/graphql");
app.UseGraphQL<ISchema>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();