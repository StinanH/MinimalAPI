//Stina Hedman
//NET23

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Models;
using System.Net;

namespace MinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            
            //----------------------SWAGGER---------
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //----------------------SWAGGER---------

            var app = builder.Build();

            //----------------------SWAGGER---------
            app.MapSwagger();
            app.UseSwaggerUI();
            //----------------------SWAGGER---------

            app.MapGet("/", () => "Welcome to the database!");

            //GET all users in the system
            app.MapGet("/users", (ApplicationContext context) =>
            {
                return Results.Json(context.Users.Select(u => new { u.FirstName, u.LastName, u.PhoneNumber, u.Id }).ToArray());
            });

            //GET all interests of a specific user
            app.MapGet("/users/{id}/interests", (ApplicationContext context, int id) =>
            {
                return Results.Json(context.Users
                    .Where(u => u.Id == id)
                    .SelectMany(u => u.Interests)
                    .Select(i => new { i.Name, i.Description, i.Id })
                    .ToArray());
            });

            //GET all websites connected to specific user
            app.MapGet("/users/{id}/webpages", (ApplicationContext context, int id) =>
            {
                return Results.Json(context.Users
                    .Where(u => u.Id == id)
                    .SelectMany(u => u.Webpages)
                    .Select(i => new { i.Url, WebpageTopic = i.Interest.Name, i.Id })
                    .ToArray());
            });

            //POST new user
            app.MapPost("/users", (ApplicationContext context, User user) =>
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);

            });

            //POST new interest
            app.MapPost("/interests", (ApplicationContext context, Interest interest) =>
            {
                context.Interests.Add(interest);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);

            });

            app.MapPost("/users/{id}/webpages", (ApplicationContext context, [FromBody] WebpageInfo webpageinfo, int id) =>
            {
                var user = context.Users.FirstOrDefault(u => u.Id == id);

                //explicit load of interests 
                context.Entry(user)
                    .Collection(u => u.Interests)
                    .Load();                

                var interest = user.Interests.FirstOrDefault(i => i.Name == webpageinfo.MyInterest);

                if (interest == null)
                {
                    return Results.BadRequest(interest);
                }

                user.Webpages.Add(new Webpage()
                {
                    Url = webpageinfo.Url,
                    Interest = interest
                });


                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);

            });

            //POST new interest
            app.MapPost("/users/{id}/interests", async (ApplicationContext context, [FromBody] InterestInfo interestInfo, [FromRoute] int id) =>
            {
                var user = context.Users.First(u => u.Id == id);
                user.Interests.Add(context.Interests.First(i => i.Name == interestInfo.Interest.ToString()));

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.OK);
            });


            app.Run();
        }

    }
}