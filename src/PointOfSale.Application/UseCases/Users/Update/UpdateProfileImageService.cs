using FluentValidation;
using Microsoft.AspNetCore.Http;
using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Users.Update;

public class UpdateProfileImageService : IUpdateProfileImageService
{
    private readonly ICryptoService _cryptoService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateRequest> _validator;

    public UpdateProfileImageService(IUserRepository userRepository, IUnitOfWork unitOfWork,
        IValidator<UserCreateRequest> validator, ICryptoService cryptoService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _cryptoService = cryptoService;
    }

    public async Task ExecuteAsync(Guid userId, IFormFile imageFile)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user is null) throw new NotFoundException("Usuário não encontrado");

        var imageUrl = string.Empty;
        //var imageUrl = await _storageService.UploadAsync(imageFile);

        user.UpdateProfileImage(imageUrl);

        await _unitOfWork.SaveChangesAsync();
    }
}