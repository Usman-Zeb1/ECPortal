using Pk.Com.Jazz.ECP.Models;
using System.Collections.Generic;

namespace Pk.Com.Jazz.ECP.ViewModels
{
    public class RequestedTrainingsViewModel
    {
        public List<TrainingRequests> Trainings { get; set; }
        public bool IsAdmin { get; set; } // For example, to check if the user can approve/reject trainings
    }
}