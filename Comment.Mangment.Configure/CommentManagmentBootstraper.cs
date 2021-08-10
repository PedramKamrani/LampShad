using CommenetManagmenrt.Infractracer.EFCore;
using CommenetManagmenrt.Infractracer.EFCore.Repository;
using Comment.Managment.Cantract.Comment;
using Commentmanagment.Domain.CommentAgg;
using CommentManagment.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Comment.Mangment.Configure
{
    public class CommentManagmentBootstraper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
