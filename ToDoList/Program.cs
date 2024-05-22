using ToDoList.Repositories; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<TaskRepository>(provider => new TaskRepository(builder.Configuration.GetConnectionString("ToDoContext")));
builder.Services.AddScoped<CategoryRepository>(provider => new CategoryRepository(builder.Configuration.GetConnectionString("ToDoContext")));
builder.Services.AddScoped<XmlTaskRepository>(provider => new XmlTaskRepository(builder.Configuration.GetConnectionString("XmlFilePath")));
builder.Services.AddScoped<XmlCategoryRepository>(provider => new XmlCategoryRepository(builder.Configuration.GetConnectionString("XmlFilePath")));



builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();