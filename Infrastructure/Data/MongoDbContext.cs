using ApplicationCore.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data;

public class MongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["CosmosDb:ConnectionString"]);
        Database = client.GetDatabase(config["CosmosDb:DatabaseName"]);
    }

    public IMongoCollection<User> Users =>
        Database.GetCollection<User>("Users");

    public IMongoCollection<Course> Courses =>
        Database.GetCollection<Course>("Courses");

    public IMongoCollection<CourseContent> CourseContents =>
        Database.GetCollection<CourseContent>("CourseContent");

    public IMongoCollection<UserCourse> UserCourses =>
        Database.GetCollection<UserCourse>("UserCourses");

    public IMongoCollection<VideoProgress> VideoProgress =>
        Database.GetCollection<VideoProgress>("VideoProgress");
}