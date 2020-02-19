
using MyProject.Identity.ViewModels.Validations;
using FluentValidation.Attributes;

namespace MyProject.Identity.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
