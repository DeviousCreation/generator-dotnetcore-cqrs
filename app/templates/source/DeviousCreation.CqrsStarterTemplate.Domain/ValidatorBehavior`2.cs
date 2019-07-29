using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeviousCreation.CqrsStarterTemplate.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace DeviousCreation.CqrsStarterTemplate.Domain
{
    public sealed class ValidatorBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior(
        IValidator<TRequest>[] validators)
        {
            this._validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = this._validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();
            if (failures.Any())
            {
                throw new CustomException(
                    $"Command Validation Errors for type {typeof(TRequest).Name}",
                    new ValidationException("Validation exception", failures));
            }

            var response = await next();
            return response;
        }
    }
}