using KenloadV2API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KenloadV2API.Data 
{
    public class Kenloadv2Data : IdentityDbContext
    {
        public Kenloadv2Data(DbContextOptions opt) : base(opt) { }
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseMySql(connectionString: @"server=localhost;database=Kenloadv2;uid=root;password=AEAadmin;", new MySqlServerVersion(new Version(8, 0, 21)));
         }*/
        //USERS MODULE
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserRoleCombination> UserRoleCombination { get; set; }
        public DbSet<UserPass> UserPass { get; set; }
        public DbSet<AccessControlPasswords> AccessControlPasswords { get; set; }
        public DbSet<AccessPassPolicy> AccessPassPolicy { get; set; }
        public DbSet<Screens> Screens { get; set; }
        public DbSet<ControlSelection> ControlSelections { get; set; }

        //WEIGHING MODULE
        public DbSet<AxleWeightConfig> AxleWeightConfig { get; set; }
        public DbSet<AxleWeights> AxleWeights { get; set; }
        public DbSet<AxleWeightXreff> AxleWeightXreff { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Hswim> Hswim { get; set; }
        public DbSet<Source> Source { get; set; }   
        public DbSet<SpecialRelease> SpecialRelease { get; set; }
        public DbSet<Trailer> Trailer { get; set; }
        public DbSet<Transporters> Transporters { get; set; }
        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<VehiclesInYard> VehiclesInYard { get; set; }
        public DbSet<WeighbridgeTransactions> WeighbridgeTransactions { get; set; }
        public DbSet<DeckWeights> DeckWeights { get; set; }
        public DbSet<AutoWeigh> AutoWeigh { get; set; }
        public DbSet<VirtualTicket> VirtualTicket { get; set; }

        //DATA MODULE
        public DbSet<AuditLogs> AuditLogs { get; set; }
        public DbSet<BackupLogs> BackupLogs { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<ClusterWeighbridge> ClusterWeighbridges { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<Origin> Origin { get; set; }
        public DbSet<RoadAuthority> RoadAuthority { get; set; }
        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Routes> Routes { get; set; }

        //WEIGHING MODULE
        public DbSet<ScaleTest> ScaleTest { get; set; }
        public DbSet<Tollerance> Tollerance { get; set; }
        public DbSet<ManualWeighing> ManualWeighing { get; set; }
        public DbSet<Wim> Wim { get; set; }
        public DbSet<Tag> Tag { get; set; }

        //Backup
        public DbSet<Backups> Backups { get; set; }
        public DbSet<BackUpDB> BackUpDB { get; set; }
        //CENSUS MODULE
        public DbSet<Census> Census { get; set; }
        public DbSet<PermitURL> PermitURL { get; set; }
        public DbSet<YardList> YardList { get; set; }
        public DbSet<Events> Events { get; set; }

        //VI MODULE
        public DbSet<WideLoad> WideLoad { get; set; }
        public DbSet<YardInspection> YardInspection { get; set; }

        //SECURITY MODULE
        //public DbSet<AuditLogs> AuditLogs { get; set; }

        //SETTINGS MODULE
        public DbSet<SystemSettings> SystemSettings { get; set; }
        public DbSet<Camera> Camera { get; set; }
        public DbSet<ProsecutionSettings> ProsecutionSettings { get; set; }
        public DbSet<IOSettings> IOSettings { get; set; }




        //TECHNICAL MODULE	
        public DbSet<HealthCheck> HealthCheck { get; set; }
        public DbSet<CalibrationCert> CalibrationCert { get; set; }

        //Prosecution Module
        public DbSet<CaseDetails> CaseDetails { get; set; }
        public DbSet<CaseResults> CaseResults { get; set; }
        public DbSet<BillingInstruction> BillingInstruction { get; set; }
        public DbSet<CaseDocs> CaseDocs { get; set; }
        public DbSet<GVWoverloadCharges> GVWoverloadCharges { get; set; }
        public DbSet<GVWdemeritPoints> GVWdemeritPoints { get; set; }
        public DbSet<AVWoverloadCharges> AVWoverloadCharges { get; set; }
        public DbSet<AVWdemeritPoints> AVWdemeritPoints { get; set; }
        public DbSet<EACAct> EACAct { get; set; }
        public DbSet<Penalties> Penalties { get; set; }
        public DbSet<DollarRate> DollarRate { get; set; }

        //Data Transfer Model
        public DbSet<DataExchange> DataExchange {get; set;}
        public DbSet<TagsExchange> TagsExchange { get; set; }
    

    }
}
