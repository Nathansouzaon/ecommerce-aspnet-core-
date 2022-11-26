using App.Core.Data;
using FluentValidation.Results;

namespace App.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (await uow.Commit() is false) AdicionarErro("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}