using Bogus;
using TM.API.ViewModels.ContextTask;
using TM.API.ViewModels.Project;
using TM.Core.Enum;
using TM.Domain.Entities;

namespace TM.UnitTest.Utilities
{
    internal static class EntityGenerator
    {
        internal static ContextTask GetContextTask()
        {
            return GetContextTask(1).First();
        }

        internal static List<ContextTask> GetContextTask(int count)
        {
            var ids = 1;
            var faker = new Faker<ContextTask>()
                .RuleFor(u => u.Id, f => ids++)
                .RuleFor(u => u.Title, f => f.Commerce.ProductName())
                .RuleFor(u => u.Description, f => f.Commerce.Department())
                .RuleFor(u => u.DueDate, f => f.Date.Soon(15))
                .RuleFor(u => u.Priority, f => (Priority)f.Random.Int(0, 2))
                .RuleFor(u => u.Status, f => CurrentTaskStatus.Pending)
                .RuleFor(u => u.ProjectId, f => 1)
                .RuleFor(u => u.UpdateAt, f => f.Date.Recent(1))
                .RuleFor(u => u.UserId, f => f.Random.Int(10, 30));

            return faker.Generate(count);
        }

        internal static CreateContextTaskViewModel GetCreateContextTaskViewModel()
        {
            var faker = new Faker<CreateContextTaskViewModel>()
                .RuleFor(u => u.Title, f => f.Commerce.ProductName())
                .RuleFor(u => u.Description, f => f.Commerce.Department())
                .RuleFor(u => u.DueDate, f => f.Date.Soon(15))
                .RuleFor(u => u.Priority, f => (Priority)f.Random.Int(0, 2))
                .RuleFor(u => u.Status, f => CurrentTaskStatus.Pending)
                .RuleFor(u => u.UserId, f => f.Random.Int(10, 30));

            return faker.Generate();
        }

        internal static CreateProjectViewModel GetCreateProjectViewModel()
        {
            var faker = new Faker<CreateProjectViewModel>()
                .RuleFor(u => u.Title, f => f.Company.CompanyName())
                .RuleFor(u => u.Description, f => f.Company.CatchPhrase())
                .RuleFor(u => u.UserId, f => f.Random.Int(10, 30));

            return faker.Generate();
        }

        internal static Project GetProject()
        {
            return GetProject(1).First();
        }

        internal static IEnumerable<Project> GetProject(int count)
        {
            var ids = 1;
            var faker = new Faker<Project>()
                .RuleFor(u => u.Id, f => ids++)
                .RuleFor(u => u.Title, f => f.Company.CompanyName())
                .RuleFor(u => u.Description, f => f.Company.CatchPhrase())
                .RuleFor(u => u.UpdateAt, f => f.Date.Recent(1))
                .RuleFor(u => u.UserId, f => f.Random.Int(10, 30))
                .RuleFor(u => u.Enabled, f => true);

            return faker.Generate(count);
        }
    }
}
