﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Studio.DotNet.Domain.T4
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbDotNetStudioEntitiesInConfig : DbContext
    {
        public DbDotNetStudioEntitiesInConfig()
            : base("name=DbDotNetStudioEntitiesInConfig")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TblArticle> TblArticle { get; set; }
        public virtual DbSet<TblArticleTblTag> TblArticleTblTag { get; set; }
        public virtual DbSet<TblTag> TblTag { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserTblArticle> TblUserTblArticle { get; set; }
    }
}
