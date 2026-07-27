using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities;

public class Attachment
{

    private Attachment()
    {
    }

    public Guid Id { get; private set; }
    public string Path { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string OriginalName { get; private set; } = string.Empty;
    public string Extension { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long Size { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public IReadOnlyCollection<ProductAttachment> Products => _products.AsReadOnly();
    private readonly List<ProductAttachment> _products = [];

    public static Attachment Create(
        string path,
        string name,
        string originalName,
        string extension,
        string contentType,
        long size)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new DomainException("Informe o caminho (path) do arquivo.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Informe o nome do arquivo no storage.");

        if (string.IsNullOrWhiteSpace(originalName))
            throw new DomainException("Informe o nome original do arquivo.");

        if (string.IsNullOrWhiteSpace(extension))
            throw new DomainException("Informe a extensão do arquivo.");

        if (string.IsNullOrWhiteSpace(contentType))
            throw new DomainException("Informe o ContentType do arquivo.");

        if (size <= 0)
            throw new DomainException("O tamanho do arquivo deve ser maior que zero.");

        return new Attachment
        {
            Id = Guid.NewGuid(),
            Path = path.Trim(),
            Name = name.Trim(),
            OriginalName = originalName.Trim(),
            Extension = extension.Trim().ToLowerInvariant(),
            ContentType = contentType.Trim().ToLowerInvariant(),
            Size = size,
            Active = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void ToggleActiveStatus()
    {
        Active = !Active;
        UpdatedAt = DateTime.UtcNow;
    }
}