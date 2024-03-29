﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VG_Review.Areas.Identity.Data;
using VG_Review.Models;

namespace VG_Review.Areas.Identity.Data;

public class VG_ReviewContext : IdentityDbContext<User>
{
    public VG_ReviewContext(DbContextOptions<VG_ReviewContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
