using Pk.Com.Jazz.ECP.Models;
using System.Collections.Generic;

namespace Pk.Com.Jazz.ECP.ViewModels
{
    public class FeedbacksViewModel
    {
        public List<FeedbackDetail> Feedbacks { get; set; }
        public bool IsAgent { get; set; }

        public class FeedbackDetail
        {
            public int Id { get; set; }
            public DateTime FeedbackDate { get; set; }
            public string FeedbackType { get; set; }
            public string Feedback { get; set; }
            public string ProvidedBy { get; set; }
            public string Comments { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}
