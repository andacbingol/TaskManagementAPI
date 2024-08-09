using Bogus;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Domain.Entities.Identity;
using TaskManagementAPI.Domain.Enums;
using Task = TaskManagementAPI.Domain.Entities.Task;

namespace TaskManagementAPI.Persistence;
public interface IDataGenerator
{
    List<AppUser> GenerateAppUser();
    List<Project> GenerateProjects();
    List<Task> GenerateTasks();
}
public class DataGenerator : IDataGenerator
{
    Faker<AppUser> _appUserFake;
    Faker<Project> _projectFake;
    Faker<Task> _taskFake;
    List<Guid> _userIds = new List<Guid>();
    List<AppUser> _users = new List<AppUser>();
    List<Project> _projects = new List<Project>();
    public DataGenerator()
    {
        
        Randomizer.Seed = new Random(123);
        _appUserFake = new Faker<AppUser>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.NormalizedUserName, (f, usr) => usr.UserName.ToUpper())
            .RuleFor(u => u.Email, (f, usr) => f.Internet.ExampleEmail(firstName: usr.UserName))
            .RuleFor(u => u.NormalizedEmail, (f, usr) => usr.Email.ToUpper())
            .RuleFor(u => u.EmailConfirmed, (f, usr) => usr.EmailConfirmed = true);

        _projectFake = new Faker<Project>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Name, f => 
            {
                string x = f.Lorem.Slug();
                return x.Length <= 25 ? x.Replace("-"," ") : x.Substring(0, 25);
            })
            .RuleFor(p => p.Description, f =>
            {
                string x = f.Lorem.Sentence(3, 2);
                return x.Length <= 150 ? x : x.Substring(0,150);
            })
            .RuleFor(p => p.StartDate, f => f.Date.Soon(3,DateTime.Now))
            .RuleFor(p => p.EndDate, (f, pro) => f.Date.Between(pro.StartDate, pro.StartDate.AddMonths(1)));

        _taskFake = new Faker<Task>()
            .RuleFor(t => t.Id, f => f.Random.Guid())
            .RuleFor(p => p.Title, f =>
            {
                string x = f.Lorem.Slug();
                return x.Length <= 25 ? x.Replace("-", " ") : x.Substring(0, 25);
            })
            .RuleFor(t => t.Description, f =>
            {
                string x = f.Lorem.Sentence(3, 2);
                return x.Length <= 150 ? x : x.Substring(0, 150);
            })
            .RuleFor(t => t.Status, f => (Domain.Enums.Status)(f.Random.Int(0, 2)))
            .RuleFor(t => t.Priority, f => (Domain.Enums.Priority)(f.Random.Int(0, 2)));
    }
    
    public List<AppUser> GenerateAppUser()
    {
        _users = _appUserFake.Generate(10);
        return _users;
    }
    public List<Project> GenerateProjects()
    {
        Random random = new Random(0);
        _projects = _projectFake.Generate(40);
        List<int> indexes = new List<int>();
        for (int i = 0; i < _projects.Count; i++)
        {
            indexes.Add(random.Next(0, _users.Count));
        }
        
        for (int i = 0;i < _projects.Count; i++)
        {
            _projects[i].UserId = _users[indexes[i]].Id;
        }
        return _projects;
    }

    public List<Task> GenerateTasks()
    {
        Random random = new Random(0);
        var tasks = _taskFake.Generate(100);
        List<int> indexes = new List<int>();
        for (int i = 0; i < tasks.Count; i++)
        {
            indexes.Add(random.Next(0, _projects.Count));
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].ProjectId = _projects[indexes[i]].Id;
            tasks[i].DueDate = _projects[indexes[i]].EndDate.AddDays(-random.Next(0,4));
        }
        return tasks;
    }
}
