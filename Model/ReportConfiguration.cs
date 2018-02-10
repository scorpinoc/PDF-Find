using System;
using System.Collections.Generic;

namespace Model
{
    public class ReportConfiguration : PrinterConfiguration
    {
        #region Static

        public static void ValidateReportName(string reportName)
        {
            if (string.IsNullOrWhiteSpace(reportName))
                throw new ArgumentException("Argument is null or whitespace", nameof(reportName));
            if (reportName.Length < 12)
                throw new ArgumentException("Report name can'be less that 12 symbols", nameof(reportName));
           if (reportName.Contains(" "))
                throw new ArgumentException("Report name must not contain spaces", nameof(reportName));

        }

        #endregion

        #region Fields

        private string _reportName;

        #endregion
        
        #region Properties

        public GroupConfiguration Group { get; set; }

        internal ICollection<ReportPrintData> Openings { get; set; }

        public string ReportName
        {
            get { return _reportName; }
            set
            {
                ValidateReportName(value);
                _reportName = value;
            }
        }

        #endregion

        #region Methods

        public void AddFile(string fileName)
            => Openings.Add(new ReportPrintData {FileName = fileName, Opening = DateTime.Now, Report = this});

        #endregion
    }
}