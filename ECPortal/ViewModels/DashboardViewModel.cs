using System;
using System.Collections.Generic;
using Pk.Com.Jazz.ECP.Models;
namespace Pk.Com.Jazz.ECP.ViewModels
{
    public class DashboardViewModel
    {
        // Employee Details
        public Employee Employee { get; set; }

        public EC EC { get; set; }

        // Summary Data
        public int EmployeeCommissionsCount { get; set; }
        public DateTime? LastCommissionDate { get; set; }

        public string Role {  get; set; }

        public int EmployeeFeedbacksCount { get; set; }
        public DateTime? LastFeedbackDate { get; set; }

        public int EmployeePerformancesCount { get; set; }
        public int EmployeePerformanceScore { get; set; }
        public DateTime? LastPerformanceDate { get; set; }

        public int EmployeeTrainingsCount { get; set; }
        public DateTime? LastTrainingDate { get; set; }

        public int EmployeeSalesCount { get; set; }
        public DateTime? LastSalesDate { get; set; }

        public int EmployeeRecognitionsCount { get; set; }
        public DateTime? LastRecognitionDate { get; set; }

        public int EmployeeTargetsCount { get; set; }
        public DateTime? LastTargetDate { get; set; }
    }
}
