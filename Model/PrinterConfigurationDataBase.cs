using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model.Entities;

namespace Model
{
    public class PrinterConfigurationDataBase : DbContext
    {
        #region Properties

        public virtual DbSet<ReportConfiguration> ReportConfigurations { get; set; }
        public virtual DbSet<GroupConfiguration> GroupConfigurations { get; set; }
        public virtual DbSet<FileReadingData> FileOpeningDatas { get; set; }

        #endregion

        public PrinterConfigurationDataBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PrinterConfiguration>().HasKey(configuration => configuration.Id);

            modelBuilder.Ignore<PrinterConfiguration>();

            var reportConfiguration = modelBuilder.Entity<ReportConfiguration>();
            reportConfiguration.
                                HasKey(report => report.Id).
                                Property(report => report.ReportName).
                                HasColumnName("Name").
                                IsRequired();
            reportConfiguration.HasRequired(report => report.Group).WithMany();
            reportConfiguration.HasMany(report => report.Openings).WithRequired();

            modelBuilder.Entity<GroupConfiguration>().
                         HasKey(group => group.Id).
                         Property(group => group.GroupName).
                         HasColumnName("Name").
                         IsRequired();

            modelBuilder.Entity<FileReadingData>().HasKey(data => data.Id);

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