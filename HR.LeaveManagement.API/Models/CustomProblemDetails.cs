using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Models
{
    public class CustomProblemDetails : ProblemDetails
    {
        //public IDictionary<string, string[]> Error { get; set; }= new Dictionary<string, string[]>();

        public List<string> Errors { get; set; }    = new List<string>();
    }
}
