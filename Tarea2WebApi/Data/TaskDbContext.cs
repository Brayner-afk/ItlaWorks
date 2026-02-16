using Microsoft.EntityFrameworkCore;
using System;
using Tarea2WebApi.Models.Entities;

namespace Tarea2WebApi.Data
{
    public class TaskDbContext: DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
           : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }


    }
}
