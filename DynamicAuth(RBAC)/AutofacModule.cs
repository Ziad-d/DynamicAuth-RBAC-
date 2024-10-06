using Autofac;
using AutoMapper;
using DynamicAuth_RBAC_.Data;
using DynamicAuth_RBAC_.MapperProfile;
using DynamicAuth_RBAC_.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace StayCation.API
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                var configuration = c.Resolve<IConfiguration>();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                              .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                              .EnableSensitiveDataLogging();

                return new Context(optionsBuilder.Options);
            }).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<RoleProfile>();
            }).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
