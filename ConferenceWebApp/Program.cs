using ConferenceWebApp.Models;
using Newtonsoft.Json;

var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultSQlite"];
if (connectionString == null) throw new Exception("There is no such string connection in web.config file");
SQliteDatabase db = new SQliteDatabase(connectionString.ConnectionString);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<SQliteDatabase>(db);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

List<Conference> conferences = db.RetrieveAll<Conference>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=OutputSortedConferences}/{jsonConferences}",
    defaults: new { jsonConferences = JsonConvert.SerializeObject(conferences)}
);

app.Run();
