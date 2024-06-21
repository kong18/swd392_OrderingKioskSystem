using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Create
{
    public class CreateKioskCommandHandler : IRequestHandler<CreateKioskCommand, string>
    {
        private readonly IKioskRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public CreateKioskCommandHandler(IKioskRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateKioskCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID không tìm thấy.");
            }

            var kioskCode = await GenerateKioskCode(request.Location);
            var kioskPin = GenerateKioskPin();

            var kiosk = new KioskEntity
            {
                NguoiTaoID = _currentUserService.UserId,
                NgayTao = DateTime.UtcNow.AddHours(7),
                Location = request.Location,
                Code = kioskCode,
                PIN = kioskPin
            };

            _repository.Add(kiosk);
            if (await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
                return "Tạo thành công";
            else
                return "Tạo thất bại";
        }

        private async Task<string> GenerateKioskCode(string location)
        {
            // Ensure location format is valid
            if (!int.TryParse(location.Replace("Floor ", ""), out int floor) || floor < 1 || floor > 7)
            {
                throw new ArgumentException("Invalid location format. Expected 'Floor 1' to 'Floor 7'.");
            }

            // Generate a unique code based on the floor and the number of kiosks on that floor
            var kiosksOnFloor = await _repository.FindAllAsync(k => k.Location == location);
            var kioskCount = kiosksOnFloor.Count() + 1; // Increment count for new kiosk

            return $"{location.Replace(" ", "")}-{kioskCount:D3}"; // Format: Floor1-001
        }

        private int GenerateKioskPin()
        {
            var random = new Random();
            return random.Next(100000, 999999); // Generate a 6-digit PIN
        }
    }
}
