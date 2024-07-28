using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfile;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Commands
{
    public class CreateleaveTypeCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockCategoryRepository;
        private Mock<ILeaveTypeRepository> _mockRepo;

        public CreateleaveTypeCommandTests()
        {
            _mockRepo= MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(s =>
            {
                s.AddProfile<LeaveTypeProfile>();
            });

            _mapper =mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);

            await handler.Handle(new CreateLeaveTypeCommand() { Name = "Test" }, CancellationToken.None);

            var leaveTypes = await _mockCategoryRepository.Object.GetAsync();
            leaveTypes.Count.ShouldBe(4);
        }
    }
}
