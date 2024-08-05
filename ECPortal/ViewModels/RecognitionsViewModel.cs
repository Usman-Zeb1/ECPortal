using Pk.Com.Jazz.ECP.Models;

namespace Pk.Com.Jazz.ECP.ViewModels
{
  
        public class RecognitionsViewModel
        {
            public List<RecognitionDetail> Recognitions { get; set; }
            public bool IsAgent { get; set; }

            public class RecognitionDetail
            {
                public int Id { get; set; }
                public DateTime RecognitionDate { get; set; }
                public string RecognitionType { get; set; }
                public string Recognition { get; set; }
                public string ProvidedBy { get; set; }
                public string Comments { get; set; }
                public string RecognizedAgent { get; set; }  // Add this property
            }
        }
    

}
