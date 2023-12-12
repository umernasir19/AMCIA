﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FL_ACME
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FL_ACMEEntities : DbContext
    {
        public FL_ACMEEntities()
            : base("name=FL_ACMEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FL_AZKAR> FL_AZKAR { get; set; }
        public virtual DbSet<FL_MASJID> FL_MASJID { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<FL_EVENTS> FL_EVENTS { get; set; }
        public virtual DbSet<FL_ARTICLE> FL_ARTICLE { get; set; }
        public virtual DbSet<FL_PROJECT> FL_PROJECT { get; set; }
        public virtual DbSet<Projecct_Images> Projecct_Images { get; set; }
        public virtual DbSet<FL_Project_Images> FL_Project_Images { get; set; }
        public virtual DbSet<FL_TIMELINE> FL_TIMELINE { get; set; }
        public virtual DbSet<FL_SpecialDate> FL_SpecialDate { get; set; }
        public virtual DbSet<FL_NAMAZ> FL_NAMAZ { get; set; }
    
        public virtual ObjectResult<sp_GetAllUsers_Result> sp_GetAllUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAllUsers_Result>("sp_GetAllUsers");
        }
    
        public virtual ObjectResult<sp_GetUserByIDPAss_Result> sp_GetUserByIDPAss(string userid, string password)
        {
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetUserByIDPAss_Result>("sp_GetUserByIDPAss", useridParameter, passwordParameter);
        }
    
        public virtual ObjectResult<sp_Masjid_GetALL_Result> sp_Masjid_GetALL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Masjid_GetALL_Result>("sp_Masjid_GetALL");
        }
    }
}
