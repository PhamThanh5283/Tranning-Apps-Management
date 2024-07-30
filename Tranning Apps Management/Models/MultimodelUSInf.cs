namespace Tranning_Apps_Management.Models
{
    public class MultimodelUSInf
    {
        public IEnumerable<Tranning_Apps_Management.Models.DB.Department> departments { get; set; }
        public IEnumerable<Tranning_Apps_Management.Models.DB.UserSignup> userSignups { get; set; }
    }
}
