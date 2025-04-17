using DBCoursework.Database;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) {
    builder.Configuration.AddUserSecrets<Program>();
} else {
    builder.Configuration.AddEnvironmentVariables();
}
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")
);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<DatabaseManager>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/{0}");
app.MapRazorPages();

app.Run();
