namespace PointOfSale.Application.DTOs.Request.Login;

public record ResetPasswordRequest(Guid UserId, string NewPassword);