namespace gamex.Auth.Services;

public record UploadProfileImageCommand(UploadProfileImageRequestDto requestDto) : IRequest<Response<UploadProfileImageResponseDto>>{}
public sealed class UploadProfileImageCommandHandler : IRequestHandler<UploadProfileImageCommand, Response<UploadProfileImageResponseDto>>
{
    private readonly IPlayerRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    private readonly AttachmentDirectories _attachmentDirectories;
    private readonly UserSessionDto _sessionUser;

    public UploadProfileImageCommandHandler(
        IPlayerRepository repo,
        IUnitOfWork unitOfWork,
        IImageService imageService,
        IOptions<AttachmentDirectories> attachmentDirectories,
        IUserSessionService sessionService)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _imageService = imageService;
        _attachmentDirectories = attachmentDirectories.Value;
        _sessionUser = sessionService.Get();
    }


    public async Task<Response<UploadProfileImageResponseDto>> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
    {
        Guid PlayerId = _sessionUser.PlayerId;
 
        var attachment = GetImageFileName(request.requestDto);

        var imageSaveResult = await _imageService.SaveImage(attachment.AbsolutePath, request.requestDto.ImageInBase64);
        if (imageSaveResult.IsFailure)
            return imageSaveResult.Error;

        Player player = await _repo.Get(p=> p.Id == PlayerId && p.IsActive == true);
        player.ProfileImagename = attachment.Filename;

        await _repo.Update(player, p => p.Id == PlayerId);

        var commitResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (commitResult.IsFailure) 
            return commitResult.Error;
        
        return  new UploadProfileImageResponseDto("Success");        
    }

    public AttachmentDirectoryDto GetImageFileName(UploadProfileImageRequestDto requestDto)  {

        string Extension = "png";

        string fileName =  $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{Random.Shared.Next(10000,99999)}.{Extension}";
        string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "attachments", _attachmentDirectories.IMAGES);

        return new(
            fileName, 
            _attachmentDirectories.IMAGES, 
            Path.Combine(FolderPath, fileName),
            Extension,
            FolderPath
        );
    }

   



}