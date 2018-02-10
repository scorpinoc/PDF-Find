using System;
using System.Data.Entity;
using System.Linq;

namespace Model
{
    public class DataBase : DbContext
    {
        #region Properties

        public virtual DbSet<ReportConfiguration> ReportConfigurations { get; set; }
        public virtual DbSet<GroupConfiguration> GroupConfigurations { get; set; }
        public virtual DbSet<FileReadingData> FileOpeningDatas { get; set; }

        #endregion

        public DataBase()
             : base()
        {

        }
        public DataBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<PrinterConfiguration>();

            var reportConfiguration = modelBuilder.Entity<ReportConfiguration>();
            var groupConfiguration = modelBuilder.Entity<GroupConfiguration>();

            reportConfiguration
                .HasKey(report => report.Id)
                .Property(report => report.ReportName)
                .HasColumnName("Name")
                .IsRequired();

            groupConfiguration
                .HasKey(group => group.Id)
                .Property(group => group.GroupName)
                .HasColumnName("Name")
                .IsRequired();

            modelBuilder.Entity<FileReadingData>()
                .HasKey(data => data.Id);

            modelBuilder.Entity<ReportPrintData>()
                .HasKey(data => data.Id);

            reportConfiguration
                .HasMany(report => report.Openings)
                .WithRequired(print => print.Report)
                .WillCascadeOnDelete();

            reportConfiguration
                .HasRequired(report => report.Group)
                .WithMany(group => group.Reports);

            groupConfiguration
                .HasMany(group => group.Reports)
                .WithRequired(rep => rep.Group);

            base.OnModelCreating(modelBuilder);
        }

        public ReportConfiguration FindReport(string reportName)
        {
            // todo exception localizations
            if (string.IsNullOrWhiteSpace(reportName))
                throw new ArgumentException("Argument is null or whitespace", nameof(reportName));

            ReportConfiguration.ValidateReportName(reportName);

            // todo inner db procedure
            return
                ReportConfigurations.FirstOrDefault(
                    configuration => configuration.ReportName.Equals(reportName));
        }

        public GroupConfiguration FindGroup(string reportName)
        {
            // todo exception localizations
            if (string.IsNullOrWhiteSpace(reportName))
                throw new ArgumentException("Argument is null or whitespace", nameof(reportName));

            // todo validation

            // todo inner db procedure
            return
                GroupConfigurations.LastOrDefault(
                    configuration => reportName.StartsWith(configuration.GroupName));
        }

        #endregion
    }
}