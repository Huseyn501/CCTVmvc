using System.ComponentModel.DataAnnotations;

namespace CCTV.ViewModels
{
    public class LoginVm
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
