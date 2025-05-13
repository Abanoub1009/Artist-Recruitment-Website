using BL.Services;
using BL.Services.Interface;
using DAL;
using DAL.Data;
using DAL.Repository;
using DAL.Repository.IRepository;
using DAL.UnitOfWork;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks.Dataflow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Application
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
//Artist Profile
builder.Services.AddScoped<IArtistProfileService, ArtistProfileService>();
builder.Services.AddScoped<IArtistProfileRepository, ArtistProfileRepository>();
//Blog Post
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
//JobPost
builder.Services.AddScoped<IJobPostRepository, JobPostRepository>();
builder.Services.AddScoped<IJobPostService, JobPostService>();
builder.Services.AddScoped<IUnitOfWork, UnitOFWork>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
    op => op.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
