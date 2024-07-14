using FluentValidation;
using FluentValidation.Validators;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands
{
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer less 70 charactors");

            RuleFor(s=>s.DefaultDays).GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100").LessThan(1)
                .WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(s=>s)
                .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists");

            this._leaveTypeRepository= leaveTypeRepository;
        }

        private  Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
